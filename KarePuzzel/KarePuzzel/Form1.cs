using System.Linq;
using System.Windows.Forms;



namespace KarePuzzel
{
    public partial class Form1 : Form
    {
        Button bos_buton = null;
        Node orginal_nodum = new Node();
        Node karýsmýs_nodum = new Node();
        List<Button> karýsmýs_buttons;
        Node orjinal_liste = new Node();
        List<Button> buttons = new List<Button>();
        Point[] locations = new Point[16];
        int hamle_sayýsý = 0;
        int score = 0;
        int sayaç = 0;
        string gamer_name;

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;

            buttons.AddRange(new Button[] { btn_piec0, btn_piec1, btn_piec2, btn_piec3, btn_piec4, btn_piec5, btn_piec6, btn_piec7, btn_piec8, btn_piec9, btn_piec10, btn_piec11, btn_piec12, btn_piec13, btn_piec14, btn_piec15 });
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }


        private void btn_picdownlo_Click(object sender, EventArgs e)
        {
            //Yüklenen fotoðrafý görüntülemek için picturebox'a çektik
            picbx_puz.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            openFileDialog1.ShowDialog();
            picbx_puz.ImageLocation = openFileDialog1.FileName;
            pictureBox1.ImageLocation = openFileDialog1.FileName;

        }

        private void btn_puzzeload_Click(object sender, EventArgs e)
        {
        }

        private void btn_save_Click(object sender, EventArgs e)
        {


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Text dosyasý içerisinde ki kullanýcý adý ve skor verilerini Listbox içerisine çekiyoruz

            string dosyaYolu = @"C:\Users\edanr\OneDrive\Resimler\KarePuzzel\KarePuzzel\enyuksekskor.txt";
            string[] satirlar = File.ReadAllLines(dosyaYolu);

            List<Tuple<string, int>> skorlar = new List<Tuple<string, int>>();

            for (int i = 0; i < satirlar.Length; i += 3)
            {
                string kullaniciAdi = satirlar[i];
                int skor = int.Parse(satirlar[i]);

                skorlar.Add(new Tuple<string, int>(kullaniciAdi, skor));
            }

            skorlar = skorlar.OrderByDescending(s => s.Item2).ToList();

            foreach (var skor in skorlar)
            {
                string satir = $"{skor.Item1} - {skor.Item2}";
                listBoxScore.Items.Add(satir);
            }

        }


        private void button2_Click(object sender, EventArgs e)// karýþtýrma

        {
            button3.Enabled = true;
            List<Button> buttons = new List<Button>() { btn_piec0, btn_piec1, btn_piec2, btn_piec3, btn_piec4, btn_piec5, btn_piec6, btn_piec7, btn_piec8, btn_piec9, btn_piec10, btn_piec11, btn_piec12, btn_piec13, btn_piec14, btn_piec15 };

            Image foto = picbx_puz.Image;

            int genislik = picbx_puz.Width / 4; // 4 satýr
            int yukseklik = picbx_puz.Height / 4; // 4 sütun

            //Bu kod sayesinde picturebox içerisinde fotonun orijinal boyutu deðil butonlara uyacak þekilde bir kopyasý oluþturulur.
            Image fotoKopya = foto.GetThumbnailImage(picbx_puz.Width, picbx_puz.Height, null, IntPtr.Zero);

            LinkedList<Image> parcalar = new LinkedList<Image>();

            for (int satir = 0; satir < 4; satir++)
            {
                for (int sutun = 0; sutun < 4; sutun++)
                {
                    Rectangle kare = new Rectangle(sutun * genislik, satir * yukseklik, genislik, yukseklik);
                    Bitmap parca = new Bitmap(genislik, yukseklik);
                    Graphics.FromImage(parca).DrawImage(fotoKopya, new Rectangle(0, 0, genislik, yukseklik), kare, GraphicsUnit.Pixel);

                    parcalar.AddLast(parca);
                }
            }


            LinkedListNode<Image> gecerliDugum = parcalar.First;

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].BackgroundImage = gecerliDugum.Value;
                gecerliDugum = gecerliDugum.Next;
            }


            Point[] locations = new Point[buttons.Count];
            for (int i = 0; i < buttons.Count; i++)
            {
                locations[i] = buttons[i].Location;
            }
            for (int i = 0; i < 16; i++)
            {
                orginal_nodum.Add(orginal_nodum, locations[i]);
            }



            Random random = new Random();



            for (int i = 0; i < buttons.Count; i++)
            {
                locations[i] = buttons[i].Location;
            }

            for (int i = 0; i < buttons.Count; i++)
            {
                int j = random.Next(i, buttons.Count);
                Point temp = locations[i];
                locations[i] = locations[j];

                locations[j] = temp;
            }


            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Location = locations[i];
                karýsmýs_nodum.Add(karýsmýs_nodum, buttons[i].Location);
            }

            int sayaç = 0;
            for (int i = 0; i < 16; i++)
            {
                if (orginal_nodum.find_data_by_index(orginal_nodum, i) == karýsmýs_nodum.find_data_by_index(karýsmýs_nodum, i))
                {
                    buttons[i].Enabled = false;
                    sayaç++;
                }



            }
            if (sayaç != 0)
            {
                button2.Enabled = false;
                foreach (Button b in buttons)
                {
                    b.Enabled = true;
                }
                button3.Enabled = false;
            }
            else
            {
                MessageBox.Show("TEKRAR KARIÞTIRIN!!!");
                karýsmýs_nodum.Clear(karýsmýs_nodum);
                foreach (Button b in buttons)
                {
                    b.Enabled = false;
                }
            }

        }

        private void button3_Click(object sender, EventArgs e) //resim yükleme
        {
            btn_save.Enabled = false;
            picbx_puz.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            openFileDialog1.ShowDialog();
            picbx_puz.ImageLocation = openFileDialog1.FileName;
            pictureBox1.ImageLocation = openFileDialog1.FileName;

            button2.Enabled = true;
        }

        public void updatelist(Button ilk, Button ikinci)
        {

            Node karýsmýs_nodenew = new Node();
            int indis1 = buttons.IndexOf(ikinci);
            int indis2 = buttons.IndexOf(ilk);
            var p = buttons[indis1].Location;
            buttons[indis1].Location = buttons[indis2].Location;
            buttons[indis2].Location = p;

            for (int i = 0; i < 16; i++)
            {
                karýsmýs_nodenew.Add(karýsmýs_nodenew, buttons[i].Location);

            }

            int sayaçc = 0;
            for (int i = 0; i < 16; i++)
            {
                if (orginal_nodum.listele(orginal_nodum)[i] != karýsmýs_nodenew.listele(karýsmýs_nodenew)[i])
                {
                    sayaçc++;
                }
            }


            if (sayaçc == 0)
            {

                string dosyaYolu = @"C:\Users\edanr\OneDrive\Resimler\KarePuzzel\KarePuzzel\enyuksekskor.txt";
                using (StreamWriter sw = File.AppendText(dosyaYolu))
                {

                    sw.WriteLine(gamer_name);
                    sw.WriteLine(score);
                    sw.WriteLine(hamle_sayýsý);
                }
                DialogResult result = MessageBox.Show("TEBRÝKLER PUZZLE'ý TAMAMLADINIZ! Scorunuz: " + lbl_score.Text + " Hamle Sayýnýz: " + lbl_hamle.Text + " TEKRAR OYNAMAK ÝSTER MÝSÝNÝZ?", "yeniden baþlat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    Application.Restart();

                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                }


            }


            hamle_sayýsý = hamle_sayýsý + 1;
            lbl_hamle.Text = (hamle_sayýsý).ToString();

            Point orj = orginal_nodum.find_data_by_index(orginal_nodum, indis2);
            Point karýsmýs = karýsmýs_nodenew.find_data_by_index(karýsmýs_nodenew, indis2);
            if (orj == karýsmýs)
            {
                buttons[indis2].Enabled = false;
                score = score + 5;
                lbl_score.Text = score.ToString();
                sayaç++;
            }
            else
            {

                score = score - 10;
                lbl_score.Text = score.ToString();

            }


        }

        void control(Button ikinci_buton)
        {


            if (bos_buton.Location != ikinci_buton.Location)
            {
                updatelist(buttons[buttons.IndexOf(bos_buton)], buttons[buttons.IndexOf(ikinci_buton)]);
                bos_buton = null;

            }

        }

        private void btn_piec0_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec0;

            }
            else
            {

                control(btn_piec0);

            }
        }

        private void btn_piec1_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec1;
            }
            else
            {
                control(btn_piec1);

            }
        }

        private void btn_piec2_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec2;
            }
            else
            {

                control(btn_piec2);
            }
        }

        private void btn_piec3_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec3;
            }
            else
            {

                control(btn_piec3);
            }
        }

        private void btn_piec4_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec4;


            }
            else
            {
                control(btn_piec4);
            }
        }

        private void btn_piec5_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec5;
            }
            else
            {

                control(btn_piec5);
            }
        }

        private void btn_piec6_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec6;
            }
            else
            {

                control(btn_piec6);
            }
        }

        private void btn_piec7_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec7;
            }
            else
            {

                control(btn_piec7);
            }
        }

        private void btn_piec8_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec8;
            }
            else
            {

                control(btn_piec8);
            }
        }

        private void btn_piec9_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec9;
            }
            else
            {

                control(btn_piec9);
            }
        }

        private void btn_piec10_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec10;
            }
            else
            {

                control(btn_piec10);
            }
        }

        private void btn_piec11_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec11;
            }
            else
            {

                control(btn_piec11);
            }
        }

        private void btn_piec12_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec12;
            }
            else
            {

                control(btn_piec12);
            }
        }

        private void btn_piec13_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec13;
            }
            else
            {

                control(btn_piec13);
            }
        }

        private void btn_piec14_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec14;
            }
            else
            {

                control(btn_piec14);
            }
        }

        private void btn_piec15_Click(object sender, EventArgs e)
        {
            if (bos_buton == null)
            {
                bos_buton = btn_piec15;
            }
            else
            {

                control(btn_piec15);
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

            MessageBox.Show("Oyuna baþlamak için önce adýnýzý giriniz");

            string dosyaYolu = @"C:\Users\edanr\OneDrive\Resimler\KarePuzzel\KarePuzzel\enyuksekskor.txt";
            string[] satirlar = File.ReadAllLines(dosyaYolu);

            List<Tuple<string, int>> skorlar = new List<Tuple<string, int>>();

            for (int i = 0; i < satirlar.Length; i += 3)
            {
                string kullaniciAdi = satirlar[i];
                int skor = int.Parse(satirlar[i + 1]);

                skorlar.Add(new Tuple<string, int>(kullaniciAdi, skor));
            }

            skorlar = skorlar.OrderByDescending(s => s.Item2).ToList();

            foreach (var skor in skorlar)
            {
                string satir = $"{skor.Item1} - {skor.Item2}";
                listBoxScore.Items.Add(satir);
            }


        }

        private void btn_save_Click_1(object sender, EventArgs e)
        {


            string dosyaYolu = @"C:\Users\edanr\OneDrive\Resimler\KarePuzzel\KarePuzzel\enyuksekskor.txt";
            using (StreamWriter sw = File.AppendText(dosyaYolu))
            {
                gamer_name = txt_gamername.Text;

            }
            button3.Enabled = true;

        }

        private void button1_Click_1(object sender, EventArgs e)//çýkýþ
        {
            this.Close();
        }
    }
}

