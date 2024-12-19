FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/PlayDate.Web.Host/PlayDate.Web.Host.csproj", "src/PlayDate.Web.Host/"]
COPY ["src/PlayDate.Web.Core/PlayDate.Web.Core.csproj", "src/PlayDate.Web.Core/"]
COPY ["src/PlayDate.Application/PlayDate.Application.csproj", "src/PlayDate.Application/"]
COPY ["src/PlayDate.Core/PlayDate.Core.csproj", "src/PlayDate.Core/"]
COPY ["src/PlayDate.EntityFrameworkCore/PlayDate.EntityFrameworkCore.csproj", "src/PlayDate.EntityFrameworkCore/"]
WORKDIR "/src/src/PlayDate.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/PlayDate.Web.Host", "src/PlayDate.Web.Host"]
COPY ["src/PlayDate.Web.Core", "src/PlayDate.Web.Core"]
COPY ["src/PlayDate.Application", "src/PlayDate.Application"]
COPY ["src/PlayDate.Core", "src/PlayDate.Core"]
COPY ["src/PlayDate.EntityFrameworkCore", "src/PlayDate.EntityFrameworkCore"]
WORKDIR "/src/src/PlayDate.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "PlayDate.Web.Host.dll"]
