# Скрипт для тестирования процесса публикации NuGet пакетов локально
param(
    [Parameter(Mandatory=$false)]
    [string]$TestMode = "dry-run"
)

Write-Host "Тестирование процесса публикации NuGet пакетов..." -ForegroundColor Green
Write-Host "Режим: $TestMode" -ForegroundColor Yellow

$solutionPath = "CommonUtils\CommonUtils.sln"

# Проверка наличия solution файла
if (!(Test-Path $solutionPath)) {
    Write-Host "❌ Файл решения не найден: $solutionPath" -ForegroundColor Red
    exit 1
}

Write-Host "📁 Найден файл решения: $solutionPath" -ForegroundColor Green

try {
    # Очистка предыдущих пакетов
    if (Test-Path "nupkg") {
        Write-Host "🗑️ Очистка предыдущих пакетов..." -ForegroundColor Yellow
        Remove-Item "nupkg" -Recurse -Force
    }

    # Restore и Build
    Write-Host "📦 Восстановление и сборка..." -ForegroundColor Yellow
    & dotnet restore $solutionPath
    if ($LASTEXITCODE -ne 0) { throw "Ошибка при restore" }

    & dotnet build $solutionPath --configuration Release --no-restore
    if ($LASTEXITCODE -ne 0) { throw "Ошибка при build" }

    # Установка тестовой версии
    $version = "1.0.0-test-$(Get-Date -Format 'yyyyMMddHHmm')"
    Write-Host "📦 Создание пакетов версии: $version" -ForegroundColor Yellow

    # Pack пакетов
    $projects = Get-ChildItem -Recurse -Filter *.csproj | Where-Object { $_.FullName -notmatch 'test|Test' }
    foreach ($project in $projects) {
        Write-Host "📦 Упаковка: $($project.Name)" -ForegroundColor Cyan
        & dotnet pack $project.FullName --configuration Release -p:PackageVersion=$version --output nupkg
        if ($LASTEXITCODE -ne 0) { throw "Ошибка при pack $($project.Name)" }
    }

    # Проверка созданных пакетов
    $packages = Get-ChildItem -Path nupkg -Filter *.nupkg
    if ($packages.Count -eq 0) {
        throw "Пакеты не были созданы!"
    }

    Write-Host "✅ Созданы пакеты:" -ForegroundColor Green
    foreach ($package in $packages) {
        Write-Host "  - $($package.Name)" -ForegroundColor White
    }

    # Проверка NuGet источников
    Write-Host "`n🔍 Проверка NuGet источников..." -ForegroundColor Yellow
    & dotnet nuget list source

    if ($TestMode -eq "dry-run") {
        Write-Host "`n🔍 Режим dry-run: публикация не выполняется" -ForegroundColor Yellow
        Write-Host "Для реальной публикации добавьте параметр: -TestMode publish" -ForegroundColor Yellow
    } elseif ($TestMode -eq "publish") {
        Write-Host "`n⚠️ ВНИМАНИЕ: Это опубликует пакеты в GitHub Packages!" -ForegroundColor Red
        $confirmation = Read-Host "Продолжить? (y/N)"
        if ($confirmation -eq "y" -or $confirmation -eq "Y") {
            # Здесь будет код публикации, когда он потребуется
            Write-Host "❌ Публикация в тестовом режиме отключена для безопасности" -ForegroundColor Red
        } else {
            Write-Host "Публикация отменена" -ForegroundColor Yellow
        }
    }

    Write-Host "`n🎉 Тестирование завершено успешно!" -ForegroundColor Green

} catch {
    Write-Host "❌ Ошибка: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
