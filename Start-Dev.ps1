# docker pull nuitsjp/adventureworks:latest
# docker create --name adventureworks -e ACCEPT_EULA=Y -e SA_PASSWORD=P@ssw0rd! -p 1433:1433 nuitsjp/adventureworks:latest
# docker start adventureworks
docker-compose -f (Join-Path $PSScriptRoot 'compose-dev.yaml') up -d

## データベースへの接続
$connectionString = 'Data Source=localhost;Initial Catalog=AdventureWorks;User ID=sa;Password=P@ssw0rd!'

$user = [System.Security.Principal.WindowsIdentity]::GetCurrent().Name
$sqlQuery = "
update 
    HumanResources.Employee 
set 
    LoginID = '$user' 
where 
    BusinessEntityID = 260;

select 'Updated login user.'"

while ($true) {
    $connection = New-Object System.Data.SQLClient.SQLConnection $connectionString
    $sqlCommand = New-Object System.Data.SQLClient.SQLCommand($sqlQuery, $connection)
    try {
        $connection.Open()
        
        Write-Host $sqlCommand.ExecuteScalar()    
        break
    }
    catch {
        Write-Host "SQL Server 起動待機..."
        Start-Sleep -Seconds 1
    }
    finally {
        $connection.Dispose()
        $sqlCommand.Dispose()
    }
}

