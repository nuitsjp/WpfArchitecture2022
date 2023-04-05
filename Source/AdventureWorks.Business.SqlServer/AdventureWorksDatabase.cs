﻿using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Business.SqlServer;

public class AdventureWorksDatabase : Database.Database
{
    public AdventureWorksDatabase(IConfiguration configuration, string userId, string password) 
        : base(configuration, userId, password)
    {
    }
}