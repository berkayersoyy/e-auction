using System;
using System.Text;
using AutoMapper;
using EAuction.Order.Application.Commands.OrderCreate;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EAuction.Order.WebApi.Consumers
{
    public class EventBusOrderCreateConsumer
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventBusOrderCreateConsumer(IRabbitMQPersistentConnection persistentConnection, IMediator mediator, IMapper mapper)
        {
            _persistentConnection = persistentConnection;
            _mediator = mediator;
            _mapper = mapper;
        }

        public void Consume()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.OrderCreateQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue:EventBusConstants.OrderCreateQueue,autoAck:true,consumer:consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var @event = JsonConvert.DeserializeObject<OrderCreateEvent>(message);

            if (e.RoutingKey == EventBusConstants.OrderCreateQueue)
            {
                var command = _mapper.Map<OrderCreateCommand>(@event);
                command.CreatedAt = DateTime.Now;
                command.TotalPrice = @event.Quantity * @event.Price;
                command.UnitPrice = @event.Price;

                var result = await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _persistentConnection.Dispose();
        }
    }
}