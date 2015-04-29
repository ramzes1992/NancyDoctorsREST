using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyDoctorsModel
{
    public class DoctorsRepository : BaseRepository
    {
        public void Add(Doctor newDoctor)
        {
            if (newDoctor == null)
                return;

            Entities.Doctors.Add(newDoctor);
        }

        public IEnumerable<Doctor> GetAll()
        {
            return Entities.Doctors;
        }

        public IEnumerable<TimeSlot> GetTimeSlots(Doctor doctor)
        {
            return Entities.TimeSlots.Where(t => t.DoctorId.Equals(doctor.Id));
        }

        public IEnumerable<Comment> GetComments(Doctor doctor)
        {
            return Entities.Comments.Where(c => c.DoctorId.Equals(doctor.Id));
        }
    }
}
