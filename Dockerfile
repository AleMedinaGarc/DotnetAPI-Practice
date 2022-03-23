# Get base SDK
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /app

# Copy de csproj file and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the project files and build
COPY . ./
RUN dotnet publish -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
EXPOSE 443
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet","APICarData.dll"]