using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyDoctorsModel
{
    public class CommentsRepository : BaseRepository
    {
        public void Add(int doctorId, string author, string comment)
        {
            Add(new Comment()
            {
                Author = author,
                Content = comment,
                DoctorId = doctorId,
                Date = DateTime.Now,
            });
        } 

        public void Add(Comment newComment)
        {
            Entities.Comments.Add(newComment);
        }

        public IEnumerable<Comment> GetAll()
        {
            return Entities.Comments;
        }
    }
}
