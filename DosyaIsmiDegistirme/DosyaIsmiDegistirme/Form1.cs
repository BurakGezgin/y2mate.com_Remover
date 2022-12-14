using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DosyaIsmiDegistirme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Listele
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            getDirectories(Application.StartupPath, listBox1);
        }

        //Kaldır
        private void button2_Click(object sender, EventArgs e)
        {
            NameChanger();

            listBox1.Items.Clear();
            getDirectories(Application.StartupPath, listBox1);
        }


        public void NameChanger()
        {
            try
            {
                string y2mateText = "y2mate.com - ";
                string YeniAd = "";
                int sayac = 0;



                DialogResult dialogResult = MessageBox.Show("İsimleri değiştirmek istediğine emin misin?", "Onay!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (var item in listBox1.Items)
                    {

                        YeniAd = item.ToString().Replace(y2mateText, "");

                        System.IO.FileInfo fi = new System.IO.FileInfo(Application.StartupPath + "\\mp3\\" + item.ToString());
                        if (fi.Exists)
                        {
                            fi.MoveTo(Application.StartupPath + "\\mp3\\" + YeniAd);
                        }


                    }

                    MessageBox.Show("İsimlerden Kaldırıldı.");
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }

        }



        public void getDirectories(string _directoryPath, ListBox _listbox)
        {
            try
            {
                if (_directoryPath != "")
                {
                    string[] directories = Directory.GetDirectories(_directoryPath);

                    if (directories.Count() > 0)
                    {
                        foreach (string LoopFolder in directories)
                        {
                            DirectoryInfo dirInfoDirectory = new DirectoryInfo(LoopFolder);
                            this.getFileInfo(LoopFolder, _listbox);
                        }
                    }
                    else
                    {
                        DirectoryInfo dirInfoDirectory = new DirectoryInfo(_directoryPath);
                        this.getFileInfo(_directoryPath, _listbox);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public void getFileInfo(string _directoryPath, ListBox _listbox)
        {
            try
            {
                DirectoryInfo dirInfoFile = new DirectoryInfo(_directoryPath);
                FileInfo[] files = dirInfoFile.GetFiles();

                string[] directoriesPath = Directory.GetDirectories(_directoryPath);

                if (directoriesPath.Count() > 0)
                {
                    this.getDirectories(_directoryPath, _listbox);
                }

                foreach (FileInfo fi in files)
                {
                    if (fi.Name.Contains("y2mate.com - "))
                        _listbox.Items.Add(fi.Name);


                }

                label1.Text = listBox1.Items.Count.ToString()+" Adet Bulundu!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
        }
    }
}
