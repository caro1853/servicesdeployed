# What is docker

Docker is a software platform that allows you to quickly build, test, and deploy applications. Docker packages software into standardized units called containers that include everything needed for the software to run, including libraries, system tools, code, and runtime. [More](https://docs.docker.com/get-started/overview/)

---
## [Previous](../README.md)
---

# What is docker compose

It is a tool for defining and running multi-container Docker applications. In Compose, a YAML file is used to configure application services. Then, with a single command, all services in the configuration are created and started.

---
## [Previous](../README.md)
---

# File docker-compose

```yaml
version: '3.4'

services:
  schedulingdb:
    image: mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
    container_name: schedulingdb
    platform: linux/amd64
    environment:
        SA_PASSWORD: "Caro123456"
        ACCEPT_EULA: "Y"
    restart: always
    ports:
        - "1433:1433"
```

# Create and run the sql server image

Once you have Docker and Docker compose installed, run the following command to generate the image and run the container.

```sh
# Create and start the container.
# -d means detach.
# --build : to recreate the containers
# downd instead up, stop containers
docker-compose -f docker-compose-schedulingdb.yaml up -d
```

---
## [Previous](../README.md)
---

## Connection String

Suppose the database is called SchedulingDb the following would be the connection string running inside the previously created container.

```json
{
  "ConnectionStrings": {
    "SchedulingConnectionString": "Server=127.0.0.1;Database=SchedulingDb;User Id=sa;Password=Caro123456;TrustServerCertificate=true"
  }
}
```

---
## [Previous](../README.md)
---

# Some useful commands

```sh
# list running container
docker ps
# OutPut
# 7ce26d00b986   mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04   "/opt/mssql/bin/perm…"   9 days ago    Up 9 hours   0.0.0.0:1433->1433/tcp   schedulingdb
# list created containers
docker ps -a
# Entrar al contenedor
docker exec -it schedulingdb sh
# Output
# $
# Usar el cliente de sql server dentro del contenedor 
/opt/mssql-tools/bin/sqlcmd -S 127.0.0.1 -U SA -P "Caro123456"
# Output
# 1>
SELECT Name from sys.databases;
GO
# Output
# Name
# ------
# master
# tempdb
# model
# SchedulingDb
use SchedulingDb
GO
# Output
# Changed database context to 'SchedulingDb'.
# Tables created within the database SchedulingDb
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'SchedulingDb'
# Output
TABLE_NAME
# ------------------------
# __EFMigrationsHistory
# Doctors
# Patients
# OperationalHours
# Appointments
# Hours
```

---
## [Previous](../README.md)
---