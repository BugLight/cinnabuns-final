FROM microsoft/dotnet:2.1-sdk AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:10 AS build-front
WORKDIR /app
COPY CinnabunsFinal/ClientApp/package*.json ./
RUN npm install
COPY CinnabunsFinal/ClientApp .
RUN npm run build

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CinnabunsFinal/CinnabunsFinal.csproj", "CinnabunsFinal/"]
RUN dotnet restore "CinnabunsFinal/CinnabunsFinal.csproj"
COPY CinnabunsFinal CinnabunsFinal
WORKDIR "/src/CinnabunsFinal"
RUN BUILD_DOCKER=1 dotnet build "CinnabunsFinal.csproj" -c Release -o /app
COPY --from=build-front /app/dist /app/ClientApp/dist

FROM build AS publish
RUN dotnet publish "CinnabunsFinal.csproj" -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CinnabunsFinal.dll"]