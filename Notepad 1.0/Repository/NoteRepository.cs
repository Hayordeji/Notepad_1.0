using Notepad_1._0.Data;
using Notepad_1._0.Interfaces;
using Notepad_1._0.Models;
using System.Linq;

namespace Notepad_1._0.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;

        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateNote(Note note)
        {
            _context.Add(note);
            return Save();
        }

        public bool DeleteNote(Note note)
        {
            _context.Remove(note);
            return Save();
        }

        public Note GetNoteById(int id)
        {
            return _context.Notes.Where(n => n.Id == id).FirstOrDefault();
            
        }

        public IEnumerable<Note> GetNotes()
        {
           return _context.Notes.OrderBy(x => x.Id);
        }

        public bool NoteExists(int id)
        {
            return _context.Notes.Any(n => n.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool UpdateNote(Note note)
        {
            _context.Update(note);
            return Save();
        }
    }
}
