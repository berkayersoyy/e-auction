#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EAuction.Order.WebApi/EAuction.Order.WebApi.csproj", "EAuction.Order.WebApi/"]
COPY ["EAuction.Order.Application/EAuction.Order.Application.csproj", "EAuction.Order.Application/"]
COPY ["EAuction.Order.Domain/EAuction.Order.Domain.csproj", "EAuction.Order.Domain/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["EAuction.Order.Infrastructure/EAuction.Order.Infrastructure.csproj", "EAuction.Order.Infrastructure/"]
RUN dotnet restore "EAuction.Order.WebApi/EAuction.Order.WebApi.csproj"
COPY . .
WORKDIR "/src/EAuction.Order.WebApi"
RUN dotnet build "EAuction.Order.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EAuction.Order.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EAuction.Order.WebApi.dll"]
