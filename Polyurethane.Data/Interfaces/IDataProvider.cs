using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyurethane.Data.Entities;

namespace Polyurethane.Data.Interfaces
{
    public interface IDataProvider
    {
        Task<CarEntity> CreateCar(CarEntity car);
    }
}
