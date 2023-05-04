#start SQL Server, start the script to create the DB and import the data, start the app
export ACCEPT_EULA=Y
export SA_PASSWORD=P@ssw0rd!
/opt/mssql/bin/sqlservr &

#run the setup script to create the DB and the schema in the DB
#do this in a loop because the timing for when the SQL instance is ready is indeterminate
for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./adventureworks/instawdb.sql
    if [ $? -eq 0 ]
    then
        echo "instawdb.sql completed"
        break
    else
        echo "not ready yet..."
        sleep 1
    fi
done

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./01_create_user.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./02_create_table.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./03_create_function.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./04_create_view.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./05_grant.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./06_setup_data.sql
