FROM node:20-alpine AS build
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build --prod

FROM nginx:alpine
COPY --from=build /app/dist/chat-bot-ui/browser /usr/share/nginx/html
EXPOSE 80
