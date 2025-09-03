# Скрипт для настройки NuGet источника с аутентификацией
# Замените YOUR_GITHUB_TOKEN на ваш реальный GitHub token

# Добавляем источник GitHub Packages
dotnet nuget add source "https://nuget.pkg.github.com/GurJack/index.json" --name "GitHub Packages" --username "GurJack" --password "YOUR_GITHUB_TOKEN" --store-password-in-clear-text

# Альтернативно, если источник уже существует, обновляем его
dotnet nuget update source "GitHub Packages" --username "GurJack" --password "YOUR_GITHUB_TOKEN" --store-password-in-clear-text

# Проверяем список источников
dotnet nuget list source

Write-Host "Настройка завершена! Перезапустите Visual Studio для применения изменений." -ForegroundColor Green
