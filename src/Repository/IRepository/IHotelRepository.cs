using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Models;

namespace BookingApp.Repository.IRepository
{
    public interface IHotelRepository: IRepository<Hotel>
    {
        void Update(Hotel obj);
        
    }
}