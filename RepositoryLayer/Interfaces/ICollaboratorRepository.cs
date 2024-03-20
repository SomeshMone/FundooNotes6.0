﻿using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICollaboratorRepository
    {
        public bool AddCollaborator(long userId, long noteId, string collaboratorEmail);
        public Collaborator DeleteCollaborator(long userId, long noteId, long collaboratorId);

        public IEnumerable<Collaborator> GetCollaborators(long userId);
        public IEnumerable<Collaborator> GetCollaboratorsByNoteId(long userId, long noteId);
    }
}
