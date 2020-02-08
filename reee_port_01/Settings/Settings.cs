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

        string spreadsheetID;
        string sheetRange;

        public bool AlwaysOnTop { get; set; }
        public bool GenarateGoogleSheet { get; set; }
        public string SpreadsheetID { get => spreadsheetID; set => spreadsheetID = GetSpreadsheetID(value); }
        public string SheetRange { get => GetSheetRange(sheetRange); set => sheetRange = value; }
        public string NoteTypesString { get; set; }
        public string[] NoteTypesArr { get => NoteTypesString.Split(',').ToArray(); }

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
            if (string.IsNullOrWhiteSpace(sheetRange) || !sheetRange.Contains("!") || !sheetRange.Contains(":"))
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
