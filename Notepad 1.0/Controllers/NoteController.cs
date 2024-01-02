using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Notepad_1._0.Data;
using Notepad_1._0.Interfaces;
using Notepad_1._0.Models;

namespace Notepad_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        private readonly ApplicationDbContext _context;

        public NoteController(INoteRepository noteRepository, ApplicationDbContext context)
        {
            _noteRepository = noteRepository;
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        public IActionResult CreateNote([FromBody] Note noteToCreate)
        {
            //Check if the form is null
            if (noteToCreate == null)
            {
                return BadRequest();
            }


            //check if model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //create note
            noteToCreate = new Note
            {
                Title = noteToCreate.Title,
                Description = noteToCreate.Description
            };

            _noteRepository.CreateNote(noteToCreate);
            return Ok("Successfully Created");
        }

        [HttpGet]
        [ProducesResponseType(400)]
        public IActionResult GetNotes()
        {
            var notes = _noteRepository.GetNotes();
            return Ok(notes);
        }

        [HttpGet("{noteId}")]
        [ProducesResponseType(400)]
        public IActionResult GetNoteById(int noteId)
        {
            if (!_noteRepository.NoteExists(noteId))
            {
                ModelState.AddModelError("", "Note not found");
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var note = _noteRepository.GetNoteById(noteId);
            return Ok(note);
        }

        [HttpPut("{noteId}")]
        [ProducesResponseType(400)]
        public IActionResult UpdateNote(int noteId, [FromBody] Note newNote)
        {
            //CHECK IF NOTE EXIST
            if (!_noteRepository.NoteExists(noteId))
            {
                return NotFound();
            }

            //CHECK IF id OF OLD NOTE IS THE SAME id OF NEW NOTE
            if (noteId != newNote.Id)
            {
                return BadRequest();
            }

            //CHECK IF THE NEW NOTE IS EMPTY
            if (newNote == null)
            {
                return BadRequest();
            }

            //CHECK IF MODEL STATE IS VALID
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_noteRepository.UpdateNote(newNote))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500,ModelState);
            }

            return Ok("Successfully Updated");
        }

        [HttpDelete("{noteId}")]
        [ProducesResponseType(400)]
        public IActionResult DeleteNote(int noteId)
        {
            //Check if note exists
            if (!_noteRepository.NoteExists(noteId))
            {
                return NotFound();
            }

            var noteToDelete = _noteRepository.GetNoteById(noteId);

            if (!_noteRepository.DeleteNote(noteToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Deleted");
        }

        
    }
}
