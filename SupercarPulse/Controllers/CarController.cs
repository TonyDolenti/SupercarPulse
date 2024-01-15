using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupercarPulse.Models;

namespace SupercarPulse.Controllers
{
    public class CarController : Controller
    {

        private readonly ICarRepository repo;

        public CarController(ICarRepository repo)
        {
            this.repo = repo;
        }


        public IActionResult Index(string searchString)
        {
            var cars = repo.GetAllCars();

            if (!string.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(x => x.Make.Contains(searchString));
            }
            return View(cars);
        }

        [HttpGet]
        public IActionResult ViewCar(int id)
        {
            var car = repo.GetCar(id);
            return View(car);
        }

        [HttpPost]
        public IActionResult UpdateCarToDatabase(Car carToUpdate, IFormFile ImageSrcFile, IFormFile EngineImgFile, IFormFile CarLogoFile)
        {

            Car existingCar = repo.GetCar(carToUpdate.Id);

            if (existingCar == null)
            {
                return View("CarNotFound");
            }

            if (ImageSrcFile != null && ImageSrcFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    ImageSrcFile.CopyTo(memoryStream);
                    carToUpdate.ImageSrc = memoryStream.ToArray();
                }
            }
            else
            {
                carToUpdate.ImageSrc = existingCar.ImageSrc;
            }

            if (EngineImgFile != null && EngineImgFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    EngineImgFile.CopyTo(memoryStream);
                    carToUpdate.EngineImg = memoryStream.ToArray();
                }
            }
            else
            {
                carToUpdate.EngineImg = existingCar.EngineImg;
            }

            if (CarLogoFile != null && CarLogoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    CarLogoFile.CopyTo(memoryStream);
                    carToUpdate.CarLogo = memoryStream.ToArray();
                }
            }
            else
            {
                carToUpdate.CarLogo = existingCar.CarLogo;
            }

            repo.UpdateCar(carToUpdate);

            return RedirectToAction("ViewCar", new { id = carToUpdate.Id });
        }

        public IActionResult UpdateCar(int id)
        {
            Car car = repo.GetCar(id);
            if (car == null)
            {
                return View("CarNotFound");
            }
            return View(car);
        }

        public IActionResult InsertCar(Car carToInsert)
        {
            return View(carToInsert);
        }

        [HttpPost]
        public IActionResult InsertCarToDatabase(Car carToInsert, IFormFile ImageSrcFile, IFormFile EngineImgFile, IFormFile CarLogoFile)
        {

            if (ImageSrcFile != null && ImageSrcFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    ImageSrcFile.CopyTo(memoryStream);
                    carToInsert.ImageSrc = memoryStream.ToArray();
                }
            }

            if (EngineImgFile != null && EngineImgFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    EngineImgFile.CopyTo(memoryStream);
                    carToInsert.EngineImg = memoryStream.ToArray();
                }
            }

            if (CarLogoFile != null && CarLogoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    CarLogoFile.CopyTo(memoryStream);
                    carToInsert.CarLogo = memoryStream.ToArray();
                }
            }
            repo.InsertCar(carToInsert);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCar(Car car)
        {
            repo.DeleteCar(car);
            return RedirectToAction("Index");
        }

        public IActionResult Search(string searchString)
        {
            var searchResults = repo.SearchCar(searchString);

            return View(searchResults);
        }
    }
}
