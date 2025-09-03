# Скрипт для проверки и исправления BOM символов в .csproj файлах
Write-Host "Проверка BOM символов в .csproj файлах..." -ForegroundColor Green

$projectFiles = @(
    "CommonUtils\Data\BaseData\BaseData.csproj",
    "CommonUtils\Data\CommonData\CommonData.csproj",
    "CommonUtils\CommonUtils\CommonUtils.csproj",
    "CommonUtils\Data\BaseMSSqlProvider\BaseMSSqlProvider.csproj"
)

$fixed = 0
$total = 0

foreach ($file in $projectFiles) {
    # Получаем корректный путь к корню проекта
    $rootPath = if ($PSScriptRoot) { $PSScriptRoot } else { Get-Location }
    $fullPath = Join-Path $rootPath $file
    $total++

    if (Test-Path $fullPath) {
        try {
            # Читаем файл как байты
            $bytes = [System.IO.File]::ReadAllBytes($fullPath)

            # Проверяем на множественные BOM (UTF-8 BOM: EF BB BF)
            $bomPattern = @(0xEF, 0xBB, 0xBF)
            $bomCount = 0

            # Считаем количество BOM в начале файла
            for ($i = 0; $i -lt $bytes.Length - 2; $i += 3) {
                if ($i + 2 -lt $bytes.Length -and
                    $bytes[$i] -eq 0xEF -and $bytes[$i+1] -eq 0xBB -and $bytes[$i+2] -eq 0xBF) {
                    $bomCount++
                } else {
                    break
                }
            }

            if ($bomCount -gt 1) {
                Write-Host "❌ Найдено $bomCount BOM символов в файле: $file" -ForegroundColor Red

                # Убираем все лишние BOM, оставляем только один
                $startIndex = ($bomCount - 1) * 3
                $newBytes = $bytes[$startIndex..($bytes.Length-1)]
                [System.IO.File]::WriteAllBytes($fullPath, $newBytes)

                Write-Host "✅ Исправлены множественные BOM в файле: $file" -ForegroundColor Green
                $fixed++
            } elseif ($bomCount -eq 1) {
                Write-Host "✅ $file - BOM корректный" -ForegroundColor Green
            } else {
                Write-Host "⚠️  $file - BOM отсутствует, добавляем" -ForegroundColor Yellow

                # Добавляем BOM в начало файла
                $bomBytes = @(0xEF, 0xBB, 0xBF)
                $newBytes = $bomBytes + $bytes
                [System.IO.File]::WriteAllBytes($fullPath, $newBytes)

                Write-Host "✅ Добавлен BOM в файл: $file" -ForegroundColor Green
                $fixed++
            }

            # Дополнительная проверка XML валидности
            $content = Get-Content $fullPath -Raw
            [xml]$xml = $content
            Write-Host "✅ $file - XML валидный" -ForegroundColor Green

        }
        catch {
            Write-Host "❌ $file - Ошибка: $($_.Exception.Message)" -ForegroundColor Red
        }
    } else {
        Write-Host "⚠️  $file - файл не найден" -ForegroundColor Yellow
    }
}

Write-Host "`nРезультат проверки:" -ForegroundColor Cyan
Write-Host "- Всего файлов проверено: $total" -ForegroundColor White
Write-Host "- Исправлено файлов: $fixed" -ForegroundColor White

if ($fixed -gt 0) {
    Write-Host "`n⚠️  Файлы были изменены! Необходимо сделать commit." -ForegroundColor Yellow
} else {
    Write-Host "`n✅ Все файлы в порядке!" -ForegroundColor Green
}

# === ДОПОЛНИТЕЛЬНО: Настройки для предотвращения проблем с VSCode ===
Write-Host "`n📝 Рекомендации для настройки VSCode:" -ForegroundColor Cyan
Write-Host "1. Откройте File → Preferences → Settings" -ForegroundColor White
Write-Host "2. Найдите 'files.encoding' и установите 'utf8'" -ForegroundColor White
Write-Host "3. Найдите 'files.autoSave' и установите 'off' или 'onFocusChange'" -ForegroundColor White
Write-Host "4. Добавьте в settings.json:" -ForegroundColor White
Write-Host '   "files.encoding": "utf8",' -ForegroundColor Gray
Write-Host '   "files.insertFinalNewline": true,' -ForegroundColor Gray
Write-Host '   "files.trimFinalNewlines": true' -ForegroundColor Gray
