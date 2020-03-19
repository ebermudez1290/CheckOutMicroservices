FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Checkout.Service/Checkout.Service.csproj", "Checkout.Service/"]
COPY ["Service.Common/Service.Common.csproj", "Service.Common/"]
RUN dotnet restore "Checkout.Service/Checkout.Service.csproj"
COPY . .
WORKDIR "/src/Checkout.Service"
RUN dotnet build "Checkout.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Checkout.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:64333
ENTRYPOINT ["dotnet", "Checkout.Service.dll"]