using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.Entities
{
    public class Photo
    {
        public int Id { get; set; }

        public byte[] Data { get; set; }

        public string ContentType { get; set; }

        public Photo(int Id)
        {
            this.Id = Id;
        }

        public Photo(byte[] data, string contentType)
        {
            this.Data = data;
            this.ContentType = contentType;
        }


    }
}
