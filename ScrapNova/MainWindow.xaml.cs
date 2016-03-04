using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace ScrapNova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Evenement> evenements = new List<Evenement>();

            //evenements.Add(new Evenement() { Date = "25-01", Heure = "20h", Titre = "super-film", Description = "un très super film" });
            //evenements.Add(new Evenement() { Date = "26-01", Heure = "20h", Titre = "film naze", Description = "ne venez pas" });

            XElement x = XElement.Load("http://www.nova-cinema.org/seances");
            
            thegrid.ItemsSource = evenements;
        }
    }
}
