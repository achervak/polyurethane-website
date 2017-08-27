using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Polyurethane.Data.DbContext;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Interfaces.DataManagers;

namespace Polyurethane.Data.Implementation.DataManagers
{
    public class ConfigurationManager : IConfigurationManager
    {

        private readonly IDataProvider _dataProvider;
        private readonly IMapper _mapper;

        public ConfigurationManager(IDataProvider dataProvider, IMapper mapper)
        {
            _dataProvider = dataProvider;
            _mapper = mapper;
        }

        public string GetValue(string name, string defaultValue = "")
        {
            using (var db = new PolyurethaneContext(_dataProvider.ConnectionString))
            {
                var dbValue = db.Configurations.FirstOrDefault( x => x.Name == name && x.IsActive );
                return dbValue == null ? defaultValue : dbValue.Value;
            }
        }
    }
}
