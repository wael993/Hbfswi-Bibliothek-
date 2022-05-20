using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace HBSFWI_Bibliotek
{
    /// <summary>
    /// Interaktionslogik für Student.xaml
    /// </summary>
    public partial class Student : Window
    {
       private ICollectionView CollectionView;
       private HBSFWI_BibleothekEntities ctx = new HBSFWI_BibleothekEntities();
        public Student()
        {
            ctx.Buecher.Load();
            CollectionView = CollectionViewSource.GetDefaultView(ctx.Buecher.Local);
            InitializeComponent();
            Ausleihen_verwaltung_Student.DataContext = ctx.Buecher.Local;

        }



        #region SideBar
        private void HauptWindows_Click(object sender, RoutedEventArgs e)
        {
            Ausleihen_verwaltung_Student.Visibility = Visibility.Hidden;
            Aktuelle_und_Status.Visibility = Visibility.Hidden;
            Child_grid.Visibility = Visibility.Visible;
        }

        private void Btn_LogOut_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        #endregion



        #region Ausleihen_verwaltung_Student 

        private void Ausleihen_Click(object sender, RoutedEventArgs e)
        {
            Ausleihen_verwaltung_Student.DataContext = ctx.Buecher.Local;
            Aktuelle_und_Status.Visibility = Visibility.Hidden;
            Child_grid.Visibility = Visibility.Hidden;
            Ausleihen_verwaltung_Student.Visibility = Visibility.Visible;
        }

        private void TB_BuchSuche_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = tbx_Buch_suche.Text.ToLower();
            CollectionView.Filter = x => ((Buecher)x).Buch_Titel.ToLower().Contains(filter)
                                          ||
                                           ((Buecher)x).Rechtung.ToLower().Contains(filter)
                                          ||
                                          ((Buecher)x).ISBN.ToString().Contains(filter);
        }
        private void BuchAusleihen_Click(object sender, RoutedEventArgs e)
        {

            //  ein list enthält die daten von Ausgewählte Buch
            int ISBn = (int)Hidden_buchISBN.Content;
            var Buch = ctx.Buecher.Where(x => x.ISBN == ISBn).ToList();
            var Rechtungbuch = Buch[0];

            //ein list enthält die daten von angemeldete Student
            string username = vonlogin.Text;
            var Student = ctx.Benutzer.Where(x => x.UserName == username).ToList();
            var angemeldeteStudent = Student[0];


            int anzahl = (int)Hidden_buchAnzahl.Content - 1;


            if (angemeldeteStudent.studiengang != Rechtungbuch.Rechtung)   // vergleich zwischen Student und Buch rechtung 
            {
                MessageBox.Show("Sie dürfen dieses buch nicht leihen ");
            }


            else
            {
                if (Rechtungbuch.Anzahl == 0) //Anzahl der ausgewelte buch wird überprüft
                {
                    MessageBox.Show("Buch ist zurzeit nicht verfügbar ");

                }
                else
                {
                    int Aktuelle_und_Status = Convert.ToInt32(Anzahl_Ausgeleihtete_Bücher.Content);
                    if (Aktuelle_und_Status >= 3)
                    {
                        MessageBox.Show("Sie haben den Limit überschritt ");
                    }
                    else
                    {
                        Random random = new Random();    //um schrank Nr zu geben
                        int schrank = random.Next(1, 55);

                        ausleihe_Prozess AP = new ausleihe_Prozess   // Ausleihe in ausleihe_Prozess Table eintragen
                        {
                            Prozess_status = "In Bearbeitung",
                            Prozess_datum = DateTime.Now,
                            Benutzer_id = angemeldeteStudent.Benutzer_id,
                            ISBN = (int)Hidden_buchISBN.Content,
                            FachNr = schrank
                        };
                        ctx.ausleihe_Prozess.Add(AP);

                        Auftraege auftraege = new Auftraege   //Ausleihe in Auftraege Table eintragen
                        {
                            Auftrag_status = "In Bearbeitung",
                            Auftrag_datum = DateTime.Now,
                            Benutzer_id = angemeldeteStudent.Benutzer_id,
                            ISBN = (int)Hidden_buchISBN.Content,
                            FachNr = schrank
                        };
                        ctx.Auftraege.Add(auftraege);
                        ctx.SaveChanges();

                                          //Anzahl der Ausgeleihtete Buch in Bücher Tabel um 1 reduzieren
                        var Bucherlist = ctx.Buecher.ToList();
                        Ausleihen_verwaltung_Student.DataContext = Bucherlist;
                        var v = Bucherlist.Where(x => x.ISBN == ISBn).ToList();
                        v.ForEach(s => s.Anzahl = anzahl);
                        ctx.SaveChanges();

                        Ausleihen_verwaltung_Student.DataContext = null;
                        Ausleihen_verwaltung_Student.DataContext = ctx.Buecher.Local;
                        MessageBox.Show($"Das ausgeleihtete Buch ist  \n morgen ab 09:00 Uhr Von Schrank  Nummer {schrank}  bereit zu abholen ");
                    }
                }
            }
        }

        #endregion


        #region Aktuelle und Status
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Aktuelle_und_Status.Visibility = Visibility.Visible;
            Child_grid.Visibility = Visibility.Hidden;
            Ausleihen_verwaltung_Student.Visibility = Visibility.Hidden;




            string username = vonlogin.Text;

            var Student = ctx.Benutzer.Where(x => x.UserName == username).ToList();
            var use = Student[0];
            int USer_id = use.Benutzer_id;

            var ausgeleitete_bücher = ctx.ausleihe_Prozess.Where(x => x.Benutzer_id == USer_id).ToList();
            Aktuelle_und_Status.DataContext = ausgeleitete_bücher;
        }
        //private void Buch_zurueck_Click(object sender, RoutedEventArgs e)
        //{



        //}
        #endregion

    }
}
