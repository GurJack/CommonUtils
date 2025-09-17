# CommonUtils

Библиотека утилит для .NET 8.0, предоставляющая унифицированный слой доступа к данным и управления базами данных.

## Пакеты

Проект содержит следующие NuGet пакеты:

- **GurJack.CommonUtils.BaseData** - Базовые абстракции и интерфейсы для работы с данными
- **GurJack.CommonUtils.CommonData** - Общие классы и утилиты для MSSQL и MySQL
- **GurJack.CommonUtils.BaseMSSqlProvider** - Базовые провайдеры для Microsoft SQL Server
- **GurJack.CommonUtils.CommonForms** - Библиотека Windows Forms компонентов с поддержкой DevExpress

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

# Установка Windows Forms компонентов
dotnet add package GurJack.CommonUtils.CommonForms --source github-gurjack
```

## Сборка проекта

### Локальная проверка
```bash
# Полная проверка (с автоматическим исправлением BOM)
.\Scripts\build-check.ps1

# Только проверка/исправление BOM символов
.\Scripts\fix-bom.ps1

# Тестирование процесса создания пакетов
.\Scripts\test-publish.ps1

# Настройка NuGet аутентификации
.\Scripts\setup-nuget-auth.ps1
```

### Обычная сборка
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
- DevExpress Components (для CommonForms)

### Структура проекта

```
CommonUtils/
├── CommonUtils.sln              # Файл решения
├── CommonUtils/                 # Основная библиотека
│   ├── Attributes/              # Пользовательские атрибуты
│   ├── Compress/                # Утилиты сжатия
│   ├── Database/                # Контекст базы данных
│   ├── Event/                   # Обработка событий
│   ├── Extensions/              # Методы расширения
│   ├── Helpers/                 # Вспомогательные классы
│   ├── Loggers/                 # Система логирования
│   ├── Security/                # Утилиты безопасности
│   ├── Serializer/              # Сериализация данных
│   └── Settings/                # Управление настройками
├── Data/
│   ├── BaseData/                # Базовые абстракции данных
│   ├── CommonData/              # Общие утилиты данных
│   └── BaseMSSqlProvider/       # Провайдер SQL Server
├── CommonForms/                 # Компоненты Windows Forms
├── LocalPackages/               # Локальные NuGet пакеты (DevExpress)
├── Tests/
│   ├── TestCore/                # Основные тесты
│   └── TestWindows/             # Windows-специфичные тесты
├── Scripts/                     # PowerShell скрипты
├── Documentation/               # Документация и XML файлы
└── .github/workflows/           # CI/CD конфигурация
```

## Документация

- [PowerShell скрипты](Scripts/README.md)
- [Настройка DevExpress](Documentation/DEVEXPRESS_SETUP.md)
- [Руководство по устранению неполадок](Documentation/TROUBLESHOOTING.md)
- [Настройка NuGet](Documentation/NUGET_SETUP_GUIDE.md)
- [Исправление предупреждений](Documentation/WARNING_RESOLUTION.md)
- [Полная документация](Documentation/README.md)

## Лицензия

Этот проект разработан для личного использования.
