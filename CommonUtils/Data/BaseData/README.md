# GurJack.CommonUtils.BaseData

A foundational library providing base classes and interfaces for data access in the CommonUtils ecosystem.

## Features

- **BaseModel**: Base entity class with unique identifier support
- **Database Provider Abstractions**: Interfaces for multi-database support
- **Entity Framework Integration**: Ready-to-use with Entity Framework Core

## Key Components

### BaseModel
```csharp
public class BaseModel
{
    public Guid Id { get; set; }
}
```

Base class for all entities in the system, providing a unique identifier for each entity.

## Usage

```csharp
using BaseData;

public class User : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
}
```

## Installation

```bash
dotnet add package GurJack.CommonUtils.BaseData --source https://nuget.pkg.github.com/GurJack/index.json
```

## Dependencies

- .NET 8.0
- Microsoft SQL Server SqlManagementObjects

## License

This project is part of the CommonUtils library ecosystem.
