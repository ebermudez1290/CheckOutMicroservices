#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["Checkout.Service/Checkout.Service.csproj", "Checkout.Service/"]
RUN dotnet restore "Checkout.Service/Checkout.Service.csproj"
COPY . .
WORKDIR "/src/Checkout.Service"
RUN dotnet build "Checkout.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Checkout.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS=http://+:64333
ENTRYPOINT ["dotnet", "Checkout.Service.dll"]