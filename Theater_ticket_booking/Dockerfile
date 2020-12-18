FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Theater_ticket_booking/Theater_ticket_booking.csproj", "Theater_ticket_booking/"]
RUN dotnet restore "Theater_ticket_booking/Theater_ticket_booking.csproj"
COPY ./Theater_ticket_booking ./Theater_ticket_booking

WORKDIR "/src/Theater_ticket_booking"
RUN dotnet build "Theater_ticket_booking.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Theater_ticket_booking.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

WORKDIR "/app/wwwroot"
RUN npm install

WORKDIR /app
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Theater_ticket_booking.dll