using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Data.DbContext;

namespace Polyurethane.Data.Initializers
{
    public class PolyurethaneContextInitializer : CreateDatabaseIfNotExists<PolyurethaneContext>
    {
        
    }
}
