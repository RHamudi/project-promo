FROM mcr.microsoft.com/dotnet/sdk:7.0@sha256:d32bd65cf5843f413e81f5d917057c82da99737cb1637e905a1a4bc2e7ec6c8d AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0@sha256:c7d9ee6cd01afe9aa80642e577c7cec9f5d87f88e5d70bd36fd61072079bc55b
WORKDIR /app
ENV SQL_COCKROACHDB="Server=project-promo-11154.6wr.aws-us-west-2.cockroachlabs.cloud;Port=26257;Database=defaultdb;Username=project-promo;Password=TqkXkqn_G9OAEz5u0aITkQ;"
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Promocoes.API.dll"]