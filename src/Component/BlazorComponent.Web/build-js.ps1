param(
  [Parameter(Mandatory = $true, HelpMessage = "Enter 'main' to build 'main.ts', enter 'input' to build 'input.ts', enter 'markdownit' to build 'markdownit'.")]
  [ValidateSet("main", "input", "markdownit")]
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
    elseif ($file -eq 'markdownit') {
      npm run build:markdownit
    }
  }
  else {
    npm run build
    npm run build:input
    npm run build:markdownit
  }

  Write-Host
  Write-Host "Builded js successfully!" -ForegroundColor Green
}
else {
  Write-Host "The npm command is not installed." -ForegroundColor Red
}