$appDb = "-ConfigurationTypeName TechmerVision.Migrations.Configuration"
$memDb = "-ConfigurationTypeName TechmerVision.ApplicationDbContextMigrations.Configuration"



#Workspace
Update-Database -ConfigurationTypeName TechmerVision.Migrations.Configuration -ConnectionString "Data Source=tcp:tpm-azuresql01.database.windows.net,1433;Initial Catalog=test-ColorProject;User ID=tpmSQLAdmin@tpm-azuresql01;Password=Egpnmas#16" -ConnectionProviderName "System.Data.SqlClient"

#membership
Update-Database -ConfigurationTypeName TechmerVision.Migrations.Configuration -ConnectionString "Data Source=tcp:tpm-azuresql01.database.windows.net,1433;Initial Catalog=test-ColorProjectMembership;User ID=tpmSQLAdmin@tpm-azuresql01;Password=Egpnmas#16" -ConnectionProviderName "System.Data.SqlClient"


Get-Migrations -ConnectionString "{Azure Database Connection String}" -ConnectionProviderName "System.Data.SqlClient"
Update-Database -TargetMigration "{TargetMigrationName}" -ConnectionString "{Azure Database Connection String}" -ConnectionProviderName "System.Data.SqlClient"
