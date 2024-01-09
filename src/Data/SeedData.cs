using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            HotelContext context = app.ApplicationServices
                .CreateScope()
                .ServiceProvider.GetRequiredService<HotelContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.HotelCategory.Any())
            {
                context.HotelCategory.AddRange(
                    new HotelCategory { Name = "Hotel" },
                    new HotelCategory { Name = "Homestay" },
                    new HotelCategory { Name = "GuestHouse" },
                    new HotelCategory { Name = "Resort" }
                );
                context.SaveChanges();
            }
            if (!context.RoomType.Any())
            {
                context.RoomType.AddRange(
                    new RoomType { Type = "Single" },
                    new RoomType { Type = "Double" },
                    new RoomType { Type = "Deluxe" },
                    new RoomType { Type = "Penthouse" }
                );
                context.SaveChanges();
            }

            if (!context.Hotel.Any())
            {
                context.Hotel.AddRange(
                    new Hotel
                    {
                        Name = "Dahill 196 Dalat",
                        CategoryId = 1,
                        Address = "196 Phù Đổng Thiên Vương, Da Lat, Vietnam",
                        Phone = "0937384686",
                        Email = "dahill@gmail.com"
                    },
                    new Hotel
                    {
                        Name = "Mercure Dalat Resort",
                        CategoryId = 4,
                        Address = "03 Nguyễn Du, Thành phố Đà Lạt, Lâm Đồng 670000, Vietnam",
                        Phone = "02633810826",
                        Email = "mercure@gmail.com"
                    },
                    new Hotel
                    {
                        Name = "Lacami Hotel",
                        CategoryId = 1,
                        Address = "36/6 Lê Văn Tám, Phường 10, Thành phố Đà Lạt, Lâm Đồng 66000, Vietnam",
                        Phone = "02633521468",
                        Email = "lacami@gmail.com"
                    },
                    new Hotel
                    {
                        Name = "An Home",
                        CategoryId = 2,
                        Address = "78 Đường Trạng Trình, Phường 9, Thành phố Đà Lạt, Lâm Đồng 66000, Vietnam",
                        Phone = "02632226868",
                        Email = "anhome@gmail.com"
                    },
                    new Hotel
                    {
                        Name = "Hoa Ly",
                        CategoryId = 3,
                        Address = "14 Phó Đức Chính, Phường 9, Thành phố Đà Lạt, Lâm Đồng, Vietnam",
                        Phone = "0942668579",
                        Email = "hoaly@gmail.com"
                    },
                    new Hotel
                    {
                        Name = "Nhà Nghỉ Ngàn Hoa",
                        CategoryId = 3,
                        Address = "09 Tương Phố, Phường 9, Thành phố Đà Lạt, Lâm Đồng 670000, Vietnam",
                        Phone = "02632231088",
                        Email = "nganhoa@gmail.com"
                    },
                    new Hotel
                    {
                        Name = "Mai Xanh Home",
                        CategoryId = 2,
                        Address = "12/1 Đ. Nguyễn Đình Chiểu, Phường 9, Thành phố Đà Lạt, Lâm Đồng, Vietnam",
                        Phone = "0911583504",
                        Email = "maixanh@gmail.com"
                    },
                    new Hotel
                    {
                        Name = "TIGON DALAT HOSTEL",
                        CategoryId = 2,
                        Address = "13 Đường Khởi Nghĩa Bắc Sơn, Phường 10, Thành phố Đà Lạt, Lâm Đồng, Vietnam",
                        Phone = "098749600",
                        Email = "tigon@gmail.com"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
