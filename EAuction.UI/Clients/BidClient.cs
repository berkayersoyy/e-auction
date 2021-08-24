using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EAuction.Core.Common;
using EAuction.Core.ResultModels;
using EAuction.UI.ViewModel;
using Newtonsoft.Json;

namespace EAuction.UI.Clients
{
    public class BidClient
    {
        public readonly HttpClient _client;

        public BidClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(CommonInfo.BaseAdress);
        }

        public async Task<Result<List<BidViewModel>>> GetBidsByAuctionId(string id)
        {
            var response = await _client.GetAsync("/Bid/GetBidsByAuctionId?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<BidViewModel>>(responseData);
                if (result != null)
                {
                    return new Result<List<BidViewModel>>(true, ResultConstant.RecordFound, result.ToList());
                }

                return new Result<List<BidViewModel>>(false, ResultConstant.RecordNotFound);
            }
            return new Result<List<BidViewModel>>(false, ResultConstant.RecordNotFound);
        }
        public async Task<Result<string>> SendBid(BidViewModel model)
        {
            var dataAsString = JsonConvert.SerializeObject(model);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync("/Bid", content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return new Result<string>(true, ResultConstant.RecordCreateSuccessfully, responseData);
            }
            return new Result<string>(false, ResultConstant.RecordCreateNotSuccessfully);
        }
    }
}