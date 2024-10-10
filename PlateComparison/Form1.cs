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

        private static double PlakaBenzerlikOrani(string plaka1, string plaka2)
        {
            // Plakayý parçalarýna ayýr ve normalize et.
            var plakaPattern = @"^(\d{2})\s*([A-Z]{1,3})\s*(\d{1,4})$";

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

            // Þehir kodunu karþýlaþtýr.
            double sehirKoduBenzerlik = sehirKodu1 == sehirKodu2 ? 1.0 : 0.0;

            // Harf kýsmýný karþýlaþtýr (Levenshtein Mesafesi kullanýlabilir).
            double harfBenzerlik = 1.0 - (double)LevenshteinMesafesi(harfler1, harfler2) / Math.Max(harfler1.Length, harfler2.Length);

            // Sayý kýsmýný karþýlaþtýr.
            double sayiBenzerlik = sayilar1 == sayilar2 ? 1.0 : 0.0;

            // Ortalama benzerlik oranýný hesapla.
            return (sehirKoduBenzerlik + harfBenzerlik + sayiBenzerlik) / 3.0;
        }

        private static int LevenshteinMesafesi(string s1, string s2)
        {
            int n = s1.Length;
            int m = s2.Length;
            int[,] dp = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    if (i == 0)
                    {
                        dp[i, j] = j;
                    }
                    else if (j == 0)
                    {
                        dp[i, j] = i;
                    }
                    else
                    {
                        int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
                        dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
                    }
                }
            }

            return dp[n, m];
        }
    }
}