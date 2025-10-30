FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY LeadTCGPRank.sln ./
COPY LeadTCGPRank.App/LeadTCGPRank.App.csproj LeadTCGPRank.App/
COPY LeadTCGPRank.App/LeadTCGPRank.App.Client/LeadTCGPRank.App.Client.csproj LeadTCGPRank.App/LeadTCGPRank.App.Client/
RUN dotnet restore

COPY . .
WORKDIR /src/LeadTCGPRank.App
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "LeadTCGPRank.App.dll"]
