## Run All services with docker compose

Start docker. In the project root folder, run the command

```
$ docker-compose up --build
```

output
```
$ docker ps

CONTAINER ID   IMAGE                        COMMAND                  CREATED              STATUS              PORTS                                                                                                         NAMES
id   cash-manager-report-api      "dotnet CashManager.…"   About a minute ago   Up About a minute   5080/tcp, 0.0.0.0:5080->80/tcp                                                                                cashmanager-cash-manager-report-api-1
id   cash-manager-daily-api       "dotnet CashManager.…"   About a minute ago   Up About a minute   5079/tcp, 0.0.0.0:5079->80/tcp                                                                                cashmanager-cash-manager-daily-api-1
id   rabbitmq:management-alpine   "docker-entrypoint.s…"   2 hours ago          Up About a minute   4369/tcp, 5671/tcp, 0.0.0.0:5672->5672/tcp, 15671/tcp, 15691-15692/tcp, 25672/tcp, 0.0.0.0:15672->15672/tcp   rabbitmq
id   mongo:latest                 "docker-entrypoint.s…"   2 hours ago          Up About a minute   0.0.0.0:27017->27017/tcp                                                                                      mongo
```

### RabbitM1
- http://localhost:15672/#/
- Connectionstring: amqp://admin:admin123@rabbitmq:5672/Staging
- Exchange created: cache-manager-exchange
- Queue created: cache-manager-transactions
- Bind routingkey: transactions.# 

### Mongo
Connectionstring: mongodb://admin:admin123@mongo:27017/?retryWrites=true&w=majority

### CashManager.Daily.Api
- Swagger: http://localhost:5079/swagger/index.html

Scripts for call service
- Create transaction => Return status code 201 with body empty
```
curl --location 'http://localhost:5079/api/v1/customertransactions' \
--header 'Content-Type: application/json' \
--data-raw '{
    "name": "Wodson",
    "email": "wodsonluiz@live.com",
    "profile": "Administrator",
    "document": "12345678910",
    "company": {
        "name": "company test ltda"
    },
    "Transaction": {
        "operationType": "credit" // debit,
        "amount": 1000
    }
}'
```

- Get all => Return status code 200 with list transactions
```
curl --location 'http://localhost:5079/api/v1/customertransactions'
```

- Get by id => Return status code 200 with one entity
```
curl --location 'http://localhost:5079/api/v1/customertransactions/5d8f4624-1d72-4365-b37d-7fd0478c927a'
```

- Get by document => Return status code 200 with one entity

```
curl --location 'http://localhost:5079/api/v1/customertransactions/12345678910/by-document'
```

### CashManger.Report.Api
- Swagger: http://localhost:5080/swagger/index.html

Scripts for call service

- Get by document => Return 200 with consolidated balance

```
curl --location 'http://localhost:5080/api/v1/reports/12345678910/by-document'
```

