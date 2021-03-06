#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CarteiraDigital.API/CarteiraDigital.API.csproj", "CarteiraDigital.API/"]
COPY ["CarteiraDigital.Infrastructure/CarteiraDigital.Infrastructure.csproj", "CarteiraDigital.Infrastructure/"]
COPY ["CarteiraDigital.Domain/CarteiraDigital.Domain.csproj", "CarteiraDigital.Domain/"]
COPY ["CarteiraDigital.Service/CarteiraDigital.Service.csproj", "CarteiraDigital.Service/"]
RUN dotnet restore "CarteiraDigital.API/CarteiraDigital.API.csproj"
COPY . .
WORKDIR "/src/CarteiraDigital.API"
#RUN dotnet build "CarteiraDigital.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarteiraDigital.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarteiraDigital.API.dll"]