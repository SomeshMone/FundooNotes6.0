using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryLayer.Services
{
    public class LabelRepository:ILabelRepository
    {
        private readonly FundooContext _fundooContext;
        public LabelRepository(FundooContext fundooContext)
        {
            _fundooContext = fundooContext;
        }

        public bool AddLabel(long userid,long noteid,string labelName)
        {
            var note = _fundooContext.UserNotes.Where(x => x.UserId == userid && x.NoteId == noteid).FirstOrDefault();
            if(note == null)
            {
                return false;
            }
            else
            {
                LabelEntity lb=new LabelEntity ();
                lb.UserId = userid;
                lb.NoteId = noteid;
                lb.LabelName = labelName;
                _fundooContext.Add(lb);
                _fundooContext.SaveChanges();
                return true;
            }
        }
        public LabelEntity UpdateLable(long userId, long labelId, string labelname)
        {
            var label = _fundooContext.Label.Where(x => x.UserId == userId && x.LabelId == labelId).FirstOrDefault();
            if (label != null)
            {
                label.LabelName = labelname;
                _fundooContext.Entry(label).State = EntityState.Modified;
                _fundooContext.SaveChanges();
                return label;
            }
            return null;

        }

        public IEnumerable<LabelEntity> GetAlllabels(long userid)
        {
            var label = _fundooContext.Label.Where(x => x.UserId == userid).ToList();
            if (label != null)
            {
                return label;
            }
            else
            {
                return null;
            }
        }

        public LabelEntity DeleteLabel(long userId, long labelId)
        {
            var label = _fundooContext.Label.Where(x => x.UserId == userId && x.LabelId == labelId).FirstOrDefault();
            if (label != null)
            {
                _fundooContext.Remove(label);
                _fundooContext.SaveChanges();
                return label;
            }
            else
            {
                return null;
            }

        }
    }
}
