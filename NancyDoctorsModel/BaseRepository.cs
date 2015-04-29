using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyDoctorsModel
{
    public class BaseRepository
    {
        protected static Entities Entities = new Entities();

        public BaseRepository()
        {

        }

        public void Save()
        {
            Entities.SaveChanges();
        }
    }
}
