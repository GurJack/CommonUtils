# Решение проблем с NuGet конфигурацией и проектами в GitHub Actions

## Проблема 1: Ошибка парсинга NuGet конфигурации
```
error: Unable to parse config file because: Credentials item must have username and password.
```

### Причина
В файле `nuget.config` была секция `packageSourceCredentials` с username, но без password, что приводило к ошибке парсинга в CI/CD.

### Решение
- Убрана секция `packageSourceCredentials` из основного файла
- Credentials перенесены в комментарий как пример
- CI/CD теперь использует динамическую настройку источников

## Проблема 2: Ошибка загрузки файлов проектов
```
error MSB4025: The project file could not be loaded. Data at the root level is invalid. Line 1, position 1.
```

### Причина
В .csproj файлах присутствовал двойной BOM (Byte Order Mark) символ в начале файла (`﻿﻿<Project ...`), что делало XML невалидным.

### Решение
Убран лишний BOM символ из начала всех .csproj файлов:
- `CommonData.csproj`
- `BaseMSSqlProvider.csproj`
- `BaseData.csproj`

### Как избежать в будущем
- Используйте редакторы, которые корректно обрабатывают BOM
- При копировании XML содержимого следите за кодировкой
- Регулярно проверяйте валидность XML файлов

### 3. Для локальной разработки
Создайте файл `nuget.config.user` на основе `nuget.config.user.example`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSourceCredentials>
    <github-gurjack>
      <add key="Username" value="GurJack" />
      <add key="ClearTextPassword" value="ВАШ_GITHUB_TOKEN" />
    </github-gurjack>
  </packageSourceCredentials>
</configuration>
```

⚠️ **Важно**: Файл `nuget.config.user` должен быть в `.gitignore` и не попадать в репозиторий!

## Проверка исправления
После этих изменений GitHub Actions должен работать без ошибок парсинга конфигурации и загрузки проектов.
