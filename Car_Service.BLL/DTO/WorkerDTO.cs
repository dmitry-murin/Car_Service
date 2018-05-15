using System.ComponentModel.DataAnnotations;

namespace Car_Service.BLL.DTO
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        [MaxLength(15)]
        [Required]
        public string Name { get; set; }
        [MaxLength(30)]
        [Required]
        public string SurName { get; set; }
        [Phone]
        public string Telephone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
