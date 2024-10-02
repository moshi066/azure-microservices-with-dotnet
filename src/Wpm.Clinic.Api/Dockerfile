#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Wpm.Clinic.Api/Wpm.Clinic.Api.csproj", "Wpm.Clinic.Api/"]
RUN dotnet restore "Wpm.Clinic.Api/Wpm.Clinic.Api.csproj"
COPY . .
WORKDIR "/src/Wpm.Clinic.Api"
RUN dotnet build "Wpm.Clinic.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wpm.Clinic.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wpm.Clinic.Api.dll"]
