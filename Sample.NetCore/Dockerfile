#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.cn-hangzhou.aliyuncs.com/newbe36524/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sample.NetCore/Sample.NetCore.csproj", "Sample.NetCore/"]
COPY ["Lazy.Captcha.Core/Lazy.Captcha.Core.csproj", "Lazy.Captcha.Core/"]
RUN dotnet restore "Sample.NetCore/Sample.NetCore.csproj"
COPY . .
WORKDIR "/src/Sample.NetCore"
RUN dotnet build "Sample.NetCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.NetCore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#修改apt-get源，加速apt下载
RUN sed -i s@/deb.debian.org/@/mirrors.163.com/@g /etc/apt/sources.list
RUN cat /etc/apt/sources.list
#安装fontconfig
RUN apt-get clean
RUN apt-get update
RUN apt-get update && apt-get install -y fontconfig

ENTRYPOINT ["dotnet", "Sample.NetCore.dll"]