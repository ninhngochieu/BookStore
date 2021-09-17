FROM mcr.microsoft.com/dotnet/sdk as sdk

WORKDIR /app

COPY . /app/

RUN dotnet restore

RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet

WORKDIR /app

COPY --from=sdk /app/out /app/

EXPOSE 80
EXPOSE 443

# ENV ASPNETCORE_URLS=http://*:$PORT

ENTRYPOINT [ "dotnet","BookStore.dll" ]