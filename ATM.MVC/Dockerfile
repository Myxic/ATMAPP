#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ATM.MVC/ATM.MVC.csproj", "ATM.MVC/"]
COPY ["ATM.BLL/ATM.BLL.csproj", "ATM.BLL/"]
COPY ["ATM.DAL/ATM.DAL.csproj", "ATM.DAL/"]
RUN dotnet restore "ATM.MVC/ATM.MVC.csproj"
COPY . .
WORKDIR "/src/ATM.MVC"
RUN dotnet build "ATM.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ATM.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ATM.MVC.dll"]
