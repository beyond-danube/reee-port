using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;


namespace reee_port_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NoteDataXml sessionree;

        public MainWindow()
        {

            InitializeComponent();
            
        }

        private void Application_Startup(object sender, EventArgs e)
        {

            //temporaty - populate combobox with data from txt
            string[] NoteTypeComboBox = File.ReadAllLines("NoteType.txt");

            NoteType.ItemsSource = NoteTypeComboBox;
            NoteType.SelectedItem = "Bug";


            NoteField.Focus();


            NoteDataXml ree = new NoteDataXml();
            ree.CreateXmlReport();

            sessionree = ree;

        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NoteFiled_KeyDown(Object sender, KeyEventArgs e)
        {
 
            if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {

                Note note = new Note(NoteType.Text, NoteField.Text);
                sessionree.WriteToXmlReport(note, sessionree);            

                NoteField.Clear();

            }       

        }

        private void NoteType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
