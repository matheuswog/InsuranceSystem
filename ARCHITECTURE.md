# Arquitetura do Sistema de Seguros

## Visão Geral

O sistema é composto por dois microserviços principais que seguem a arquitetura hexagonal (Ports & Adapters):

## Microserviços

### 1. PropostaService (Porta 7001)
**Responsabilidades:**
- Gerenciar propostas de seguro
- Controlar status das propostas (Em Análise, Aprovada, Rejeitada)
- Expor API REST para operações CRUD

**Endpoints:**
- `POST /api/propostas` - Criar proposta
- `GET /api/propostas` - Listar propostas
- `GET /api/propostas/{id}` - Obter proposta por ID
- `GET /api/propostas/por-status/{status}` - Listar por status
- `PUT /api/propostas/{id}/status` - Alterar status
- `GET /api/propostas/{id}/existe` - Verificar existência

### 2. ContratacaoService (Porta 7002)
**Responsabilidades:**
- Contratar propostas aprovadas
- Comunicar-se com PropostaService para validações
- Armazenar informações de contratação

**Endpoints:**
- `POST /api/contratacoes` - Contratar proposta
- `GET /api/contratacoes` - Listar contratações
- `GET /api/contratacoes/{id}` - Obter contratação por ID
- `GET /api/contratacoes/por-proposta/{propostaId}` - Obter por proposta

## Arquitetura Hexagonal

Cada microserviço possui 4 camadas bem definidas:

### Domain (Domínio)
- **Entidades:** Proposta, Contratacao
- **Enums:** StatusProposta
- **Regras de negócio:** Validações e comportamentos das entidades

### Application (Aplicação)
- **Commands:** CriarProposta, AlterarStatusProposta, CriarContratacao
- **Queries:** ObterProposta, ListarPropostas, VerificarPropostaExiste
- **DTOs:** Objetos de transferência de dados
- **Interfaces:** Contratos para repositórios e serviços externos

### Infrastructure (Infraestrutura)
- **DbContext:** Configuração do Entity Framework
- **Repositories:** Implementação dos repositórios
- **HTTP Clients:** Comunicação entre microserviços
- **Migrations:** Versionamento do banco de dados

### API (Apresentação)
- **Controllers:** Endpoints REST
- **Program.cs:** Configuração e injeção de dependência
- **Swagger:** Documentação da API

## Banco de Dados

- **SQL Server** com duas bases separadas:
  - `PropostaServiceDb` - Para o PropostaService
  - `ContratacaoServiceDb` - Para o ContratacaoService

## Comunicação Entre Microserviços

- **HTTP REST** para comunicação síncrona
- ContratacaoService consulta PropostaService para:
  - Verificar se proposta existe
  - Validar se proposta está aprovada

## Tecnologias

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0**
- **SQL Server**
- **Docker & Docker Compose**
- **xUnit** (Testes)
- **Swagger** (Documentação)

## Padrões Implementados

- **Arquitetura Hexagonal (Ports & Adapters)**
- **Domain-Driven Design (DDD)**
- **SOLID Principles**
- **Clean Code**
- **Dependency Injection**
- **Repository Pattern**
- **CQRS (Command Query Responsibility Segregation)**
- **Testes Unitários**

## Fluxo de Uso

1. **Criar Proposta** → PropostaService
2. **Alterar Status para Aprovada** → PropostaService
3. **Contratar Proposta** → ContratacaoService (valida com PropostaService)
4. **Consultar Contratações** → ContratacaoService

## Docker

O sistema pode ser executado completamente containerizado usando Docker Compose, incluindo:
- SQL Server
- PropostaService
- ContratacaoService
- Rede interna para comunicação
