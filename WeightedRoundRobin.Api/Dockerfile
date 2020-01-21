# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /
COPY *.sln .
COPY /*.csproj WeightedRoundRobin.Api/
RUN dotnet restore
COPY . .
#
## testing
#FROM build AS testing
#WORKDIR /
#RUN dotnet build
#WORKDIR /
#RUN dotnet test
#
# publish
FROM build AS publish
WORKDIR /
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /publish .
# ENTRYPOINT ["dotnet", "WeightedRoundRobin.Api.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet WeightedRoundRobin.Api.dll