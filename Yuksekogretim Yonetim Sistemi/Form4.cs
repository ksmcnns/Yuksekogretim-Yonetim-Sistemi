﻿using System;
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
    public partial class Form4 : Form
    {
        YYS_DBEntities db = new YYS_DBEntities();

        string Username = "";
        string UserSurname = "";
        string UserUniversity = "";
        string UserFaculty = "";
        string UserDepartment = "";
        string UserNo = "";
        public Form4(string userName, string userSurname, string userUniversity, string userFaculty, string userDepartment, string userNo)
        {
            InitializeComponent();
            Username = userName;
            UserSurname = userSurname;
            UserUniversity = userUniversity;
            UserFaculty = userFaculty;
            UserDepartment = userDepartment;
            UserNo = userNo;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            lblAd.Text = Username;
            lblSoyad.Text = UserSurname;
            lblUni.Text = UserUniversity;
            lblFak.Text = UserFaculty;
            lblBol.Text = UserDepartment;
            lblNo.Text = "Personel No: " + UserNo;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 frm1 = new Form1();
            frm1.Show();
        }
    }
    }

