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
    public class LabelBusiness:ILabelBusiness
    {
        private readonly ILabelRepository _repo;
        public LabelBusiness(ILabelRepository repo)
        {
            _repo = repo;
        }

        public bool AddLabel(long userid, long noteid, string labelName)
        {
            return _repo.AddLabel(userid, noteid, labelName);
        }
        public LabelEntity UpdateLable(long userId, long labelId, string labelname)
        {
            return _repo.UpdateLable(userId, labelId, labelname);
        }
        public IEnumerable<LabelEntity> GetAlllabels(long userid)
        {
            return _repo.GetAlllabels(userid);
        }
        public LabelEntity DeleteLabel(long userId, long labelId)
        {
            return _repo.DeleteLabel(userId, labelId);
        }
    }
}
