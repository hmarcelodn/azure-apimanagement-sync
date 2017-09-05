FROM microsoft/aspnetcore:2.0.0
ARG source
RUN echo "source: $source"
WORKDIR /app
EXPOSE 6000
COPY ${source:-/build} .
ENTRYPOINT ["dotnet", "ApiMgmSynchronizer.Service.dll"]
