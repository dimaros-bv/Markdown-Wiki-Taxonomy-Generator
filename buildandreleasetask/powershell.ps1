[CmdletBinding()]
param()

$root = Get-VstsInput -Name 'root'
$taxonomyPath = Get-VstsInput -Name 'taxonomyPath'
$headerStart = Get-VstsInput -Name 'headerStart'
$headerEnd = Get-VstsInput -Name 'headerEnd'

Write-Host "Path to the wiki: $root"
Write-Host "Path to the taxonomy: $taxonomyPath"
Write-Host "Beginning of the taxonomy header: $headerStart"
Write-Host "Ending of the taxonomy header: $headerEnd"