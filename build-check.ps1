# Скрипт для локальной проверки сборки проектов
Write-Host "Проверка сборки проектов CommonUtils..." -ForegroundColor Green

# Сначала проверяем и исправляем BOM символы
Write-Host "🔍 Проверка BOM символов..." -ForegroundColor Cyan
& .\fix-bom.ps1

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Ошибка при проверке BOM символов" -ForegroundColor Red
    exit $LASTEXITCODE
}

$solutionPath = "CommonUtils\CommonUtils.sln"

# Проверка наличия solution файла
if (!(Test-Path $solutionPath)) {
    Write-Host "❌ Файл решения не найден: $solutionPath" -ForegroundColor Red
    exit 1
}

Write-Host "📁 Найден файл решения: $solutionPath" -ForegroundColor Green

# Пытаемся собрать проект
Write-Host "`n🔨 Попытка сборки проекта..." -ForegroundColor Cyan

try {
    # Restore пакетов
    Write-Host "📦 Восстановление пакетов..." -ForegroundColor Yellow
    & dotnet restore $solutionPath

    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Ошибка при восстановлении пакетов" -ForegroundColor Red
        exit $LASTEXITCODE
    }

    # Сборка
    Write-Host "🔨 Сборка проекта..." -ForegroundColor Yellow
    & dotnet build $solutionPath --configuration Release --no-restore

    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Ошибка при сборке проекта" -ForegroundColor Red
        exit $LASTEXITCODE
    }

    Write-Host "✅ Проект успешно собран!" -ForegroundColor Green

    # Запуск тестов
    Write-Host "🧪 Запуск тестов..." -ForegroundColor Yellow
    & dotnet test $solutionPath --configuration Release --no-build --verbosity normal

    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Ошибка при выполнении тестов" -ForegroundColor Red
        exit $LASTEXITCODE
    }

    Write-Host "✅ Все тесты пройдены!" -ForegroundColor Green
}
catch {
    Write-Host "❌ Неожиданная ошибка: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host "`n🎉 Все проверки пройдены успешно!" -ForegroundColor Green
