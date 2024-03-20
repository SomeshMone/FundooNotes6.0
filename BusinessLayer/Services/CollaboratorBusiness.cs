using BusinessLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollaboratorBusiness:ICollaboratorBusiness
    {
        private readonly ICollaboratorRepository _repos;
        public CollaboratorBusiness(ICollaboratorRepository repos)
        {
            _repos = repos;
        }
        public bool AddCollaborator(long userId, long noteId, string collaboratorEmail)
        {
            return _repos.AddCollaborator(userId, noteId, collaboratorEmail);
        }
        public Collaborator DeleteCollaborator(long userId, long noteId, long collaboratorId)
        {
            return _repos.DeleteCollaborator(userId,noteId,collaboratorId);
        }
        public IEnumerable<Collaborator> GetCollaborators(long userId)
        {
            return _repos.GetCollaborators(userId);
        }
        public IEnumerable<Collaborator> GetCollaboratorsByNoteId(long userId, long noteId)
        {
            return _repos.GetCollaboratorsByNoteId(userId, noteId);
        }

    }
}
