function BuildTaxonomy {
    [CmdletBinding()]
    param()

    $root = Get-VstsInput -Name 'root'
    $taxonomyPath = Get-VstsInput -Name 'taxonomyPath'
    $headerStart = Get-VstsInput -Name 'headerStart'
    $headerEnd = Get-VstsInput -Name 'headerEnd'

    $currentLocation = Get-Location
    $dllLocation = Join-Path -Path $currentLocation -ChildPath "WikiTaxonomyGenerator\bin\Release\netcoreapp3.1\WikiTaxonomyGenerator.dll"

    git init

    $result = Get-ChildItem -Path "D:\a\1\s"
    Write-Host "Result before deletion $result"

    # Remove old taxonomy
    Get-ChildItem -Path "D:\a\1\s\" -Directory -Filter $taxonomyPath | Remove-Item -Recurse -Confirm:$false -Force -Verbose

    $result = Get-ChildItem -Path "D:\a\1\s"
    Write-Host "Result after deletion $result"

    # Create new taxonomy
    Set-Location -Path "D:\a\1\s\"

    dotnet $dllLocation --root $root --header-start $headerStart --header-end $headerEnd

    $result = Get-ChildItem -Path "D:\a\1\s"
    Write-Host "Result after generation $result"

    Write-Host "-----------------------------------------------------------------------------"
    Write-Host "View of the total directory"
    Get-ChildItem -Path $dllLocation -Recurse
    Write-Host "-----------------------------------------------------------------------------"

    # Commit Changes
    git status
    git config user.name "Taxonomy Generator"
    git config user.email "-"
    git add Taxonomy
    git commit -m "Generated Taxonomies"
    git checkout -b markdown-extension-test
    git push origin markdown-extension-test
}

BuildTaxonomy