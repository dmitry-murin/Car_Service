using Car_Service.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Service.BLL.Services
{
    public class Clock : IClock
    {
        public DateTime CurentUtcDateTime()
        {
            return DateTime.Now.ToUniversalTime();
        }
    }
}
