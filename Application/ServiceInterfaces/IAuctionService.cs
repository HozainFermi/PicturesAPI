
using Application.DTOs.Auctions;
using Application.Models.Pages;
using Pictures.Application.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Application.ServiceInterfaces
{
    public interface IAuctionService
    {
        

            public Task<AuctionDto> StartAuctionAsync(Guid auctionId);
            public Task<AuctionDto> CloseAuctionAsync(Guid auctionId);


            public Task<BidResultDto> PlaceBidAsync(PlaceBidDto bidDto);
            public Task<CurrentBidDto> GetHighestBidAsync(Guid auctionId);


            public Task<AuctionDetailsDto> GetAuctionDetailsAsync(Guid auctionId);
            public Task<PageDto<AuctionPreviewDto>> GetActiveAuctionsAsync(PageParams pageParams);


            public Task CheckExpiredAuctionsAsync();
        


    }
}
