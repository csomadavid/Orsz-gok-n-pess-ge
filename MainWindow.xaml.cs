using System.IO;
using System.Windows;

namespace adatbovites
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnKilepes_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            if (long.Parse(tbFovarosLakossaga.Text) > (long.Parse(tbNepesseg.Text)))
            {
                lbUzenet.Content = "A főváros lakossága nem lehet több a népességnél!";
                tbNepesseg.Text = tbFovarosLakossaga.Text;
                return;
            }
            File.AppendAllText("ujadat.txt", $"{tbOrszagnev.Text};{tbTerulet.Text};{tbNepesseg.Text};{tbFovaros.Text};{tbFovarosLakossaga.Text}\n");
            lbUzenet.Content = "A mentés sikeres!";
        }
    }
}
