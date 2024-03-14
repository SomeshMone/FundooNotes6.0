using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INotesBusiness
    {
        public string CreateNote(NotesModel request, long userid);
        public IEnumerable<NotesEntity> GetAllNotes();
        public bool UpdateNote(long userid, long noteid, NotesModel model);

        public bool DeleteNote(long userid, long noteid);

        public NotesEntity TogglePin(long userid, long noteid);
        public NotesEntity ToggleArchive(long userid, long noteid);

        public NotesEntity ToggleTrash(long userid, long noteid);

        public NotesEntity AddColor(long userid, long noteid, string color);

        public NotesEntity GetNoteById(long userid, long noteid);

        public IEnumerable<NotesEntity> GetNotesByUserId(long userid);
    }
}
