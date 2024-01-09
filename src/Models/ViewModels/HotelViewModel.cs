using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;

namespace BookingApp.Models.ViewModels
{
    public class HotelViewModel
    {
        public Hotel? Hotel { get; set; }

        // public string ImgData { get; set; }
        public string ToUrl()
        {
            return "data:image;base64," + Convert.ToBase64String(Hotel.Image);
        }

        // string imreBase64Data = Convert.ToBase64String(h=>h.ImgUrl);
        // string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
        // //Passing image data in viewbag to view
        // ViewBag.ImageDatas = images;
    }
}
