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
    /// Interaktionslogik für Mitarbeiter.xaml
    /// </summary>
    public partial class Mitarbeiter : Window
    {
        private ICollectionView CollectionView;
         HBSFWI_BibleothekEntities ctx = new HBSFWI_BibleothekEntities();
        public Mitarbeiter()
        {
            InitializeComponent();
            ctx.Buecher.Load();
            CollectionView = CollectionViewSource.GetDefaultView(ctx.Buecher.Local);
            Parents_Grid.DataContext = CollectionView;
            var Student = ctx.Benutzer.Where(x => x.Rolle == 3 || x.Rolle == 31).ToList();
            Student_verwaltung_Mitarbeiter.DataContext = Student;
            var Mitarbeterliste = ctx.Benutzer.Where(x => x.Rolle == 2 || x.Rolle == 21).ToList();
            Rückgabe_verwaltung_Mitarbeiter.DataContext = ctx.ausleihe_Prozess.ToList();
            Auftraege_Mitarbeiter.DataContext = ctx.Auftraege.ToList();
        }


        #region SideBar
        private void HauptWindows_Click(object sender, RoutedEventArgs e)
        {
            Rückgabe_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Auftraege_Mitarbeiter.Visibility = Visibility.Hidden;
            Student_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Bucher_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Child_grid.Visibility = Visibility.Visible;
        }

        private void BucherVerwaltung_Click(object sender, RoutedEventArgs e)
        {
            Rückgabe_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Auftraege_Mitarbeiter.Visibility = Visibility.Hidden;
            Student_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Child_grid.Visibility = Visibility.Hidden;
            Bucher_verwaltung_Mitarbeiter.Visibility = Visibility.Visible;
        }

        private void Student_Click(object sender, RoutedEventArgs e)
        {
            Rückgabe_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Auftraege_Mitarbeiter.Visibility = Visibility.Hidden;
            Student_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Child_grid.Visibility = Visibility.Hidden;
            Student_verwaltung_Mitarbeiter.Visibility = Visibility.Visible;
        }


        private void Buecher_Zurueck_Click(object sender, RoutedEventArgs e)
        {
            Rückgabe_verwaltung_Mitarbeiter.DataContext = ctx.ausleihe_Prozess.ToList();
            
            Auftraege_Mitarbeiter.Visibility = Visibility.Hidden;
            Student_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Child_grid.Visibility = Visibility.Hidden;
            Student_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Rückgabe_verwaltung_Mitarbeiter.Visibility = Visibility.Visible;
        }

       

        private void Btn_Auftraege_Click(object sender, RoutedEventArgs e)
        {
            Auftraege_Mitarbeiter.DataContext = ctx.Auftraege.ToList();

            Student_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Child_grid.Visibility = Visibility.Hidden;
            Student_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Rückgabe_verwaltung_Mitarbeiter.Visibility = Visibility.Hidden;
            Auftraege_Mitarbeiter.Visibility = Visibility.Visible;

        }

        private void Btn_LogOut_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        #endregion


        #region Bucher_verwaltung
               

        private void Btn_BuecherLoeschen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Möchten Sie den Buch wirklich löschen?",
                         "Save file",
                         MessageBoxButton.YesNo,
                         MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int tbISBN = Convert.ToInt32(Hidden_ISBN.Content);
                Buecher n = ctx.Buecher.Where(x => x.ISBN == tbISBN).FirstOrDefault();
                ctx.Buecher.Remove(n);
                ctx.SaveChanges();
                Parents_Grid.DataContext = null;
                Parents_Grid.DataContext = ctx.Buecher.ToList();
            }

        }

        private void Btn_BuecherUpdat_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Änderung wird gespeichert.");
            ctx.SaveChanges();
        }

        private void Btn_BuecherHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            if (TB_new_buch_Titel.Text == "" || TB_New_ISBN.Text == "" || CB_Rechtung.Text == "" || TB_new_Beschreibung.Text == "" || TB_new_Anzahl.Text == "")
            {
                MessageBox.Show("Bitte kontrollieren Sie Ihre angabe.");
            }
            else
            {
                Buecher buecher = new Buecher
                {
                    ISBN = Convert.ToInt32(TB_New_ISBN.Text),
                    Buch_Titel = TB_new_buch_Titel.Text,
                    Rechtung = CB_Rechtung.Text,
                    Anzahl = Convert.ToInt32(TB_new_Anzahl.Text),
                    Beschreibung = TB_new_Beschreibung.Text
                };
                ctx.Buecher.Add(buecher);
                ctx.SaveChanges();
                Parents_Grid.DataContext = null;
                Parents_Grid.DataContext = ctx.Buecher.ToList();

                TB_new_buch_Titel.Text = "";
                TB_New_ISBN.Text = "";
                CB_Rechtung.Text = "";
                TB_new_Beschreibung.Text = "";
                TB_new_Anzahl.Text = "";
                MessageBox.Show("Neu Buch wird hinzugefügt.");
            }
        }


        #endregion


        #region Student Verwaltung
        private void BT_StudentLoeschen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Möchten Sie wirklich den Student löschen?",
                       "Save file",
                       MessageBoxButton.YesNo,
                       MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int Hidde_St_ID = Convert.ToInt32(Hidden_St_ID.Content);
                Benutzer n = ctx.Benutzer.Where(x => x.Benutzer_id == Hidde_St_ID).FirstOrDefault();
                ctx.Benutzer.Remove(n);
                ctx.SaveChanges();
                var list = ctx.Benutzer.Where(x => x.Rolle == 3 || x.Rolle == 31).ToList();
                Student_verwaltung_Mitarbeiter.DataContext = null;
                Student_verwaltung_Mitarbeiter.DataContext = list;
            }
        }


        private void Bt_StudentUpdat_Click(object sender, RoutedEventArgs e)
        {
            ctx.SaveChanges();
            MessageBox.Show("Änderung wird gespeichert.");

        }

        private void Bt_StudentHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            if (TB_StudentVorName.Text == "" || TB_StudentNachName.Text == "" || TB_StudentID.Text == "" ||
               CB_StudentSemester.Text == "" || CB_Studienangang.Text == "" || TB_SdtudentUserName.Text == "" || TB_SdtudentPass.Text == "")
            {
                MessageBox.Show("Bitte kontrollieren Sie Ihre eingabe .");
            }
            else
            {
                string ss = CB_StudentStatus.Text;
                if (ss == "Aktiv")
                {
                    ss = 3.ToString();
                }
                else { ss = 31.ToString(); }

                Benutzer student = new Benutzer
                {
                    Benutzer_id = Convert.ToInt32(TB_StudentID.Text),
                    UserName = TB_SdtudentUserName.Text,
                    Passwort = TB_SdtudentPass.Text,
                    Vorname = TB_StudentVorName.Text,
                    Nachname = TB_StudentNachName.Text,
                    Semester = Convert.ToInt32(CB_StudentSemester.Text),
                    studiengang = CB_Studienangang.Text,
                    Rolle = Convert.ToInt32(ss)
                };
                ctx.Benutzer.Add(student);
                ctx.SaveChanges();
                var Student = ctx.Benutzer.Where(x => x.Rolle == 3 || x.Rolle == 31).ToList();
                Student_verwaltung_Mitarbeiter.DataContext = null;
                Student_verwaltung_Mitarbeiter.DataContext = Student;

                TB_StudentVorName.Text = "";
                TB_StudentNachName.Text = "";
                TB_StudentID.Text = "";
                CB_StudentSemester.Text = "";
                CB_Studienangang.Text = "";
                TB_SdtudentUserName.Text = "";
                TB_SdtudentPass.Text = "";

                MessageBox.Show("Neu Student wird hinzugefügt.");
            }
        }




        #endregion

        #region Rückgabe_verwaltung
         private void Buch_zurueck_Click(object sender, RoutedEventArgs e)
         {
            int ausgelehtete_buecher = Convert.ToInt32(Anzahl_Ausgeleihtete_Bücher.Content);
            if (ausgelehtete_buecher == 0)
            {
                MessageBox.Show("Ihre auslehen Korp ist leer ");
            }
            else
            {


                int USer_id = Convert.ToInt32(Hidden_user_ID.Content);
                int ISBn = Convert.ToInt32(Hidden_ausbuchISBN.Content);


         
                //Anzahl der zurück gegebene buch um 1 erhöhen
                var Bucherlist = ctx.Buecher.ToList();
                Rückgabe_verwaltung_Mitarbeiter.DataContext = Bucherlist;
                var vaa = Bucherlist.Where(x => x.ISBN == ISBn).ToList();
                vaa.ForEach(s => s.Anzahl++);
                ctx.SaveChanges();
                Parents_Grid.DataContext = null;
                Parents_Grid.DataContext = ctx.Buecher.ToList();




                //buch von ausleihe_Prozess löschen
                ausleihe_Prozess n = ctx.ausleihe_Prozess.Where(x => x.ISBN == ISBn && x.Benutzer_id == USer_id).FirstOrDefault();
                ctx.ausleihe_Prozess.Remove(n);
                ctx.SaveChanges();
                Rückgabe_verwaltung_Mitarbeiter.DataContext = null;
                Rückgabe_verwaltung_Mitarbeiter.DataContext = ctx.ausleihe_Prozess.ToList();



                var ausgeleitetebuch = ctx.Auftraege.ToList();

                var vc = ausgeleitetebuch.Where(x => x.Auftrag_id == n.Prozess_id).ToList();
                vc.ForEach(sc => sc.Auftrag_status = "Abgeschlossen");
                ctx.SaveChanges();
                Auftraege_Mitarbeiter.DataContext = null;
                Auftraege_Mitarbeiter.DataContext = ctx.Auftraege.ToList();
            }
        }


        #endregion



        #region Aufträge


        private void bearbeitet_Click(object sender, RoutedEventArgs e)
        {
            string abhol = Hdidden_status.Content.ToString();
            int AuftragID = (int)Hidden_Auftrag_NR.Content;

            var Bucherlist = ctx.Auftraege.ToList();
            Auftraege_Mitarbeiter.DataContext = Bucherlist;
            var v = Bucherlist.Where(x => x.Auftrag_status == abhol && x.Auftrag_id == AuftragID).ToList();
            v.ForEach(s => s.Auftrag_status = "Abhol bereit");
            ctx.SaveChanges();
            Auftraege_Mitarbeiter.DataContext = null;
            Auftraege_Mitarbeiter.DataContext = Bucherlist;


            var ausgeleitetebuch = ctx.ausleihe_Prozess.ToList();

            var vc = ausgeleitetebuch.Where(x => x.Prozess_id == AuftragID).ToList();
            vc.ForEach(sc => sc.Prozess_status = "Abhol bereit");
            ctx.SaveChanges();
            Auftraege_Mitarbeiter.DataContext = null;
            Auftraege_Mitarbeiter.DataContext = Bucherlist;
        }

        private void drueck_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();

            Window currentMainWindow = Application.Current.MainWindow;

            Application.Current.MainWindow = this;

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                Application.Current.MainWindow = currentMainWindow; 
                dlg.PrintVisual(DG_Auftraege, "Auftrag_Nr");
            }
        }




        private void In_Bearbeitung_Click(object sender, RoutedEventArgs e)
        {
            Auftraege_Mitarbeiter.DataContext = ctx.Auftraege.Where(x => x.Auftrag_status == "In Bearbeitung").ToList();
        }

        private void Abhol_bereit_Click(object sender, RoutedEventArgs e)
        {
            Auftraege_Mitarbeiter.DataContext = ctx.Auftraege.Where(x => x.Auftrag_status == "Abhol bereit").ToList();
        }

        private void Abgeschlossen_Click(object sender, RoutedEventArgs e)
        {
            Auftraege_Mitarbeiter.DataContext = ctx.Auftraege.Where(x => x.Auftrag_status == "Abgeschlossen").ToList();
        }

        private void Auftraege_Mitarbeiter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)     //wenn User Escape auf Tastatur tippt  ==> Felter löschen
            {
                Auftraege_Mitarbeiter.DataContext = ctx.Auftraege.ToList(); ;
            }


        }   
          
        
        
        
        #endregion

    }    
}
