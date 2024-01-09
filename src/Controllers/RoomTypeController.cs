using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NToastNotify;
using BookingApp.Data;
using BookingApp.Repository.IRepository;
using BookingApp.Validations;
using Microsoft.AspNetCore.Authorization;
using BookingApp.Utilities;

namespace BookingApp.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class RoomTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification _toastNotification;

        public RoomTypeController(IUnitOfWork unitOfWork, IToastNotification toastNotification)
        {
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            List<RoomType> roomTypes = _unitOfWork.RoomType.GetAll().ToList();
            return View(roomTypes);
        }

        public IActionResult Create()
        {
            // List<RoomType> roomTypes = _unitOfWork.RoomType.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomType obj)
        {
            RoomTypeValidator validator = new();
            ValidationResult result = validator.Validate(obj);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.RoomType.Add(obj);
                _unitOfWork.Save();
                _toastNotification.AddSuccessToastMessage("Success!");

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            RoomType? roomType = _unitOfWork.RoomType.Get(u => u.Id == id);
            //RoomType? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //RoomType? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }

        [HttpPost]
        public IActionResult Edit(RoomType obj)
        {
            RoomTypeValidator validator = new();
            ValidationResult result = validator.Validate(obj);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.RoomType.Update(obj);
                _unitOfWork.Save();
                _toastNotification.AddSuccessToastMessage("Updated Successfully!");
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || _unitOfWork == null)
            {
                return NotFound();
            }

            var roomType = _unitOfWork.RoomType.Get(u => u.Id == id);
            // var roomType = await _unitOfWork.RoomType.FirstOrDefaultAsync(m => m.Id == id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            RoomType? obj = _unitOfWork.RoomType.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.RoomType.Remove(obj);
            _unitOfWork.Save();
            _toastNotification.AddSuccessToastMessage("Deleted Successfully!");
            return RedirectToAction("Index");
        }
    }
}
