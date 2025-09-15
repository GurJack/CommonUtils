# Script to encode DevExpress packages to base64 for GitHub Secrets

param(
    [string]$PackagesPath = "..\LocalPackages"
)

Write-Host "Searching for DevExpress packages in: $PackagesPath"

# Check if folder exists
if (-not (Test-Path $PackagesPath)) {
    Write-Host "Folder not found: $PackagesPath"
    exit 1
}

# Get list of .nupkg files
$packages = Get-ChildItem -Path $PackagesPath -Filter "*.nupkg" -Recurse

if ($packages.Count -eq 0) {
    Write-Host "DevExpress packages not found in folder: $PackagesPath"
    Write-Host "First run: .\copy-devexpress-packages.ps1"
    exit 1
}

Write-Host "Found packages: $($packages.Count)"

# Create folder for results
$outputDir = "GitHubSecrets"
if (-not (Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir -Force | Out-Null
}

Write-Host "Encoding packages to base64..."

foreach ($package in $packages) {
    Write-Host "Processing: $($package.Name)"

    # Read file and encode to base64
    $bytes = [IO.File]::ReadAllBytes($package.FullName)
    $base64 = [Convert]::ToBase64String($bytes)

    # Create secret name (replace . and - with _)
    $secretName = $package.Name.Replace(".", "_").Replace("-", "_").ToUpper()

    # Save base64 to file
    $outputFile = Join-Path $outputDir "$secretName.txt"
    Set-Content -Path $outputFile -Value $base64 -Encoding UTF8

    Write-Host "  Encoded: $secretName"
    Write-Host "  Size: $([math]::Round($bytes.Length / 1KB, 2)) KB"
    Write-Host "  File: $outputFile"
    Write-Host ""
}

Write-Host "All packages successfully encoded!"
Write-Host "Results saved in folder: $outputDir"

Write-Host "Next steps:"
Write-Host "1. Create secrets in GitHub repository settings"
Write-Host "2. Use the following secret names:"
foreach ($package in $packages) {
    $secretName = $package.Name.Replace(".", "_").Replace("-", "_").ToUpper()
    Write-Host "   - $secretName"
}
Write-Host "3. Paste content of .txt files as secret values"
