# T07-SurveyApplication

https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-7.0

dotnet publish -c Release -o published

dotnet published\SurveyApplication.API.dll

docker build -t survey-api:v1.0.0 -f ./Dockerfile .