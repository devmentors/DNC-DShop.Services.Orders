FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5005
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet DShop.Services.Orders.dll