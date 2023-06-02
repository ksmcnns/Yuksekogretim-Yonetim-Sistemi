using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
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
    public partial class Form2 : System.Windows.Forms.Form
    {
        YYS_DBEntities db = new YYS_DBEntities();
        private int VerifyCode = 0;
        private int StudentNo = 0;
        private string StudentFirstPassword = "";
        private string ImagePath = "";

        public Form2()
        {
            InitializeComponent();
        }
        
        private void Form2_Load(object sender, EventArgs e)
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
        private void createStudentNo()
        {
            Random rnd = new Random();
            int studentNo = rnd.Next(10000, 100000);
            StudentNo = studentNo;
            createStudentFirstPassword();
        }
        private void createStudentFirstPassword()
        {
            Random rnd = new Random();
            int studentFirstPassword = rnd.Next(10000, 100000);
            StudentFirstPassword = studentFirstPassword.ToString();
        }

        private void createVerifyCode()
        {      
            Random rnd = new Random();
            int code = rnd.Next(1000, 10000);
            VerifyCode = code;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtMail == null)
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
                createStudentNo();
                registerTheStudent();
            }            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            this.Close();
            frm1.Show();
        }
        private void registerTheStudent()
        {
            Ogrenci o = new Ogrenci();
            o.OgrNo = StudentNo;
            o.OgrAdi = txtName.Text;
            o.OgrSoyAdi = txtSurname.Text;
            o.OgrDogumT = dateTimePicker1.Value.ToString();
            o.OgrKayitT = dateTimePicker2.Value.ToString();         
            o.OgrTel = txtPhone.Text;
            o.OgrMail = txtMail.Text;
            o.OgrAdres = txtAdress.Text;
            o.OgrUniversite = comboBox1.Text;
            o.OgrFakulte = comboBox2.Text;
            o.OgrBolum = comboBox3.Text;
            o.OgrSifre = StudentFirstPassword;
            o.OgrFoto = ImagePath;
            db.Ogrenci.Add(o);
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
                "Sayın " + txtName.Text +" "+ txtSurname.Text + "\n" +
                "Üniversite: " + comboBox1.Text + "\n" +
                "Fakülte : " + comboBox2.Text + "\n" +
                "Bölüm: " + comboBox3.Text + "\n" +
                "Ogrenci numaranız: " + StudentNo + "\n" +
                "Öğrenci giriş şifreniz: " + StudentFirstPassword + "\n" +
                "E-Posta adresiniz: " + txtMail.Text + "\n" + "\n" +
                "Öğrenim hayatınızda başarılar dileriz!";
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
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Resim Seç";
            ofd.Filter = "Files|*.jpg;*.jpeg;*.png;";
            if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImagePath = ofd.FileName;
                button4.Text = "Fotograf Secildi";
            }
        }
    }
}
