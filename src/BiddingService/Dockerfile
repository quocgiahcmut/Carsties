FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
EXPOSE 80

COPY Carsties.sln Carsties.sln
COPY src/Auction/AuctionService/AuctionService.csproj src/Auction/AuctionService/AuctionService.csproj
COPY src/Search/SearchService/SearchService.csproj src/Search/SearchService/SearchService.csproj
COPY src/BiddingService/BiddingService.csproj src/BiddingService/BiddingService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/Common/Contracts/Contracts.csproj src/Common/Contracts/Contracts.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj

RUN dotnet restore Carsties.sln

COPY src/BiddingService src/BiddingService
COPY src/Common/Contracts src/Common/Contracts
WORKDIR /app/src/BiddingService
RUN dotnet publish -c Release -o /app/src/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "BiddingService.dll" ]