using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazilim_yapimi_islem_kismi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        int toplam_puan = 0;
        int puan = 10;
        int gecicipuan;
        Random rnd = new Random();
        public static double secondsTaken = 0;
        public static int bestSoFar = 0;
        private void Hesaplama()
        {
           
            
                int[] teksayi = new int[6];
                teksayi[0] = Convert.ToInt32(txt_sayi1.Text);
                teksayi[1] = Convert.ToInt32(txt_sayi2.Text);
                teksayi[2] = Convert.ToInt32(txt_sayi3.Text);
                teksayi[3] = Convert.ToInt32(txt_sayi4.Text);
                teksayi[4] = Convert.ToInt32(txt_sayi5.Text);
                teksayi[5] = Convert.ToInt32(txt_sayi6.Text);
                int hedefsayi = Convert.ToInt32(txt_hedef.Text);
                DateTime startTime = DateTime.Now;
                while (true)
                {
                    secondsTaken = (DateTime.Now - startTime).TotalSeconds;

                    int num = rnd.Next(6);
                    int temp = teksayi[num];
                    teksayi[num] = teksayi[0];
                    teksayi[0] = temp;

                    int thisTotal = teksayi[0];

                    string solution = teksayi[0].ToString();

                    int numbersToUse = rnd.Next(1, 7);
                    for (int i = 1; i < numbersToUse; i++)
                    {

                        int operation = rnd.Next(4);
                        if (operation == 0)
                        {
                            thisTotal += teksayi[i];
                            solution += " + " + teksayi[i].ToString();
                        }
                        if (operation == 1)
                        {
                            thisTotal -= teksayi[i];
                            solution += " - " + teksayi[i].ToString();
                        }
                        if (operation == 2)
                        {
                            thisTotal *= teksayi[i];
                            solution += " x " + teksayi[i].ToString();
                        }
                        if (operation == 3)
                        {
                            if (thisTotal % teksayi[i] != 0) continue;
                            thisTotal /= teksayi[i];
                            solution += " / " + teksayi[i].ToString();
                        }

                    }
                    solution += " = " + thisTotal.ToString();

                    if (secondsTaken >= 10)
                    {
                        if (Math.Abs(hedefsayi - bestSoFar) < 10)
                        {
                            gecicipuan = puan - Math.Abs(hedefsayi - bestSoFar);
                            txt_kpuan.Text = Convert.ToString(gecicipuan);
                            toplam_puan += gecicipuan;
                            txt_puan.Text = Convert.ToString(toplam_puan);
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Çözüm Bulunamadı");
                            break;
                        }
                    }
                    if (Math.Abs(hedefsayi - thisTotal) < Math.Abs(hedefsayi - bestSoFar))
                    {
                        bestSoFar = thisTotal;
                        txt_islem.Text = solution;

                    }

                    if (thisTotal == hedefsayi)
                    {
                        txt_islem.Text = solution;
                        txt_kpuan.Text = Convert.ToString(puan);
                        toplam_puan += puan;
                        txt_puan.Text = Convert.ToString(toplam_puan);
                        break;
                    }
                }
                txt_islemsuresi.Text = Convert.ToString(secondsTaken);

            


        }
            private void Form1_Load(object sender, EventArgs e)
            {

            }
        private void TextEnable(bool a)
        {
            txt_sayi6.Enabled = a;
            txt_sayi1.Enabled = a;
            txt_sayi2.Enabled = a;
            txt_sayi3.Enabled = a;
            txt_sayi4.Enabled = a;
            txt_sayi5.Enabled = a;
            txt_hedef.Enabled = a;
            if (a == true)
            {
                txt_sayi6.Clear();
                txt_sayi1.Clear();
                txt_sayi2.Clear();
                txt_sayi3.Clear();
                txt_sayi4.Clear();
                txt_sayi5.Clear();
                txt_hedef.Clear();
            }
        }
        private void rdo_random_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_random.Checked == true)
            {
                TextEnable(false);
                int[] randomsayiuret = new int[6];

                for (int i = 1; i < 6; i++)
                {
                    randomsayiuret[i] = rnd.Next(1, 10);
                }
                txt_sayi1.Text = randomsayiuret[1].ToString();
                txt_sayi2.Text = randomsayiuret[2].ToString();
                txt_sayi3.Text = randomsayiuret[3].ToString();
                txt_sayi4.Text = randomsayiuret[4].ToString();
                txt_sayi5.Text = randomsayiuret[5].ToString();

                int randomciftsayiuret = rnd.Next(10, 99);
                randomciftsayiuret = randomciftsayiuret - randomciftsayiuret % 10;
                txt_sayi6.Text = randomciftsayiuret.ToString();

                int randomhedef = rnd.Next(100, 999);
                txt_hedef.Text = randomhedef.ToString();
            }
            txt_islem.Text = " ";
        }

        private void rdo_kendingir_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_kendingir.Checked == true)
            {
                TextEnable(true);
            }
            txt_islem.Text = " ";
        }

        private void btn_hesapla_Click(object sender, EventArgs e)
        {
            if (txt_hedef.Text == "")
            {
                MessageBox.Show("Lütfen Sayıları Boş Bırakmayınız!");

            }
            else
            {
                Hesaplama();
            }
            
        }
    }
    }
