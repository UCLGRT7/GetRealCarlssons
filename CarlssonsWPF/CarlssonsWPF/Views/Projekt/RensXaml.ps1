param(
    [string]$InputFile = "ViewProjectView.xaml",
    [string]$OutputFile = "ViewProjectView_Clean.xaml"
)

# Få fuld sti på inputfilen
$inputFullPath = Resolve-Path $InputFile
$inputDir = Split-Path $inputFullPath

# Lav output filsti i samme mappe som inputfilen
$outputFullPath = Join-Path $inputDir $OutputFile

# Læs fil som UTF8 tekst
$content = Get-Content -Raw -Encoding UTF8 $inputFullPath

# Fjern usynlige / kontroltegn (undtagen newline, carriage return og tab)
$cleanContent = -join ($content.ToCharArray() | Where-Object {
    $c = [int][char]$_
    -not ([char]::IsControl($_) -and $_ -ne "`r" -and $_ -ne "`n" -and $_ -ne "`t")
})

# Gem til ny fil med UTF8 uden BOM
[System.IO.File]::WriteAllText($outputFullPath, $cleanContent, [Text.Encoding]::UTF8)

Write-Host "Filen er renset og gemt som $outputFullPath"
