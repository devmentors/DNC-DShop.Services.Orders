FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY ./src/DShop.Services.Orders/bin/docker .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 5000
ENTRYPOINT dotnet DShop.Services.Orders.dll