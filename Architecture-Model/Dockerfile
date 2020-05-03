
FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . .

WORKDIR /src/OrderManagementApplication
RUN dotnet restore ./OrderManagementApplication.csproj
RUN dotnet build "OrderManagementApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManagementApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManagementApplication.dll"];
