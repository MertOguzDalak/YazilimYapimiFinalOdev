using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace KelimeBul
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection n = new SqlConnection("Server=.\\SQLEXPRESS;Database=BirKelime;trusted_connection=true;");
        DataSet daset = new DataSet();
        public static double secondsTaken = 0;
        string kelimelistesi;
        int a;
        int b;
        private void rdo_rastgele_CheckedChanged(object sender, EventArgs e)
        {
            Random rastgele = new Random();
            Random rastgele2 = new Random();
            string[] uret = new string[9];
            string sesliharfler = "aeıioöuü";
            string sessizharfler = "bcdfgğhjklmnprsştvyz";
            for (int i = 0; i < 8; i++)
            {
                uret[i] = "";
            }
            for (int x = 0; x < 4; x++)
            {
                
                uret[x] = sesliharfler[rastgele.Next(sesliharfler.Length)].ToString();
            }
            for (int j = 4; j < 8; j++)
            {
                
                uret[j] = sessizharfler[rastgele2.Next(sessizharfler.Length)].ToString();
            }

            txt_bir.Text = uret[0];
            txt_iki.Text = uret[1];
            txt_uc.Text = uret[2];
            txt_dort.Text = uret[3];
            txt_bes.Text = uret[4];
            txt_alti.Text = uret[5];
            txt_yedi.Text = uret[6];
            txt_sekiz.Text = uret[7]; 
            txt_joker.Text = "A-Z";
            txt_bulunankelime.Text = " ";


        }

        private void rdo_kendingir_CheckedChanged(object sender, EventArgs e)
        {
            txt_bir.Text = "";
            txt_iki.Text = "";
            txt_uc.Text = "";
            txt_dort.Text = "";
            txt_bes.Text = "";
            txt_alti.Text = "";
            txt_yedi.Text = "";
            txt_sekiz.Text = "";
            txt_joker.Text = "A-Z";

            txt_bulunankelime.Text = " ";


        }

        private void btn_hesapla_Click(object sender, EventArgs e)
        {

            KelimeBul();
            
            
            
        }

        private void KelimeBul()
        {
            
            
                

                DateTime startTime = DateTime.Now;

                while (true)
                {
                    secondsTaken = (DateTime.Now - startTime).TotalSeconds;

                    SqlCommand comm = new SqlCommand("SELECT F1 FROM Sayfa1$ ORDER BY LEN(F1) DESC", n);

                    n.Open();

                    SqlDataReader dr = comm.ExecuteReader();
                string[] ayrilmisharf = new string[9];
                while (dr.Read())
                    {

                    ayrilmisharf[0] = txt_bir.Text;
                    ayrilmisharf[1] = txt_iki.Text;
                    ayrilmisharf[2] = txt_uc.Text;
                    ayrilmisharf[3] = txt_dort.Text;
                    ayrilmisharf[4] = txt_bes.Text;
                    ayrilmisharf[5] = txt_alti.Text;
                    ayrilmisharf[6] = txt_yedi.Text;
                    ayrilmisharf[7] = txt_sekiz.Text;
                    
                    a = 0;

                    kelimelistesi = Convert.ToString(dr["F1"]);
                        char[] harflereayir = kelimelistesi.ToCharArray();
                        for (int i = 0; i < kelimelistesi.Length; i++)
                        {
                            if (Array.IndexOf(ayrilmisharf, harflereayir[i].ToString()) == -1)
                            {
                                b = i;
                                a++;

                            }
                            else
                            {
                            for (int y = 0; y < 8; y++)
                            {
                                if (ayrilmisharf[y] == harflereayir[i].ToString())
                                {
                                    ayrilmisharf[y] = " ";
                                    break;
                                }
                            }
                            }
                        }
                        if (a == 0)
                        {
                            ayrilmisharf[8] = harflereayir[b].ToString();
                            txt_bulunankelime.Text = kelimelistesi;
                            puanhesapla();
                            break;
                        }
                        else if (a == 1)
                        {
                            ayrilmisharf[8] = harflereayir[b].ToString();
                            txt_joker.Text = ayrilmisharf[8];
                            txt_bulunankelime.Text = kelimelistesi;
                            puanhesapla();
                            break;
                        }

                    }

                    dr.Close();
                    n.Close();
                    secondsTaken = (DateTime.Now - startTime).TotalSeconds;
                    break;
                }
            txt_sure.Text = secondsTaken.ToString();
            
        }
        int kelimepuan = 0;
        int toppuan = 0;
        private void puanhesapla()
        {
            for (int i = 9; i >= 0; i--)
            {
               if (txt_bulunankelime.Text.Length == i)
                {
                    if (i == 9)
                    {
                        kelimepuan = 15;
                    }
                    else if (i == 8)
                    {
                        kelimepuan = 11;
                    }
                    else if (i == 7)
                    {
                        kelimepuan = 9;
                    }
                    else if (i == 6)
                    {
                        kelimepuan = 7;
                    }
                    else if (i == 5)
                    {
                        kelimepuan = 5;
                    }
                    else if (i == 4)
                    {
                        kelimepuan = 4;
                    }
                    else if (i == 3)
                    {
                        kelimepuan = 3;
                    }
                    txt_kelimepuan.Text = kelimepuan.ToString();
                    toppuan += kelimepuan;
                    txt_toplampuan.Text = toppuan.ToString();
                }
            }
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txt_toplampuan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

