using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;

namespace reee_port_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReeeportSettings settings;
  
        private GoogleSheetHandler sheetHandler;

        private string settingsPath = Environment.CurrentDirectory + @"\Resources\reeeportsettings.xml";


        public MainWindow()
        {

            InitializeComponent();
            this.DataContext = settings;
            
        }

        private void Application_Startup(object sender, EventArgs e)
        {

            settings = ReeeportSettings.SettingsReader(settingsPath);
            sheetHandler = new GoogleSheetHandler();
    
            NoteType.ItemsSource = settings.NoteTypesArr;
            NoteType.SelectedIndex = 0;

            MainWindow mw = this;
            mw.Topmost = settings.AlwaysOnTop;
        }


        private void NoteFiled_KeyDown(Object sender, KeyEventArgs e)
        {
 
            if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {

                Note note = new Note(NoteType.Text, NoteField.Text);

                if (settings.GenarateGoogleSheet == true)
                {
                    try
                    {
                        sheetHandler.AppendToSheet(note, settings.SpreadsheetID, settings.SheetRange);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Cannot write note to spreadsheet.\n\nPlease, check following:\n • Google Spreadsheet URL and Sheet Name are correct.\n • Internet connection is fine.", "Cannot Write to Spreadsheet");                     
                    }
                    
                }            

                NoteField.Clear();

            }       

        }

        private void OnExit(object sender, EventArgs e)
        {
            ReeeportSettings.SettingsSaver(settingsPath, settings);
        }

        public void Application_Deactivated(object sender, EventArgs e)
        {
            MainWindow mw = this;
            mw.Topmost = settings.AlwaysOnTop;
        }
    }
}
