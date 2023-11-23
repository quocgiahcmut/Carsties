using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionDeletedConsumer : IConsumer<AuctionDeleted>
{
    private readonly ILogger<AuctionDeletedConsumer> _logger;
    private readonly IMapper _mapper;

    public AuctionDeletedConsumer(ILogger<AuctionDeletedConsumer> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<AuctionDeleted> context)
    {
        _logger.LogInformation("--> Consuming auction deleted: " + context.Message.Id);

        var result = await DB.DeleteAsync<Item>(context.Message.Id);

        if (!result.IsAcknowledged)
            throw new MessageException(typeof(AuctionDeleted), "Problem deleting auction in mongodb");
    }
}
