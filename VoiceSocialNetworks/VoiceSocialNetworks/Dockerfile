FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["./VoiceSocialNetworks.csproj", "VoiceSocialNetworks/"]
RUN dotnet restore "VoiceSocialNetworks/VoiceSocialNetworks.csproj"
COPY . ./VoiceSocialNetworks
WORKDIR "/src/VoiceSocialNetworks"
RUN dotnet build "VoiceSocialNetworks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VoiceSocialNetworks.csproj" -c Release -o /app/publish

FROM node as React
WORKDIR /app
COPY "ClientApp" ./ClientApp/
WORKDIR /app/ClientApp
RUN npm install
RUN npm install webpack-cli -g
RUN webpack-cli --config "webpack.config.js"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=React /app/ClientApp ./ClientApp
ENTRYPOINT ["dotnet", "VoiceSocialNetworks.dll"]
