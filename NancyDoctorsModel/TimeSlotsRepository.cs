using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyDoctorsModel
{
    public class TimeSlotsRepository : BaseRepository
    {
        public void Add(int doctorId, DateTime date, string visitor = "")
        {
            Add(new TimeSlot()
            {
                DoctorId = doctorId,  
                Date = date,
                Visitor = visitor,
            });
        }

        public void Add(TimeSlot newTimeSlot)
        {
            Entities.TimeSlots.Add(newTimeSlot);
        }

        public IEnumerable<TimeSlot> GetAll()
        {
            return Entities.TimeSlots;
        }
    }
}
