using Epam.SCS.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.SCS.Entities;
using Epam.SCS.DalContracts;

namespace Epam.SCS.Logic
{
    public class PhotoLogic : IPhotoLogic
    {
        private IPhotoDao photoDao;
        public PhotoLogic()
        {
            photoDao = DaoProvider.PhotoDao;
        }
        public bool AddPhotoToUser(int ID, Photo photo)
        {
            return photoDao.AddUsersPhoto(ID, photo);
        }

        public Photo GetSmallPhoto(int ID)
        {
            return photoDao.GetSmallPhoto(ID);
        }

        public Photo GetNormalPhoto(int ID)
        {
            return photoDao.GetNormalPhoto(ID);
        }
    }
}
