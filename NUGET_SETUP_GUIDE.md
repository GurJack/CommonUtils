# Инструкция по настройке NuGet аутентификации для GitHub Packages

## Способ 1: Через командную строку (Рекомендуется)

Замените YOUR_GITHUB_TOKEN на ваш реальный GitHub token и выполните:

```powershell
dotnet nuget add source "https://nuget.pkg.github.com/GurJack/index.json" --name "GitHub Packages" --username "GurJack" --password "YOUR_GITHUB_TOKEN" --store-password-in-clear-text
```

## Способ 2: Через NuGet.Config файл

1. Откройте или создайте файл: `%APPDATA%\NuGet\NuGet.Config`
2. Добавьте следующее содержимое:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
    <add key="GitHub Packages" value="https://nuget.pkg.github.com/GurJack/index.json" protocolVersion="3" />
  </packageSources>

  <packageSourceCredentials>
    <GitHub_x0020_Packages>
      <add key="Username" value="GurJack" />
      <add key="ClearTextPassword" value="YOUR_GITHUB_TOKEN" />
    </GitHub_x0020_Packages>
  </packageSourceCredentials>
</configuration>
```

## Проверка

После настройки выполните:
```powershell
dotnet nuget list source
```

Должен появиться источник "GitHub Packages".

## В Visual Studio

1. Tools → NuGet Package Manager → Package Manager Settings
2. Package Sources → должен появиться "GitHub Packages"
3. Manage NuGet Packages for Solution → Package source: "GitHub Packages"
4. Browse → поиск: "GurJack.CommonUtils"
