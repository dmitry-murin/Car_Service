using Car_Service.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Service.DAL.Interfaces
{
    public interface IConfirmReservation : IDisposable
    {
        ConfirmReservation Get(Guid guid);
        void Create(ConfirmReservation item);
        void Update(ConfirmReservation item);

    }
}
