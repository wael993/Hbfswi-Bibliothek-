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

namespace HBSFWI_Bibliotek
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Admin admin = new Admin();
        Mitarbeiter mitarbeiter = new Mitarbeiter();
        Student student = new Student();




        public MainWindow()
        {
            InitializeComponent();
        }
        private void Login1()
        {
          //  student.vonlogin.Text = TB_Username.Text;
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
                    using (TOnline__BibleothekEntities1 db = new TOnline__BibleothekEntities1())
                    {
                        var list = db.Benutzer.Where(x => x.UserName == TB_Username.Text && x.Passwort == TB_Password.Password).ToList();
                        foreach (var b in list)
                        {
                            if (b.Rolle == 1)
                            {
                                admin.Show();
                                this.Close();
                                return;
                            }
                            else if (b.Rolle == 2)
                            {
                                mitarbeiter.Show();
                                this.Close();
                                return;
                            }
                            else if (b.Rolle == 21 || b.Rolle == 31)
                            {
                                MessageBox.Show("Ihre konto ist gesperrt !!" + "\n" + " Bitte kontaktieren Sie der Admin");
                                TB_Username.Text = "";
                                TB_Password.Password = "";
                                return;
                            }
                            else if (b.Rolle == 3)
                            {
                                student.Show();
                                this.Close();
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
        private void BTN_Clear_Click(object sender, RoutedEventArgs e)
        {
            TB_Username.Text = "";
            TB_Password.Password = "";

        }
        private void BTN_LOGIN_Click(object sender, RoutedEventArgs e)
        {
            Login1();
        }
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login1();
            }
            else if (e.Key == Key.Delete)
            {
                TB_Username.Text = "";
                TB_Password.Password = "";
            }
        }

        private void LogIn(object sender, ExecutedRoutedEventArgs e)
        {
            Login1();
        }

        private void Clear(object sender, ExecutedRoutedEventArgs e)
        {
            TB_Username.Text = "";
            TB_Password.Password = "";
        }
    }
}