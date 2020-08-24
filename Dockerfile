ARG version='1.0.0'
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
ARG version
WORKDIR /src

# Copia os arquivos para dentro do container
#COPY nuget.config .
COPY . .

# Executa o build na solução inteira
RUN dotnet build /p:RestoreUseSkipNonexistentTargets="false"

# Executa o publish da API
WORKDIR /src/Cadastro
RUN dotnet publish /p:LangVersion=latest /p:RestoreUseSkipNonexistentTargets="false" /p:Version=$version --self-contained false -c 'Release'  -o /app
RUN mkdir /app/logs/
RUN mkdir /app/wwwroot/

#Copia os arquivos do artefato para o file system do host
FROM build AS CopyToHost
VOLUME [ "/output" ]
WORKDIR /app
ENTRYPOINT ["cp","-p","-R", "./","/output/"]

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
EXPOSE 80
WORKDIR /app
ENV ASPNETCORE__URLS=http://+:80
COPY --from=build /app . 
ENTRYPOINT ["dotnet", "Cadastro.dll" ]