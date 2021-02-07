using System;

namespace reeeport
{
    public class Note
    {
        DateTime noterecordtime;

        public string NoteType { get; set; }
        public string NoteContent { get; set; }
        public string NoteRecordtime { get => noterecordtime.ToString("yyyy/MM/dd HH:mm"); }
        public string Id { get; set; }
        public string[] AttachedFiles { get; set; }

        public Note()
        {
            noterecordtime = DateTime.Now;
        }

        public Note(string notetype, string notecontent)
            : this()
        {
            NoteType = notetype;
            NoteContent = notecontent;
            Id = Guid.NewGuid().ToString();
        }

        public Note(string notetype, string notecontent, string[] attachedFiles)
            : this(notetype, notecontent)
        {
            AttachedFiles = attachedFiles;
        }
    }
    
}
