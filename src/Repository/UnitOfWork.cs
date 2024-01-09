using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Data;
using BookingApp.Repository.IRepository;

namespace BookingApp.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly HotelContext _context;
        public IRoomTypeRepository RoomType { get; private set; }
        public IRoomRepository Room { get; private set; }

        public IHotelRepository Hotel { get; private set; }
        public IHotelCategoryRepository HotelCategory { get; private set; }

        public UnitOfWork(HotelContext context)
        {
            _context = context;
            Hotel = new HotelRepository(_context);
            HotelCategory = new HotelCategoryRepository(_context);
            RoomType = new RoomTypeRepository(_context);
            Room = new RoomRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}