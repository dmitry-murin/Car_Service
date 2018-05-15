using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Car_Service.BLL.DTO
{
    public class ReservationDTO
    {
        public int WorkerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required]
        public bool IsEmergency { get; set; }
        [Required]
        public string Purpose { get; set; }
        [Required]
        public string BreakdownDetails { get; set; }
        [Required]
        public string DesiredDiagnosis { get; set; }
        [Required]
        public string Captcha { get; set; }
        public List<ImageDTO> File { get; set; }
        public class ImageDTO
        {
            public byte[] ImageBytes { get; set; }
            public string Extension { get; set; }
        }
    }
}
