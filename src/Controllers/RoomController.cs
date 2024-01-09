using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using BookingApp.Repository.IRepository;
using BookingApp.Validations;
using BookingApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BookingApp.Utilities;

namespace BookingApp.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class RoomController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification _toastNotification;

        public RoomController(IUnitOfWork unitOfWork, IToastNotification toastNotification)
        {
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            List<Room> roomList = _unitOfWork.Room.GetAll(includeProperties:"RoomType,Hotel").ToList();
            return View(roomList);
        }

        public IActionResult Upsert(int? id)
        {
            RoomVM roomVM =
                new()
                {
                    HotelList = _unitOfWork.Hotel
                        .GetAll()
                        .Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
                    RoomTypeList = _unitOfWork.RoomType
                        .GetAll()
                        .Select(u => new SelectListItem { Text = u.Type, Value = u.Id.ToString() }),
                    Room = new Room()
                };
            if (id == null || id == 0)
            {
                //create
                return View(roomVM);
            }
            else
            {
                //update
                roomVM.Room = _unitOfWork.Room.Get(u => u.Id == id);
                return View(roomVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(RoomVM roomVM)
        {
            RoomValidator validator = new();
            ValidationResult result = validator.Validate(roomVM.Room);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
            }
            if (ModelState.IsValid)
            {
                if (roomVM.Room.Id == 0)
                {
                    _unitOfWork.Room.Add(roomVM.Room);
                    _toastNotification.AddSuccessToastMessage("Insert Success!");

                }
                else
                {
                    _unitOfWork.Room.Update(roomVM.Room);
                    _toastNotification.AddSuccessToastMessage("Update Success!");
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                roomVM.HotelList = _unitOfWork.Hotel
                    .GetAll()
                    .Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
                roomVM.RoomTypeList = _unitOfWork.RoomType
                    .GetAll()
                    .Select(u => new SelectListItem { Text = u.Type, Value = u.Id.ToString() });
                return View(roomVM);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Room> objRoomList = _unitOfWork.Room.GetAll(includeProperties: "RoomType,Hotel").ToList();
            return Json(new { data = objRoomList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var roomToBeDeleted = _unitOfWork.Room.Get(u => u.Id == id);
            if (roomToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Room.Remove(roomToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

        // public IActionResult Delete(int? id)
        // {
        //     if (id == null || _unitOfWork == null)
        //     {
        //         return NotFound();
        //     }

        //     var roomType = _unitOfWork.Room.Get(u => u.Id == id);
        //     // var roomType = await _unitOfWork.Room.FirstOrDefaultAsync(m => m.Id == id);
        //     if (roomType == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(roomType);
        // }

        // [HttpPost, ActionName("Delete")]
        // public IActionResult DeletePOST(int? id)
        // {
        //     Room? obj = _unitOfWork.Room.Get(u=>u.Id==id);
        //     if (obj == null)
        //     {
        //         return NotFound();
        //     }
        //     _unitOfWork.Room.Remove(obj);
        //     _unitOfWork.Save();
        //     _toastNotification.AddSuccessToastMessage("Deleted Successfully!");
        //     return RedirectToAction("Index");
        // }
    }
}
