using Core.Entities.Police;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficControl.Core.Interfaces.RepositoryInterfaces
{
    public interface ICarRepository
    {
        public Task<Car> GetCarByPlateNumber(string PlateNumber);
    }
}
