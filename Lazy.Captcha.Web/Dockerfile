#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lazy.Captcha.Web/Lazy.Captcha.Web.csproj", "Lazy.Captcha.Web/"]
COPY ["Lazy.Captcha.Redis/Lazy.Captcha.Redis.csproj", "Lazy.Captcha.Redis/"]
COPY ["Lazy.Captcha.Core/Lazy.Captcha.Core.csproj", "Lazy.Captcha.Core/"]
RUN dotnet restore "Lazy.Captcha.Web/Lazy.Captcha.Web.csproj"
COPY . .
WORKDIR "/src/Lazy.Captcha.Web"
RUN dotnet build "Lazy.Captcha.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lazy.Captcha.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lazy.Captcha.Web.dll"]