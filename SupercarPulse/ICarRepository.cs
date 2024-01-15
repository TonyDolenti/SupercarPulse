using SupercarPulse.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupercarPulse
{
    public interface ICarRepository
    {
        public IEnumerable<Car> GetAllCars();
        public Car GetCar(int id);
        public void UpdateCar(Car car);
        public void InsertCar(Car carToInsert);
        public IEnumerable<Car> SearchCar(string searchString);
        public void DeleteCar(Car car);
    }




}
