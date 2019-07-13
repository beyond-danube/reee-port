using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using reee_port_01;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //Settings set = new Settings();

            //set.GenerateXml = true;
            //set.GenarateGoogleSheet = false;
            //set.SpreadsheetID = "ah";
            //set.XmlReportPath = "oh";
            //set.NoteTypes.Add("note1");
            //set.NoteTypes.Add("note2");


            //XmlSerializer xsSubmit = new XmlSerializer(typeof(Settings));
            //var xml = "";

            //using (var sww = new StringWriter())
            //{
            //    using (XmlWriter writer = XmlWriter.Create(sww))
            //    {
            //        xsSubmit.Serialize(writer, set);
            //        xml = sww.ToString();        
            //    }
            //}


            //    XmlDocument settingsfile = new XmlDocument();
            //    settingsfile.LoadXml(xml);
            //settingsfile.Save("ttt.xml");


            XmlSerializer serializer = new XmlSerializer(typeof(ReeeportSettings));

            // Declare an object variable of the type to be deserialized.
            ReeeportSettings i;

            using (Stream reader = new FileStream("ttt.xml", FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                i = (ReeeportSettings)serializer.Deserialize(reader);
            }

            Console.WriteLine(i.SpreadsheetID);

            Console.ReadLine();

        }

    }
}
