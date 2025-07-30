using Application.DTOs.Auctions;
using Application.Models.Pages;
using Pictures.Application.Models.Pages;
using Pictures.Application.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuctionService : IAuctionService
    {
        public Task CheckExpiredAuctionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuctionDto> CloseAuctionAsync(Guid auctionId)
        {
            throw new NotImplementedException();
        }

        public Task<PageDto<AuctionPreviewDto>> GetActiveAuctionsAsync(PageParams pageParams)
        {
            throw new NotImplementedException();
        }

        public Task<AuctionDetailsDto> GetAuctionDetailsAsync(Guid auctionId)
        {
            throw new NotImplementedException();
        }

        public Task<CurrentBidDto> GetHighestBidAsync(Guid auctionId)
        {
            throw new NotImplementedException();
        }

        public Task<BidResultDto> PlaceBidAsync(PlaceBidDto bidDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuctionDto> StartAuctionAsync(Guid auctionId)
        {
            throw new NotImplementedException();
        }
    }
}
