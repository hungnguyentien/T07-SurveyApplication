#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /api
#COPY ["./bin/Debug/net6.0", "/api"]
#EXPOSE 80/tcp
#
#
#ENTRYPOINT ["dotnet", "SurveyApplication.API.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY published/ ./
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "SurveyApplication.API.dll"]