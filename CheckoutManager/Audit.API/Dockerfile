FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["Audit.API/Audit.API.csproj", "Audit.API/"]
RUN dotnet restore "Audit.API/Audit.API.csproj"
COPY . .
WORKDIR "/src/Audit.API"
RUN dotnet build "Audit.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Audit.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS=http://+:61990
ENTRYPOINT ["dotnet", "Audit.API.dll"]