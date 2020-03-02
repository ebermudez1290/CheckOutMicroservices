#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1809 AS build
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
ENV ASPNETCORE_URLS=http://+:61983
ENTRYPOINT ["dotnet", "Audit.API.dll"]