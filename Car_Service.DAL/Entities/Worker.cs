using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Service.DAL.Entities
{
    public class Worker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public List<Reservation> Reservation { get; set; }
        public List<WorkTime> WorkTimes { get; set; }
    }
}
