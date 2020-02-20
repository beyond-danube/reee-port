using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace reee_port_01
{
    public class Note
    {
        DateTime noterecordtime;

        public string NoteType { get; set; }
        public string NoteContent { get; set; }
        public string NoteRecordtime { get => noterecordtime.ToString("yyyy/MM/dd HH:mm"); }

        public Note()
        {
            noterecordtime = DateTime.Now;
        }

        public Note(string notetype, string notecontent)
            : this()
        {
            NoteType = notetype;
            NoteContent = notecontent;
        }
        
    }
    
}
