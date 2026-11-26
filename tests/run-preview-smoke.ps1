$ErrorActionPreference = 'Stop'
$env:ASPNETCORE_ENVIRONMENT = 'Development'
$env:Tools__PreviewAll = 'true'
$env:ASPNETCORE_URLS = 'http://127.0.0.1:5187'
$server = Start-Process dotnet -ArgumentList 'bin/Debug/net8.0/ToolSite.dll' -WindowStyle Hidden -PassThru
try {
  $chromeProfile = Join-Path $env:TEMP ('toolsite-chrome-' + [guid]::NewGuid().ToString('N'))
  $chromePath = Join-Path $env:ProgramFiles 'Google/Chrome/Application/chrome.exe'
  $chrome = Start-Process $chromePath -ArgumentList @('--headless', '--disable-gpu', '--remote-debugging-port=9223', ('--user-data-dir="' + $chromeProfile + '"'), 'about:blank') -WindowStyle Hidden -PassThru
  try {
    $ready = $false
    for ($attempt = 0; $attempt -lt 40; $attempt++) {
      try {
        Invoke-WebRequest -Uri 'http://127.0.0.1:5187/' -TimeoutSec 1 | Out-Null
        Invoke-WebRequest -Uri 'http://127.0.0.1:9223/json/list' -TimeoutSec 1 | Out-Null
        $ready = $true
        break
      } catch { Start-Sleep -Milliseconds 250 }
    }
    if (!$ready) { throw 'ToolSite or Chrome did not start.' }
    node tests/preview-smoke.mjs
    if ($LASTEXITCODE -ne 0) { throw 'Browser smoke tests failed.' }
  } finally {
    Stop-Process -Id $chrome.Id -ErrorAction SilentlyContinue
  }
} finally {
  Stop-Process -Id $server.Id -ErrorAction SilentlyContinue
}
