#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Development
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Weekly Weather.csproj", "./"]
RUN dotnet restore "./Weekly Weather.csproj"
COPY . .
RUN dotnet build "Weekly Weather.csproj" -c Release -o /app/build

#Migrations * Move to CI/CD Pipleines
FROM build AS migrations
WORKDIR /src
RUN dotnet tool install --version 7.0.14 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet-ef database update --project /src/ --startup-project /src/

#Production
FROM build AS publish
RUN dotnet publish "Weekly Weather.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM build AS final
#Copy HTTPS Certs 
WORKDIR /https
COPY ["./Https/aspnetapp.pfx" ,  "./"]
RUN update-ca-certificates
#HTTPS Certs
RUN dotnet dev-certs https --clean --import ./aspnetapp.pfx -p hyejin      
RUN dotnet dev-certs https --check --trust

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Weekly Weather.dll"]


