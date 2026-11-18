param(
    [string]$Template,
    [switch]$Watch
)

$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
$packages = $env:NUGET_PACKAGES
if (-not $packages) {
    $packages = Join-Path $env:USERPROFILE '.nuget/packages'
}
$transform = Join-Path $packages 'mono.texttransform/1.0.0/tools/TextTransform.exe'
if (-not (Test-Path -LiteralPath $transform)) {
    throw 'T4 processor is missing. Run dotnet restore Foundations/Foundations.csproj first.'
}

function Transform-Template([string]$Path) {
    $inputFile = Get-Item -LiteralPath $Path
    if ($inputFile.Extension -ne '.tt') {
        throw 'Select a .tt file to transform.'
    }

    # Keep compiled template assemblies on a local path, including in restricted shells.
    $temporaryDirectory = Join-Path $projectRoot 'Foundations/obj/T4'
    [System.IO.Directory]::CreateDirectory($temporaryDirectory) | Out-Null
    $outputFile = [System.IO.Path]::ChangeExtension($inputFile.FullName, '.cs')
    $temporaryOutput = Join-Path $temporaryDirectory ([guid]::NewGuid().ToString() + '.cs')
    $previousTemp = $env:TEMP
    $previousTmp = $env:TMP
    try {
        $env:TEMP = $temporaryDirectory
        $env:TMP = $temporaryDirectory
        & $transform -o $temporaryOutput $inputFile.FullName
        if ($LASTEXITCODE -ne 0 -or -not (Test-Path -LiteralPath $temporaryOutput)) {
            throw "T4 transformation failed for $($inputFile.FullName)."
        }

        if (-not (Test-Path -LiteralPath $outputFile) -or
            [System.IO.File]::ReadAllText($temporaryOutput) -cne [System.IO.File]::ReadAllText($outputFile)) {
            Move-Item -LiteralPath $temporaryOutput -Destination $outputFile -Force
        }
        Write-Host "Transformed $($inputFile.FullName)"
    }
    finally {
        $env:TEMP = $previousTemp
        $env:TMP = $previousTmp
        Remove-Item -LiteralPath $temporaryOutput -ErrorAction SilentlyContinue
    }
}

if (-not $Watch) {
    if (-not $Template) {
        throw 'Specify -Template path/to/file.tt or -Watch.'
    }
    Transform-Template $Template
    exit
}

# Process saved changes serially. Generated .cs files never trigger another transformation.
$timestamps = @{}
$initialized = $false
$folders = @(Join-Path $projectRoot 'Foundations'; Join-Path $projectRoot 'UnitTests')
while ($true) {
    foreach ($file in Get-ChildItem -LiteralPath $folders -Recurse -File -Filter '*.tt') {
        $stamp = "$($file.LastWriteTimeUtc.Ticks):$($file.Length)"
        if ($initialized -and $timestamps[$file.FullName] -ne $stamp) {
            try {
                Transform-Template $file.FullName
            }
            catch {
                # Report the error and keep watching so that the next save can fix it.
                Write-Host "ERROR: $_" -ForegroundColor Red
            }
        }
        $timestamps[$file.FullName] = $stamp
    }
    if (-not $initialized) {
        Write-Host 'Watching .tt files. Save a template to regenerate its .cs file.'
        $initialized = $true
    }
    Start-Sleep -Milliseconds 500
}
