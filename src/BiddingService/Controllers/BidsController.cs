using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using BiddingService.Repository;
using BiddingService.Services;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace BiddingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BidsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly GrpcAuctionClient _grpcAuctionClient;
    private readonly IBiddingRepo _biddingRepo;
    public BidsController(IMapper mapper, IPublishEndpoint publishEndpoint,
        GrpcAuctionClient grpcAuctionClient, IBiddingRepo biddingRepo)
    {
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _grpcAuctionClient = grpcAuctionClient;
        _biddingRepo = biddingRepo;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<BidDto>> PlaceBid(string auctionId, int amount)
    {
        var auction = await _biddingRepo.FindAuctionByIdAsync(auctionId);

        if (auction == null)
        {
            auction = _grpcAuctionClient.GetAuction(auctionId);

            if (auction == null) return BadRequest("Cannot accept bids on this auction at this time");
        }

        if (auction.Seller == User.Identity.Name)
        {
            return BadRequest("You cannot bid on your own auction");
        }

        var bid = new Bid
        {
            Amount = amount,
            AuctionId = auctionId,
            Bidder = User.Identity.Name
        };

        if (auction.AuctionEnd < DateTime.UtcNow)
        {
            bid.BidStatus = BidStatus.Finished;
        }
        else
        {
            var highBid = await _biddingRepo.FindHighestBidAsync(auctionId);

            if (highBid != null && amount > highBid.Amount || highBid == null)
            {
                bid.BidStatus = amount > auction.ReservePrice
                    ? BidStatus.Accepted
                    : BidStatus.AcceptedBelowReserve;
            }

            if (highBid != null && bid.Amount <= highBid.Amount)
            {
                bid.BidStatus = BidStatus.TooLow;
            }

        }

        await _biddingRepo.SaveBidAsync(bid);

        await _publishEndpoint.Publish(_mapper.Map<BidPlaced>(bid));

        return Ok(_mapper.Map<BidDto>(bid));
    }

    [HttpGet("{auctionId}")]
    public async Task<ActionResult<List<BidDto>>> GetBidsForAuction(string auctionId)
    {
        var bids = await _biddingRepo.GetBidsForAuctionAsync(auctionId);

        return bids.Select(_mapper.Map<BidDto>).ToList();
    }

}
