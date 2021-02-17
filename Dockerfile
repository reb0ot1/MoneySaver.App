#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["MoneySaver.App/MoneySaver.App/MoneySaver.App.csproj", "MoneySaver.App/"]
COPY ["MoneySaver.App/MoneySaver.App.Models/MoneySaver.App.Models.csproj", "MoneySaver.App.Models/"]
RUN dotnet restore "MoneySaver.App/MoneySaver.App.csproj"
COPY ["MoneySaver.App/MoneySaver.App/.", "MoneySaver.App/"]
COPY ["MoneySaver.App/MoneySaver.App.Models/.", "MoneySaver.App.Models/"]
RUN dotnet build "MoneySaver.App/MoneySaver.App.csproj" -c Release -o /MoneySaver.App/build

FROM build AS publish
RUN dotnet publish "MoneySaver.App/MoneySaver.App.csproj" -c Release -o /MoneySaver.App/publish

FROM nginx:alpine AS final
COPY ["testcert/localhost.crt", "/home/pki/localhost.crt"]
COPY ["testcert/localhost.key", "/home/pki/localhost.key"]
COPY ["testcert/RootCA.crt", "/usr/local/share/ca-certificates/RootCA.crt"]
RUN update-ca-certificates
WORKDIR /usr/share/nginx/html
COPY --from=publish /MoneySaver.App/publish/wwwroot .
COPY MoneySaver.App/MoneySaver.App/nginx.conf /etc/nginx/nginx.conf