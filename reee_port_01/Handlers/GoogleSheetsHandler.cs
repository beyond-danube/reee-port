
using System.Collections.Generic;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace reee_port_01
{
    public class GoogleSheetHandler : GoogleHandler
    {
        private ValueRange RequestBody { get; set; }
        private AppendRequest Request { get; set; }

        private SheetsService service;


        public GoogleSheetHandler()
        {
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            RequestBody = new ValueRange();
        }

        public void AppendToSheet(Note note, string spreadsheetID, string sheetRange)
        {

            string spreadsheetId = spreadsheetID;
            string range = sheetRange;

            AppendRequest.ValueInputOptionEnum valueInputOption = AppendRequest.ValueInputOptionEnum.RAW;
            AppendRequest.InsertDataOptionEnum insertDataOption = AppendRequest.InsertDataOptionEnum.INSERTROWS;

            var arr = new string[] { note.NoteType, note.NoteContent, note.Id, note.NoteRecordtime};

            RequestBody.Values = new List<IList<object>> { arr };

            Request = service.Spreadsheets.Values.Append(RequestBody, spreadsheetId, range);
            Request.ValueInputOption = valueInputOption;
            Request.InsertDataOption = insertDataOption;

            Request.Execute();
        }
    }
}
