FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim  AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Customer.API/Customer.API.csproj", "Customer.API/"]
COPY ["Service.Common/Service.Common.csproj", "Service.Common/"]
RUN dotnet restore "Customer.API/Customer.API.csproj"
COPY . .
WORKDIR "/src/Customer.API"
RUN dotnet build "Customer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:60045
ENTRYPOINT ["dotnet", "Customer.API.dll"]