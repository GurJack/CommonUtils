# Скрипт для проверки и удаления BOM символов во всех файлах проекта
Write-Host "Поиск и удаление BOM символов в файлах проекта..." -ForegroundColor Green

# Получаем путь к корню проекта
$rootPath = if ($PSScriptRoot) {
    Split-Path $PSScriptRoot -Parent
} else {
    Get-Location
}

Write-Host "Корневая папка проекта: $rootPath" -ForegroundColor Cyan

# Типы файлов для проверки
$fileExtensions = @(
    "*.csproj",
    "*.cs",
    "*.xml",
    "*.json",
    "*.md",
    "*.txt",
    "*.yml",
    "*.yaml"
)

# Папки для исключения из поиска
$excludeDirs = @(
    "bin",
    "obj",
    ".git",
    ".vs",
    "packages",
    "node_modules",
    "LocalPackages"
)

Write-Host "Поиск файлов для проверки..." -ForegroundColor Yellow

# Получаем все файлы для проверки
$allFiles = @()
foreach ($extension in $fileExtensions) {
    $files = Get-ChildItem -Path $rootPath -Filter $extension -Recurse -File | Where-Object {
        $exclude = $false
        foreach ($dir in $excludeDirs) {
            if ($_.FullName -like "*\$dir\*") {
                $exclude = $true
                break
            }
        }
        -not $exclude
    }
    $allFiles += $files
}

Write-Host "Найдено файлов для проверки: $($allFiles.Count)" -ForegroundColor Cyan

$fixed = 0
$total = 0
$skipped = 0

foreach ($file in $allFiles) {
    $relativePath = $file.FullName.Replace($rootPath, "").TrimStart('\', '/')
    $total++

    try {
        # Читаем файл как байты
        $bytes = [System.IO.File]::ReadAllBytes($file.FullName)

        # Проверяем, если файл пустой
        if ($bytes.Length -eq 0) {
            Write-Host "SKIP: $relativePath - пустой файл" -ForegroundColor Yellow
            $skipped++
            continue
        }

        # Проверяем на наличие BOM (UTF-8 BOM: EF BB BF)
        $bomCount = 0

        # Считаем количество BOM в начале файла
        for ($i = 0; $i -lt $bytes.Length - 2; $i += 3) {
            if ($i + 2 -lt $bytes.Length -and $bytes[$i] -eq 0xEF -and $bytes[$i+1] -eq 0xBB -and $bytes[$i+2] -eq 0xBF) {
                $bomCount++
            } else {
                break
            }
        }

        if ($bomCount -gt 0) {
            Write-Host "FIXED: Найдено $bomCount BOM символов в файле: $relativePath" -ForegroundColor Red

            # Удаляем все BOM символы
            $startIndex = $bomCount * 3
            $newBytes = $bytes[$startIndex..($bytes.Length-1)]
            [System.IO.File]::WriteAllBytes($file.FullName, $newBytes)

            Write-Host "OK: Удалены BOM символы из файла: $relativePath" -ForegroundColor Green
            $fixed++
        } else {
            Write-Host "OK: $relativePath - BOM отсутствует" -ForegroundColor Green
        }

        # Для XML файлов дополнительная проверка валидности
        if ($file.Extension -eq ".csproj" -or $file.Extension -eq ".xml") {
            try {
                $content = Get-Content $file.FullName -Raw -Encoding UTF8
                if ($content.Trim().Length -gt 0) {
                    [xml]$xml = $content
                    Write-Host "  XML валидный" -ForegroundColor Gray
                }
            }
            catch {
                Write-Host "  WARNING: XML невалидный: $($_.Exception.Message)" -ForegroundColor Yellow
            }
        }

    }
    catch {
        Write-Host "ERROR: $relativePath - Ошибка: $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "=== РЕЗУЛЬТАТ ОБРАБОТКИ ===" -ForegroundColor Cyan
Write-Host "- Всего файлов проверено: $total" -ForegroundColor White
Write-Host "- Исправлено файлов: $fixed" -ForegroundColor White
Write-Host "- Пропущено файлов: $skipped" -ForegroundColor White
Write-Host "- Корректных файлов: $($total - $fixed - $skipped)" -ForegroundColor White

if ($fixed -gt 0) {
    Write-Host ""
    Write-Host "WARNING: Файлы были изменены! Необходимо сделать commit." -ForegroundColor Yellow
    Write-Host "Рекомендуемые команды:" -ForegroundColor Cyan
    Write-Host "   git add -A" -ForegroundColor Gray
    Write-Host "   git commit -m 'fix: удалены BOM символы из $fixed файлов'" -ForegroundColor Gray
} else {
    Write-Host ""
    Write-Host "SUCCESS: Все файлы в порядке!" -ForegroundColor Green
}

Write-Host ""
Write-Host "=== РЕКОМЕНДАЦИИ ===" -ForegroundColor Cyan
Write-Host "1. Настройте VSCode (.vscode/settings.json):" -ForegroundColor White
Write-Host '   "files.encoding": "utf8"' -ForegroundColor Gray
Write-Host "2. Проверяйте .gitattributes для консистентности кодировки" -ForegroundColor White
Write-Host "3. Запускайте этот скрипт периодически для контроля" -ForegroundColor White
Write-Host ""
Write-Host "Скript завершен!" -ForegroundColor Green
