# GurJack.CommonUtils.BaseData

Фундаментальная библиотека, предоставляющая базовые классы и интерфейсы для доступа к данным в экосистеме CommonUtils.

## Возможности

- **BaseModel**: Базовый класс сущности с поддержкой уникального идентификатора
- **Абстракции провайдера базы данных**: Интерфейсы для поддержки нескольких баз данных
- **Интеграция с Entity Framework**: Готово к использованию с Entity Framework Core

## Основные компоненты

### BaseModel
```csharp
public class BaseModel
{
    public Guid Id { get; set; }
}
```

Базовый класс для всех сущностей в системе, предоставляющий уникальный идентификатор для каждой сущности.

## Использование

```csharp
using BaseData;

public class User : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
}
```

## Установка

```bash
dotnet add package GurJack.CommonUtils.BaseData --source https://nuget.pkg.github.com/GurJack/index.json
```

## Зависимости

- .NET 8.0
- Microsoft SQL Server SqlManagementObjects

## Лицензия

Этот проект является частью экосистемы библиотеки CommonUtils.
