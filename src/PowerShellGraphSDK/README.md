# Table of Contents
- [Table of Contents](#table-of-contents)
- [Intune-PowerShell-SDK](#intune-powershell-sdk)
- [Azure CloudShell](#azure-cloudshell)
- [Getting started](#getting-started)
    - [One-time setup](#one-time-setup)
    - [Each time you use the module](#each-time-you-use-the-module)
    - [Discovering available commands](#discovering-available-commands)
- [Example usage](#example-usage)
    - [Retrieving objects](#retrieving-objects)
    - [Creating objects](#creating-objects)
    - [Modifying objects](#modifying-objects)
    - [Deleting objects](#deleting-objects)
    - [Calling functions and actions](#calling-functions-and-actions)
- [Notable features](#notable-features)
- [Known issues and workarounds](#known-issues-and-workarounds)

# Intune-PowerShell-SDK
This repository contains the source code for the PowerShell module which provides support for the Intune API through Microsoft Graph.

This project welcomes contributions and suggestions.  Most contributions require you to agree to a Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

# Azure CloudShell
This module has been designed to work with Azure CloudShell.

[![Launch Cloud Shell](https://shell.azure.com/images/launchcloudshell.png "Launch Cloud Shell")](https://shell.azure.com/powershell)

# Getting started
## One-time setup
If you don't have the Intune module installed, install it from PowerShell Gallery:
```PowerShell
Install-Module Intune
```
If you do have the Intune module installed, you can update it by running this command:
```PowerShell
Update-Module Intune
```
## Each time you use the module
Import the module:
```PowerShell
Import-Module Intune
```
To authenticate with Microsoft Graph (this is not required when using CloudShell):
```PowerShell
Connect-MSGraph
```
## Discovering available commands
Get the full list of available cmdlets:
```PowerShell
Get-Command -Module Intune
```
Get documentation on a particular cmdlet:
```PowerShell
Get-Help <cmdlet name>
```
Use a UI to see the parameter sets more easily:
```PowerShell
Show-Command <cmdlet name>
```

# Example usage
## Retrieving objects
Get all Intune applications:
```PowerShell
Get-DeviceAppManagement_MobileApps
```
Get all Intune device configurations:
```PowerShell
Get-DeviceManagement_DeviceConfigurations
```
Get all Intune managed devices:
```PowerShell
Get-DeviceManagement_ManagedDevices
```
## Creating objects
Create a web application:
```PowerShell
$bingWebApp = New-DeviceAppManagement_MobileApps -WebApp -DisplayName 'Bing' -Publisher 'Microsoft Corporation' -AppUrl 'https://www.bing.com'
```

## Modifying objects
Update the web application that we created in the '[Creating objects](#creating-objects)' section:
```PowerShell
$bingWebApp | Update-DeviceAppmanagement_MobileApps -WebApp -DisplayName 'Bing Search'
```

## Deleting objects
Delete the web application that we created in the '[Creating objects](#creating-objects)' section:
```PowerShell
$bingWebApp | Remove-DeviceAppmanagement_MobileApps
```

## Calling functions and actions
Lock a managed device:
```PowerShell
# Get a device to lock
$allDevices = Get-DeviceManagement_ManagedDevices
$deviceToLock = $allDevices[0]

# Lock this device
$deviceToLock | Invoke-DeviceManagement_ManagedDevices_RemoteLock
```

# Notable features
- Standard PowerShell objects are used for input/output, meaning that all built-in PowerShell features/utilities/tricks work, including:
    - Piping of objects between cmdlets
    - Formatting of output: `Format-Table`, `Out-GridView`, `ConvertTo-Csv`, `ConvertTo-Json`, etc.
    - Getting help on usage: `Get-Help`
    - Visualizing input parameters: `Show-Command`
    - Using the 'tab' key to auto-complete or cycle through available options
- Documentation which is available in the schema is injected into the cmdlet documentation
- Auto-complete and validation on Enum parameters as well as some query parmeters (i.e. $select, $expand and $orderBy)
- Utility cmdlets for some common tasks
    - Getting the authentication token: `Connect-MSGraph`
    - Getting service metadata: `Get-MSGraphMetadata`
    - Paging: `Get-MSGraphNextPage` and `Get-MSGraphAllPages`
    - Changing environment settings, e.g. Graph schema version: `Update-MSGraphEnvironment -Schema beta -AppId 00000000-0000-0000-0000-000000000000`
- The PowerShell module can be generated for any valid Graph schema

# Known issues and workarounds
- Importing the `MSOnline` cmdlets before importing this `Intune` module will cause errors. Please use the `AzureAD` module instead, as the `MSOnline` module is deprecated.
    - If you absolutely must use the `MSOnline` module, it should be imported AFTER the `Intune` module. Note, however, that this is not officially supported.