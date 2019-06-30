using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace reee_port_01
{

    public class XMLHandler
    { 

        public void CreateXmlReport(string xmlPath)
        {
            XDocument xmlreport =
                new XDocument(
                    new XElement("Report")
                    );
            xmlreport.Save(xmlPath);

        }

        public void WriteToXmlReport(Note note, string xmlPath)
        {
            XDocument xmlreport = XDocument.Load(xmlPath);
            XElement rootelement = new XElement("Note");

            rootelement.Add(new XElement("Time", note.NoteRecordtime));
            rootelement.Add(new XElement("Type", note.NoteType));
            rootelement.Add(new XElement("Content", note.NoteContent));

            xmlreport.Element("Report").Add(rootelement);
            xmlreport.Save(xmlPath);
        }

    }
}
