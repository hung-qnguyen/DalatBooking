using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;

namespace BookingApp.Repository.IRepository
{
    public interface IRoomTypeRepository: IRepository<RoomType>
    {
        void Update(RoomType obj);
        void Save();
    }
}