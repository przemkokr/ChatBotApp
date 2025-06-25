FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ChatBot.API/ChatBot.API.csproj", "ChatBot.API/"]
COPY ["ChatBot.Domain/ChatBot.Domain.csproj", "ChatBot.Domain/"]
COPY ["ChatBot.Infrastructure/ChatBot.Infrastructure.csproj", "ChatBot.Infrastructure/"]
RUN dotnet restore "ChatBot.API/ChatBot.API.csproj"

COPY . .

RUN dotnet build "ChatBot.API/ChatBot.API.csproj" -c Release -o /app/build

RUN dotnet publish "ChatBot.API/ChatBot.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ChatBot.API.dll"]
