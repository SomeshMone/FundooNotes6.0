using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System.Net.NetworkInformation;
using System.Text;

namespace FundooNoteApplication6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBusiness _business;
        private readonly FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;
        public NotesController(INotesBusiness business, IDistributedCache distributedCache)
        {
            _business = business;
            distributedCache = distributedCache;
        }

        //[Authorize]
        [HttpPost("CreateNotes")]
        public IActionResult Create([FromForm] NotesModel model)
        {
            //byte[] userbyte = HttpContext.Session.Get("UserId");
            //long userid = BitConverter.ToInt64(userbyte, 0);
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
        public IActionResult UpdateNotes(long Userid, long Noteid, [FromForm] NotesModel model)
        {
            var notes = _business.UpdateNote(Userid, Noteid, model);
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
        public IActionResult DeleteNote(long Userid, long Noteid)
        {
            var res = _business.DeleteNote(Userid, Noteid);
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
            var pin = _business.TogglePin(userid, noteid);
            if (pin != null)
            {
                return Ok(new { success = true, message = "TogglePinned Successsfully", Data = pin });
            }
            else
            {
                return BadRequest(new { success = false, message = "TogglePin not Successful/Something went wrong", Data = pin });
            }
        }

        [Authorize]
        [HttpPost("ToggleArchive")]
        public IActionResult ToggleArchive(long noteid)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var archive = _business.ToggleArchive(userid, noteid);
            if (archive != null)
            {
                return Ok(new { success = true, message = "ToggleArchive Successsfully", Data = archive });
            }
            else
            {
                return Ok(new { success = false, message = "ToggleArchive not Successful/Something went wrong", Data = archive });
            }

        }

        [HttpGet("getNoOfUserNotes")]
        public IActionResult GetNotes(long userid)
        {
            var notes=_business.GetNofNotes(userid);
            if (notes != 0)
            {
                return Ok(new { success = true, message = "No of notes are", data = notes });
            }
            else
            {
                return BadRequest("Notes not found");
            }
        }

        [Authorize]
        [HttpPost("ToggleTrash")]

        public IActionResult ToggleTrash(long noteid)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var trash = _business.ToggleTrash(userid, noteid);
            if (trash != null)
            {
                return Ok(new { success = true, message = "ToggleTrashed Successsfully", Data = trash });
            }
            else
            {
                return BadRequest(new { success = false, message = "Toggled not successful", Data = trash });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AddColour")]
        public IActionResult AddColor(long noteid, string color)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var note = _business.AddColor(userid, noteid, color);
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
            var note = _business.GetNoteById(userid, noteid);
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
            var notes = _business.GetNotesByUserId(userid);
            if (notes != null)
            {
                return Ok(new { success = true, message = "User notes are", Data = notes });
            }
            else
            {
                return BadRequest(new { succedd = false, message = "User notes are not found", Data = notes });
            }
        }
        [HttpGet("NotedByTitle")]
        public IActionResult GetNoteByTitle(long userid,string title, string desc)
        {
            var notes = _business.GetNoteTitle(userid, title,desc);
            if (notes != null)
            {
                return Ok(new { success = true, mesaage = "Note found", Data = notes })
;            }
            else
            {
                return BadRequest(new { success = false, message = "Note not found" });
            }
        }

        [Authorize]
        [HttpPost("AddReminder")]
        public IActionResult AddReminder(long noteid, DateTime reminder)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var note = _business.AddReminder(userid, noteid, reminder);
            if (note != null)
            {
                return Ok(new { success = true, message = "AddReminder added Successfully", Data = note });
            }
            else
            {
                return BadRequest(new { success = false, message = "something worong!", Data = note });
            }

        }

        [Authorize]
        [HttpPost("Addimage")]
        public IActionResult ImageAdd(int noteId, IFormFile imageUrl)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result = _business.AddImage(userid, noteId, imageUrl);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Image addedsucessfully..", Data = result });
            }
            else
            {
                return BadRequest(new { Success = false, Message = "Some thing went wrong..!", Data = result });
            }

        }

        [HttpGet("redis")]

        public async Task<IActionResult> GetAllNotesUsiingRedisCache()
        {
            var cachKey = "NotesList";
            string serializedNotedList;
            var NotesList = new List<NotesEntity>();
            byte[] redisNotesList = await distributedCache.GetAsync(cachKey);
            if (redisNotesList != null)
            {
                serializedNotedList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotedList);

            }
            else
            {
                NotesList = fundooContext.UserNotes.ToList();
                serializedNotedList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotedList);
                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cachKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
        

    }
}
