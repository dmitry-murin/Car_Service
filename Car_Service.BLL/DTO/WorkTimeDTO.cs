using System;
using System.ComponentModel.DataAnnotations;

namespace Car_Service.BLL.DTO
{
    public class WorkTimeDTO
    {
        public int UserId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }
}
