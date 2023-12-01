# stage 1
FROM node:21.0.0  as node
WORKDIR /app
COPY . .
RUN npm install --force
RUN npm run build --prod

# stage 2
FROM nginx:alpine
COPY --from=node /app/dist/survey /usr/share/nginx/html
COPY /nginx.conf  /etc/nginx/conf.d/default.conf
EXPOSE 80