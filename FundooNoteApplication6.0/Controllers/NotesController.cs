using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace FundooNoteApplication6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBusiness _business;
        public NotesController(INotesBusiness business)
        {
            _business = business;
        }

        [Authorize]
        [HttpPost("CreateNotes")]
        public IActionResult Create([FromForm] NotesModel model)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            _business.CreateNote(model, userid);
            return Ok(model);

        }

        [HttpGet("GetAllNotes")]
        public IActionResult GetNotes()
        {
            var notes = _business.GetAllNotes();
            if (notes != null)
            {
                return Ok(new { success = true, message = "All Notes Fetched", Data = notes });
            }
            else
            {
                return BadRequest(new { success = false, message = "Error Occured" });
            }
        }

        [HttpPut("UpdateNotes")]
        public IActionResult UpdateNotes(long Userid,long Noteid, [FromForm] NotesModel model)
        {
            var notes=_business.UpdateNote(Userid, Noteid, model);
            if (notes)
            {
                return Ok(new { success = true, message = "Note Updated SuccessFully", data = true });
            }
            else
            {
                return BadRequest(new { success = false, message = "Note not updated" });
            }
        }

        [HttpDelete("DeleteNote")]
        public IActionResult DeleteNote(long Userid,long Noteid) 
        {
            var res=_business.DeleteNote(Userid,Noteid);
            if (res)
            {
                return Ok(new { success = true, message = "Note Deleted Successfully" });

            }
            else
            {
                return BadRequest(new { success = false, message = "Note not deleted or User//Note found" });
            }
        }

        [Authorize]
        [HttpPost("TogglePin")]
        public IActionResult TogglePin(long noteid)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var pin=_business.TogglePin(userid,noteid);
            if (pin != null)
            {
                return Ok(new { success = true, message = "TogglePinned Successsfully",Data=pin });
            }
            else
            {
                return BadRequest(new { success = false, message = "TogglePin not Successful/Something went wrong",Data=pin });
            }
        }

        [Authorize]
        [HttpPost("ToggleArchive")]
        public IActionResult ToggleArchive(long noteid)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var archive = _business.ToggleArchive(userid, noteid);
            if(archive != null)
            {
                return Ok(new { success = true, message = "ToggleArchive Successsfully", Data = archive });
            }
            else
            {
                return Ok(new { success = false, message = "ToggleArchive not Successful/Something went wrong", Data = archive });
            }

        }

        [Authorize]
        [HttpPost("ToggleTrash")]

        public IActionResult ToggleTrash(long noteid)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var trash= _business.ToggleTrash(userid, noteid);
            if(trash != null)
            {
                return Ok(new { success = true, message = "ToggleTrashed Successsfully", Data = trash });
            }
            else
            {
                return BadRequest(new {success=false,message="Toggled not successful",Data=trash});
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AddColour")]
        public IActionResult AddColor(long noteid,string color)
        {
            long userid= long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var note=_business.AddColor(userid,noteid,color);
            if (note != null)
            {
                return Ok(new { success = "true", message = "Color Added Successfully", Data = note });
            }
            else
            {
                return BadRequest(new { success = false, message = "Color not Added", Data = note });
            }
        }

        [Authorize]
        [HttpGet("NoteById")]
        public IActionResult GetNoteById(long noteid)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var note=_business.GetNoteById(userid,noteid);
            if (note != null)
            {
                return Ok(new { success = true, message = "Note Fetched Successfully", Data = note });
            }
            else
            {
                return BadRequest(new { success = false, message = "Note not fetched", Data = note });
            }
        }

        [HttpGet("GetNotesByUserId")]
        public IActionResult GetNotesByUserId(long userid)
        {
            var notes= _business.GetNotesByUserId(userid);
            if (notes != null)
            {
                return Ok(new { success = true, message = "User notes are", Data = notes });
            }
            else
            {
                return BadRequest(new { succedd = false, message = "User notes are not found", Data = notes });
            }
        }




    }
}
