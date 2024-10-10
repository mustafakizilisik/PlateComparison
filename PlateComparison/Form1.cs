using System.Text.RegularExpressions;

namespace PlateComparison
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblResult.Text = "Do�ruluk Oran�: " + PlakaBenzerlikOrani(txtFirstPlate.Text, txtLastPlate.Text);
        }

        private static string EnBenzerPlakayiBul(string plate, List<string> plateList, double truthShare)
        {
            double maxBenzerlikOrani = -1.0;
            string enBenzerPlaka = string.Empty;
            var lockObject = new object();

            Parallel.ForEach(plateList, plaka =>
            {
                double benzerlikOrani = PlakaBenzerlikOrani(plate, plaka);

                lock (lockObject)
                {
                    if (benzerlikOrani > maxBenzerlikOrani)
                    {
                        maxBenzerlikOrani = benzerlikOrani;
                        enBenzerPlaka = plaka;
                    }
                }
            });

            if (maxBenzerlikOrani >= truthShare)
            {
                Console.WriteLine($"Tan�ms�z Plaka: {plate} \"{enBenzerPlaka}\" Olarak G�ncellendi. Do�ruluk Oran�: {maxBenzerlikOrani}");
                return enBenzerPlaka;
            }

            return plate;
        }

        public static double PlakaBenzerlikOrani(string plaka1, string plaka2)
        {
            // Plakalar� �ehir kodu, harf ve say� olarak ��e ay�r.
            var plakaPattern = @"^(\d{2})\s*([A-Z]{1,3})\s*(\d+)$";

            var match1 = Regex.Match(plaka1.Replace(" ", "").ToUpper(), plakaPattern);
            var match2 = Regex.Match(plaka2.Replace(" ", "").ToUpper(), plakaPattern);

            if (!match1.Success || !match2.Success)
            {
                return 0.0; // Plaka format� uygun de�ilse, benzerlik 0.
            }

            // �ehir kodu, harfler ve say� k�sm�n� al.
            string sehirKodu1 = match1.Groups[1].Value;
            string harfler1 = match1.Groups[2].Value;
            string sayilar1 = match1.Groups[3].Value;

            string sehirKodu2 = match2.Groups[1].Value;
            string harfler2 = match2.Groups[2].Value;
            string sayilar2 = match2.Groups[3].Value;

            // �ehir kodunu birebir kar��la�t�r.
            double sehirKoduBenzerlik = sehirKodu1 == sehirKodu2 ? 1.0 : 0.0;

            // Harf k�sm�n� kar��la�t�r, daha k�sa olan� baz alarak de�erlendirme yap.
            double harfBenzerlik = KarakterBenzerligi(harfler1, harfler2);

            // Say� k�sm�n� kar��la�t�r, �rne�in '477' ile '0477' ayn� kabul edilir.
            double sayiBenzerlik = sayilar1.TrimStart('0') == sayilar2.TrimStart('0') ? 1.0 : 0.0;

            // Ortalama benzerlik oran�n� hesapla.
            return (sehirKoduBenzerlik + harfBenzerlik + sayiBenzerlik) / 3.0;
        }

        public static double KarakterBenzerligi(string s1, string s2)
        {
            // �ki harf grubunun benzerlik oran�n� uzunlu�a g�re de�erlendir.
            int minLength = Math.Min(s1.Length, s2.Length);
            int maxLength = Math.Max(s1.Length, s2.Length);

            // Ortak karakter say�s�n� hesapla.
            int ortakKarakterSayisi = 0;
            for (int i = 0; i < minLength; i++)
            {
                if (s1[i] == s2[i])
                {
                    ortakKarakterSayisi++;
                }
            }

            // Harf benzerli�i, ortak karakterlerin ortalamas� al�narak hesaplan�r.
            return (double)(ortakKarakterSayisi + 1) / (maxLength + 1);
        }
    }
}