using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.VisualBasic;

namespace TesekkurTaktirHesaplama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ArrayList kalanlar = new ArrayList();
        ArrayList kalanlarnot = new ArrayList();
        ArrayList ilkCombos = new ArrayList();
        ArrayList ikinciCombos = new ArrayList();
        ArrayList Numerics = new ArrayList();
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=hesaplama.accdb");

        private void baglantiYap()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
        }

        private void ekle(int sayi)
        {
            ComboBox dersler = new ComboBox();
            dersler.Items.Add("Beden Eğitimi");
            dersler.Items.Add("Biyoloji");
            dersler.Items.Add("Coğrafya");
            dersler.Items.Add("Çağdaş Türk ve Dünya Tar.");
            dersler.Items.Add("Dil ve Anlatım");
            dersler.Items.Add("Din Kültürü");
            dersler.Items.Add("Felsefe");
            dersler.Items.Add("Fizik");
            dersler.Items.Add("Geometri");
            dersler.Items.Add("Görsel Sanatlar ve Müzik");
            dersler.Items.Add("İkinci Yabancı Dil");
            dersler.Items.Add("Kimya");
            dersler.Items.Add("Mantık");
            dersler.Items.Add("Matematik");
            dersler.Items.Add("Milli Güvenlik Bilgisi");
            dersler.Items.Add("Osmanlı Türkçesi");
            dersler.Items.Add("Psikoloji");
            dersler.Items.Add("Sağlık Bilgisi");
            dersler.Items.Add("Sosyoloji");
            dersler.Items.Add("T.C.İnkılap Tarihi");
            dersler.Items.Add("Tarih");
            dersler.Items.Add("Trafik ve İlkyardım");
            dersler.Items.Add("Türk Edebiyatı");
            dersler.Items.Add("Türkçe");
            dersler.Items.Add("Yabancı Dil");

            ComboBox saati = new ComboBox();
            saati.Items.Add("1");
            saati.Items.Add("2");
            saati.Items.Add("3");
            saati.Items.Add("4");
            saati.Items.Add("5");
            saati.Items.Add("6");
            saati.Items.Add("7");
            saati.Items.Add("8");
            saati.Items.Add("9");
            saati.Items.Add("10");
            saati.Items.Add("11");
            saati.Items.Add("12");
            saati.Items.Add("13");
            saati.Items.Add("14");
            saati.Items.Add("15");
            saati.Items.Add("16");
            saati.Items.Add("17");
            saati.Items.Add("18");
            saati.Items.Add("19");
            saati.Items.Add("20");
            int x = 10, y = 10;
            if (xtraScrollableControl1.Controls.Count > 0)
            {
                ilkCombos.Clear();
                ikinciCombos.Clear();
                Numerics.Clear();
                for (int i = 0; i < xtraScrollableControl1.Controls.Count; i += 3)
                {
                    ilkCombos.Add(((ComboBox)xtraScrollableControl1.Controls[i]).Text);
                    ikinciCombos.Add(((ComboBox)xtraScrollableControl1.Controls[i + 1]).Text);
                    Numerics.Add(((NumericUpDown)xtraScrollableControl1.Controls[i + 2]).Value);
                }
            }
            xtraScrollableControl1.Controls.Clear();
            for (int i = 0; i < sayi; i++)
            {
                x = 10;
                ComboBox ilkcomboBox = new ComboBox();
                ilkcomboBox.Location = new Point(x, y);
                for (int v = 0; v < dersler.Items.Count; v++)
                {
                    ilkcomboBox.Items.Add(dersler.Items[v]);
                }
                xtraScrollableControl1.Controls.Add(ilkcomboBox);
                ComboBox ikincicomboBox = new ComboBox();
                x = ilkcomboBox.Right + 5;
                ikincicomboBox.Location = new Point(x, y);
                for (int v = 0; v < saati.Items.Count; v++)
                {
                    ikincicomboBox.Items.Add(saati.Items[v]);
                }
                xtraScrollableControl1.Controls.Add(ikincicomboBox);
                NumericUpDown numeric = new NumericUpDown();
                x = ikincicomboBox.Right + 5;
                numeric.Location = new Point(x, y);
                numeric.DecimalPlaces = 2;
                xtraScrollableControl1.Controls.Add(numeric);
                y = ilkcomboBox.Bottom + 5;
            }
            if (ilkCombos.Count > 0 && ikinciCombos.Count > 0 && Numerics.Count > 0)
            {
                for (int i = 0, art = 0; i < xtraScrollableControl1.Controls.Count; i += 3, art++)
                {
                    ((ComboBox)xtraScrollableControl1.Controls[i]).Text = ilkCombos[art].ToString();
                    ((ComboBox)xtraScrollableControl1.Controls[i + 1]).Text = ikinciCombos[art].ToString();
                    ((NumericUpDown)xtraScrollableControl1.Controls[i + 2]).Value = decimal.Parse(Numerics[art].ToString());
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ekle(int.Parse(comboBox1.Text));
        }

        int sayisi = 0;
        int haftalik = 0;
        double agirlikli = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            bool hatali = false;
            try
            {
                sayisi = Convert.ToInt32(comboBox1.Text);
                kalanlar.Clear();
                haftalik = 0;
                agirlikli = 0;
                for (int i = 1; i < xtraScrollableControl1.Controls.Count; i += 3)
                {
                    NumericUpDown not = (NumericUpDown)xtraScrollableControl1.Controls[i + 1];
                    ComboBox derssaati = (ComboBox)xtraScrollableControl1.Controls[i];
                    ComboBox dersadi = (ComboBox)xtraScrollableControl1.Controls[i - 1];
                    haftalik += Convert.ToInt32(derssaati.Text);
                    agirlikli += Convert.ToDouble(not.Value) * Convert.ToInt32(derssaati.Text);
                    if (not.Value < 50)
                    {
                        kalanlar.Add(dersadi.Text);
                        kalanlarnot.Add(not.Value);
                    }
                }
                double ortalama = agirlikli / haftalik;
                ortalama = Convert.ToDouble(string.Format("{0:F2}", ortalama));

                if (ortalama < 50)
                {
                    sonuc.ForeColor = Color.Red;
                    sonuc.Text = "Maalesef kaldınız. (" + string.Format("{0:F2}", (50 - ortalama)) + " puanla geçemiyorsunuz.)";
                }
                else if (ortalama < 70)
                {
                    sonuc.ForeColor = Color.YellowGreen;
                    sonuc.Text = "Maalesef hiç bir belge almaya hak kazanamadınız. (" + string.Format("{0:F2}", (70 - ortalama)) + " puanla teşekkürü kaçırıyorsunuz.)";
                }
                else if (ortalama < 85)
                {
                    sonuc.ForeColor = Color.Blue;
                    sonuc.Text = "Tebrikler, teşekkür belgesi almaya hak kazandınız. (" + string.Format("{0:F2}", (85 - ortalama)) + " puanla taktiri kaçırıyorsunuz.)";
                }
                else if (ortalama <= 100)
                {
                    sonuc.ForeColor = Color.Green;
                    sonuc.Text = "Tebrikler, taktir belgesi almaya hak kazandınız.";
                }

                if (kalanlar.Count > 0)
                {
                    label13.Visible = true;
                    listView1.Visible = true;
                    if (ortalama >= 70)
                    {
                        sonuc.Text = "Maalesef " + kalanlar.Count + " adet dersten geçemediğiniz için teşekkür belgesi almaya hak kazanamadınız. Ortalama: " + ortalama;
                    }
                    else if (ortalama >= 85)
                    {
                        sonuc.Text = "Maalesef " + kalanlar.Count + " adet dersten geçemediğiniz için taktir belgesi almaya hak kazanamadınız. Ortalama: " + ortalama;
                    }
                }
                else
                {
                    label13.Visible = false;
                    listView1.Visible = false;
                }

                ders_sayisi.Text = comboBox1.Text;
                toplam_ders_saati.Text = haftalik.ToString();
                agirlikli_puan_toplami.Text = agirlikli.ToString();
                agirlikli_puan_ortalamasi.Text = ortalama.ToString();
                listView1.Clear();
                listView1.Columns.Add("Ders Adı");
                listView1.Columns.Add("Ders Notu");
                for (int i = 0; i < kalanlar.Count; i++)
                {
                    int count = listView1.Items.Count;
                    listView1.Items.Add(kalanlar[i].ToString());
                    listView1.Items[count].SubItems.Add(kalanlarnot[i].ToString());
                }
                int x = 10, y = 10;
                xtraScrollableControl2.Controls.Clear();
                for (int i = 1; i < xtraScrollableControl1.Controls.Count; i += 3)
                {
                    x = 10;
                    NumericUpDown not = (NumericUpDown)xtraScrollableControl1.Controls[i + 1];
                    ComboBox derssaati = (ComboBox)xtraScrollableControl1.Controls[i];
                    ComboBox dersadi = (ComboBox)xtraScrollableControl1.Controls[i - 1];
                    TextBox dersAdiTextBox = new TextBox();
                    dersAdiTextBox.Enabled = false;
                    dersAdiTextBox.Text = dersadi.Text;
                    dersAdiTextBox.Location = new Point(x, y);
                    xtraScrollableControl2.Controls.Add(dersAdiTextBox);
                    TextBox agirlikliPuanTextBox = new TextBox();
                    agirlikliPuanTextBox.Enabled = false;
                    agirlikliPuanTextBox.Text = string.Format("{0:F2}", (Convert.ToDouble(not.Value) * Convert.ToInt32(derssaati.Text)).ToString());
                    x = dersAdiTextBox.Right + 5;
                    agirlikliPuanTextBox.Location = new Point(x, y);
                    xtraScrollableControl2.Controls.Add(agirlikliPuanTextBox);
                    ProgressBar getiriYuzdeProgress = new ProgressBar();
                    getiriYuzdeProgress.Value = Convert.ToInt32(((Convert.ToDouble(not.Value) * Convert.ToDouble(derssaati.Text)) / agirlikli) * 100);
                    x = agirlikliPuanTextBox.Right + 5;
                    getiriYuzdeProgress.Location = new Point(x, y);
                    xtraScrollableControl2.Controls.Add(getiriYuzdeProgress);
                    Label yuzdeLabel = new Label();
                    yuzdeLabel.Text = "%" + string.Format("{0:F2}", ((Convert.ToDouble(not.Value) * Convert.ToDouble(derssaati.Text)) / agirlikli) * 100).ToString();
                    x = getiriYuzdeProgress.Right + 5;
                    yuzdeLabel.Location = new Point(x, y);
                    xtraScrollableControl2.Controls.Add(yuzdeLabel);
                    y = agirlikliPuanTextBox.Bottom + 5;
                }
            }
            catch (Exception hata)
            {
                hatali = true;
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (hatali == false)
            {
                xtraTabControl1.TabPages[1].PageEnabled = true;
                xtraTabControl1.SelectedTabPageIndex = 1;
            }
            else
            {
                listView1.Clear();
                xtraScrollableControl2.Controls.Clear();
                ders_sayisi.Text = "-";
                toplam_ders_saati.Text = "-";
                agirlikli_puan_ortalamasi.Text = "-";
                agirlikli_puan_toplami.Text = "-";
                sonuc.Text = "-";
                xtraTabControl1.TabPages[1].PageEnabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            baglantiYap();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM hesaplama", baglanti);
            OleDbDataReader okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                comboBox2.Items.Add(okuyucu["adi"].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                string dersler = "", saatler = "", notlar = "";
                baglantiYap();
                OleDbCommand komut = new OleDbCommand("SELECT * FROM hesaplama WHERE adi='" + comboBox2.Text + "'", baglanti);
                OleDbDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    dersler = okuyucu["dersler"].ToString();
                    saatler = okuyucu["saatler"].ToString();
                    notlar = okuyucu["notlar"].ToString();
                }
                baglanti.Close();
                string[] aDersler = dersler.Split('|');
                string[] aSaatlar = saatler.Split('|');
                string[] aNotlar = notlar.Split('|');
                comboBox1.Text = aDersler.Length.ToString();
                if (int.Parse(comboBox1.Text.ToString()) != aDersler.Length)
                {
                    ekle(aDersler.Length);
                }

                for (int i = 0, art = 0; i < xtraScrollableControl1.Controls.Count; i += 3, art++)
                {
                    ((ComboBox)xtraScrollableControl1.Controls[i]).Text = aDersler[art];
                    ((ComboBox)xtraScrollableControl1.Controls[i + 1]).Text = aSaatlar[art];
                    ((NumericUpDown)xtraScrollableControl1.Controls[i + 2]).Value = decimal.Parse(aNotlar[art]);
                }
            }
        }

        private bool kontrol()
        {
            bool donecek = true;
            for (int i = 0; i < xtraScrollableControl1.Controls.Count; i += 3)
            {
                if (((ComboBox)xtraScrollableControl1.Controls[i]).Text == "" || ((ComboBox)xtraScrollableControl1.Controls[i + 1]).Text == "" || ((NumericUpDown)xtraScrollableControl1.Controls[i + 2]).Value <= 0)
                {
                    donecek = false;
                }
            }
            return donecek;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (xtraScrollableControl1.Controls.Count > 0 && kontrol())
            {
                string veri = Microsoft.VisualBasic.Interaction.InputBox("Kayıt adını giriniz:", "Kayıt", "");
                if (veri != "" && veri != "CANCEL" && veri != "OK")
                {
                    baglantiYap();
                    string dersler = "", saatler = "", notlar = "";
                    for (int i = 0; i < xtraScrollableControl1.Controls.Count; i += 3)
                    {
                        dersler += "|" + ((ComboBox)xtraScrollableControl1.Controls[i]).Text;
                        saatler += "|" + ((ComboBox)xtraScrollableControl1.Controls[i + 1]).Text;
                        notlar += "|" + ((NumericUpDown)xtraScrollableControl1.Controls[i + 2]).Value.ToString();
                    }
                    dersler = dersler.Trim('|');
                    saatler = saatler.Trim('|');
                    notlar = notlar.Trim('|');
                    OleDbCommand komut = new OleDbCommand("INSERT INTO hesaplama (adi, dersler, saatler, notlar) VALUES ('" + veri.Trim() + "', '" + dersler + "', '" + saatler + "', '" + notlar + "')", baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kayıt işlemi başarıyla tamamlandı.");
                }
               
            }
            else
            {
                MessageBox.Show("Boş yer bırakmamalısınız.");
            }
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            baglantiYap();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM hesaplama", baglanti);
            OleDbDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                while (okuyucu.Read())
                {
                    comboBox2.Items.Add(okuyucu["adi"].ToString());
                }
            }
            Application.Restart();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                if (MessageBox.Show("\"" + comboBox2.Text + "\" adlı veriyi silmek istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    baglantiYap();
                    OleDbCommand komut = new OleDbCommand("DELETE FROM hesaplama WHERE adi='" + comboBox2.Text + "'", baglanti);
                    komut.ExecuteNonQuery();
                    OleDbCommand komut2 = new OleDbCommand("SELECT * FROM hesaplama", baglanti);
                    OleDbDataReader okuyucu = komut2.ExecuteReader();
                    if (okuyucu.Read())
                    {
                        while (okuyucu.Read())
                        {
                            comboBox2.Items.Add(okuyucu["adi"].ToString());
                        }
                    }
                    baglanti.Close();
                    MessageBox.Show("Veri başarıyla silindi.");
                    Application.Restart();
                }
            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                Form1 form1 = new Form1();
                MessageBox.Show("Teşekkür Taktir Hesaplama v2.00\nGeliştirici: Halit Izgın - Ready\nhttp://www.kodevreni.com", "Taktir Teşekkür Hesaplama Hakkında", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
