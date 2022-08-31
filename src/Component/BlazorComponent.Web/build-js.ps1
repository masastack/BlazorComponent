param(
  [Parameter()]
  [ValidateSet("main", "input")]
  [string]$file
)

if (Get-Command npm -ErrorAction SilentlyContinue) {
  Write-Host "Installing packages..." -ForegroundColor Yellow
  npm install

  Write-Host
  Write-Host "Building js..." -ForegroundColor Yellow
  if ($file) {
    if ($file -eq 'main') {
      npm run build
    }
    elseif ($file -eq 'input') {
      npm run build:input
    }
  }
  else {
    npm run build
    npm run build:input
  }

  Write-Host
  Write-Host "Builded js successfully!" -ForegroundColor Green
}
else {
  Write-Host "The npm command is not installed." -ForegroundColor Red
}