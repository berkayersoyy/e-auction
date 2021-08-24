# <div align="center">E-Auction with Microservice Architecture</div>
</br>
An auction application that I created with .Net 5 and microservices architecture, where you can bid on products with others.

</br>

# 🚀 Building and Running for Production

1. Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)

2. At the root directory which include docker-compose.yml files, run below command:

        docker-compose up -d --build
        
3. You can launch microservices as below urls:

* Product API -> http://localhost:8000/swagger/index.html 
* Sourcing API -> http://localhost:8001/swagger/index.html 
* Order API -> http://localhost:8002/swagger/index.html 
* API Gatewar -> http://localhost:5000/
