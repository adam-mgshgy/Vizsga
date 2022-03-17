#stage 1
FROM node:14.18 as node
RUN npm install -g @angular/cli
WORKDIR /app
COPY . .
RUN sed -i 's|base href="/"|base href="/moveyourbody/"|g' /app/src/index.html
RUN npm install
RUN ng build --prod
#stage 2
FROM nginx:alpine
COPY --from=node /app/dist/moveyourbody /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf
