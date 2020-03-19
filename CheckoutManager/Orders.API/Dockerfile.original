
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 57325

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["Orders.API/Orders.API.csproj", "Orders.API/"]
RUN dotnet restore "Orders.API/Orders.API.csproj"
COPY . .
WORKDIR "/src/Orders.API"
RUN dotnet build "Orders.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Orders.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS=http://+:57325
ENTRYPOINT ["dotnet", "Orders.API.dll"]