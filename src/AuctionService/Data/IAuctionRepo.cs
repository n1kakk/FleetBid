using AuctionService.DTOs;
using AuctionService.Entities;

namespace AuctionService.Data;

public interface IAuctionRepo
{
    Task<List<AuctionDto>> GetAuctionsAsync(string date);
    Task<AuctionDto?> GetAuctionByIdAsync(Guid id);
    void AddAuction(Auction auction);
    Task<Auction?> GetAuctionEntityByIdAsync(Guid id);
    void RemoveAuction(Auction auction);
    Task<bool> SaveChangesAsync();
}
