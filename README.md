# T07-SurveyApplication

https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-7.0
https://www.commandprompt.com/education/how-to-create-a-postgresql-database-in-docker/#:~:text=To%20create%20a%20PostgreSQL%20database%20in%20Docker%2C%20first%2C%20pull%2F,postgres%E2%80%9D%20command.
https://hevodata.com/learn/docker-postgresql/

dotnet publish -c Release -o published

dotnet published\SurveyApplication.API.dll

docker build -t survey-api:v1.0.0 -f ./Dockerfile .

docker pull postgres
docker run --name postgres -p 5432:5432 -e POSTGRES_USER=dev -e POSTGRES_PASSWORD=123qwe -v /data:/var/lib/postgresql/data -d postgres
docker pull dpage/pgadmin4:latest
docker run --name pgadmin -p 8088:80 -e 'PGADMIN_DEFAULT_EMAIL=test@domain.local' -e 'PGADMIN_DEFAULT_PASSWORD=123qwe' -d dpage/pgadmin4

docker exec -it postgres bash
create user dev with encrypted password '123qwe'
docker ps
docker inspect c0ddd7acab8a| findstr IPAddress

docker volume create postgres