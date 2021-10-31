function BuildTaxonomy {
    [CmdletBinding()]
    param()

    $root = Get-VstsInput -Name 'root'
    $taxonomyPath = Get-VstsInput -Name 'taxonomyPath'
    $headerStart = Get-VstsInput -Name 'headerStart'
    $headerEnd = Get-VstsInput -Name 'headerEnd'

    $currentLocation = Get-Location
    $dllLocation = Join-Path -Path $currentLocation -ChildPath "WikiTaxonomyGenerator\bin\Release\netcoreapp3.1\WikiTaxonomyGenerator.dll"
    #$dllLocation = Join-Path -Path $currentLocation -ChildPath "WikiTaxonomyGenerator\bin\Release\netcoreapp3.1\wronglocation.dll"

    Set-Location -Path "D:\a\1\s"
    git init

    # Actual path to the taxonomy
    $pathToTaxonomy = Join-Path -Path "D:\a\1\s\" -ChildPath $taxonomyPath

    $result = Get-ChildItem -Path $pathToTaxonomy
    Write-Host "Result before deletion $result"

    # Remove old taxonomy
    Get-ChildItem -Path $pathToTaxonomy | Remove-Item -Recurse -Confirm:$false -Force -Verbose

    $result = Get-ChildItem -Path $pathToTaxonomy
    Write-Host "Result after deletion $result"

    # Create new taxonomy
    dotnet $dllLocation --root "D:\a\1\s" --header-start $headerStart --header-end $headerEnd

    $result = Get-ChildItem -Path $pathToTaxonomy
    Write-Host "Result after generation $result"

    Write-Host "-----------------------------------------------------------------------------"
    Write-Host "View of the taxonomy directory"
    Get-ChildItem -Path "D:\a\1\s\" -Recurse
    Write-Host "-----------------------------------------------------------------------------"

    # Commit Changes
    git status
    git config user.name "Taxonomy Generator"
    git config user.email "-"
    git add Taxonomy
    git commit -m "Generated Taxonomies"
    git checkout -b taxonomy-generator-test
    git push origin taxonomy-generator-test
}

BuildTaxonomy