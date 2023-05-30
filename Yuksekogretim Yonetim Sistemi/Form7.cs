using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yuksekogretim_Yonetim_Sistemi.Models;


namespace Yuksekogretim_Yonetim_Sistemi
{
    public partial class Form7 : Form
    {
        YYS_DBEntities db = new YYS_DBEntities();
        private int EmployeeCatagoryId = 0;
        private string EmployeeTitle = "";
        private int VerifyCode = 0;
        private int EmployeeNo = 0;
        private string EmployeeFirstPassword = "";
        public Form7()
        {
            InitializeComponent();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == "Profesör Doktor" || comboBox4.Text == "Doçent Doktor" || comboBox4.Text == "Yardımcı Doçent Doktor" ||
                comboBox4.Text == "Doktor Öğretim Görevlisi " || comboBox4.Text == "Araştırma Görevlisi" || comboBox4.Text == "Yarı Zamanlı Araştırma Görevlisi")
            {
                EmployeeCatagoryId = 1;
                EmployeeTitle = comboBox4.Text;
            }
            
            if (comboBox4.Text == "Rektör" || comboBox4.Text == "Dekan" || comboBox4.Text == "Rektör Yardımcısı" ||
                comboBox4.Text == "Bölüm Başkanı ")
            {
                EmployeeCatagoryId = 2;
                EmployeeTitle = comboBox4.Text;
            }

            if (comboBox4.Text == "Memur" || comboBox4.Text == "İşçi" || comboBox4.Text == "Sekreter")
            {
                EmployeeCatagoryId = 3;
                EmployeeTitle = comboBox4.Text;
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = db.universiteler.Select(u => u.uni_ad).ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                universiteler SecilenUni = db.universiteler.Where(x => x.uni_ad == comboBox1.Text).FirstOrDefault();
                foreach (fakulteler item in db.fakulteler.Where(x => x.uni_id == SecilenUni.uni_id).ToList())
                {

                    comboBox2.Items.Add(item.fakulte_ad);
                }
            }
            catch (Exception)
            {

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox3.Items.Clear();
                fakulteler SecilenFakulte = db.fakulteler.Where(x => x.fakulte_ad == comboBox2.Text).FirstOrDefault();
                foreach (bolumler item in db.bolumler.Where(x => x.fakulte_id == SecilenFakulte.fakulte_id).ToList())
                {
                    comboBox3.Items.Add(item.bolum_ad);
                }
            }
            catch (Exception)
            {

            }
        }

        private void createEmployeeNo()
        {
            Random rnd = new Random();
            int studentNo = rnd.Next(10000, 100000);
            EmployeeNo = studentNo;
            createEmployeeFirstPassword();
        }
        private void createEmployeeFirstPassword()
        {
            Random rnd = new Random();
            int studentFirstPassword = rnd.Next(10000, 100000);
            EmployeeFirstPassword = studentFirstPassword.ToString();
        }

        private void createVerifyCode()
        {
            Random rnd = new Random();
            int code = rnd.Next(1000, 10000);
            VerifyCode = code;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMail == null)
            {
                MessageBox.Show("Mail Adresinizi giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                createVerifyCode();
                string code = VerifyCode.ToString();
                VerifyCode = Convert.ToInt32(code);
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("ksmcn2016@hotmail.com", "Ks.31213000");
                smtp.Port = 587;
                smtp.Host = "smtp.outlook.com";
                smtp.EnableSsl = true;
                mail.To.Add(txtMail.Text);
                mail.From = new MailAddress("ksmcn2016@hotmail.com");
                mail.Subject = "Yuksek Ogretim e-Posta doğrulama";
                mail.Body = "Dogrulama kodu: " + code;
                smtp.Send(mail);
                label13.Text = "Dogrulama kodu ePosta\r\nadresinize gonderildi\r\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(txtVerifyCode.Text == VerifyCode.ToString()))
                MessageBox.Show("Doğrulama kodunuz hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                createEmployeeNo();
                registerTheEmployee();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            this.Close();
            frm6.Show();
        }
        private void registerTheEmployee()
        {
            Personel p = new Personel();
            p.KatagoriId = EmployeeCatagoryId;
            p.PersonelNo = EmployeeNo;
            p.PersonelAd = txtName.Text;
            p.PersonelSoyad = txtSurname.Text;
            p.PersonelDogumT = dateTimePicker1.Value.ToString();
            p.PersonelKayitT = dateTimePicker2.Value.ToString();
            p.PersonelTel = txtPhone.Text;
            p.PersonelMail = txtMail.Text;
            p.PersonelAdres = txtAdress.Text;
            p.PersonelUniversite = comboBox1.Text;
            p.PersonelFakulte = comboBox2.Text;
            p.PersonelBolum = comboBox3.Text;
            p.PersonelSifre = EmployeeFirstPassword;
            db.Personel.Add(p);
            try
            {
                db.SaveChanges();
                sendInformationMail(); // The email about registration information
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
        }

        private void sendInformationMail()
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("ksmcn2016@hotmail.com", "Ks.31213000");
            smtp.Port = 587;
            smtp.Host = "smtp.outlook.com";
            smtp.EnableSsl = true;
            mail.To.Add(txtMail.Text);
            mail.From = new MailAddress("ksmcn2016@hotmail.com");
            mail.Subject = "Kayıt bilgilerinizi içeren E-postadır";
            mail.Body =
                "Sayın " + txtName.Text + txtSurname.Text + "\n" +
                "Üniversite: " + comboBox1.Text + "\n" +
                "Fakülte : " + comboBox2.Text + "\n" +
                "Bölüm: " + comboBox3.Text + "\n" +
                "Personel numaranız: " + EmployeeNo + "\n" +
                "Personel giriş şifreniz: " + EmployeeFirstPassword + "\n" +
                "E-Posta adresiniz: " + txtMail.Text + "\n" + "\n" ;
            smtp.Send(mail);
            label13.Text = "Kaydınız başarı ile tamalandı!";
            clearScreen();
        }
        private void clearScreen()
        {
            txtAdress.Text = "";
            txtMail.Text = "";
            txtName.Text = "";
            txtSurname.Text = "";
            txtPhone.Text = "";
            txtVerifyCode.Text = "";
        }
    }
}

