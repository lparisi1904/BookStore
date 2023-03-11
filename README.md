# BookStore
Progetto Web API con UI Bazor usando .NET 7.0

## Technologie
- .NET 7
- Entity Framework 7
- Mapster Mapping
- Swagger
- SQL Server

## Architettura
- 4 Layers:
  - Web-UI layer (SPA)
  
  - Application layer (API)
    - Controllers
    - Dtos

  - Domain layer
    - Entities
    - Interfaces
    - Services
    
  - Infrastructure layer
    - Repository Pattern

  ## Unit tests
- xUnit
- Moq
- Fluent Assertions