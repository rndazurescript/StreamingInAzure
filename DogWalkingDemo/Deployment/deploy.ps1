#Requires -Version 5.1
#Requires -Module Az.Resources
#Requires -Module Az.Storage
#Requires -Module Az.ApplicationInsights

[CmdletBinding()]
param(
  [Parameter(Mandatory=$true)]
  [string]$ResourceGroupName = "",
  [Parameter(Mandatory=$true)]
  [string]$DemoName = ""
)

# Login to specific subscription in the specific tenant (normally you don't need to be that specific)
# Connect-AzAccount -Subscription $SubscriptionId  -Tenant $TenantId

$EventHubName="$($DemoName)-eh"
$StorageAccountName="$($DemoName)stg"
$AzureStreamAnalyticsJobName="$($DemoName)-asa"

New-AzResourceGroupDeployment -Mode Incremental `
    -TemplateFile "pipeline_010_resources.json" `
    -ResourceGroupName $ResourceGroupName `
    -eventHubName $EventHubName `
    -storageAccountName $StorageAccountName `
    -azureStreamAnalyticsJobName $AzureStreamAnalyticsJobName

# Upload reference data
&"$PSScriptRoot\pipeline_020_Storage_Upload_Containers.ps1" -storageAccountName $StorageAccountName

# Generate secrets.json file for EventGenerator
$EventHubConnectionString = $(Get-AzEventHubKey -ResourceGroupName $ResourceGroupName -NamespaceName $EventHubName -AuthorizationRuleName RootManageSharedAccessKey).PrimaryConnectionString
@{EventHubName=$EventHubName;ConnectionString=$EventHubConnectionString} | ConvertTo-Json | Out-File "$PSScriptRoot\..\EventGenerator\secrets.json"

Write-Host "$($MyInvocation.MyCommand.Name) finished for environment $environment"