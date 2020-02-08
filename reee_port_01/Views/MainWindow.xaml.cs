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

        }


        private void NoteFiled_KeyDown(Object sender, KeyEventArgs e)
        {
 
            if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {

                Note note = new Note(NoteType.Text, NoteField.Text);

                if (settings.GenarateGoogleSheet == true)
                {
                    sheetHandler.AppendToSheet(note, settings.SpreadsheetID, settings.SheetRange);
                }            

                NoteField.Clear();

            }       

        }

        private void OnExit(object sender, EventArgs e)
        {
            ReeeportSettings.SettingsSaver(settingsPath, settings);
        }

    }
}
