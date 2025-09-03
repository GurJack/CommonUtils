# GurJack.CommonUtils

Комплексная библиотека утилит для .NET 8.0, предоставляющая широкий набор инструментов для разработки приложений.

## 🚀 Возможности

### 🗄️ Работа с базами данных
- **Entity Framework Integration** - готовые контексты и провайдеры
- **Database Helpers** - утилиты для работы с SQL Server
- **Data Extensions** - расширения для удобной работы с данными

### 📝 Логирование
- **Multi-logger Support** - поддержка NLog и log4net
- **Structured Logging** - структурированное логирование
- **Performance Monitoring** - мониторинг производительности

### 🔧 Утилиты и расширения
- **String Extensions** - расширения для работы со строками
- **Collection Extensions** - утилиты для коллекций
- **DateTime Helpers** - помощники для работы с датами
- **Object Extensions** - расширения для объектов

### 🔒 Безопасность
- **Encryption Utilities** - утилиты шифрования
- **Security Helpers** - помощники безопасности
- **Hash Functions** - функции хеширования

### ⚙️ Сериализация
- **JSON Serialization** - работа с JSON (Newtonsoft.Json)
- **XML Serialization** - работа с XML
- **Custom Serializers** - пользовательские сериализаторы

### 🎯 Другие возможности
- **AutoMapper Integration** - интеграция с AutoMapper
- **Exception Handling** - обработка исключений
- **Configuration Management** - управление настройками
- **Random Generators** - генераторы случайных данных
- **Compression Utilities** - утилиты сжатия

## 📦 Установка

### Из GitHub Packages
```bash
dotnet add package GurJack.CommonUtils --source https://nuget.pkg.github.com/GurJack/index.json
```

### Настройка источника пакетов
```bash
dotnet nuget add source "https://nuget.pkg.github.com/GurJack/index.json" --name "GitHub Packages" --username "GurJack" --password "YOUR_GITHUB_TOKEN" --store-password-in-clear-text
```

## 🔧 Использование

### Быстрый старт
```csharp
using CommonUtils;
using CommonUtils.Extensions;
using CommonUtils.Helpers;

// Работа со строками
string text = "Hello World".ToTitleCase();

// Логирование
var logger = LoggerFactory.CreateLogger("MyApp");
logger.LogInformation("Application started");

// Работа с настройками
var settings = SettingsManager.LoadSettings<MySettings>();

// Генерация случайных данных
var randomString = RandomGenerator.GenerateString(10);
```

### Работа с базой данных
```csharp
using CommonUtils.Database;

// Использование готовых контекстов и расширений
// (подробности в документации)
```

### Сериализация
```csharp
using CommonUtils.Serializer;

// JSON сериализация
var json = JsonSerializer.Serialize(myObject);
var obj = JsonSerializer.Deserialize<MyClass>(json);
```

## 🏗️ Требования

- **.NET 8.0** или выше
- **Entity Framework Core** 9.0+
- **AutoMapper** 15.0+
- **Newtonsoft.Json** 13.0+

## 🤝 Зависимости

| Пакет | Версия | Назначение |
|-------|--------|------------|
| Microsoft.EntityFrameworkCore | 9.0.8 | ORM для работы с БД |
| Microsoft.EntityFrameworkCore.SqlServer | 9.0.8 | SQL Server provider |
| AutoMapper | 15.0.1 | Маппинг объектов |
| Newtonsoft.Json | 13.0.3 | JSON сериализация |
| NLog | 6.0.3 | Логирование |
| log4net | 3.1.0 | Альтернативное логирование |

## 📚 Документация

Полная документация доступна в [GitHub репозитории](https://github.com/GurJack/CommonUtils).

## 🐛 Отчеты об ошибках

Если вы нашли ошибку, пожалуйста, создайте [issue](https://github.com/GurJack/CommonUtils/issues) в GitHub репозитории.

## 📄 Лицензия

Этот проект лицензирован под MIT License - см. файл [LICENSE](https://github.com/GurJack/CommonUtils/blob/main/LICENSE) для деталей.

## 👥 Авторы

- **GurJack** - *Основной разработчик* - [GitHub](https://github.com/GurJack)

---

⭐ Не забудьте поставить звезду, если проект оказался полезным!
