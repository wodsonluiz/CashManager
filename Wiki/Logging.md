## Logging

Logs escritos no Seq com o uso do Serilog.

Acesso: http://localhost:9000/#/events?range=1d

### Querys por aplicativo

#### CashManager.Daily.Api
É logado todas as actions POST/GET da controller.

```
Project = 'CashManager.Daily.Api'
``` 
![Screenshot 2024-07-01 at 15 44 16](https://github.com/wodsonluiz/CashManager/assets/13908258/c45f08f8-4e5e-42fd-9149-9c737c65d567)

#### CashManager.Report.Api

É logado o processamento de consumo dos eventos e eventuais erros.

```
Project = 'CashManager.Report.Api'
```

![Screenshot 2024-07-01 at 15 45 11](https://github.com/wodsonluiz/CashManager/assets/13908258/aca4a414-3191-43d9-af4a-0d00abd5c7bd)
