. .\..\..\..\..\Tools\Scripts\GlobalFunctions.ps1
. .\..\..\..\..\Tools\Scripts\SubscriptionFunctions.ps1

Set-DefaultAzureSubscription -path D:\AllFiles\Mod11\DemoFiles\ACSForServiceBus

Write-Host Retrieving information, please wait... -ForegroundColor Cyan

$sbNamespace = new-object psobject -Property $props

$sbNamespaces = Get-AzureSBNamespace
$sbNamespaces = $sbNamespaces | where {$_.Name.ToLower() -like 'blueyonderserverdemo11*'}
if ($sbNamespaces -eq $null -or $sbNamespaces.Length -eq 0)
{
    $initials = Get-Initials

    $date = Get-Date
    $date = $date.ToString("yyyy-MM-dd")

    $newSbNamespaceName = 'blueyonderserverdemo11' + $initials
        
    $region = Get-Region
    Write-Host Creating Service Bus namespace, please wait... -ForegroundColor Cyan
    Write-Host 

    $newSbNameSpace = New-AzureSBNamespace -Location $region -Name $newSbNamespaceName -CreateACSNamespace $true -NamespaceType Messaging
	while ($newSbNameSpace.Status -ne 'Active')
	{
		sleep 5
		$newSbNameSpace = Get-AzureSBNamespace $newSbNamespaceName
	}    
    
    Write-Host "Please use the following Service Bus namespace in this demo:"; Write-host $newSbNameSpace.Name -ForegroundColor Cyan 
    
}
else
{
    Write-Host "Please use the following Service Bus namespace in this demo:"; Write-host $sbNamespaces[0].Name -ForegroundColor Cyan     
}


