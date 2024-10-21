#build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src


#restore/download the packages
COPY ["PersonalWebsite.csproj","./"]
RUN dotnet restore 

#build
COPY . .
RUN dotnet build "PersonalWebsite.csproj" -c Release -o /app/build

#publish
FROM build AS publish
RUN dotnet publish "PersonalWebsite.csproj" -c Release -o /app/publish

#Run
FROM mcr.microsoft.com/dotnet/sdk:6.0
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","PersonalWebsite.dll"]