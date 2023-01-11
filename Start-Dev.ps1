# docker pull nuitsjp/adventureworks:latest
# docker create --name adventureworks -e ACCEPT_EULA=Y -e SA_PASSWORD=P@ssw0rd! -p 1433:1433 nuitsjp/adventureworks:latest
# docker start adventureworks
docker-compose -f (Join-Path $PSScriptRoot 'compose-dev.yaml') up -d


# update
# 	HumanResources.Employee
# set
# 	LoginID = 'DESKTOP-AUA6P9K\atsus'
# where
# 	BusinessEntityID = 260