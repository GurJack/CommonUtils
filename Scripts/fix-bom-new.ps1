# Скрипт для проверки и исправления BOM символов в .csproj файлах
Write-Host "Проверка BOM символов в .csproj файлах..." -ForegroundColor Green

$projectFiles = @(
    "CommonUtils\Data\BaseData\BaseData.csproj",
    "CommonUtils\Data\CommonData\CommonData.csproj",
    "CommonUtils\CommonUtils\CommonUtils.csproj",
    "CommonUtils\Tests\TestWindows\Windows.Tests.csproj",
    "CommonUtils\Data\BaseMSSqlProvider\BaseMSSqlProvider.csproj"
)

$fixed = 0
$total = 0

foreach ($file in $projectFiles) {
    $rootPath = if ($PSScriptRoot) { $PSScriptRoot } else { Get-Location }
    $fullPath = Join-Path $rootPath $file
    $total++

    if (Test-Path $fullPath) {
        try {
            $bytes = [System.IO.File]::ReadAllBytes($fullPath)
            $bomPattern = @(0xEF, 0xBB, 0xBF)
            $bomCount = 0

            for ($i = 0; $i -lt $bytes.Length - 2; $i += 3) {
                if ($i + 2 -lt $bytes.Length -and $bytes[$i] -eq 0xEF -and $bytes[$i+1] -eq 0xBB -and $bytes[$i+2] -eq 0xBF) {
                    $bomCount++
                } else {
                    break
                }
            }

            if ($bomCount -gt 1) {
                Write-Host "Найдено $bomCount BOM символов в файле: $file" -ForegroundColor Red
                $startIndex = ($bomCount - 1) * 3
                $newBytes = $bytes[$startIndex..($bytes.Length-1)]
                [System.IO.File]::WriteAllBytes($fullPath, $newBytes)
                Write-Host "Исправлены множественные BOM в файле: $file" -ForegroundColor Green
                $fixed++
            } elseif ($bomCount -eq 1) {
                Write-Host "$file - BOM корректный" -ForegroundColor Green
            } else {
                Write-Host "$file - BOM отсутствует, добавляем" -ForegroundColor Yellow
                $bomBytes = @(0xEF, 0xBB, 0xBF)
                $newBytes = $bomBytes + $bytes
                [System.IO.File]::WriteAllBytes($fullPath, $newBytes)
                Write-Host "Добавлен BOM в файл: $file" -ForegroundColor Green
                $fixed++
            }

            $content = Get-Content $fullPath -Raw
            [xml]$xml = $content
            Write-Host "$file - XML валидный" -ForegroundColor Green

        } catch {
            Write-Host "$file - Ошибка: $($_.Exception.Message)" -ForegroundColor Red
        }
    } else {
        Write-Host "$file - файл не найден" -ForegroundColor Yellow
    }
}

Write-Host "Результат проверки:" -ForegroundColor Cyan
Write-Host "- Всего файлов проверено: $total" -ForegroundColor White
Write-Host "- Исправлено файлов: $fixed" -ForegroundColor White

if ($fixed -gt 0) {
    Write-Host "Файлы были изменены! Необходимо сделать commit." -ForegroundColor Yellow
} else {
    Write-Host "Все файлы в порядке!" -ForegroundColor Green
}
