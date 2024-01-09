using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;
using BookingApp.Data;
using BookingApp.Repository.IRepository;

namespace BookingApp.Repository
{
    public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
    {
        private readonly HotelContext _context;

        public RoomTypeRepository(HotelContext context)
            : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(RoomType obj)
        {
            _context.RoomType.Update(obj);
        }
    }
}
