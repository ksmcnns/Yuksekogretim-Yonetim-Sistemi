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
    public partial class Form3 : System.Windows.Forms.Form
    {
        YYS_DBEntities db = new YYS_DBEntities();

        string Username = "";
        string UserSurname = "";
        string UserUniversity = "";
        string UserFaculty = "";
        string UserDepartment = "";
        string UserNo = "";
        string UserImage = "";
        public Form3(string userName, string userSurname, string userUniversity, string userFaculty, string userDepartment, string userNo, string userImage)
        {
            InitializeComponent();
            Username = userName;
            UserSurname = userSurname;
            UserUniversity = userUniversity;
            UserFaculty = userFaculty;
            UserDepartment = userDepartment;
            UserNo = userNo;
            UserImage = userImage;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 frm1 = new Form1();
            frm1.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            lblAd.Text = Username;
            lblSoyad.Text = UserSurname;
            lblUni.Text = UserUniversity;
            lblFak.Text = UserFaculty;
            lblBol.Text = UserDepartment;
            lblNo.Text = "Öğrenci No: " + UserNo;
            pictureBox1.Image = Image.FromFile(UserImage);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            List<Notlar> grades = db.Notlar.Where(g => g.OgrID.ToString() == UserNo).ToList();
            dataGridView1.DataSource = grades;
            setStudentCondition();
            dataGridView1.Columns["NotID"].Visible = false;
            dataGridView1.Columns["OgrID"].Visible = false;
            dataGridView1.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();
        }
        private void setStudentCondition()
        {
            List<Notlar> notlar = db.Notlar.Where(n => n.OgrID.ToString() == UserNo).ToList();

            foreach (var not in notlar)
            {
                decimal vizeOrtalama = (decimal)((not.Vize1 + not.Vize2) / 2);
                decimal finalOrtalama = (decimal)((not.Final1 + not.Final2) / 2);

                decimal ortalama = vizeOrtalama * 0.2m + finalOrtalama * 0.3m;
                string durum = ortalama >= 50 ? "Geçti" : "Kaldı";

                not.Ortalama = ortalama;
                if(ortalama > 50)
                    not.Durum = "Geçti";
                else
                    not.Durum = "Kaldı";
            }
            db.SaveChanges();
        }
    }
}

//try
//{
//   // dataGridView1.DataSource = db.NOTLISTESI();
//}
//catch (Exception ex)
//{
//    // İç hata mesajını görüntüleme veya kaydetme
//    Console.WriteLine(ex.InnerException.Message);
//    // veya
//    // Loglama işlemleri
//}



//// Kullanıcı veritabanında bulundu
//// İlgili işlemleri gerçekleştir
//Username = user.OgrAdi;
//UserSurname = user.OgrSoyAdi;
//UserNo = user.OgrNo.ToString();
//UserUniversity = user.OgrUniversite;
//UserFaculty = user.OgrFakulte;
//UserDepartment = user.OgrBolum;
//UserImage = user.OgrFoto;
