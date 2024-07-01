## Run All services with docker compose

Start docker. In the project root folder, run the command

```
$ docker-compose up --build
```

output
```
$ docker ps

CONTAINER ID   IMAGE                        COMMAND                  CREATED         STATUS         PORTS                                                                                                         NAMES
c9636fb3d9bd   cash-manager-report-api      "dotnet CashManager.…"   8 minutes ago   Up 5 minutes   5080/tcp, 0.0.0.0:5080->80/tcp                                                                                cashmanager-cash-manager-report-api-1
fa481ded4446   cash-manager-daily-api       "dotnet CashManager.…"   8 minutes ago   Up 8 minutes   5079/tcp, 0.0.0.0:5079->80/tcp                                                                                cashmanager-cash-manager-daily-api-1
b2354809fec5   mongo:latest                 "docker-entrypoint.s…"   8 minutes ago   Up 8 minutes   0.0.0.0:27017->27017/tcp                                                                                      mongo
689918d926eb   datalust/seq                 "/bin/seqentry"          8 minutes ago   Up 8 minutes   443/tcp, 45341/tcp, 0.0.0.0:5341->5341/tcp, 0.0.0.0:9000->80/tcp                                              seq
ae85aa35c9da   rabbitmq:management-alpine   "docker-entrypoint.s…"   8 minutes ago   Up 8 minutes   4369/tcp, 5671/tcp, 0.0.0.0:5672->5672/tcp, 15671/tcp, 15691-15692/tcp, 25672/tcp, 0.0.0.0:15672->15672/tcp   rabbitmq
```

<img width="1371" alt="Screenshot 2024-07-01 at 15 47 54" src="https://github.com/wodsonluiz/CashManager/assets/13908258/c6d8b9ef-f10c-40ee-b214-8da9878e4734">


### RabbitM1
- http://localhost:15672/#/
- Connectionstring: amqp://admin:admin123@rabbitmq:5672/Staging
- Exchange created: cache-manager-exchange
- Queue created: cache-manager-transactions
- Bind routingkey: transactions.#

### Seq
- http://localhost:9000/#/events?range=1d
- Logging applications

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

