using Car_Service.DAL.Entities;
using System;
using System.Collections.Generic;

namespace Car_Service.DAL.Interfaces
{
    public interface IImageManager : IDisposable
    {
        void Create(Image item);
        List<Image> Get(int id);
    }
}
