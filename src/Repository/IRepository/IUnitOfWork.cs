using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IHotelRepository Hotel { get; }
        IHotelCategoryRepository HotelCategory { get; }
        IRoomTypeRepository RoomType { get; }
        IRoomRepository Room { get; }

        void Save();
    }
}