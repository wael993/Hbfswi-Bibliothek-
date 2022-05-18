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
using System.Windows.Shapes;

namespace HBSFWI_Bibliotek
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {    
        
        
        Admin admin = new Admin();
        Mitarbeiter mitarbeiter = new Mitarbeiter();
        Student student = new Student();
        public Login()
        {
            InitializeComponent();
        }




        private void LOGIN()
        {
              student.vonlogin.Text = TB_Username.Text;
            //  Mitarbeiter.vonlogin.Text = TB_Username.Text;
            // admin.vonlogin.Text = TB_Username.Text;
            if (TB_Username.Text == "" || TB_Password.Password == "")
            {
                MessageBox.Show("Please Enter Username and Password");
                TB_Username.Text = "";
                TB_Password.Password = "";
            }

            else
            {
                try
                {
                    using (HBSFWI_BibleothekEntities db = new HBSFWI_BibleothekEntities())    //Verbindung zur Datenbank wird aufgebaut und  neuen DBContext Angelegt. 
                    {
                        var list = db.Benutzer.Where(x => x.UserName == TB_Username.Text && x.Passwort == TB_Password.Password).ToList();// Zugriff auf ("UserName","Passwort")Spalten in "Benutzer" Tabell. 
                        foreach (var b in list)
                        {
                            if (b.Rolle == 1)  //Inhalt der Spalte "Rolle" wird Vergleicht (Wenn 1 => login als Admin)
                            {
                                admin.Show();
                                this.Close();
                                return;
                            }
                            else if (b.Rolle == 2) //Inhalt der Spalte "Rolle" wird Vergleicht (Wenn 2 =>login als Mitarbeiter)
                            {
                                mitarbeiter.Show();
                                this.Close();
                                return;
                            }
                            else if (b.Rolle == 3)   //Inhalt der Spalte "Rolle" wird Vergleicht (Wenn 3 =>login als Student)
                            {
                                student.Show();
                                this.Close();
                                return;
                            }
                            else if (b.Rolle == 21 || b.Rolle == 31) //(Wenn 21 oder 31 => Gesperrt Konto)
                            {
                                MessageBox.Show("Ihre konto ist gesperrt !!" + "\n" + " Bitte kontaktieren Sie der Admin");
                                TB_Username.Text = "";
                                TB_Password.Password = "";
                                return;
                            }
                        }
                        MessageBox.Show("Benutzername oder Passwort falsch!");
                        TB_Username.Text = "";
                        TB_Password.Password = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message");
                }
            }
        }
        private void BTN_Clear_Click(object sender, RoutedEventArgs e) //wenn User auf Button Clear Clickt
        {
            TB_Username.Text = "";
            TB_Password.Password = "";
        }
        private void BTN_LOGIN_Click(object sender, RoutedEventArgs e)//wenn User auf Button login Clickt
        {
            LOGIN();
        }
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)     //wenn User Enter auf Tastatur tippt  ==>Login
            {
                LOGIN();
            }
            else if (e.Key == Key.Delete) //wenn User Delete auf Tastatur tippt  ==> Username und Passwot von Textbox löschen
            {
                TB_Username.Text = "";
                TB_Password.Password = "";
            }
        }
    }
}
   
