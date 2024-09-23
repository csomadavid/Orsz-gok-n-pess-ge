using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nepesseg
{
    class Orszag
    {
        public string Orszagnev { get; private set; }
        public int Terulet { get; private set; }
        public int Nepesseg { get; private set; }
        public string Fovaros { get; private set; }
        public int FovarosNepesseg { get; private set; }

        public int Népsűrűség => (int)Math.Round((double)Nepesseg / Terulet, MidpointRounding.AwayFromZero);
        public bool fővárosbanLakik30 => FovarosNepesseg * 1000.0 / Nepesseg > 0.3;

        public Orszag(string adatsor)
        {
            string[] m = adatsor.Split(';');
            Orszagnev = m[0];
            Terulet = int.Parse(m[1]);
            string s = m[2];
            Nepesseg = s.Contains('g') ? int.Parse(s.Replace("g", "")) * 10000 : int.Parse(s);
            Fovaros = m[3];
            FovarosNepesseg = int.Parse(m[4]);
        }
        public override string ToString() => $"\t{Orszagnev} ({Fovaros})";
    }
    class Nepesseg
    {
        static void Main()
        {
            List<Orszag> országok = new List<Orszag>();
            foreach (var sor in File.ReadLines("adatok-utf8.txt").Skip(1))
            {
                országok.Add(new Orszag(sor));
            }
            Console.WriteLine($"4. feladat\nA beolvasott országok száma {országok.Count}.\n");

            Console.WriteLine($"5. feladat\nKína népsűrűsége: {országok.Where(x => x.Orszagnev == "Kína").First().Népsűrűség} fő/km^2\n");

            int kínaFő = országok.Where(x => x.Orszagnev == "Kína").First().Nepesseg;
            int indiaFő = országok.Where(x => x.Orszagnev == "India").First().Nepesseg;
            Console.WriteLine($"6. feladat\nKínában a lakosság {kínaFő - indiaFő} fővel volt több.\n");

            Orszag? harmadik = null;
            foreach (var ország in országok)
            {
                if (ország.Orszagnev != "Kína" && ország.Orszagnev != "India")
                {
                    if (harmadik == null) harmadik = ország;
                    else if (ország.Nepesseg > harmadik.Nepesseg) harmadik = ország;
                }
            }
            Console.WriteLine($"7. feladat\nA harmadik legnépesebb ország: {harmadik!.Orszagnev}, a lakosság {harmadik!.Nepesseg} fő.\n");

            Console.WriteLine("8. feladat - A következő országok lakosságának több mint 30% a fővárosban lakik:");
            országok.Where(x => x.fővárosbanLakik30).ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}