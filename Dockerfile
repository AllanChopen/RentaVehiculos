FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as a separate step to cache dependencies
COPY ["Renta.csproj", "./"]
RUN dotnet restore "Renta.csproj"

# Copy the rest of the files and publish
COPY . .
RUN dotnet publish "Renta.csproj" -c Release -o /app/publish /p:TrimUnusedDependencies=true

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Bind to port 8080 (Koyeb and many platforms expect an explicit port)
ENV ASPNETCORE_URLS="http://+:8080"
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "Renta.dll"]
