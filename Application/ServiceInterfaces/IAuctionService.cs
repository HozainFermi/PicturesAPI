using Application.DTOs.Auctions;
using Domain.Models.Pagination;

namespace Application.ServiceInterfaces
{
    
        public interface IAuctionService
        {

        public Task<AuctionDto> Create(CreateAuctionRequest req);
        public Task<AuctionDto> StartAuctionAsync(Guid auctionId);
        public Task<AuctionDto> CloseAuctionAsync(Guid auctionId);


        public Task<BidResultDto> PlaceBidAsync(PlaceBidDto bidDto);
        public Task<CurrentBidDto> GetHighestBidAsync(Guid auctionId);


        public Task<AuctionDetailsDto> GetAuctionDetailsAsync(Guid auctionId);
        public Task<PageDto<AuctionPreviewDto>> GetActiveAuctionsAsync(PageParams pageParams);


        public Task CheckExpiredAuctionsAsync();
        }


    
}
