﻿using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;

public class RePurchasingDatabase : Database.Database
{
    public RePurchasingDatabase(IConfiguration configuration, string userId, string password)
        : base(configuration, userId, password)
    {
    }
}