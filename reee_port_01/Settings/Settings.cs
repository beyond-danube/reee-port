using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reee_port_01
{
    public class Settings
    {
        string settingsFile;

        string spreadsheetID;
        string xmlReportPath;

        public string SpreadsheetID { get => spreadsheetID; set => spreadsheetID = value; }
        public string XmlReportPath { get => xmlReportPath; set => xmlReportPath = value; }

        public Settings(string spreadsheetID, string xmlReportPath)
        {
            this.spreadsheetID = GetSpreadsheetID(spreadsheetID);
            this.xmlReportPath = xmlReportPath;
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

        public static void SettingsReader()
        {

        }

    }
}
