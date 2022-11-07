FROM mcr.microsoft.com/mssql/server:2022-latest as Builder
WORKDIR /work/
COPY build/ .
RUN /bin/bash ./setup.sh

FROM mcr.microsoft.com/mssql/server:2022-latest
WORKDIR /var/opt/mssql/data/
COPY --from=Builder /var/opt/mssql/data/AdventureWorks.mdf .
COPY --from=Builder /var/opt/mssql/data/AdventureWorks_log.ldf .
COPY run/ /opt/adventureworks

ENTRYPOINT ["/bin/bash", "/opt/adventureworks/entrypoint.sh"]
