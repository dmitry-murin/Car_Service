using Car_Service.DAL.EF;
using Car_Service.DAL.Entities;
using Car_Service.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Car_Service.DAL.Repositories
{
    public class ImageManager : IImageManager
    {
        private ApplicationContext _db;
        public ImageManager(ApplicationContext db)
        {
            _db = db;
        }
        public List<Image> Get(int id)
        {
            return _db.Image.Where(s => s.Reservation.Id == id).ToList();
        }

        public void Create(Image item)
        {
            _db.Image.Add(item);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
