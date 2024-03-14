using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBusiness
    {
        public bool AddLabel(long userid, long noteid, string labelName);
        public LabelEntity UpdateLable(long userId, long labelId, string labelname);
        public IEnumerable<LabelEntity> GetAlllabels(long userid);
        public LabelEntity DeleteLabel(long userId, long labelId);
    }
}
