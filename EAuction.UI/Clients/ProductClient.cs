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
    public class ProductClient
    {
        public readonly HttpClient _client;

        public ProductClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(CommonInfo.LocalProductBaseAdress);
        }

        public async Task<Result<List<ProductViewModel>>> GetProducts()
        {
            var response = await _client.GetAsync("api/v1/Products");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ProductViewModel>>(responseData);
                if (result.Any())
                {
                    return new Result<List<ProductViewModel>>(true,ResultConstant.RecordFound,result.ToList());
                }

                return new Result<List<ProductViewModel>>(false,ResultConstant.RecordNotFound);
            }
            //TODO: BadSuccess message need to be added!
            return new Result<List<ProductViewModel>>(false, ResultConstant.RecordNotFound);
        }

    }
}