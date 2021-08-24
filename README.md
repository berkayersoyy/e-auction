# <div align="center">E-Auction with Microservice Architecture</div>
</br>
An auction application that I created with .Net 5 and microservices architecture, where you can bid on products with others.

</br>

# Whats Including In This Repository

Product microservice which includes;
*  ASP.NET Core Web API application
*  REST API principles, CRUD operations
*  MongoDB database connection and containerization
*  Repository Pattern Implementation
*  Swagger Open API implementation

Sourcing microservice whic includes;
*  ASP.NET Web API application
*  REST API principles, CRUD operations
*  MongoDB database connection and containerization
*  Repository Pattern Implementation
*  Swagger Open API implementation
*  ASP.NET gRPC Server application

Microservices Communication
*  Sync inter-service gRPC Communication
*  Async Microservices Communication with RabbitMQ Message-Broker Service
*  Using RabbitMQ Publish/Subscribe Topic Exchange Model

Order Microservice
*  Implementing DDD, CQRS, and Clean Architecture with using Best Practices
*  Developing CQRS with using MediatR, FluentValidation and AutoMapper packages
*  SqlServer database connection and containerization
*  Using Entity Framework Core ORM and auto migrate to SqlServer when application startup

API Gateway Ocelot Microservice
*  Implement API Gateways with Ocelot
*  Sample microservices/containers to reroute through the API Gateways
*  Run multiple different API Gateway container types

Docker Compose establishment with all microservices on docker;
*  Containerization of microservices
*  Containerization of databases
*  Override Environment variables

# Project Structure

```
src
├── ApiGateway
|   └── EAuction.ApiGateway
├── Services                    
│   ├── Common
|   |   └── EventBustRabbitMQ
|   ├── Order
|   |   ├── EAuction.Order.Application
|   |   ├── EAuction.Order.Domain
|   |   ├── EAuction.Order.Infrastructure
|   |   └── EAuction.Order.Api
│   ├── Product
|   |   └── EAuction.Products.Api
│   └── Sourcing  
|       └── EAuction.Sourcing.Api
└── WebApp
    ├── EAuction.Core
    ├── EAuction.Infrastructure
    └── EAuction.UI
```
    

# 🚀 Building and Running for Production

1. Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)

2. At the root directory which include docker-compose.yml files, run below command:

        docker-compose up -d --build
        
3. You can launch microservices as below urls:

* Product API -> http://localhost:8000/swagger/index.html 
* Sourcing API -> http://localhost:8001/swagger/index.html 
* Order API -> http://localhost:8002/swagger/index.html 
* API Gateway -> http://localhost:5000/
