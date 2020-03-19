FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Audit.API/Audit.API.csproj", "Audit.API/"]
COPY ["Service.Common/Service.Common.csproj", "Service.Common/"]
RUN dotnet restore "Audit.API/Audit.API.csproj"
COPY . .
WORKDIR "/src/Audit.API"
RUN dotnet build "Audit.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Audit.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Audit.API.dll"]