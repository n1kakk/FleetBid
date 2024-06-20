using BiddingService.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiddingService.Repository;

public interface IBiddingRepo
{
    public Task<Auction?> FindAuctionByIdAsync(string auctionId);
    public Task<Bid?> FindHighestBidAsync(string auctionId);
    public Task SaveBidAsync(Bid bid);
    public Task<List<Bid>> GetBidsForAuctionAsync(string auctionId);
}
