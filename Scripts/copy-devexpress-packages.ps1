# Скрипт для копирования пакетов DevExpress в локальный feed
# Copy DevExpress packages to local feed

param(
    [string]$DevExpressVersion = "23.2.3",
    [string]$SourcePath = "",
    [switch]$Force = $false
)

$ErrorActionPreference = "Stop"
$LocalPackagesPath = Join-Path $PSScriptRoot "..\LocalPackages"

Write-Host "🔍 Поиск пакетов DevExpress..." -ForegroundColor Green

# Возможные пути к пакетам DevExpress
$PossiblePaths = @(
    "C:\Program Files (x86)\DevExpress $($DevExpressVersion.Substring(0,4))\Components\Bin\Framework",
    "C:\Program Files\DevExpress $($DevExpressVersion.Substring(0,4))\Components\Bin\Framework",
    "$env:USERPROFILE\.nuget\packages",
    "$env:ProgramData\Microsoft\VisualStudio\Packages"
)

if ($SourcePath) {
    $PossiblePaths = @($SourcePath) + $PossiblePaths
}

# Необходимые пакеты
$RequiredPackages = @(
    "DevExpress.Win.Design.$DevExpressVersion.nupkg",
    "DevExpress.Office.Core.$DevExpressVersion.nupkg",
    "DevExpress.Utils.$DevExpressVersion.nupkg",
    "DevExpress.Win.$DevExpressVersion.nupkg"
)

# Создаем папку если не существует
if (-not (Test-Path $LocalPackagesPath)) {
    New-Item -ItemType Directory -Path $LocalPackagesPath -Force | Out-Null
    Write-Host "✅ Создана папка: $LocalPackagesPath" -ForegroundColor Green
}

# Поиск пакетов
$FoundPackages = @{}
$SearchPaths = @()

foreach ($path in $PossiblePaths) {
    if (Test-Path $path) {
        $SearchPaths += $path
        Write-Host "🔍 Поиск в: $path" -ForegroundColor Yellow

        foreach ($package in $RequiredPackages) {
            if (-not $FoundPackages.ContainsKey($package)) {
                $packagePath = Get-ChildItem -Path $path -Name $package -Recurse -ErrorAction SilentlyContinue | Select-Object -First 1
                if ($packagePath) {
                    $fullPath = Join-Path $path $packagePath
                    $FoundPackages[$package] = $fullPath
                    Write-Host "✅ Найден: $package" -ForegroundColor Green
                }
            }
        }
    }
}

Write-Host "`n📊 Результаты поиска:" -ForegroundColor Cyan
Write-Host "Найдено пакетов: $($FoundPackages.Count) из $($RequiredPackages.Count)" -ForegroundColor Cyan

if ($FoundPackages.Count -eq 0) {
    Write-Host "❌ Пакеты DevExpress не найдены!" -ForegroundColor Red
    Write-Host "`nВозможные причины:" -ForegroundColor Yellow
    Write-Host "1. DevExpress не установлен" -ForegroundColor Yellow
    Write-Host "2. Неправильная версия ($DevExpressVersion)" -ForegroundColor Yellow
    Write-Host "3. Пакеты находятся в другом месте" -ForegroundColor Yellow
    Write-Host "`nПопробуйте:" -ForegroundColor Yellow
    Write-Host "- Указать путь вручную: -SourcePath 'C:\Path\To\DevExpress'" -ForegroundColor Yellow
    Write-Host "- Изменить версию: -DevExpressVersion '23.1.5'" -ForegroundColor Yellow
    exit 1
}

# Копирование найденных пакетов
Write-Host "`n📦 Копирование пакетов..." -ForegroundColor Green
$CopiedCount = 0

foreach ($packageName in $FoundPackages.Keys) {
    $sourcePath = $FoundPackages[$packageName]
    $destPath = Join-Path $LocalPackagesPath $packageName

    if ((Test-Path $destPath) -and -not $Force) {
        Write-Host "⏭️  Пропущен (уже существует): $packageName" -ForegroundColor Yellow
        $CopiedCount++
    } else {
        try {
            Copy-Item -Path $sourcePath -Destination $destPath -Force
            Write-Host "✅ Скопирован: $packageName" -ForegroundColor Green
            $CopiedCount++
        } catch {
            Write-Host "❌ Ошибка копирования $packageName : $_" -ForegroundColor Red
        }
    }
}

Write-Host "`n🎉 Завершено!" -ForegroundColor Green
Write-Host "Скопировано: $CopiedCount пакетов" -ForegroundColor Green
Write-Host "Путь: $LocalPackagesPath" -ForegroundColor Green

# Проверка содержимого папки
$LocalPackages = Get-ChildItem -Path $LocalPackagesPath -Name "*.nupkg"
if ($LocalPackages) {
    Write-Host "`n📋 Пакеты в LocalPackages:" -ForegroundColor Cyan
    foreach ($pkg in $LocalPackages) {
        Write-Host "   📦 $pkg" -ForegroundColor White
    }
} else {
    Write-Host "`n⚠️  В LocalPackages нет .nupkg файлов" -ForegroundColor Yellow
}

Write-Host "`n💡 Следующие шаги:" -ForegroundColor Cyan
Write-Host "1. Добавьте LocalPackages/* в .gitignore (уже сделано)" -ForegroundColor White
Write-Host "2. Соберите проект: dotnet build" -ForegroundColor White
Write-Host "3. Для GitHub Actions настройте секреты с пакетами" -ForegroundColor White

