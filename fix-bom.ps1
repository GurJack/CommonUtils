# Скрипт для проверки и исправления BOM символов в .csproj файлах
Write-Host "Проверка BOM символов в .csproj файлах..." -ForegroundColor Green

$projectFiles = @(
    "CommonUtils\Data\BaseData\BaseData.csproj",
    "CommonUtils\Data\CommonData\CommonData.csproj",
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

            # Проверяем на двойной BOM (UTF-8 BOM: EF BB BF)
            if ($bytes.Length -ge 6 -and
                $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF -and
                $bytes[3] -eq 0xEF -and $bytes[4] -eq 0xBB -and $bytes[5] -eq 0xBF) {

                Write-Host "❌ Найден двойной BOM в файле: $file" -ForegroundColor Red

                # Убираем первые 3 байта (первый BOM)
                $newBytes = $bytes[3..($bytes.Length-1)]
                [System.IO.File]::WriteAllBytes($fullPath, $newBytes)

                Write-Host "✅ Исправлен двойной BOM в файле: $file" -ForegroundColor Green
                $fixed++
            } elseif ($bytes.Length -ge 3 -and
                      $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF) {
                Write-Host "✅ $file - BOM корректный" -ForegroundColor Green
            } else {
                Write-Host "⚠️  $file - BOM отсутствует" -ForegroundColor Yellow
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
