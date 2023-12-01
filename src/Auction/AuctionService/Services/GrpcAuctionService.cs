using AuctionService.Data;
using Grpc.Core;

namespace AuctionService.Services;

public class GrpcAuctionService : GrpcAuction.GrpcAuctionBase
{
    private readonly ILogger<GrpcAuctionService> _logger;
    private readonly AuctionDbContext _context;

    public GrpcAuctionService(ILogger<GrpcAuctionService> logger, AuctionDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public override async Task<GrpcAuctionResponse> GetAuction(GetAuctionRequest request, ServerCallContext context)
    {
        _logger.LogInformation("==> Received Grpc request for auction");

        var auction = await _context.Auctions.FindAsync(Guid.Parse(request.Id)) 
            ?? throw new RpcException(new Status(StatusCode.NotFound, "Not found"));

        var response = new GrpcAuctionResponse
        {
            Auction = new GrpcAuctionModel
            {
                AuctionEnd = auction.AuctionEnd.ToString(),
                Id = auction.Id.ToString(),
                ReservePrice = auction.ReservePrice,
                Seller = auction.Seller,
            }
        };

        return response;
    }
}
