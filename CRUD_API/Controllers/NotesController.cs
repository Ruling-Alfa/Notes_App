using CRUD_API.Business;
using CRUD_API.Business.Interfaces;
using CRUD_API.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }
        // GET: api/<NotesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var loggedInUserId = GetLoggedInUserId();
            var noteModelList = await _noteService.GetUserNotes(loggedInUserId);
            if (noteModelList is null)
            {
                noteModelList = new List<NoteModel>();
            }
            return Ok(noteModelList);
        }

        // GET api/<NotesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Request Data");
            }
            var loggedInUserId = GetLoggedInUserId();
            var noteModel = await _noteService.GetNote(id, loggedInUserId);
            if (noteModel is null)
            {
                return NotFound("Note not found");
            }
            return Ok(noteModel);
        }

        // POST api/<NotesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NoteModel noteModel)
        {
            if (noteModel is null)
            {
                return BadRequest("Invalid Request Data");
            }
            int loggedInUserId = GetLoggedInUserId();
            noteModel.CreatedByUser = loggedInUserId;
            var newNote = await _noteService.CreateNote(noteModel);
            if(newNote is null || newNote.Id <= 0)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return NoContent();
        }

        // PUT api/<NotesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] NoteModel noteModel)
        {

            if (noteModel is null)
            {
                return BadRequest("Invalid Request Data");
            }

            int loggedInUserId = GetLoggedInUserId();
            noteModel.CreatedByUser = loggedInUserId;
            var updaedNote = await _noteService.UpdateNote(noteModel);

            if (updaedNote is null || updaedNote.Id <= 0)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return Ok(updaedNote);
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int loggedInUserId = GetLoggedInUserId();
            await _noteService.DeleteNote(id, loggedInUserId);
            return NoContent();
        }

        private int GetLoggedInUserId()
        {
            return Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
        }
    }
}
