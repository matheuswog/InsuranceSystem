# 🏢 Sistema de Seguros - Microserviços

Olá! 👋 Este é um sistema completo para gerenciar propostas de seguro e realizar contratações. Foi desenvolvido pensando em escalabilidade, manutenibilidade e boas práticas de desenvolvimento.

## 🎯 O que este sistema faz?

Imagine que você trabalha em uma seguradora e precisa de uma ferramenta para:
- 📝 **Criar propostas de seguro** para novos clientes
- 📊 **Acompanhar o status** de cada proposta (em análise, aprovada ou rejeitada)
- ✅ **Contratar propostas aprovadas** de forma segura
- 🔍 **Consultar informações** sobre propostas e contratações

Este sistema resolve exatamente isso! Ele é dividido em duas partes principais que trabalham juntas:

### 🚀 PropostaService - O "Criador de Propostas"
Este serviço é responsável por tudo relacionado às propostas:
- ✨ Criar novas propostas de seguro
- 📋 Listar todas as propostas cadastradas
- 🔄 Alterar o status das propostas (Em Análise → Aprovada/Rejeitada)
- 🌐 Expor uma API REST para outras aplicações consumirem

### 💼 ContratacaoService - O "Finalizador de Negócios"
Este serviço cuida da parte final do processo:
- 🤝 Contratar propostas que foram aprovadas
- 💾 Armazenar todas as informações da contratação
- 🔗 Conversar com o PropostaService para validar se a proposta pode ser contratada
- 🌐 Também expõe uma API REST

## 🛠️ Tecnologias que usamos

Escolhemos tecnologias modernas e confiáveis:
- **.NET 9.0** - A versão mais recente do framework da Microsoft
- **ASP.NET Core Web API** - Para criar APIs REST robustas
- **Entity Framework Core 9.0** - Para trabalhar com banco de dados de forma fácil
- **SQL Server** - Banco de dados confiável e performático
- **Docker & Docker Compose** - Para facilitar a instalação e execução
- **xUnit** - Para garantir que tudo funciona corretamente com testes

## 📁 Como está organizado o projeto?

O projeto segue a **Arquitetura Hexagonal** (também conhecida como Ports & Adapters), que é uma forma elegante de organizar o código:

```
InsuranceSystem/
├── src/
│   ├── PropostaService/                    # 🚀 Serviço de Propostas
│   │   ├── PropostaService.API/           # 🌐 Interface web (Controllers)
│   │   ├── PropostaService.Application/   # 🧠 Lógica de negócio (Commands/Queries)
│   │   ├── PropostaService.Domain/        # 💎 Regras do domínio (Entidades)
│   │   └── PropostaService.Infrastructure/ # 🔧 Acesso a dados (Repositories)
│   └── ContratacaoService/                # 💼 Serviço de Contratações
│       ├── ContratacaoService.API/        # 🌐 Interface web (Controllers)
│       ├── ContratacaoService.Application/ # 🧠 Lógica de negócio (Commands/Queries)
│       ├── ContratacaoService.Domain/     # 💎 Regras do domínio (Entidades)
│       └── ContratacaoService.Infrastructure/ # 🔧 Acesso a dados (Repositories)
├── tests/                                 # 🧪 Testes unitários
└── docker/                               # 🐳 Configurações do Docker
```

## 🚀 Vamos colocar para funcionar!

### Pré-requisitos
Antes de começar, você vai precisar de:
- **.NET 9.0 SDK** - Para compilar e executar o projeto
- **Docker Desktop** - Para rodar tudo de forma isolada (recomendado)
- **SQL Server** - Só se não quiser usar Docker

### Opção 1: Usando Docker (Mais Fácil) 🐳

Esta é a forma mais simples de executar o sistema:

1. **Clone o repositório:**
```bash
git clone <url-do-repositorio>
cd InsuranceSystem
```

2. **Execute tudo com um comando:**
```bash
cd docker
docker-compose up --build
```

3. **Acesse as APIs:**
- PropostaService: http://localhost:7001
- ContratacaoService: http://localhost:7002
- Documentação interativa: 
  - http://localhost:7001/swagger
  - http://localhost:7002/swagger

Pronto! 🎉 O sistema estará rodando com banco de dados, APIs e tudo mais configurado automaticamente.

### Opção 2: Execução Local (Para Desenvolvedores) 💻

Se você quer executar localmente para desenvolvimento:

1. **Clone e restaure as dependências:**
```bash
git clone <url-do-repositorio>
cd InsuranceSystem
dotnet restore
```

2. **Configure o banco de dados** (as connection strings já estão configuradas para LocalDB)

3. **Execute as APIs:**
```bash
# Terminal 1 - PropostaService
cd src/PropostaService/PropostaService.API
dotnet run

# Terminal 2 - ContratacaoService
cd src/ContratacaoService/ContratacaoService.API
dotnet run
```

## 🧪 Testando o sistema

Para garantir que tudo está funcionando perfeitamente:

```bash
# Executar todos os testes
dotnet test

# Executar testes específicos
dotnet test tests/PropostaService.Tests
dotnet test tests/ContratacaoService.Tests
```

## 🌐 Como usar as APIs

### PropostaService (Porta 7001)

| Método | Endpoint | O que faz |
|--------|----------|-----------|
| POST | `/api/propostas` | Cria uma nova proposta |
| GET | `/api/propostas` | Lista todas as propostas |
| GET | `/api/propostas/{id}` | Busca uma proposta específica |
| GET | `/api/propostas/por-status/{status}` | Lista propostas por status |
| PUT | `/api/propostas/{id}/status` | Altera o status de uma proposta |
| GET | `/api/propostas/{id}/existe` | Verifica se uma proposta existe |

### ContratacaoService (Porta 7002)

| Método | Endpoint | O que faz |
|--------|----------|-----------|
| POST | `/api/contratacoes` | Contrata uma proposta aprovada |
| GET | `/api/contratacoes` | Lista todas as contratações |
| GET | `/api/contratacoes/{id}` | Busca uma contratação específica |
| GET | `/api/contratacoes/por-proposta/{propostaId}` | Busca contratação por proposta |

## 📝 Exemplo prático de uso

Vamos simular um fluxo completo:

### 1. Criar uma Proposta
```bash
curl -X POST "http://localhost:7001/api/propostas" \
  -H "Content-Type: application/json" \
  -d '{
    "nomeCliente": "João Silva",
    "cpfCliente": "12345678901",
    "emailCliente": "joao@email.com",
    "valorSegurado": 100000,
    "valorPremio": 1000,
    "observacoes": "Seguro de vida"
  }'
```

### 2. Aprovar a Proposta
```bash
curl -X PUT "http://localhost:7001/api/propostas/{id}/status" \
  -H "Content-Type: application/json" \
  -d '{
    "status": 2,
    "observacoes": "Aprovada para contratação"
  }'
```

### 3. Contratar a Proposta
```bash
curl -X POST "http://localhost:7002/api/contratacoes" \
  -H "Content-Type: application/json" \
  -d '{
    "propostaId": "{id-da-proposta}",
    "observacoes": "Contratação realizada"
  }'
```

## 📊 Status das Propostas

- **1** - Em Análise (proposta recém-criada)
- **2** - Aprovada (pode ser contratada)
- **3** - Rejeitada (não pode ser contratada)

## 🏗️ Padrões e Boas Práticas

Este projeto implementa várias práticas importantes da indústria:

- **🏛️ Arquitetura Hexagonal** - Separação clara de responsabilidades
- **🎯 Domain-Driven Design (DDD)** - Foco nas regras de negócio
- **🔧 SOLID Principles** - Código limpo e manutenível
- **📚 Clean Code** - Código legível e bem documentado
- **💉 Dependency Injection** - Baixo acoplamento entre componentes
- **🗃️ Repository Pattern** - Abstração do acesso a dados
- **⚡ CQRS** - Separação entre comandos e consultas
- **🧪 Testes Unitários** - Garantia de qualidade
- **🐳 Docker** - Facilidade de deploy e execução
- **🌐 HTTP REST** - Comunicação entre microserviços

## 🗄️ Banco de Dados

O sistema usa **SQL Server** com duas bases de dados separadas:
- **PropostaServiceDb** - Armazena as propostas
- **ContratacaoServiceDb** - Armazena as contratações

As tabelas são criadas automaticamente quando você executa o sistema! 🎉

## 📈 Monitoramento e Logs

O sistema registra tudo que acontece usando o sistema de logging do .NET, facilitando a identificação de problemas e o acompanhamento do comportamento.

## 🤝 Contribuindo

Adoraríamos sua contribuição! Aqui está como você pode ajudar:

1. **Fork** o projeto
2. **Crie** uma branch para sua feature (`git checkout -b feature/MinhaFeatureIncrivel`)
3. **Commit** suas mudanças (`git commit -m 'Adiciona uma feature incrível'`)
4. **Push** para a branch (`git push origin feature/MinhaFeatureIncrivel`)
5. **Abra** um Pull Request

## 📞 Precisa de ajuda?

Se você tiver dúvidas ou encontrar algum problema:
- 📖 Consulte a documentação do Swagger nas URLs das APIs
- 🐛 Abra uma issue no repositório
- 💬 Entre em contato com a equipe de desenvolvimento

---

**Desenvolvido com ❤️ usando as melhores práticas da indústria**