﻿using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
        Console.WriteLine("Consuming Auction finished search service");
        var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);
        if (context.Message.ItemSold)
        {
            auction.Winner = context.Message.Winner;
            auction.SoldAmount = context.Message.Amount ?? auction.SoldAmount;
        }
        auction.Status = "Finished";
        await auction.SaveAsync();  
    }
}
