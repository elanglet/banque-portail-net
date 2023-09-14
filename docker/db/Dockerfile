FROM mariadb:10.8

COPY ./sources/init-db.sql /docker-entrypoint-initdb.d

COPY --chmod=777 ./sources/tests-scripts/ .
