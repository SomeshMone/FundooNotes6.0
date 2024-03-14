using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteApplication6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness _business;
        public LabelController(ILabelBusiness business)
        {
            _business = business;
        }

        [Authorize]
        [HttpPost("Addlabel")]
        public IActionResult Addlabel(long noteid,string labelName)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var res = _business.AddLabel(userid, noteid, labelName);
            if (res != null)
            {
                return Ok(new { success = true, message = "labelAdded", Data = res });

            }
            else
            {
                return BadRequest(new { success = false, message = "label not added" });
            }
        }
        [Authorize]
        [HttpPut("Updatelabel")]
        public IActionResult Updatelabel(long labelid,string labelName)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var res = _business.UpdateLable(userid, labelid, labelName);
            if (res!=null)
            {
                return Ok(new { success = true, message = "label is updated",Data=res});

            }
            else
            {
                return BadRequest(new { success = false,message = "label not updated" });
            }

        }
        [Authorize]
        [HttpGet("GetAllLabels")]
        public IActionResult GetAlllabels()
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var labels = _business.GetAlllabels(userid);
            if (labels != null)
            {
                return Ok(new { success = true, message = "all labels are",Data=labels });

            }
            else
            {
                return BadRequest(new { success = false, message = "something went wrong" });
            }

        }
        [Authorize]
        [HttpDelete("Deletelabel")]
        public IActionResult DeleteLabel(long labelid)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var res = _business.DeleteLabel(userid, labelid);
            if (res != null)
            {
                return Ok(new { success = true, message = "label is deleted", Data = res });
            }
            else
            {
                return BadRequest(new { success = false, message = "label not deleted", Data = res });
            }
        }
    }
}
