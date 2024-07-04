## Executar todos os serviços e recursos

1. Iniciar o docker na maquina. 
2. Na pasta raiz do projeto, executar o comando:

```
$ docker-compose up --build
```


```
$ docker ps

//output

CONTAINER ID   IMAGE                        COMMAND                  CREATED         STATUS         PORTS                                                                                                         NAMES
1234   cash-manager-report-api      "dotnet CashManager.…"   8 minutes ago   Up 5 minutes   5080/tcp, 0.0.0.0:5080->80/tcp                                                                                cashmanager-cash-manager-report-api-1
1234   cash-manager-daily-api       "dotnet CashManager.…"   8 minutes ago   Up 8 minutes   5079/tcp, 0.0.0.0:5079->80/tcp                                                                                cashmanager-cash-manager-daily-api-1
1234   mongo:latest                 "docker-entrypoint.s…"   8 minutes ago   Up 8 minutes   0.0.0.0:27017->27017/tcp                                                                                      mongo
1234   datalust/seq                 "/bin/seqentry"          8 minutes ago   Up 8 minutes   443/tcp, 45341/tcp, 0.0.0.0:5341->5341/tcp, 0.0.0.0:9000->80/tcp                                              seq
1234   rabbitmq:management-alpine   "docker-entrypoint.s…"   8 minutes ago   Up 8 minutes   4369/tcp, 5671/tcp, 0.0.0.0:5672->5672/tcp, 15671/tcp, 15691-15692/tcp, 25672/tcp, 0.0.0.0:15672->15672/tcp   rabbitmq
```

<img width="1371" alt="Screenshot 2024-07-01 at 15 47 54" src="https://github.com/wodsonluiz/CashManager/assets/13908258/c6d8b9ef-f10c-40ee-b214-8da9878e4734">


### Infraestrutura

#### RabbitM1
- Acesso: http://localhost:15672/#/
- Connectionstring: amqp://admin:admin123@rabbitmq:5672/Staging
- Exchange criada: cache-manager-exchange
- Queue criada: cache-manager-transactions
- RoutingKey criada: transactions.#

#### Seq
- Acesso: http://localhost:9000/#/events?range=1d


#### Mongo
- Connectionstring: mongodb://admin:admin123@mongo:27017/?retryWrites=true&w=majority
- Essa connectionstring pode ser usada em qualquer client do mongo. (Ex.: Studio3T)

### Aplicativos
#### CashManager.Daily.Api
- Swagger: http://localhost:5079/swagger/index.html

Curl para chamada dos recursos
- Cria a transação => Retorna status code 201 com corpo de resposta vazio
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

- Lista todas as transações => Retorna status code 200 com a lista de transações
```
curl --location 'http://localhost:5079/api/v1/customertransactions'
```

- Lista por id => Retorna status code 200 com uma entidade
```
curl --location 'http://localhost:5079/api/v1/customertransactions/5d8f4624-1d72-4365-b37d-7fd0478c927a'
```

- Lista por documento => Return status code 200 com uma entidade

```
curl --location 'http://localhost:5079/api/v1/customertransactions/12345678910/by-document'
```

### CashManger.Report.Api
- Swagger: http://localhost:5080/swagger/index.html

Curl para chamada dos recursos

- Lista por documento => Return status code 200 com uma entidade contendo o saldo consolidado

```
curl --location 'http://localhost:5080/api/v1/reports/12345678910/by-document'
```

