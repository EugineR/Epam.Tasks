using Epam.SCS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.LogicContracts
{
    public interface IPhotoLogic
    {
        bool AddPhotoToUser(int ID, Photo photo);        

        Photo GetSmallPhoto(int ID);
        Photo GetNormalPhoto(int ID);
    }
}
