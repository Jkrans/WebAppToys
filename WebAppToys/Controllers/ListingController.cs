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
    public class ListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ListingController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Listing.List().Where(listing => listing.ExpirationDate > DateTime.Now) });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Listing.Get(c => c.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Listing.Delete(objFromDb);
            _unitOfWork.Commit();
            return Json(new { success = true, message = "Deleted Successfully" });
        }

    }

}

