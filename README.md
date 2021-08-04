# Taxonomy Generator

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

## Usage
To execute the generator, you need to provide two to four parameters:
```
Mandatory:
--root <path>           : The path to the wiki
--header-start <string> : The start token of the taxonomy (e.g. **_Taxonomy Header_**)

Optional:
--taxonomy-path         : The path to the taxonomy part in the wiki (default: '/Taxonomy')
--header-end <string>   : The end token of the taxonomy (default: equal to the --header-start parameter)
```
