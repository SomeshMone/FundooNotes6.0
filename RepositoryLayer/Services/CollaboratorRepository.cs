using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CollaboratorRepository:ICollaboratorRepository
    {
        private readonly FundooContext _fundoo;
        public CollaboratorRepository(FundooContext fundoo)
        {
            this._fundoo = fundoo;
        }
        public bool AddCollaborator(long userId, long noteId, string collaboratorEmail)
        {
            var notes = _fundoo.UserNotes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
            if (notes != null)
            {

                Collaborator collaborator = new Collaborator();
                collaborator.UserId = userId;
                collaborator.NoteId = noteId;
                collaborator.CollaboratorsEmail = collaboratorEmail;
                _fundoo.Add(collaborator);
                _fundoo.SaveChanges();

                return true;

            }
            else
            {
                return false;
            }

        }

        public Collaborator DeleteCollaborator(long userId, long noteId, long collaboratorId)
        {
            var collab = _fundoo.collaborators.Where(x => x.UserId == userId && x.NoteId == noteId && x.CollaboratorsId == collaboratorId).FirstOrDefault();
            if (collab != null)
            {
                _fundoo.collaborators.Remove(collab);
                _fundoo.SaveChanges();
                return collab;
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<Collaborator> GetCollaborators(long userId)
        {
            var res = _fundoo.collaborators.Where(x => x.UserId == userId).ToList();
            if (res != null)
            {
                return res;
            }
            return null;
        }

        public IEnumerable<Collaborator> GetCollaboratorsByNoteId(long userId, long noteId)
        {
            var collab = _fundoo.collaborators.Where(x => x.UserId == userId && x.NoteId == noteId).ToList();
            if (collab != null)
            {
                return collab;
            }
            else
            {
                return null;
            }
        }
    }
}
