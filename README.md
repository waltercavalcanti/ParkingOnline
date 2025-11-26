# ParkingOnline

Projeto ParkingOnline — sistema para gerenciar estacionamentos de forma online, com interface web e API para operações de reserva, liberação e administração de vagas.

## Objetivo

Fornecer uma aplicação completa para gestão de vagas de estacionamento, permitindo que usuários reservem e liberem vagas, e que administradores monitorem e gerenciem inventário de vagas, tarifas e relatórios. A solução segue uma arquitetura em camadas para separar responsabilidades e facilitar manutenção.

## Estrutura do repositório

- `src/ParkingOnline.UI` — Aplicação web (Razor Pages) que fornece a interface do usuário para clientes e administradores.
- `src/ParkingOnline.WebApi` — API HTTP que expõe endpoints para operações do sistema (reservas, consulta de vagas, autenticação, relatórios, etc.).
- `src/ParkingOnline.Core` — Núcleo do domínio, contendo modelos, interfaces e regras de negócio.
- `src/ParkingOnline.Infrastructure` — Implementações de infraestrutura: persistência de dados (DbContext/repositórios), integrações externas e configuração de serviços.

## Funcionalidades principais

- Cadastro e autenticação de usuários (clientes e administradores).
- Reserva de vaga por período e liberação automática/no check-out.
- Consulta em tempo real do status das vagas (livre/ocupada/reservada).
- Gestão administrativa: criação/edição de vagas, definição de tarifas e geração de relatórios.
- API REST para integração com clientes externos (aplicativos mobile, serviços de pagamento etc.).

## Tecnologias

- .NET 10
- ASP.NET Core (Razor Pages para UI; Web API para endpoints)
- Organização em projetos separados para UI, Core, Infrastructure e WebApi
- Possível uso de Entity Framework Core ou outro provedor de persistência (implementado em `Infrastructure`).

## Como executar localmente

Pré-requisitos: .NET 10 SDK instalado.

1. Restaurar e compilar a solução:

   `dotnet restore`

   `dotnet build`

2. Executar os projetos (exemplos):

   - UI: `cd src/ParkingOnline.UI && dotnet run`
   - API: `cd src/ParkingOnline.WebApi && dotnet run`

3. Ajustar configurações:

   - Defina a string de conexão e outros segredos em `appsettings.json` ou variáveis de ambiente.
   - Se houver migrações do EF Core, aplique-as antes de executar:

     `dotnet ef database update --project src/ParkingOnline.Infrastructure --startup-project src/ParkingOnline.WebApi`

## Desenvolvimento e contribuições

- Siga as convenções do repositório para commits e pull requests.
- Abra issues para bugs e propostas de novas funcionalidades.
- Documente alterações relevantes e adicione testes quando aplicável.

## Licença

Consulte o arquivo `LICENSE` no repositório para informações sobre a licença do projeto.