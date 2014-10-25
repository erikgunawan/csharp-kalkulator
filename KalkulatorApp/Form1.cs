using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KalkulatorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSatu_Click(object sender, EventArgs e)
        {
            checkNol("1");
        }

        private void btnDua_Click(object sender, EventArgs e)
        {
            checkNol("2");
        }

        private void btnTiga_Click(object sender, EventArgs e)
        {
            checkNol("3");
        }

        private void btnEmpat_Click(object sender, EventArgs e)
        {
            checkNol("4");
        }

        private void btnLima_Click(object sender, EventArgs e)
        {
            checkNol("5");
        }

        private void btnEnam_Click(object sender, EventArgs e)
        {
            checkNol("6");
        }

        private void btnTujuh_Click(object sender, EventArgs e)
        {
            checkNol("7");
        }

        private void btnDelapan_Click(object sender, EventArgs e)
        {
            checkNol("8");
        }

        private void btnSembilan_Click(object sender, EventArgs e)
        {
            checkNol("9");
        }

        private void btnNol_Click(object sender, EventArgs e)
        {
            int length = txtOperasi.Text.Length;
            if (length > 1)
            {
                if (!txtOperasi.Text.ElementAt(length - 1).Equals(' '))
                    txtOperasi.AppendText("0");
            }
            else if (length == 1)
            {
                if (!txtOperasi.Text.Equals("0"))
                    txtOperasi.AppendText("0");
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            insertSimbol(" + ");
        }

        private void btnKurang_Click(object sender, EventArgs e)
        {
            insertSimbol(" - ");
        }

        private void btnKali_Click(object sender, EventArgs e)
        {
            insertSimbol(" * ");
        }

        private void btnBagi_Click(object sender, EventArgs e)
        {
            insertSimbol(" / ");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            hasil();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            int length = txtOperasi.Text.Length;

            if (length > 1)
            {
                if (!txtOperasi.Text.ElementAt(length - 1).Equals(' '))
                    txtOperasi.Text = txtOperasi.Text.Substring(0, length - 1);
                else
                    txtOperasi.Text = txtOperasi.Text.Substring(0, length - 3);
            }
            else if (length == 1)
            {
                reset();
            }
        }

        private void btnPersen_Click(object sender, EventArgs e)
        {
            int length = txtOperasi.Text.Length;

            String[] arr = txtOperasi.Text.Split();

            if (length >= 1)
            {
                if (!txtOperasi.Text.ElementAt(length - 1).Equals(' '))
                {
                    txtOperasi.Text = "";
                    for (int i = 0; i < arr.Length-1; i++)
                    {
                        txtOperasi.AppendText(arr[i] + " ");
                    }
                    txtOperasi.AppendText((Convert.ToDouble(arr[arr.Length-1])/100) + "");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reset();
            txtOperasi.ReadOnly = true; 
            txtOperasi.BackColor = System.Drawing.SystemColors.Window;
        }

        private void checkSamaDengan()
        {
            Boolean b;
            if (txtOperasi.Text.Contains("="))
                b = true;
            else
                b = false;

            if (b == true)
                txtOperasi.Text = "0";
        }

        private void checkNol(String angka)
        {
            checkSamaDengan();

            int length = txtOperasi.Text.Length;

            if (length > 1)
                txtOperasi.AppendText(angka);
            else if (length == 1)
            {
                if (txtOperasi.Text.Equals("0"))
                    txtOperasi.Text = angka;
                else
                    txtOperasi.AppendText(angka);
            }
        }

        private void insertSimbol(String simbol)
        {
            checkSamaDengan();

            int length = txtOperasi.Text.Length;

            if (length >= 1)
            {
                if (!txtOperasi.Text.ElementAt(length - 1).Equals(' '))
                    txtOperasi.AppendText(simbol);
                else
                    txtOperasi.Text = txtOperasi.Text.Substring(0, length - 3) + simbol;
            }
        }

        private void changeKurangToTambah(List<String> listSimbol, List<double> listAngka)
        {
            for (int i = 0; i < listSimbol.Count(); i++)
            {
                if (listSimbol.ElementAt(i).Equals("-"))
                {
                    listSimbol.RemoveAt(i);
                    listSimbol.Insert(i, "+");

                    double replace = listAngka.ElementAt(i + 1) * -1;
                    listAngka.RemoveAt(i + 1);
                    listAngka.Insert(i + 1, replace);
                }
            }
        }

        private void hitung(List<String> listSimbol, List<double> listAngka, String simbol)
        {

            for (int i = 0; i < listSimbol.Count(); i++)
            {
                if (listSimbol.ElementAt(i).Equals(simbol))
                {
                    double a = listAngka.ElementAt(i);
                    double b = listAngka.ElementAt(i + 1);
                    listAngka.RemoveRange(i, 2);

                    double c = 0;
                    if (simbol.Equals("*"))
                        c = a * b;
                    else if (simbol.Equals("/"))
                        c = a / b;
                    else if (simbol.Equals("+"))
                        c = a + b;

                    listAngka.Insert(i, c);
                    listSimbol.RemoveAt(i);
                }

            }
        }
        
        private void hasil()
        {
            checkSamaDengan();

            if (txtOperasi.Text.Trim().Split().Length %2 == 0)
                txtOperasi.Text = txtOperasi.Text.Substring(0, txtOperasi.Text.Length-3);

            String text = txtOperasi.Text;

            String[] arr = text.Trim().Split();

            List<double> listAngka = new List<double>();
            List<String> listSimbol = new List<String>();

            Boolean cek = true;

            for (int i = 0; i < arr.Length; i++)
            {
                int j = 0, k = 0;
                if (i % 2 == 0)
                {
                    listAngka.Add(Convert.ToDouble(arr[i]));
                    j++;
                }
                else if (i % 2 == 1)
                {
                    listSimbol.Add(arr[i]);
                    k++;
                }
            }

            changeKurangToTambah(listSimbol, listAngka);

            while (cek == true)
            {
                hitung(listSimbol, listAngka, "*");
                hitung(listSimbol, listAngka, "/");
                hitung(listSimbol, listAngka, "-");
                hitung(listSimbol, listAngka, "+");

                if (listSimbol.Count != 0)
                    cek = true;
                else
                    cek = false;
            }

            txtOperasi.AppendText(" = " + listAngka.ElementAt(0));
        }

        private void reset()
        {
            txtOperasi.Text = "0";
        }
    }
}
