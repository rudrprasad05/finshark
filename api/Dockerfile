# Use lightweight ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production

# Copy the published output from the build stage
COPY ./bin/Release/net8.0/publish/ .

# Expose port 8081
EXPOSE 8081 3306

# Run the application
ENTRYPOINT ["dotnet", "api.dll"]