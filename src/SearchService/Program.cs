using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<AuctionServiceHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    x.AddConsumersFromNamespaceContaining<AuctionDeletedConsumer>();
    x.AddConsumersFromNamespaceContaining<AuctionUpdatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint("search-auction-created", e =>
        {
            e.UseMessageRetry(a => a.Interval(5, 5));
            e.ConfigureConsumer<AuctionCreatedConsumer>(context);
        });
        cfg.ReceiveEndpoint("search-auction-deleted", e =>
        {
            e.UseMessageRetry(a => a.Interval(5, 5));
            e.ConfigureConsumer<AuctionDeletedConsumer>(context);
        });
        cfg.ReceiveEndpoint("search-auction-updated", e =>
        {
            e.UseMessageRetry(a => a.Interval(5, 5));
            e.ConfigureConsumer<AuctionUpdatedConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });


});


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        await DbInitializer.InitDb(app);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }
});


app.Run();

static IAsyncPolicy<HttpResponseMessage> GetPolicy() 
    => HttpPolicyExtensions
    .HandleTransientHttpError()
    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
    .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
