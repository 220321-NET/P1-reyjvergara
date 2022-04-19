FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY . .

RUN dotnet clean P1-reyjvergara.sln
RUN dotnet publish WebAPI --configuration Debug -o ./publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run

WORKDIR /app

COPY --from=build /app/publish .

# When user runs our image in their container, execute dotnet WebAPI.dll
CMD ["dotnet", "WebAPI.dll"]