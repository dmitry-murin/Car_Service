using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Service.BLL.DTO
{
    public class ReservationViewDTO
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string NameWorker { get; set; }
        public string Purpose { get; set; }
        public string BreakdownDetails { get; set; }
        public string DesiredDiagnosis { get; set; }
        public List<string> Image { get; set; }
    }
}
