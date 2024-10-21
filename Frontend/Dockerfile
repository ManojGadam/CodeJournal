FROM node:20-alpine3.19
WORKDIR /app
ADD package*.json ./
RUN npm install
ADD . .
EXPOSE 3000
CMD npm run dev

