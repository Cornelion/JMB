using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
         
           // XElement x = XElement.Load("http://www.nova-cinema.org/seances");

            WebRequest request = WebRequest.Create("http://www.nova-cinema.org/seances");
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }
            string first = @"class=""menu-entree item"">";
            string second = "Ouloulou";
            html = html.Replace(first, second);
                                
            //XElement x = XElement.Parse(html);
            //var container = from elem in x.Elements()
            //                let attr = elem.Attribute("class")
            //                where attr.Equals("rub2157")
            //                select (string)elem;
            //string text="";
            //foreach(string s in container) { text += s; }

            texttest.Text = html;
            thegrid.ItemsSource = evenements;
        }
    }
}
