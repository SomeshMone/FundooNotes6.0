using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NotesBusiness:INotesBusiness
    {
        private readonly INotesRepository _repository;
        public NotesBusiness(INotesRepository repository)
        { 
            this._repository = repository;
        }
        public string CreateNote(NotesModel request, long userid)
        {
            return _repository.CreateNote(request, userid);

        }

        public IEnumerable<NotesEntity> GetAllNotes()
        {
            return _repository.GetAllNotes();

        }
        public bool UpdateNote(long userid, long noteid, NotesModel model)
        {
            return _repository.UpdateNote(userid, noteid, model);

        }
        public bool DeleteNote(long userid, long noteid)
        {
            return _repository.DeleteNote(userid, noteid);
        }

        public NotesEntity TogglePin(long userid, long noteid)
        {
            return _repository.TogglePin(userid, noteid);   
        }

        public NotesEntity ToggleArchive(long userid, long noteid)
        {
            return _repository.ToggleArchive(userid, noteid);
        }
        public NotesEntity ToggleTrash(long userid, long noteid)
        {
            return _repository.ToggleTrash(userid, noteid);
        }
        public NotesEntity AddColor(long userid, long noteid, string color)
        {
            return _repository.AddColor(userid, noteid, color);

        }
        public NotesEntity GetNoteById(long userid, long noteid)
        {
            return _repository.GetNoteById(userid, noteid);
        }
        public IEnumerable<NotesEntity> GetNotesByUserId(long userid)
        {
            return _repository.GetNotesByUserId(userid);
        }
    }
}
