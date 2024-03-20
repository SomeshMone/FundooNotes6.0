using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace FundooNoteApplication6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBusiness _business;
        public CollaboratorController(ICollaboratorBusiness business)
        {
            _business = business;
        }

        [Authorize]
        [HttpPost]

        public IActionResult AddCollaborator(long noteId, string collaboratorEmail)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

            var result = _business.AddCollaborator(userid, noteId, collaboratorEmail);
            if (result)
            {
                return Ok(new { Success = true, Message = "Collaborator added" });
            }
            else
            {
                return BadRequest(new { Success = false, Message = "Ccollaborator not added" });
            }
        }


        [Authorize]
        [HttpDelete("DelteCollaborator")]
        public IActionResult DeleteCollaborator(long noteId, long collaboratorId)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result = _business.DeleteCollaborator(userid, noteId, collaboratorId);
            if (result != null)
            {
                return Ok(new ResponseModel<Collaborator>() { IsSuccess = true, Message = "Collaborator deleted ", Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<Collaborator>() { IsSuccess = false, Message = "Collaborator not deleted", Data = null });
            }
        }


        [Authorize]
        [HttpGet("GetAllCollaborators")]
        public IActionResult GetCollaborators()
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result = _business.GetCollaborators(userid);
            if (result != null)
            {
                return Ok(new ResponseModel<IEnumerable<Collaborator>>() { IsSuccess = true, Message = "Collaborator found", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<IEnumerable<Collaborator>>() { IsSuccess = false, Message = "Collaborator not found", Data = null });
            }

        }

        [Authorize]
        [HttpGet("GetCollaboratorsbyNotedid")]
        public IActionResult GetCollaboratorsByNoteId(long noteId)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result = _business.GetCollaboratorsByNoteId(userid, noteId);
            if (result != null)
            {
                return Ok(new ResponseModel<IEnumerable<Collaborator>>() { IsSuccess = true, Message = "Collaborator found ", Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<IEnumerable<Collaborator>>() { IsSuccess = false, Message = "Collaborator not Not found", Data = null });
            }

        }

    }
}
