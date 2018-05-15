using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Service.DAL.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
