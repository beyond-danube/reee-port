using System.Collections.Generic;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace reeeport
{
    public class GoogleHandlerSheet : GoogleHandler
    {
        private ValueRange RequestBody { get; set; }
        private AppendRequest Request { get; set; }

        private SheetsService service;

        public GoogleHandlerSheet()
        {
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            RequestBody = new ValueRange();
        }

        public void AppendToSheet(Note note, string spreadsheetID, string sheetRange, string attachmentsFolderLink)
        {

            string spreadsheetId = spreadsheetID;
            string range = sheetRange;

            AppendRequest.ValueInputOptionEnum valueInputOption = AppendRequest.ValueInputOptionEnum.USERENTERED;
            AppendRequest.InsertDataOptionEnum insertDataOption = AppendRequest.InsertDataOptionEnum.INSERTROWS;

            var arr = new string[] { note.NoteType, note.NoteContent, note.NoteRecordtime, attachmentsFolderLink};

            RequestBody.Values = new List<IList<object>> { arr };

            Request = service.Spreadsheets.Values.Append(RequestBody, spreadsheetId, range);
            Request.ValueInputOption = valueInputOption;
            Request.InsertDataOption = insertDataOption;

            Request.Execute();
        }
    }
}
