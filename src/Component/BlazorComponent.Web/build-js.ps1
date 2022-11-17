param(
  [Parameter(Mandatory = $true, HelpMessage = "main | echarts | input | markdownit | gridstack")]
  [ValidateSet("main", "echarts", "input", "markdownit", "gridstack")]
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
    elseif ($file -eq 'gridstack') {
      npm run build:gridstack
    }
    elseif ($file -eq 'echarts') {
      npm run build:echarts
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