using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkalaskuriKotitehtava
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Boolean lopeta = false;
            Console.WriteLine("Tervetuloa ikälaskuriin.");
            while (lopeta != true) 
            {
                Console.WriteLine("Laskuri kertoo jäljellä olevan eliniän eliniänodotteen perusteella.");
                Laskuri();
                Console.WriteLine("Suoritetaanko uusi laskenta? K=Kyllä/E=Ei");
                Console.ResetColor();
                if (Console.ReadLine().ToUpper() == "E") lopeta = true;
            }
        }

        private static void Laskuri()
        {
            Double aikaJaljella = 0;
            int elinIanOdote = 0, jaljellaVuodet = 0;
            string alkuAika = "", sp = "";
            string vuodetKuukaudetPaivat = "";
            DateTime syntymaAikaDT, elinianOdotusDT;
            DateTime tanaan = DateTime.Today;
            string formaatti = "dd.MM.yyyy";//ParseExact käyttää pvm-formaattia joka esitellään tässä
            CultureInfo kulttuuri = CultureInfo.InvariantCulture;//ParseExact tarvitsee tämän parametrin

            Console.WriteLine("Kerro sukupuoli, M=Mies/N=Nainen");
            sp = Console.ReadLine().ToUpper();
            switch (sp)
            {
                case "M":
                    elinIanOdote = 78;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case "N":
                    elinIanOdote = 84;
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.WriteLine("Virheellinen valinta!");
                    elinIanOdote = 0;
                    break;
            }

            Console.WriteLine("Anna syntymäaika muodossa PP.KK.VVVV");
            alkuAika = Console.ReadLine();
            try
            {
                syntymaAikaDT = DateTime.ParseExact(alkuAika, formaatti, kulttuuri);
                elinianOdotusDT = syntymaAikaDT.AddYears(elinIanOdote);
                aikaJaljella = elinianOdotusDT.Subtract(tanaan).TotalDays;//Laskutoimitus.
                if (aikaJaljella < 0) aikaJaljella = 0;
                DateTime paivat = new DateTime(new TimeSpan((int)aikaJaljella, 0, 0, 0).Ticks);
                vuodetKuukaudetPaivat = string.Format("{0} vuotta {1} kuukautta ja {2} päivää", paivat.Year - 1, paivat.Month - 1, paivat.Day - 1);
                jaljellaVuodet = paivat.Year - 1;
            }
            catch (Exception ee)
            {
                Console.WriteLine("Ohjelma ei osannut laskea päivämääräerotusta. Tarkista pvm-formaatti!");
                Console.WriteLine(ee.Message);
                vuodetKuukaudetPaivat = "";
                aikaJaljella = 0;//Jos pvm-formaatti on väärä, antaa vastauksen 0 tuntia.
            }
            if (jaljellaVuodet < 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Beep();
            }
            else if (jaljellaVuodet < 20)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("Odotettua elinaikaa jäljellä " + vuodetKuukaudetPaivat + ".");
            
        }
    }
}
