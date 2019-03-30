FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CinnabunsFinal/CinnabunsFinal.csproj", "CinnabunsFinal/"]
RUN dotnet restore "CinnabunsFinal/CinnabunsFinal.csproj"
COPY . .
WORKDIR "/src/CinnabunsFinal"
RUN dotnet build "CinnabunsFinal.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CinnabunsFinal.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CinnabunsFinal.dll"]