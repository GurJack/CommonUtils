# Проверка валидности XML в файлах проектов
$projectFiles = @(
    "CommonUtils\Data\BaseData\BaseData.csproj",
    "CommonUtils\Data\CommonData\CommonData.csproj",
    "CommonUtils\Data\BaseMSSqlProvider\BaseMSSqlProvider.csproj"
)

Write-Host "Проверка валидности XML файлов проектов..." -ForegroundColor Green

foreach ($file in $projectFiles) {
    $fullPath = Join-Path $PSScriptRoot $file
    if (Test-Path $fullPath) {
        try {
            $xml = [xml](Get-Content $fullPath -Raw)
            Write-Host "✅ $file - XML валидный" -ForegroundColor Green
        }
        catch {
            Write-Host "❌ $file - XML невалидный: $($_.Exception.Message)" -ForegroundColor Red
        }
    } else {
        Write-Host "⚠️  $file - файл не найден" -ForegroundColor Yellow
    }
}

Write-Host "`nПроверка завершена." -ForegroundColor Cyan
