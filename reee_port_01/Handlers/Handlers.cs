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

     public class NoteDataXml
    {
        
        string xmlreportpath = Environment.CurrentDirectory + "//ree_" + DateTime.Now.ToString("yyyy-mm-dd-HH_mm_ss") + ".xml";

        public string XmlReportPath { get => xmlreportpath; }

        public void CreateXmlReport()
        {
            XDocument xmlreport =
                new XDocument(
                    new XElement("Report")
                    );
            xmlreport.Save(xmlreportpath);

        }

        public void WriteToXmlReport(Note note, NoteDataXml xmldocumnet)
        {
            XDocument xmlreport = XDocument.Load(xmldocumnet.XmlReportPath);
            XElement rootelement = new XElement("Note");

            rootelement.Add(new XElement("Time", note.NoteRecordtime));
            rootelement.Add(new XElement("Type", note.NoteType));
            rootelement.Add(new XElement("Content", note.NoteContent));

            xmlreport.Element("Report").Add(rootelement);
            xmlreport.Save(xmldocumnet.XmlReportPath);
        }

    }

    
}
