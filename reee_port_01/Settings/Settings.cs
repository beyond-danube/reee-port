using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace reee_port_01
{
    [Serializable]
    public class ReeeportSettings
    {

        bool genarateGoogleSheet;
        bool generateXml;

        List<string> noteTypes = new List<string>();

        string spreadsheetID;
        string sheetRange;

        string xmlReportPath;

        public string XmlFolder { get; set; }
        public string XmlSubFolder { get; set; }
        public string XmlDocName { get; set; }


        public string SpreadsheetID { get => spreadsheetID; set => spreadsheetID = GetSpreadsheetID(value); }
        public string XmlReportPath { get => xmlReportPath = Path.Combine(XmlFolder, XmlSubFolder, XmlDocName + ".xml"); set => xmlReportPath = Path.Combine(XmlFolder, XmlSubFolder, XmlDocName + ".xml"); }
        public string SheetRange { get => sheetRange; set => sheetRange = value; }
        public bool GenarateGoogleSheet { get => genarateGoogleSheet; set => genarateGoogleSheet = value; }
        public bool GenerateXml { get => generateXml; set => generateXml = value; }
        public List<string> NoteTypes { get => noteTypes; set => noteTypes = value; }

        public ReeeportSettings()
        {

        }


        public static string GetSpreadsheetID(string spreadsheetID)
        {
            string urlStart = "https://docs.google.com/spreadsheets/d/";
            string urlEnd = "/edit#gid=0";

            if (spreadsheetID.Contains(urlStart))
            {
                return spreadsheetID.Substring(urlStart.Length, spreadsheetID.Length - urlEnd.Length - urlStart.Length);
            }

            else return spreadsheetID;
        }

        public static string GetSheetRange(string sheetRange)
        {
            if (sheetRange == null || sheetRange == "")
            {
                return "Sheet1!A:C";
            }

            else return sheetRange;
        }

        public static ReeeportSettings SettingsReader(string settingsPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ReeeportSettings));

            ReeeportSettings settings;

            using (Stream reader = new FileStream(settingsPath, FileMode.Open))
            {
                return settings = (ReeeportSettings)serializer.Deserialize(reader);
                
            }
        }

        public static void SettingsSaver(string settingsPath, ReeeportSettings settings)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ReeeportSettings));
            var xml = "";

            using (var sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    serializer.Serialize(writer, settings);
                    xml = sw.ToString();
                }
            }

            XmlDocument settingsfile = new XmlDocument();
            settingsfile.LoadXml(xml);
            settingsfile.Save(settingsPath);
        }

    }
}
