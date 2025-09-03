# CommonUtils

Библиотека утилит для .NET 8.0, предоставляющая унифицированный слой доступа к данным и управления базами данных.

## Пакеты

Проект содержит следующие NuGet пакеты:

- **GurJack.CommonUtils.BaseData** - Базовые абстракции и интерфейсы для работы с данными
- **GurJack.CommonUtils.CommonData** - Общие классы и утилиты для MSSQL и MySQL
- **GurJack.CommonUtils.BaseMSSqlProvider** - Базовые провайдеры для Microsoft SQL Server

## Установка из GitHub Packages

### Настройка источника пакетов

1. Создайте файл `nuget.config.user` на основе `nuget.config.user.example`
2. Замените `YOUR_GITHUB_TOKEN` на ваш персональный токен доступа GitHub
3. Убедитесь, что токен имеет права `read:packages`

### Установка пакетов

```bash
# Установка базового пакета
dotnet add package GurJack.CommonUtils.BaseData --source github-gurjack

# Установка общих утилит
dotnet add package GurJack.CommonUtils.CommonData --source github-gurjack

# Установка SQL Server провайдера
dotnet add package GurJack.CommonUtils.BaseMSSqlProvider --source github-gurjack
```

## Сборка проекта

```bash
# Восстановление зависимостей
dotnet restore

# Сборка проекта
dotnet build --configuration Release

# Запуск тестов
dotnet test
```

## Автоматическая публикация

Пакеты автоматически публикуются в GitHub Packages при:
- Push в ветку `main` или `master`
- Создании тега версии (например, `v1.0.1`)

## Разработка

### Требования

- .NET 8.0 SDK
- Visual Studio 2022 или Visual Studio Code
- Git

### Структура проекта

```
CommonUtils/
├── Data/
│   ├── BaseData/          # Базовые абстракции
│   ├── CommonData/        # Общие утилиты
│   └── BaseMSSqlProvider/ # SQL Server провайдеры
├── Tests/
│   ├── TestCore/         # Основные тесты
│   └── TestWindows/      # Windows-специфичные тесты
└── Documentation/        # XML документация
```

## Лицензия

Этот проект разработан для личного использования.
