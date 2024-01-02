using Notepad_1._0.Models;

namespace Notepad_1._0.Interfaces
{
    public interface INoteRepository
    {
        public bool CreateNote(Note note);
        public IEnumerable<Note>GetNotes();
        public Note GetNoteById(int id);
        public bool NoteExists(int id);
        public bool UpdateNote(Note note);
        public bool DeleteNote(Note note);

        public bool Save();
    }
}
