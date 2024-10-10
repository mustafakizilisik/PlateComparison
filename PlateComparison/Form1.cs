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
            lblResult.Text = "Doðruluk Oraný: " + PlakaBenzerlikOrani(txtFirstPlate.Text, txtLastPlate.Text);
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
                Console.WriteLine($"Tanýmsýz Plaka: {plate} \"{enBenzerPlaka}\" Olarak Güncellendi. Doðruluk Oraný: {maxBenzerlikOrani}");
                return enBenzerPlaka;
            }

            return plate;
        }

        public static double PlakaBenzerlikOrani(string plaka1, string plaka2)
        {
            // Plakalarý þehir kodu, harf ve sayý olarak üçe ayýr.
            var plakaPattern = @"^(\d{2})\s*([A-Z]{1,3})\s*(\d+)$";

            var match1 = Regex.Match(plaka1.Replace(" ", "").ToUpper(), plakaPattern);
            var match2 = Regex.Match(plaka2.Replace(" ", "").ToUpper(), plakaPattern);

            if (!match1.Success || !match2.Success)
            {
                return 0.0; // Plaka formatý uygun deðilse, benzerlik 0.
            }

            // Þehir kodu, harfler ve sayý kýsmýný al.
            string sehirKodu1 = match1.Groups[1].Value;
            string harfler1 = match1.Groups[2].Value;
            string sayilar1 = match1.Groups[3].Value;

            string sehirKodu2 = match2.Groups[1].Value;
            string harfler2 = match2.Groups[2].Value;
            string sayilar2 = match2.Groups[3].Value;

            // Þehir kodunu birebir karþýlaþtýr.
            double sehirKoduBenzerlik = sehirKodu1 == sehirKodu2 ? 1.0 : 0.0;

            // Harf kýsmýný karþýlaþtýr, daha kýsa olaný baz alarak deðerlendirme yap.
            double harfBenzerlik = KarakterBenzerligi(harfler1, harfler2);

            // Sayý kýsmýný karþýlaþtýr, örneðin '477' ile '0477' ayný kabul edilir.
            double sayiBenzerlik = sayilar1.TrimStart('0') == sayilar2.TrimStart('0') ? 1.0 : 0.0;

            // Ortalama benzerlik oranýný hesapla.
            return (sehirKoduBenzerlik + harfBenzerlik + sayiBenzerlik) / 3.0;
        }

        public static double KarakterBenzerligi(string s1, string s2)
        {
            // Ýki harf grubunun benzerlik oranýný uzunluða göre deðerlendir.
            int minLength = Math.Min(s1.Length, s2.Length);
            int maxLength = Math.Max(s1.Length, s2.Length);

            // Ortak karakter sayýsýný hesapla.
            int ortakKarakterSayisi = 0;
            for (int i = 0; i < minLength; i++)
            {
                if (s1[i] == s2[i])
                {
                    ortakKarakterSayisi++;
                }
            }

            // Harf benzerliði, ortak karakterlerin ortalamasý alýnarak hesaplanýr.
            return (double)(ortakKarakterSayisi + 1) / (maxLength + 1);
        }
    }
}