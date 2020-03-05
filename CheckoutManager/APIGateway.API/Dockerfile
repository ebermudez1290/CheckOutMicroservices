#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1809 AS build
WORKDIR /src
COPY ["APIGateway.API/APIGateway.API.csproj", "APIGateway.API/"]
RUN dotnet restore "APIGateway.API/APIGateway.API.csproj"
COPY . .
WORKDIR "/src/APIGateway.API"
RUN dotnet build "APIGateway.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "APIGateway.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS=http://+:50434
ENTRYPOINT ["dotnet", "APIGateway.API.dll"]