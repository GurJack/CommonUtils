# GurJack.CommonUtils.BaseMSSqlProvider

Реализация провайдера базы данных SQL Server для экосистемы библиотеки CommonUtils.

## Возможности

- **Провайдер SQL Server**: Специализированный провайдер базы данных для Microsoft SQL Server
- **Интеграция с Entity Framework**: Бесшовная интеграция с Entity Framework Core
- **Операции с базой данных**: Комплексные операции CRUD для SQL Server

## Основные компоненты

Этот пакет предоставляет реализации провайдера базы данных, специфичные для SQL Server, определенные в интерфейсах BaseData.

## Использование

```csharp
using BaseMSSqlProvider;

// Настройте ваш DbContext для использования провайдера SQL Server
// Примеры использования будут добавлены по мере развития библиотеки
```

## Установка

```bash
dotnet add package GurJack.CommonUtils.BaseMSSqlProvider --source https://nuget.pkg.github.com/GurJack/index.json
```

## Зависимости

- .NET 8.0
- GurJack.CommonUtils.BaseData
- GurJack.CommonUtils.CommonData
- Microsoft Entity Framework Core SQL Server

## Конфигурация

Этот провайдер предназначен для работы с базами данных SQL Server и требует правильной конфигурации строки подключения.

## Статус разработки

Эта библиотека активно разрабатывается и предоставляет основу для операций с базой данных SQL Server в экосистеме CommonUtils.

## Лицензия

Этот проект является частью экосистемы библиотеки CommonUtils.
