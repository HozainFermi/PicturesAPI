
using Pictures.Application.Models.ProductExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Application.ServiceInterfaces
{
    public interface IAuctionService
    {
        public interface IAuctionService
        {
          
            Task<AuctionDto> StartAuctionAsync(Guid auctionId);
            Task<AuctionDto> CloseAuctionAsync(Guid auctionId);

           
            Task<BidResultDto> PlaceBidAsync(PlaceBidDto bidDto);
            Task<CurrentBidDto> GetHighestBidAsync(Guid auctionId);

          
            Task<AuctionDetailsDto> GetAuctionDetailsAsync(Guid auctionId);
            Task<PageDto<AuctionPreviewDto>> GetActiveAuctionsAsync(PageParams pageParams);

            
            Task CheckExpiredAuctionsAsync();
        }


    }
}
