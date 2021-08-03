using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnsClient.Internal;
using EAuction.Products.Api.Repositories.Abstractions;
using Microsoft.Extensions.Logging;

namespace EAuction.Products.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Variables

        private IProductRepository _productRepository;
        private ILogger<ProductsController> _logger;

        #endregion
        #region Constructor
        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        #endregion
    }
        
}
