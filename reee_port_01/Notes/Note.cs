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
        string notetype;
        string notecontent;
        DateTime noterecordtime;

        public string NoteType { get => notetype; set => notetype = value; }
        public string NoteContent { get => notecontent; set => notecontent = value; }
        public string NoteRecordtime { get => noterecordtime.ToString("yyyy-MM-dd-HH_mm"); }

        public Note()
        {
            noterecordtime = DateTime.Now;
        }

        public Note(string notetype, string notecontent)
            : this()
        {
            this.notetype = notetype;
            this.notecontent = notecontent;
        }
        
    }

    
}
