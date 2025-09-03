# Решение проблем с NuGet конфигурацией в GitHub Actions

## Проблема
```
error: Unable to parse config file because: Credentials item must have username and password.
```

## Причина
В файле `nuget.config` была секция `packageSourceCredentials` с username, но без password, что приводило к ошибке парсинга в CI/CD.

## Решение

### 1. Исправлен основной nuget.config
- Убрана секция `packageSourceCredentials` из основного файла
- Credentials перенесены в комментарий как пример
- CI/CD теперь использует динамическую настройку источников

### 2. Обновлен GitHub Actions workflow
- Добавлен шаг очистки локальных конфигов
- Добавлена очистка глобального кеша NuGet
- Динамическая настройка источника с токеном из secrets

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
После этих изменений GitHub Actions должен работать без ошибок парсинга конфигурации.
