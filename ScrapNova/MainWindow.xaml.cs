using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

            //Match truc = Regex.Match(html, @"<li[^A-Za-z0-9_]?class=""menu-entree item"">[^A-Za-z0-9_]*<li[^A-Za-z0-9_]?class=""menu-entree item"">"); 


            //CORRIGER LA PREMIERE ERREURXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            //string  htmlcut= Regex.Replace(html, @"<li[^A-Za-z0-9_]?class=""menu-entree item"">[^A-Za-z0-9_]*<li[^A-Za-z0-9_]?class=""menu-entree item"">", @"<li class=""menu-entree item"">");

            //167---3279
            // Match truc = Regex.Match(html, @"<div class=""hentry"" id=""contenu"">.*</div><div style=""clear:both""></div>[^A-Za-z0-9_]*</li>[^A-Za-z0-9_]*</ul>[^A-Za-z0-9_]*</div>[^A-Za-z0-9_]*</div>",RegexOptions.Singleline);
            //TRIMERXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            //Match truc = Regex.Match(html, @"<div[^A-Za-z0-9_]?class=""hentry""[^A-Za-z0-9_]?id=""contenu"">.*</div><div[^A-Za-z0-9_]?style=""clear:both""></div>[^A-Za-z0-9_]*</li>[^A-Za-z0-9_]*</ul>[^A-Za-z0-9_]*</div>[^A-Za-z0-9_]*</div>", RegexOptions.Singleline);
            //string htmlcut = truc.Value;
            //texttest.Text = htmlcut;
            // XElement x = XElement.Parse(@"<!DOCTYPE html PUBLIC  + ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">"+truc.Value);
            //var container = from elem in x.Elements()
            //                let attr = elem.Attribute("class")
            //                where attr.Equals("rub2157")
            //                select (string)elem;
            //string text="";
            //foreach(string s in container) { text += s; }

            var htmldoc = new HtmlDocument();
            //htmldoc.LoadHtml(new WebClient().DownloadString("http://www.nova-cinema.org/seances"));
            WebClient wb = new WebClient();
            htmldoc.Load(wb.OpenRead("http://www.nova-cinema.org/seances"), Encoding.UTF8);
            
            var root = htmldoc.DocumentNode;
            var mars = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("liste agenda rub2157")).Single();
            foreach(HtmlNode x in mars.Descendants().Where(n=>n.GetAttributeValue("class","").Equals("item") ) )
            {
                string temp = String.Empty;
                Evenement Ev = new Evenement() { Lieu = "Nova" };

                HtmlNode y = x.Descendants("span").Where(n => n.GetAttributeValue("class", "").Equals("date")).Single();
                string[] st = y.InnerText.Split(' ');
                
                for(int i = 0; i < st.Length-1; i++) { temp += (st[i]); };
                Ev.Date = temp.Trim().clean();
                Ev.Heure = st[st.Length - 1].Trim().clean();
               

                y = x.Descendants("span").Where(n => n.GetAttributeValue("class", "").Equals("titre")).Single();
                temp = (y.InnerText).Substring(1);
                Ev.Titre =temp.clean();
                y = x.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("descriptif court")).Single();
                Ev.Description = y.InnerText.clean();
                evenements.Add(Ev);
            }
           // texttest.Text = mars.InnerHtml;
            thegrid.ItemsSource = evenements;
        }
    }
}
