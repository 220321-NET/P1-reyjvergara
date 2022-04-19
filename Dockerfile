FROM mcr.microsoft.com/dotnet/sdk:6.0 
#AS build
ENV ASPNETCORE_URLS=http://+:5000

WORKDIR /app

COPY . .

RUN dotnet restore

# RUN dotnet clean
RUN dotnet build 
RUN dotnet publish WebAPI --configuration Debug -o ./publish
#EXPOSE 5000
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run

#WORKDIR /app

#COPY --from=build app/publish .

# When user runs our image in their container, execute dotnet WebAPI.dll
CMD ["dotnet", "./publish/WebAPI.dll"]