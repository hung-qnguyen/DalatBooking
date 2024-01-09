using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Data;
using BookingApp.Models;
using BookingApp.Repository.IRepository;

namespace BookingApp.Repository
{
    public class HotelRepository: Repository<Hotel>, IHotelRepository
    {
        private readonly HotelContext _context;
        public HotelRepository(HotelContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Hotel obj)
        {
            _context.Hotel.Update(obj);
        }
        
    }
}