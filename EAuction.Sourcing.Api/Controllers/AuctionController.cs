using System;
using EAuction.Sourcing.Api.Entities;
using EAuction.Sourcing.Api.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;


namespace EAuction.Sourcing.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _eventBus;
        private readonly ILogger<AuctionController> _logger;

        public AuctionController(IAuctionRepository auctionRepository, ILogger<AuctionController> logger, IBidRepository bidRepository, IMapper mapper, EventBusRabbitMQProducer eventBus)
        {
            _auctionRepository = auctionRepository;
            _logger = logger;
            _bidRepository = bidRepository;
            _mapper = mapper;
            _eventBus = eventBus;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctions()
        {
            var result = await _auctionRepository.GetAuctions();
            return Ok(result);
        }
        [HttpGet("{id:length(24)}", Name = "GetAuction")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Auction>> GetAuction(string id)
        {
            var result = await _auctionRepository.GetAuction(id);
            if (result == null)
            {
                _logger.LogError($"Auction with id : {id}, has not been found in database.");
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> CreateAuction([FromBody] Auction auction)
        {
            await _auctionRepository.Create(auction);
            return CreatedAtRoute("GetAuction", new { id = auction.Id },auction);
        }
        [HttpPut]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAuction([FromBody] Auction auction)
        {
            var result = await _auctionRepository.Update(auction);
            return Ok(result);
        }
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAuction(string id)
        {
            var result = await _auctionRepository.Delete(id);
            return Ok(result);
        }
        [HttpPost("CompleteAuction")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]

        public async Task<ActionResult> CompleteAuction([FromBody] string id)
        {
            Auction auction = await _auctionRepository.GetAuction(id);
            if (auction == null)
            {
                return NotFound();
            }

            if (auction.Status != (int)Status.Active)
            {
                _logger.LogError("Auction can not be completed");
                return BadRequest();
            }

            Bid bid = await _bidRepository.GetWinnerBid(id);
            if (bid == null)
            {
                return NotFound();
            }

            OrderCreateEvent eventMessage = _mapper.Map<OrderCreateEvent>(bid);
            eventMessage.Quantity = auction.Quantity;

            auction.Status = (int) Status.Closed;
            bool updateResponse = await _auctionRepository.Update(auction);
            if (!updateResponse)
            {
                _logger.LogError("Auction can not updated");
                return BadRequest();
            }

            try
            {
                _eventBus.Publish(EventBusConstants.OrderCreateQueue,eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.Id,"Sourcing");
                throw;
            }

            return Accepted();
        }

        [HttpPost("TestEvent")]
        public ActionResult<OrderCreateEvent> TestEvent()
        {
            OrderCreateEvent eventMessage = new OrderCreateEvent
            {
                Id = "1",
                AuctionId = "dummy1",
                ProductId = "dummy_product_1",
                Price = 10,
                Quantity = 100,
                SellerUserName = "test@test.com"
            };
            try
            {
                _eventBus.Publish(EventBusConstants.OrderCreateQueue,eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"ERROR Publishing integration event: {EventId} from {AppName}",eventMessage.Id,"Sourcing");
                throw;
            }

            return Accepted(eventMessage);

        }

    }
}
