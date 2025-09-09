# Sistema de Seguros - Microservi�os

Sistema desenvolvido para gerenciar propostas de seguro e efetuar sua contrata��o, utilizando arquitetura hexagonal e microservi�os.

## Arquitetura

O sistema � composto por dois microservi�os principais:

### 1. PropostaService
Respons�vel por:
- Criar propostas de seguro
- Listar propostas
- Alterar status da proposta (Em An�lise, Aprovada, Rejeitada)
- Expor API REST

### 2. ContratacaoService
Respons�vel por:
- Contratar uma proposta (somente se Aprovada)
- Armazenar informa��es da contrata��o
- Comunicar-se com o PropostaService para verificar status da proposta
- Expor API REST

## Tecnologias Utilizadas

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0**
- **SQL Server**
- **Docker & Docker Compose**
- **xUnit** (Testes)
- **Moq** (Mocks para testes)

## Estrutura do Projeto

`
InsuranceSystem/
 src/
    PropostaService/
       PropostaService.API/          # Camada de apresenta��o
       PropostaService.Application/  # Camada de aplica��o
       PropostaService.Domain/       # Camada de dom�nio
       PropostaService.Infrastructure/ # Camada de infraestrutura
    ContratacaoService/
        ContratacaoService.API/       # Camada de apresenta��o
        ContratacaoService.Application/ # Camada de aplica��o
        ContratacaoService.Domain/    # Camada de dom�nio
        ContratacaoService.Infrastructure/ # Camada de infraestrutura
 tests/
    PropostaService.Tests/            # Testes unit�rios
    ContratacaoService.Tests/         # Testes unit�rios
 docker/
    docker-compose.yml               # Orquestra��o dos containers
    Dockerfile.PropostaService       # Imagem do PropostaService
    Dockerfile.ContratacaoService    # Imagem do ContratacaoService
 README.md
`

## Pr�-requisitos

- .NET 9.0 SDK
- Docker Desktop
- SQL Server (opcional, se n�o usar Docker)

## Como Executar

### Op��o 1: Usando Docker Compose (Recomendado)

1. Clone o reposit�rio:
`ash
git clone <url-do-repositorio>
cd InsuranceSystem
`

2. Execute o Docker Compose:
`ash
cd docker
docker-compose up --build
`

3. Acesse as APIs:
- PropostaService: http://localhost:7001
- ContratacaoService: http://localhost:7002
- Swagger UI: http://localhost:7001/swagger e http://localhost:7002/swagger

### Op��o 2: Execu��o Local

1. Clone o reposit�rio:
`ash
git clone <url-do-repositorio>
cd InsuranceSystem
`

2. Restaure os pacotes:
`ash
dotnet restore
`

3. Configure as connection strings nos arquivos ppsettings.json de cada API

4. Execute as migrations:
`ash
# Para PropostaService
cd src/PropostaService/PropostaService.API
dotnet ef database update

# Para ContratacaoService
cd ../../ContratacaoService/ContratacaoService.API
dotnet ef database update
`

5. Execute as APIs:
`ash
# Terminal 1 - PropostaService
cd src/PropostaService/PropostaService.API
dotnet run

# Terminal 2 - ContratacaoService
cd src/ContratacaoService/ContratacaoService.API
dotnet run
`

## Executando os Testes

`ash
# Executar todos os testes
dotnet test

# Executar testes espec�ficos
dotnet test tests/PropostaService.Tests
dotnet test tests/ContratacaoService.Tests
`

## Endpoints da API

### PropostaService (Porta 7001)

- POST /api/propostas - Criar proposta
- GET /api/propostas - Listar todas as propostas
- GET /api/propostas/{id} - Obter proposta por ID
- GET /api/propostas/por-status/{status} - Listar propostas por status
- PUT /api/propostas/{id}/status - Alterar status da proposta
- GET /api/propostas/{id}/existe - Verificar se proposta existe

### ContratacaoService (Porta 7002)

- POST /api/contratacoes - Contratar proposta
- GET /api/contratacoes - Listar todas as contrata��es
- GET /api/contratacoes/{id} - Obter contrata��o por ID
- GET /api/contratacoes/por-proposta/{propostaId} - Obter contrata��o por proposta

## Exemplo de Uso

### 1. Criar uma Proposta

`ash
curl -X POST "http://localhost:7001/api/propostas" \
  -H "Content-Type: application/json" \
  -d '{
    "nomeCliente": "Jo�o Silva",
    "cpfCliente": "12345678901",
    "emailCliente": "joao@email.com",
    "valorSegurado": 100000,
    "valorPremio": 1000,
    "observacoes": "Seguro de vida"
  }'
`

### 2. Aprovar a Proposta

`ash
curl -X PUT "http://localhost:7001/api/propostas/{id}/status" \
  -H "Content-Type: application/json" \
  -d '{
    "status": 2,
    "observacoes": "Aprovada para contrata��o"
  }'
`

### 3. Contratar a Proposta

`ash
curl -X POST "http://localhost:7002/api/contratacoes" \
  -H "Content-Type: application/json" \
  -d '{
    "propostaId": "{id-da-proposta}",
    "observacoes": "Contrata��o realizada"
  }'
`

## Status das Propostas

- 1 - Em An�lise
- 2 - Aprovada
- 3 - Rejeitada

## Padr�es e Boas Pr�ticas Implementadas

- **Arquitetura Hexagonal (Ports & Adapters)**
- **Domain-Driven Design (DDD)**
- **SOLID Principles**
- **Clean Code**
- **Dependency Injection**
- **Repository Pattern**
- **CQRS (Command Query Responsibility Segregation)**
- **Testes Unit�rios com xUnit e Moq**
- **Docker para containeriza��o**
- **Comunica��o entre microservi�os via HTTP REST**

## Banco de Dados

O sistema utiliza SQL Server com duas bases de dados separadas:
- PropostaServiceDb - Para o PropostaService
- ContratacaoServiceDb - Para o ContratacaoService

As migrations s�o executadas automaticamente na inicializa��o das aplica��es.

## Monitoramento e Logs

O sistema implementa logging estruturado usando ILogger do .NET, com diferentes n�veis de log configur�veis.

## Contribui��o

1. Fa�a um fork do projeto
2. Crie uma branch para sua feature (git checkout -b feature/AmazingFeature)
3. Commit suas mudan�as (git commit -m 'Add some AmazingFeature')
4. Push para a branch (git push origin feature/AmazingFeature)
5. Abra um Pull Request
