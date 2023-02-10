using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAppToys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ToyController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Toy.List() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Toy.Get(c => c.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Toy.Delete(objFromDb);
            _unitOfWork.Commit();
            return Json(new { success = true, message = "Deleted Successfully" });
        }

    }

}

