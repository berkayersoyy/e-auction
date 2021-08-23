using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EAuction.Core.Common;
using EAuction.Core.ResultModels;
using EAuction.UI.ViewModel;
using Newtonsoft.Json;

namespace EAuction.UI.Clients
{
    public class AuctionClient
    {
        public readonly HttpClient _client;

        public AuctionClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(CommonInfo.LocalAuctionBaseAdress);
        }

        public async Task<Result<AuctionViewModel>> CreateAuction(AuctionViewModel model)
        {
            var dataAsString = JsonConvert.SerializeObject(model);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("/api/v1/Auction", content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuctionViewModel>(responseData);
                if (result!=null)
                {
                    return new Result<AuctionViewModel>(true, ResultConstant.RecordCreateSuccessfully, result);
                }
                else
                {
                    return new Result<AuctionViewModel>(false, ResultConstant.RecordCreateNotSuccessfully);
                }
            }
            return new Result<AuctionViewModel>(false, ResultConstant.RecordCreateNotSuccessfully);
        }

        public async Task<Result<List<AuctionViewModel>>> GetAuctions()
        {
            var response = await _client.GetAsync("/api/v1/Auction");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<AuctionViewModel>>(responseData);
                if (result!=null)
                {
                    return new Result<List<AuctionViewModel>>(true,ResultConstant.RecordFound,result.ToList());
                }
                return new Result<List<AuctionViewModel>>(false, ResultConstant.RecordNotFound);
            }
            return new Result<List<AuctionViewModel>>(false, ResultConstant.RecordNotFound);
        }
    }
}