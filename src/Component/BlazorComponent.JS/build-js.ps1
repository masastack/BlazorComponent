if (Get-Command npm -ErrorAction SilentlyContinue) {
  Write-Host "Installing packages..." -ForegroundColor Yellow
  npm install

  Write-Host
  Write-Host "Building js..." -ForegroundColor Yellow
  npm run gulp:components

  Write-Host
  Write-Host "Builded js successfully!" -ForegroundColor Green
}
else {
  Write-Host "The npm command is not installed." -ForegroundColor Red
}