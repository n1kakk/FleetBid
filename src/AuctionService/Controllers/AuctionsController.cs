using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuctionRepo _auctionRepo;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuctionsController(IMapper mapper, IPublishEndpoint publishEndpoint, IAuctionRepo auctionRepo)
    {
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _auctionRepo = auctionRepo;
    }


    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string? date)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        if(date == null) return BadRequest(); 

        return await _auctionRepo.GetAuctionsAsync(date); 
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var auction = await _auctionRepo.GetAuctionByIdAsync(id);

        if(auction == null) return NotFound();

        return Ok(auction);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var auction = _mapper.Map<Auction>(auctionDto);

        auction.Seller = User.Identity.Name;

        _auctionRepo.AddAuction(auction);

        var newAuction = _mapper.Map<AuctionDto>(auction);
        await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));

        var result = await _auctionRepo.SaveChangesAsync();

        if (!result) return BadRequest("Could not save changes to dataBase");

        return CreatedAtAction(nameof(GetAuctionById), new {auction.Id}, newAuction);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto auctionDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var auction = await _auctionRepo.GetAuctionEntityByIdAsync(id);

        if(auction == null) return NotFound();

        if (auction.Seller != User.Identity.Name) return Forbid();

        auction.Item.Make = auctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = auctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = auctionDto.Color ?? auction.Item.Color;
        auction.Item.Mileage = auctionDto.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = auctionDto.Year ?? auction.Item.Year;

       
        await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));
        var result = await _auctionRepo.SaveChangesAsync();

        if (result) return Ok();
        return BadRequest("Problem saving changes");
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var auction = await _auctionRepo.GetAuctionEntityByIdAsync(id);

        if (auction == null) return NotFound();

        if (auction.Seller != User.Identity.Name) return Forbid();

        _auctionRepo.RemoveAuction(auction);

        await _publishEndpoint.Publish(new AuctionDeleted { Id = auction.Id.ToString() });

        var result = await _auctionRepo.SaveChangesAsync();


        if (!result) return BadRequest("Could not update dataBase");
        return Ok();
    }
}
