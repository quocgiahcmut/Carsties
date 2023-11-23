using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
{
    private readonly ILogger<AuctionCreatedFaultConsumer> _logger;

    public AuctionCreatedFaultConsumer(ILogger<AuctionCreatedFaultConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
    {
        _logger.LogInformation("--> Consuming faulty creation");

        var exception = context.Message.Exceptions.First();

        if (exception.ExceptionType == "System.ArgumentException")
        {
            context.Message.Message.Model = "";
            await context.Publish(context.Message.Message);
        }
        else
        {
            _logger.LogInformation("Not an argument exception - update error dashboard somewhere");
        }
    }
}
