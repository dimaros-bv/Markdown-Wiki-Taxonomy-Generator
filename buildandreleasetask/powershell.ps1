function BuildTaxonomy {
    [CmdletBinding()]
    param()

    $root = Get-VstsInput -Name 'root'
    $taxonomyPath = Get-VstsInput -Name 'taxonomyPath'
    $headerStart = Get-VstsInput -Name 'headerStart'
    $headerEnd = Get-VstsInput -Name 'headerEnd'
    $branch = Get-VstsInput -Name 'branch'

    $currentLocation = Get-Location

    $dllLocation = Join-Path -Path $currentLocation -ChildPath "WikiTaxonomyGenerator\bin\Release\netcoreapp3.1\WikiTaxonomyGenerator.dll"

    Set-Location -Path $root
    git init

    # Remove old taxonomy
    $pathToTaxonomy = Join-Path -Path ($root + '/') -ChildPath $taxonomyPath

    Get-ChildItem -Path $pathToTaxonomy | Remove-Item -Recurse -Confirm:$false -Force -Verbose

    # Create new taxonomy
    dotnet $dllLocation --root $root --header-start $headerStart --header-end $headerEnd

    # Commit Changes
    git status
    git config user.name "Taxonomy Generator"
    git config user.email "-"
    git add $taxonomyPath
    git commit -m "Generated Taxonomies"
    git checkout -b $branch
    git push origin $branch
}

BuildTaxonomy