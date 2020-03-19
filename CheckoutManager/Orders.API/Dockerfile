FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Orders.API/Orders.API.csproj", "Orders.API/"]
COPY ["Service.Common/Service.Common.csproj", "Service.Common/"]
RUN dotnet restore "Orders.API/Orders.API.csproj"
COPY . .
WORKDIR "/src/Orders.API"
RUN dotnet build "Orders.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orders.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:57325
ENTRYPOINT ["dotnet", "Orders.API.dll"]