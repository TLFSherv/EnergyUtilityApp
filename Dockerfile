FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Install Node.js (Same as before)
RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash -
RUN apt-get install -y nodejs

# 2. Copy the .csproj and the package.json files
COPY ["EnergyUtilityApp.csproj", "./"]
COPY ["package.json", "./"]
# If you have a package-lock.json, copy that too!
# COPY ["package-lock.json", "./"]

# 3. Install both .NET and NPM dependencies
RUN dotnet restore
RUN npm install

# 4. Copy the rest of the source code
COPY . .

# 5. Now publish (this will trigger your npm run build:css successfully)
RUN dotnet publish -c Release -o /app/out

# --- Final Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "EnergyUtilityApp.dll"]