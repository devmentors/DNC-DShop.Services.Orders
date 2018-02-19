#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/DShop.Services.Orders
dotnet run --no-restore