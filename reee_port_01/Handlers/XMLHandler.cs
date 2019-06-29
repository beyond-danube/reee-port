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
        string xmlreportpath;

        public string XmlReportPath { get => xmlreportpath; set => xmlreportpath = value; }

        public void CreateXmlReport()
        {
            XDocument xmlreport =
                new XDocument(
                    new XElement("Report")
                    );
            xmlreport.Save(xmlreportpath);

        }

        public void WriteToXmlReport(Note note, XMLHandler xmldocumnet)
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
