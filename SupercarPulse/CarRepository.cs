using Dapper;
using SupercarPulse.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SupercarPulse
{
    public class CarRepository : ICarRepository
    {
        private readonly IDbConnection _conn;

        public CarRepository(IDbConnection conn)
        {
            _conn = conn;     
        }

        public IEnumerable<Car> GetAllCars()
        {
            return _conn.Query<Car>("Select * From cars");
        }

        public Car GetCar(int id)
        {
            
             return _conn.QuerySingle<Car>("SELECT * FROM Cars WHERE id = @id", new { id = id });
            
        }


        public void InsertCar(Car carToInsert)
        {
            _conn.Execute("INSERT INTO CARS (CARLOGO, MAKE, MODEL, ZERO_TO_SIXTY, ENGINE_TYPE, HORSEPOWER, ENGINE_LITER, IMAGESRC, ENGINEIMG, PRICE, ID) VALUES (@CarLogo, @make, @model, @zero_to_sixty, @engine_type, @horsepower, @engine_liter, @ImageSrc, @EngineImg, @price, @id);",
                new { carlogo = carToInsert.CarLogo, make = carToInsert.Make, model = carToInsert.Model, zero_to_sixty = carToInsert.Zero_To_Sixty, engine_type = carToInsert.Engine_Type, horsepower = carToInsert.Horsepower, engine_liter = carToInsert.Engine_Liter, imagesrc = carToInsert.ImageSrc, engineimg = carToInsert.EngineImg, price = carToInsert.Price, id = carToInsert.Id});
        }

        public IEnumerable<Car> SearchCar(string searchString)
        {
            return _conn.Query<Car>("SELECT * FROM CARS WHERE MAKE LIKE @searchString OR MODEL LIKE @searchString;",
                new { searchString = "%" + searchString + "%" });
        }

        public void UpdateCar(Car car)
        {
            _conn.Execute("UPDATE cars SET CarLogo = @CarLogo, make = @make, model = @model, zero_to_sixty = @zero_to_sixty, engine_type = @engine_type, horsepower = @horsepower, engine_liter = @engine_liter, ImageSrc = @ImageSrc, EngineImg = @EngineImg, price = @price WHERE id = @id",
            new { carlogo = car.CarLogo, make = car.Make, model = car.Model, zero_to_sixty = car.Zero_To_Sixty, engine_type = car.Engine_Type, horsepower = car.Horsepower, engine_liter = car.Engine_Liter, imageSrc = car.ImageSrc,engineimg = car.EngineImg, price = car.Price, id = car.Id });
        }
        public void DeleteCar(Car car)
        {
            _conn.Execute("DELETE FROM CARS WHERE id = @id;", new { id = car.Id });
        }

    }
}
