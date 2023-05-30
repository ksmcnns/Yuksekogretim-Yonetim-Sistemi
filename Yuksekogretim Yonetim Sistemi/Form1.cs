using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yuksekogretim_Yonetim_Sistemi.Models;

namespace Yuksekogretim_Yonetim_Sistemi
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        YYS_DBEntities db = new YYS_DBEntities();

        public Form1()
        {
            InitializeComponent();
        }
        private string VerifyCode = "";
        private string FormSelection = "";
        private string Username = "";
        private string UserSurname = "";
        private string UserUniversity = "";
        private string UserFaculty = "";
        private string UserDepartment = "";
        private string UserNo = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if(FormSelection == " ")
            {
                MessageBox.Show("Lutfen giris yapmak istediginiz alana tıklayıp tekrar deneyin!");
            }
            if(FormSelection == "Ogrenci")
            {
                string username = txtNo.Text;
                string password = txtPassword.Text;
                
                var user = db.Ogrenci.FirstOrDefault(p => p.OgrNo.ToString() == username && p.OgrSifre == password);

                if (user != null)
                {
                    // Kullanıcı veritabanında bulundu
                    // İlgili işlemleri gerçekleştir
                    Username = user.OgrAdi;
                    UserSurname = user.OgrSoyAdi;
                    UserNo = user.OgrNo.ToString();
                    UserUniversity = user.OgrUniversite;
                    UserFaculty = user.OgrFakulte;
                    UserDepartment = user.OgrBolum;
                    if(lblVerify.Text == textVerify.Text)
                    {
                        Form3 frm3 = new Form3(Username, UserSurname, UserUniversity, UserFaculty, UserDepartment, UserNo);
                        this.Hide();
                        frm3.Show();
                    }
                    else
                    {
                        // Doğrulama kodu hatası
                        MessageBox.Show("Doğrulama kodunu yanlış girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textVerify.Text = "";
                        createVerifyCode();
                    }
                   
                }
                else
                {
                    // Kullanıcı veritabanında bulunamadı
                    // Gerekli işlemleri yap
                    MessageBox.Show("Hatalı giriş!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (FormSelection == "Ogretim_Gorevlisi")
            {
                string username = txtNo.Text;
                string password = txtPassword.Text;
               
                var user = db.Personel.FirstOrDefault(p => p.PersonelNo.ToString() == username && p.PersonelSifre == password);

                if (user != null)
                {
                    // Kullanıcı veritabanında bulundu
                    // İlgili işlemleri gerçekleştir
                    Username = user.PersonelAd;
                    UserSurname = user.PersonelSoyad;
                    UserNo = user.PersonelNo.ToString();
                    UserUniversity = user.PersonelUniversite;
                    UserFaculty = user.PersonelFakulte;
                    UserDepartment = user.PersonelBolum;
                    if (lblVerify.Text == textVerify.Text)
                    {
                        Form4 frm4 = new Form4(Username, UserSurname, UserUniversity, UserFaculty, UserDepartment, UserNo);
                        this.Hide();
                        frm4.Show();
                    }
                    else
                    {
                        // Doğrulama kodu hatası
                        MessageBox.Show("Doğrulama kodunu yanlış girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textVerify.Text = "";
                        createVerifyCode();
                    }
                }
                else
                {
                    // Kullanıcı veritabanında bulunamadı
                    // Gerekli işlemleri yap
                    MessageBox.Show("Hatalı giriş!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (FormSelection == "Idari_Personel")
            {
                string username = txtNo.Text;
                string password = txtPassword.Text;
                
                var user = db.Personel.FirstOrDefault(p => p.PersonelNo.ToString() == username && p.PersonelSifre == password);

                if (user != null)
                {
                    // Kullanıcı veritabanında bulundu
                    // İlgili işlemleri gerçekleştir
                    Username = user.PersonelAd;
                    UserSurname = user.PersonelSoyad;
                    UserNo = user.PersonelNo.ToString();
                    UserUniversity = user.PersonelUniversite;
                    UserFaculty = user.PersonelFakulte;
                    UserDepartment = user.PersonelBolum;
                    if (lblVerify.Text == textVerify.Text)
                    {
                        Form5 frm5 = new Form5(Username, UserSurname, UserUniversity, UserFaculty, UserDepartment, UserNo);
                        this.Hide();
                        frm5.Show();
                    }
                    else
                    {
                        // Doğrulama kodu hatası
                        MessageBox.Show("Doğrulama kodunu yanlış girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textVerify.Text = "";
                        createVerifyCode();
                    }
                }
                else
                {
                    // Kullanıcı veritabanında bulunamadı
                    // Gerekli işlemleri yap
                    MessageBox.Show("Hatalı giriş!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (FormSelection == "Sekreterlik")
            {
                string username = txtNo.Text;
                string password = txtPassword.Text;
                
                var user = db.Personel.FirstOrDefault(p => p.PersonelNo.ToString() == username && p.PersonelSifre == password);

                if (user != null)
                {
                    // Kullanıcı veritabanında bulundu
                    // İlgili işlemleri gerçekleştir
                    Username = user.PersonelAd;
                    UserSurname = user.PersonelSoyad;
                    UserNo = user.PersonelNo.ToString();
                    UserUniversity = user.PersonelUniversite;
                    UserFaculty = user.PersonelFakulte;
                    UserDepartment = user.PersonelBolum;
                    if (lblVerify.Text == textVerify.Text)
                    {
                        Form6 frm6 = new Form6(Username, UserSurname, UserUniversity, UserFaculty, UserDepartment, UserNo);
                        this.Hide();
                        frm6.Show();
                    }
                    else
                    {
                        // Doğrulama kodu hatası
                        MessageBox.Show("Doğrulama kodunu yanlış girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textVerify.Text = "";
                        createVerifyCode();
                    }
                }
                else
                {
                    // Kullanıcı veritabanında bulunamadı
                    // Gerekli işlemleri yap
                    MessageBox.Show("Hatalı giriş!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }
        private void createVerifyCode()
        {
            Random rnd = new Random();
            int x = rnd.Next(1000, 9999);
            VerifyCode = x.ToString();
            lblVerify.Text = VerifyCode;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            label7.Text = "Ogrenci No";
            FormSelection = "Ogrenci";
            createVerifyCode();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label7.Text = "Personel No";
            FormSelection = "Ogretim_Gorevlisi";
            createVerifyCode();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label7.Text = "Personel No";
            FormSelection = "Idari_Personel";
            createVerifyCode();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label7.Text = "Personel No";
            FormSelection = "Sekreterlik";
            createVerifyCode();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            this.Hide();
            frm2.Show();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Blue;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Blue;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Blue;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Blue;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Blue;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
        }       
    }
    internal class YYS_DBEntitiesConnectionDb
    {
        public YYS_DBEntitiesConnectionDb()
        {
        }
    }
}
