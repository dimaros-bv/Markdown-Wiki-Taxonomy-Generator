function BuildTaxonomy {
    [CmdletBinding()]
    param()

    $root = Get-VstsInput -Name 'root'
    $taxonomyPath = Get-VstsInput -Name 'taxonomyPath'
    $headerStart = Get-VstsInput -Name 'headerStart'
    $headerEnd = Get-VstsInput -Name 'headerEnd'

    $branchName = "$(Build.SourceBranch)" -replace "refs/heads",""
    Write-Host "Current branch name: $branchName"

    $currentLocation = Get-Location

    $dllLocation = Join-Path -Path $currentLocation -ChildPath "WikiTaxonomyGenerator\bin\Release\netcoreapp3.1\WikiTaxonomyGenerator.dll"

    Set-Location -Path "D:\a\1\s"
    git init

    # Remove old taxonomy
    $pathToTaxonomy = Join-Path -Path "D:\a\1\s\" -ChildPath $taxonomyPath

    Get-ChildItem -Path $pathToTaxonomy | Remove-Item -Recurse -Confirm:$false -Force -Verbose

    # Create new taxonomy
    dotnet $dllLocation --root "D:\a\1\s" --header-start $headerStart --header-end $headerEnd

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