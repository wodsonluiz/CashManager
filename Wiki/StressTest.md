## Testes de estresse
Todos os testes foram executados em ambiente de desenvolvimento (localhost) :fire:

#### Criar transação
- Total requests: 13k
- RPM: 97.48
- AVG response: 6ms
- MIN response: 2ms
- MAX response: 603ms
- Error rate: 0

<img width="1278" alt="Screenshot 2024-06-27 at 22 30 54" src="https://github.com/wodsonluiz/CashManager/assets/13908258/e6d1c64e-ca00-4610-b7bb-57b3996856e1">

#### Listar saldo consolidado
- Dados por requisição trafegados na rede : ~1mb, com 13k transações
- Total request: 1.4k
- AVG response: 3s
- MIN response: 65ms
- MAX response: 11s
- Error rate: 0

<img width="1268" alt="Screenshot 2024-06-27 at 22 38 57" src="https://github.com/wodsonluiz/CashManager/assets/13908258/4b9be163-810d-4b51-a04a-5a739df91d69">
