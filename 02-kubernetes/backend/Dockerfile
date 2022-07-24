FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ARG serviceName
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG serviceName
WORKDIR /build
COPY *.sln src/**/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done && dotnet restore "${serviceName}.sln"
COPY . .
RUN dotnet build --no-restore "${serviceName}.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "src/Backend/Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]