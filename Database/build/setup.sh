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

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./01_serilog.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./02_adventure_works.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./03_purchasing.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd! -d master -i ./04_re_purchasing.sql
