FROM mcr.microsoft.com/dotnet/sdk as sdk

WORKDIR /app

COPY . /app/

RUN dotnet restore

RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet

WORKDIR /app

COPY --from=sdk /app/out /app/

EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5001

ENTRYPOINT [ "dotnet","BookStore.dll" ]