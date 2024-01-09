using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Data;
using BookingApp.Models;
using BookingApp.Repository.IRepository;

namespace BookingApp.Repository
{
    public class HotelCategoryRepository: Repository<HotelCategory>, IHotelCategoryRepository
    {
        private readonly HotelContext _context;
        public HotelCategoryRepository(HotelContext context) : base(context)
        {
            _context = context;
        }

        public void Update(HotelCategory obj)
        {
            _context.HotelCategory.Update(obj);
        }
    }
}