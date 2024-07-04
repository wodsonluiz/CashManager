# CashManager :arrow_down: :arrow_up:
Solução para cotronle de débitos e creditação no saldo do cliente.


![CashManager (1)](https://github.com/wodsonluiz/CashManager/assets/13908258/85943bb4-d543-491c-a738-91657875afdc)

- [Início/ Requisitos](https://github.com/wodsonluiz/CashManager/blob/main/Wiki/QuickStart.md)
- [Executar todos os serviços e recursos](https://github.com/wodsonluiz/CashManager/blob/main/Wiki/RunAllServicesWithDockerCompose.md)
- [Executar cada aplicativo e recurso](https://github.com/wodsonluiz/CashManager/blob/main/Wiki/RunSegregateServices.md)
- [Testes de unidade](https://github.com/wodsonluiz/CashManager/blob/main/Wiki/UnitTests.md)
- [Testes de estresse](https://github.com/wodsonluiz/CashManager/blob/main/Wiki/StressTest.md)
- [Health check](https://github.com/wodsonluiz/CashManager/blob/main/Wiki/HealthCheck.md)
- [Log dos aplicativos](https://github.com/wodsonluiz/CashManager/blob/main/Wiki/Logging.md)



### O Projeto
Tecnologias utilizadas/infraestrutura
- **Projeto**: O projeto foi construído utilizando .NET 8. Decidi usar .NET 8 desde já, pois o suporte da Microsoft para o .NET 6 vai até novembro de 2024.
- **Mensageria**: Para prover o fluxo assíncrono, o input de transações é repassado via RabbitMQ.
- **Banco de Dados**: O banco utilizado é MongoDB, que além de fornecer uma API simples de implementar, é excelente para escrita de dados.

### Disponibilidade
#### Disclaimer - Triplo Nine
Os aplicativos têm uma interconexão de recursos. Em um contexto mais rigoroso de microserviços, o ideal seria fornecer uma base de dados diferente para cada aplicativo. No mercado, nos deparamos com serviços com SLA de 99,9%, o que garante conformidade e disponibilidade para nossos aplicativos clientes desses serviços.

#### Disponibilidade dos aplicativos
Alta disponibilidade em aplicativos significa garantir que eles estejam sempre disponíveis e em funcionamento, minimizando o tempo de inatividade. Neste projeto, isso é alcançado por meio da segregação de contexto delimitado (metodologia praticada no DDD). A indisponibilidade de qualquer contexto não impacta diretamente os outros.
- **CashManager.Daily.Api**: Responsável por realizar o input das transações e produzir os eventos do fluxo assíncrono.
- **CashManager.Report.Api**: Responsável por prover a consolidação de saldo e consumir os eventos transacionais.

### Dominio
Na classe `CustomerTransaction` do código fornecido, podemos identificar vários conceitos do DDD:

**Entity**: A classe `CustomerTransaction` é uma entidade porque possui uma identidade única representada pela propriedade Id. As entidades são objetos que têm uma identidade distinta e contínua ao longo do tempo e diferentes estados.

**Value Objects**: Os parâmetros como `Document`, `Name`, `Email`, `Profile`, `Company`, e `Transaction` podem ser considerados objetos de valor. Eles são imutáveis, e duas instâncias desses objetos são iguais se todos os seus atributos forem iguais. O `Company` e o `Transaction` podem ser exemplos de objetos de valor mais complexos se eles também forem definidos dessa forma em seus respectivos contextos.

**Constructor Validation**: O método `InvalidThrowCustomer` no construtor garante que a criação de uma instância de `CustomerTransaction` só aconteça se todos os valores fornecidos forem válidos. Isso está relacionado ao conceito de **Invariants** no DDD, onde regras de negócios são aplicadas para manter a consistência do modelo.

**Aggregate Root**: CustomerTransaction pode ser um agregado raiz (aggregate root) se for a principal entidade através da qual outras entidades e objetos de valor são acessados e modificados. No DDD, o agregado raiz é responsável pela consistência de todo o agregado e é a única entidade que pode ser diretamente acessada e manipulada pelo repositório.

**Encapsulamento de Lógica de Negócio**: A validação dentro do método `InvalidThrowCustomer` encapsula regras de negócio, garantindo que qualquer instância de `CustomerTransaction` esteja sempre em um estado válido. Isso promove a consistência e integridade do modelo.

### Testes
É possível identificar um projeto de teste, onde todas as regras pertencentes ao domínio são testadas.

#### Disclaimer
**Testes de unidade têm a finalidade de validar comportamentos**. Neste projeto, concentrei-me em realizar testes exclusivamente na camada de domínio. No entanto, essa abordagem também pode ser aplicada na camada de serviço, que pode conter orquestrações e regras pertinentes à criação de entidades e ao direcionamento de fluxo.

### Executando o projeto
O projeto pode ser executado de duas maneiras. Em ambas, será necessária a instalação do Docker para prover os recursos de banco de dados e mensageria.

1.	Executar os aplicativos e recursos via Docker Compose. [Link para as instruções](https://github.com/wodsonluiz/CashManager/blob/main/Wiki/RunAllServicesWithDockerCompose.md)
2.	Executar cada aplicativo de forma segregada. Nesse caso, será necessário também executar o Docker Compose para cada recurso, disponível na pasta resources. [Link para as instruções]([https://github.com/wodsonluiz/CashManager/blob/main/Wiki/RunAllServicesWithDockerCompose.md](https://github.com/wodsonluiz/CashManager/blob/main/Wiki/RunSegregateServices.md)

## Observalibilidade
Optei por escrever logs nos fluxos críticos. **Um aplicativo precisa ter um nível mínimo de observabilidade para permitir a detecção, diagnóstico e resolução rápida de problemas, garantindo desempenho, disponibilidade e confiabilidade**. Além dos logs, há endpoints de health checks disponíveis. [Link das instruções](https://github.com/wodsonluiz/CashManager/blob/main/Wiki/Logging.md)


#### :dizzy: Estrelar o projeto se você gostou =)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/wodsonluiz/CashManager/dotnet.yml)
