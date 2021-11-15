# Taxonomy Generator Build/Release step

This tool generates tables of content for pages with a taxonomy header.
A taxonomy header consists out of some start token, links to the taxonomy pages and an end token.

Example:
```md
--------------------------------------------------------
**_Taxonomy Header_**

|Projects:|[](/Taxonomy/Projects) |
|--|--|

|Organization:|[Dimaros](/Taxonomy/Organization/Dimaros)  |
|--|--|

|Processes:|[](/Taxonomy/Processes)  |
|--|--|

|Applications:|[Azure DevOps](/Taxonomy/Applications/Azure-DevOps)  |
|--|--|

|Platforms:|[Azure Functions](/Taxonomy/Platform/Azure-Functions), [Power Automate ('Flow')](/Taxonomy/Platform/Microsoft-Power-Platform/Power-Automate-\('Flow'\))  |
|--|--|
--------------------------------------------------------
```

The generator puts a link targeting the page with the header on each of the linked taxonomy pages.
The example above results in the following taxonomy:

```
/Taxonomy/
    Organization/Dimaros.md
    Applications/Azure DevOps.md
	Platform/
        Microsoft Power Platform/Power Automate ('Flow').md
        Azure Functions.md
```

Links without text (e.g. `[](/Taxonomy/Projects/)`) and links that do not start with `/Taxonomy` are skipped.

## Parameter Usage
Required parameters:
- root: The path to the wiki (For example: '$(Build.SourcesDirectory)')  
- headerStart: String to indicate the start of the taxonomy header (For example '--------------------------------------------------------')  

<br/>

Optional parameters:
- branch: The branch name of the Wiki to generate the taxonomy for (Default: 'main')  
- taxonomyPath: The path to the taxonomy (Default: 'Taxonomy')  
- headerEnd: String to indicate the end of the taxonomy header (Default: equal to headerStart)  