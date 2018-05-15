using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Service.DAL.Entities
{
    public class ConfirmReservation
    {
        [Key]
        [ForeignKey("Reservation")]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsConfirm { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
