﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeless.DependencyServices
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetConnection();
    }
}