FROM mcr.microsoft.com/mssql/server:2022-latest
WORKDIR /work/

COPY build/ .

RUN /bin/bash ./setup.sh

ENTRYPOINT ["/opt/mssql/bin/sqlservr"]
