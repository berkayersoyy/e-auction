using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EAuction.Sourcing.Api.Entities;
using EAuction.Sourcing.Api.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EAuction.Sourcing.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<BidController> _logger;

        public BidController(IBidRepository bidRepository, ILogger<BidController> logger)
        {
            _bidRepository = bidRepository;
            _logger = logger;
        }
        [HttpPost]
        [ProducesResponseType(typeof(Bid),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> SendBid([FromBody]Bid bid)
        {
            await _bidRepository.SendBid(bid);
            return Ok();
        }

        [HttpGet("GetBidsByAuctionId")]
        [ProducesResponseType(typeof(IEnumerable<Bid>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidsByAuctionId(string id)
        {
            var bids = await _bidRepository.GetBidsByAuctionId(id);
            return Ok(bids);
        }

        [HttpGet("GetWinnerBid")]
        [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Bid>> GetWinnerBid(string id)
        {
            var bid = await _bidRepository.GetWinnerBid(id);
            return Ok(bid);
        }
    }
}
