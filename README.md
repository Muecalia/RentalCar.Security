# RentalCar.Security

# Language
1. C#

# Framework
1. .NET CORE 8.0

# Data Base
1. SqlServer

# Arquitectura
1. Arquitetura Limpa (Clean Architecture)

# Padrões
1. CQRS
2. Repository

# Container
1. Docker
2. docker-compose
3. Requirements: Docker installed
4. Run container: docker-compose up -d
5. Down container: docker-compose down

# Testes
1. Unitario
2. Integração

# Messageria
1. RabbitMq
2. Run RabbitMQ: docker run -d --hostname localhost --name rabbitmq_unitel -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=Admin2k24@ rabbitmq:3-management
3. browser: http://localhost:15672/

Autenticação e Autorização
1. JSON Web Tokens (JWT)

CI/CD
1. GitHub Actions

Logs
1. Serilog
