#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FC.Codeflix.catalog.Api/FC.Codeflix.catalog.Api.csproj", "src/FC.Codeflix.catalog.Api/"]
RUN dotnet restore "src/FC.Codeflix.catalog.Api/FC.Codeflix.catalog.Api.csproj"
COPY . .
WORKDIR "/src/src/FC.Codeflix.catalog.Api"
RUN dotnet build "FC.Codeflix.catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FC.Codeflix.catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FC.Codeflix.catalog.Api.dll"]