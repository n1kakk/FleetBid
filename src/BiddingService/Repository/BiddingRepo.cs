using BiddingService.DTOs;
using BiddingService.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace BiddingService.Repository;

public class BiddingRepo : IBiddingRepo
{
    public async Task<Auction?> FindAuctionByIdAsync(string auctionId)
    {
        return await DB.Find<Auction>().OneAsync(auctionId);
    }

    public async Task<Bid?> FindHighestBidAsync(string auctionId)
    {
        return await DB.Find<Bid>()
            .Match(a => a.AuctionId == auctionId)
            .Sort(b => b.Descending(x => x.Amount))
            .ExecuteFirstAsync();

    }

    public async Task<List<Bid>> GetBidsForAuctionAsync(string auctionId)
    {
        return await DB.Find<Bid>()
            .Match(a => a.AuctionId == auctionId)
            .Sort(b => b.Descending(x => x.BidTime))
            .ExecuteAsync();
    }

    public async Task SaveBidAsync(Bid bid)
    {
        await DB.SaveAsync(bid);
    }
}
