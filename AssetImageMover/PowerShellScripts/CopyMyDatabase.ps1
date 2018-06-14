#import SQL Server module
Import-Module SQLPS -DisableNameChecking
 
#your SQL Server Instance Name
$SQLInstanceName = "DESKTOP-RBIFU5T"
$Server  = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Server -ArgumentList $SQLInstanceName
 
#provide your database name which you want to copy
$SourceDBName   = "TechmerVisionAppNextDeploy"
 
#create SMO handle to your database
$SourceDB = $Server.Databases[$SourceDBName]
 
#create a database to hold the copy of your source database
$CopyDBName = "TechmerVisionAppNextDeploy_BackUp1"
$CopyDB = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Database -ArgumentList $Server , $CopyDBName
$CopyDB.Create()
 
#Use SMO Transfer Class by specifying source database
#you can specify properties you want either brought over or excluded, when the copy happens
$ObjTransfer   = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Transfer -ArgumentList $SourceDB
$ObjTransfer.CopyAllTables = $true
$ObjTransfer.Options.WithDependencies = $true
$ObjTransfer.Options.ContinueScriptingOnError = $true
$ObjTransfer.DestinationDatabase = $CopyDBName
$ObjTransfer.DestinationServer = $Server.Name
$ObjTransfer.DestinationLoginSecure = $true
$ObjTransfer.CopySchema = $true
 
#if you wish to just generate the copy script
$ObjTransfer.ScriptTransfer()
 
#When you are ready to bring the data and schema over,
#you can use the TransferData method
$ObjTransfer.TransferData()