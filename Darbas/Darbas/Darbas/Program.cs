using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace L4_U4_11
{
    class Program
    {
        public const int VisuZodziuDydis = 100000;  //Masyvo, kuriame talpinami visi teksto žodžiai, dydis

        const string Knyga1 = "Knyga1.txt"; //Pirmas duomenų failas.
        const string Knyga2 = "Knyga2.txt"; //Antras duomenų failas.
        const string Rodikliai = "Rodikliai.txt"; // Pirmas rezultatų failas.
        const string ManoKnyga = "ManoKnyga.txt"; // Antras rezultatų failas.
        public static char[] skyrikliai = { ' ', '.', ',', '!', '?', ':', ';', '(', ')', '\t', '\r', '\n' }; //Visi skyrikliai.
        public static char[] skyrikliai2 = { '.', '!', '?', ';' }; //Sakinio pabaigos skyrikliai

        public static void Main(string[] args)
        {
            //Trinami failai, kad nesikartotų tekstas.
            if (File.Exists(ManoKnyga) && File.Exists(Rodikliai))
            {
                File.Delete(ManoKnyga);
                File.Delete(Rodikliai);
            }

            Console.OutputEncoding = Encoding.UTF8; //Konsolėje rašomos lietuviškos raidės
            Program p = new Program(); //Sukuriamas Program klasės objektas

            //Sukuriami objektai, kuriuose talpinami žodžiai su skyrikliais
            ZodziuKonteineris Knygos1ZodziaiSuSkyrikliais = new ZodziuKonteineris(VisuZodziuDydis);
            ZodziuKonteineris Knygos2ZodziaiSuSkyrikliais = new ZodziuKonteineris(VisuZodziuDydis);

            //Sukuriami obejktai, kuriuose talpinami žodžiai be skyriklių
            ZodziuKonteineris Knygos1ZodziaiBeSkyrikliu = new ZodziuKonteineris(VisuZodziuDydis);
            ZodziuKonteineris Knygos2ZodziaiBeSkyrikliu = new ZodziuKonteineris(VisuZodziuDydis);

            //Apdoroja failus, ir sudeda žodžius į atitinkamus konteinerius
            p.Apdorojimas(Knyga1, skyrikliai, ref Knygos1ZodziaiSuSkyrikliais, ref Knygos1ZodziaiBeSkyrikliu);
            p.Apdorojimas(Knyga2, skyrikliai, ref Knygos2ZodziaiSuSkyrikliais, ref Knygos2ZodziaiBeSkyrikliu);

            //Žodžių konteineris, talpinantis ilgiausius žodžius
            ZodziuKonteineris IlgiausiuZodziuKonteneris = new ZodziuKonteineris(VisuZodziuDydis);
            p.IlgiausiZodziai(Knygos1ZodziaiBeSkyrikliu, Knygos2ZodziaiBeSkyrikliu, IlgiausiuZodziuKonteneris);

            string trumpiausiasSakinys1 = "";
            string trumpiausiasSakinys2 = "";
            int trumpiausioSakinioVieta1 = 0;
            int trumpiausioSakinioVieta2 = 0;
            int zodziuSkaicius1 = 0;
            int zodziuSkaicius2 = 0;

            //Metodai, kurie randa trumpiausio sakinio vietą.
            trumpiausioSakinioVieta1 = p.TrumpiausioSakinioVieta(Knyga1, skyrikliai2, skyrikliai, trumpiausiasSakinys1, zodziuSkaicius1);
            trumpiausioSakinioVieta2 = p.TrumpiausioSakinioVieta(Knyga2, skyrikliai2, skyrikliai, trumpiausiasSakinys2, zodziuSkaicius2);

            //Metodai, kurie randa trumpiausia sakinį, ir jo žodžių skaičių.
            p.TrumpiausiasSakinys(Knyga1, skyrikliai2, skyrikliai, out trumpiausiasSakinys1, out zodziuSkaicius1);
            p.TrumpiausiasSakinys(Knyga2, skyrikliai2, skyrikliai, out trumpiausiasSakinys2, out zodziuSkaicius2);

            //Metodas, kuris spausdina į ekraną atliktas 1 ir 2 užduotis.
            p.SpausdinimasEkrane(IlgiausiuZodziuKonteneris, trumpiausiasSakinys1, trumpiausioSakinioVieta1, trumpiausiasSakinys2, trumpiausioSakinioVieta2, zodziuSkaicius1, zodziuSkaicius2);
            //Metodas, kuris spausdina į failą atliktas 1 ir 2 užduotis.
            p.SpausdinimasFaile(Rodikliai, IlgiausiuZodziuKonteneris, trumpiausiasSakinys1, trumpiausioSakinioVieta1, trumpiausiasSakinys2, trumpiausioSakinioVieta2, zodziuSkaicius1, zodziuSkaicius2);

            StringBuilder Tekstas = new StringBuilder();  //Bendro teksto objektas
            Tekstas = p.TekstoPertvarkymas(Knygos1ZodziaiSuSkyrikliais, Knygos2ZodziaiSuSkyrikliais); //Sudaro vieną tekstą

            //Patikrina ar tekstas yra, jeigu taip, tada jį atspausdina
            if (Tekstas.Length == 0)
            {
                Console.WriteLine("Duomenų failuose nėra");
                p.PertvarkytoTekstoSpausdinimasFaile(ManoKnyga, Knygos1ZodziaiSuSkyrikliais, Knygos2ZodziaiSuSkyrikliais);
            }
            else
            {
                p.PertvarkytoTekstoSpausdinimasFaile(ManoKnyga, Knygos1ZodziaiSuSkyrikliais, Knygos2ZodziaiSuSkyrikliais);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Apdoroja žodžius
        /// </summary>
        /// <param name="line">Eilutė</param>
        /// <param name="skyrikliai">Visi skyrikliai</param>
        /// <param name="ZodziaiSuSKyrikliais">Žodžiai su skyrikliais</param>
        /// <param name="ZodziaiBeSkyrikliu">Žodžiai be skyriklių</param>
        /// <param name="eilute">Eilutės numeris</param>
        void ZodziuApdorojimas(string line, char[] skyrikliai, ref ZodziuKonteineris ZodziaiSuSKyrikliais, ref ZodziuKonteineris ZodziaiBeSkyrikliu, int eilute)
        {
            string[] dalys = line.Split(' ');
            foreach (string dalis in dalys)
            {
                if (dalis.Length > 0)
                {
                    Zodis Zodziukas = new Zodis(dalis.ToLower(), 0, dalis.Length, eilute);
                    ZodziaiSuSKyrikliais.PridetiZodi(Zodziukas);
                }
            }
            string[] zodziai = line.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);
            foreach (string zodis in zodziai)
            {
                Zodis zdz = new Zodis(zodis.ToLower(), 0, zodis.Length, eilute);
                ZodziaiBeSkyrikliu.PridetiZodi(zdz);
            }
        }

        /// <summary>
        /// Apdoroja duomenis
        /// </summary>
        /// <param name="failas">Duomenų failo pavadinimas</param>
        /// <param name="skyrikliai">Visi skyrikliai</param>
        /// <param name="ZodziaiSuSkyrikliais">Žodžiai su skyrikliais</param>
        /// <param name="ZodziaiBeSkyrikliu">Žodžiai be skyriklių</param>
        void Apdorojimas(string failas, char[] skyrikliai, ref ZodziuKonteineris ZodziaiSuSkyrikliais, ref ZodziuKonteineris ZodziaiBeSkyrikliu)
        {
            int eilute = 0;
            string[] lines = File.ReadAllLines(failas, Encoding.UTF8);
            foreach (string line in lines)
            {
                eilute++;
                if (line.Length > 0)
                {
                    ZodziuApdorojimas(line, skyrikliai, ref ZodziaiSuSkyrikliais, ref ZodziaiBeSkyrikliu, eilute);
                }
            }
        }

        /// <summary>
        /// Suranda 10 ilgiausių žodžių ir juos sudeda į konteinerį
        /// </summary>
        /// <param name="Knygos1ZodziaiBeSkyrikliu">Pirmo duomenų failo žodžiai be skiriklų</param>
        /// <param name="Knygos2ZodziaiBeSkyrikliu">Antro duomenų failo žodžiai be skiriklų</param>
        /// <param name="IlgiausiuZodziuKonteneris">10 ilgiausių žodžių</param>
        /// <returns></returns>
        ZodziuKonteineris IlgiausiZodziai(ZodziuKonteineris Knygos1ZodziaiBeSkyrikliu, ZodziuKonteineris Knygos2ZodziaiBeSkyrikliu, ZodziuKonteineris IlgiausiuZodziuKonteneris)
        {
            bool jauYra = false;
            ZodziuKonteineris temp = new ZodziuKonteineris(30);

            for (int i = 0; i < Knygos1ZodziaiBeSkyrikliu.ZodziuSkaicius; i++)
            {
                jauYra = false;
                for (int j = 0; j < Knygos2ZodziaiBeSkyrikliu.ZodziuSkaicius; j++)
                {
                    if (Knygos1ZodziaiBeSkyrikliu.GautiZodi(i).ZodzioPavadinimas.ToLower() == Knygos2ZodziaiBeSkyrikliu.GautiZodi(j).ZodzioPavadinimas.ToLower())
                    {
                        jauYra = true;
                        break;
                    }
                }
                if (jauYra == false)
                {
                    int pasikartojaIndexas = temp.PasikartojancioIndexas(Knygos1ZodziaiBeSkyrikliu.GautiZodi(i));
                    if (pasikartojaIndexas < 0)
                    {
                        temp.PridetiZodi(Knygos1ZodziaiBeSkyrikliu.GautiZodi(i));
                        continue;
                    }
                    temp.GautiZodi(pasikartojaIndexas).Pasikartojimai += 1;
                }
            }
            temp = Rikiavimas(temp);
            for (int i = 0; i <= temp.ZodziuSkaicius; i++)
            {
                if (i >= 10)
                {
                    break;
                }
                IlgiausiuZodziuKonteneris.PridetiZodi(temp.GautiZodi(i));
            }
            return IlgiausiuZodziuKonteneris;

        }

        /// <summary>
        /// Surikiuoja 10 ilgiausių žodžių mažėjimo tvarka
        /// </summary>
        /// <param name="Rikiuojamas">Rikiuojamas konteineris</param>
        /// <returns>Surikiuotą žodžių konteinerį</returns>
        static ZodziuKonteineris Rikiavimas(ZodziuKonteineris Rikiuojamas)
        {
            for (int i = 0; i < Rikiuojamas.ZodziuSkaicius; i++)
            {
                for (int j = 0; j < Rikiuojamas.ZodziuSkaicius - 1; j++)
                {
                    if (Rikiuojamas.GautiZodi(j).Ilgis < Rikiuojamas.GautiZodi(j + 1).Ilgis)
                    {
                        Rikiuojamas.Swap(j, j + 1);
                    }
                }
            }
            return Rikiuojamas;
        }

        /// <summary>
        /// Suranda trumpiausią sakinį ir jo žodžių skaičių
        /// </summary>
        /// <param name="knyga">Duomenų failas</param>
        /// <param name="skyrikliai2">Sakinio pabaigos skyrikliai</param>
        /// <param name="skyrikliai">Visi skyrikliai</param>
        /// <param name="trumpiausiasSakinys">Trumpiasias sakinys</param>
        /// <param name="zodziuSkaicius">Trumpiausio sakinio žodžių skaičius</param>
        void TrumpiausiasSakinys(string knyga, char[] skyrikliai2, char[] skyrikliai, out string trumpiausiasSakinys, out int zodziuSkaicius)
        {
            string tekstas = File.ReadAllText(knyga).Replace("\n", String.Empty).Replace(". ", ".").Replace("? ", "?").Replace("! ", "!").Replace("; ", ";"); ;
            string[] sakiniai = tekstas.Split(skyrikliai2);

            zodziuSkaicius = 10000;
            trumpiausiasSakinys = "";
            foreach (string sakinys in sakiniai)
            {
                string[] zodziai = sakinys.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);

                if (zodziai.Length < zodziuSkaicius && ArDaugiauNeiTrys(sakinys) == true)
                {
                    zodziuSkaicius = zodziai.Length;
                    trumpiausiasSakinys = sakinys;
                }
            }
        }

        /// <summary>
        /// Suranda trumpiausio sakinio vietą
        /// </summary>
        /// <param name="knyga">Duomenų failas</param>
        /// <param name="skyrikliai2">Sakinio pabaigos skyrikliai</param>
        /// <param name="skyrikliai">Visi skyrikliai</param>
        /// <param name="trumpiausiasSakinys">Trumpiasias sakinys</param>
        /// <param name="zodziuSkaicius">Trumpiausio sakinio žodžių skaičius</param>
        /// <returns></returns>
        int TrumpiausioSakinioVieta(string knyga, char[] skyrikliai2, char[] skyrikliai, string trumpiausiasSakinys, int zodziuSkaicius)
        {
            string tekstas = File.ReadAllText(knyga).Replace("\n", String.Empty);

            int simbSkaicius = 0;
            int eilutesNr = 0;
            int atsakymas = 0;

            TrumpiausiasSakinys(knyga, skyrikliai2, skyrikliai, out trumpiausiasSakinys, out zodziuSkaicius);

            int trumpiausioIndeksas = tekstas.IndexOf(trumpiausiasSakinys);

            string[] eilutes = File.ReadAllLines(knyga, Encoding.GetEncoding(1257));
            
            foreach (string eilute in eilutes)
            {
                eilutesNr++;    
                simbSkaicius = simbSkaicius + eilute.Length;

                if (simbSkaicius > trumpiausioIndeksas)
                {
                    atsakymas = eilutesNr;
                    break;
                }
            }
            return atsakymas;
        }

        /// <summary>
        /// Tikrina ar yra daugiau negu 3 žodžiai
        /// </summary>
        /// <param name="sakinys">Sakinys</param>
        /// <returns>Sakinį, kuriame yra daugiau negu 3 žodžiai</returns>
        static bool ArDaugiauNeiTrys(string sakinys)
        {
            string[] zodziai = sakinys.Split(' ');
            if (zodziai.Length > 3)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodas, kuris tikrina du tekstus ir juos jungia į vieną
        /// </summary>
        /// <param name="Knygos1ZodziaiSuSkyrikliais">Pirmo failo žodžiai su skyrikliais</param>
        /// <param name="Knygos2ZodziaiSuSkyrikliais">Antro failo žodžiai su skyrikliais</param>
        /// <returns>Sudarytą bendrą tekstą</returns>
        StringBuilder TekstoPertvarkymas(ZodziuKonteineris Knygos1ZodziaiSuSkyrikliais, ZodziuKonteineris Knygos2ZodziaiSuSkyrikliais)
        {
            StringBuilder VisasTekstas = new StringBuilder();
            int k = 0;
            int AntroZodzioPradzia = 0;
            for (int i = 0; i < Knygos1ZodziaiSuSkyrikliais.ZodziuSkaicius; i++)
            {
                string PirmasZodis = Regex.Replace(Knygos1ZodziaiSuSkyrikliais.GautiZodi(i).ZodzioPavadinimas, "[.,;()\"]", String.Empty);
                string AntrasZodis = Regex.Replace(Knygos2ZodziaiSuSkyrikliais.GautiZodi(AntroZodzioPradzia).ZodzioPavadinimas, "[.,;()\"]", String.Empty);
                if (PirmasZodis != AntrasZodis)
                {
                    VisasTekstas.Append(Knygos1ZodziaiSuSkyrikliais.GautiZodi(i).ZodzioPavadinimas);
                    VisasTekstas.Append(" ");
                }
                else
                {
                    if (i + 1 < Knygos1ZodziaiSuSkyrikliais.ZodziuSkaicius)
                    {
                        PirmasZodis = Regex.Replace(Knygos1ZodziaiSuSkyrikliais.GautiZodi(i + 1).ZodzioPavadinimas, "[.,;()\"]", String.Empty);
                    }
                    else
                    {

                        break;
                    }
                    for (int g = AntroZodzioPradzia; g < Knygos2ZodziaiSuSkyrikliais.ZodziuSkaicius; g++)
                    {
                        AntrasZodis = Regex.Replace(Knygos2ZodziaiSuSkyrikliais.GautiZodi(g).ZodzioPavadinimas, "[.,;()\"]", String.Empty);
                        if (PirmasZodis != AntrasZodis)
                        {
                            VisasTekstas.Append(Knygos2ZodziaiSuSkyrikliais.GautiZodi(g).ZodzioPavadinimas);
                            VisasTekstas.Append(" ");
                            if (g == Knygos2ZodziaiSuSkyrikliais.ZodziuSkaicius - 1)
                            {
                                AntroZodzioPradzia = Knygos2ZodziaiSuSkyrikliais.ZodziuSkaicius - 1;
                            }
                        }
                        else
                        {

                            if (g + 1 < Knygos2ZodziaiSuSkyrikliais.ZodziuSkaicius - 1)
                            {
                                AntroZodzioPradzia = g + 1;
                            }
                            else
                            {
                                AntroZodzioPradzia = Knygos2ZodziaiSuSkyrikliais.ZodziuSkaicius;

                            }
                            break;
                        }
                    }
                }
            }

            if (AntroZodzioPradzia < Knygos2ZodziaiSuSkyrikliais.ZodziuSkaicius - 1)
            {
                for (int i = AntroZodzioPradzia; i < Knygos2ZodziaiSuSkyrikliais.ZodziuSkaicius; i++)
                {
                    VisasTekstas.Append(Knygos2ZodziaiSuSkyrikliais.GautiZodi(i).ZodzioPavadinimas);
                    VisasTekstas.Append(" ");
                }
            }
            return VisasTekstas;
        }

        /// <summary>
        /// Spausdina atliktas 1 ir 2 užduotis į ekraną
        /// </summary>
        /// <param name="IlgiausiuZodziuKonteneris">Ilgiausi 10 žodžių</param>
        /// <param name="trumpiausiasSakinys1">Pirmo duomenų failo trumpiausias sakinys</param>
        /// <param name="trumpiausioSakinioVieta1">Pirmo duomenų failo trumpiausio sakinio vieta</param>
        /// <param name="trumpiausiasSakinys2">Antro duomenų failo trumpiausias sakinys</param>
        /// <param name="trumpiausioSakinioVieta2">Antro duomenų failo trumpiausio sakinio vieta</param>
        /// <param name="zodziuSkaicius1">Pirmo duomenų failo trumpiausio sakinio žodžių skaičius</param>
        /// <param name="zodziuSkaicius2">Antro duomenų failo trumpiausio sakinio žodžių skaičius</param>
        void SpausdinimasEkrane(ZodziuKonteineris IlgiausiuZodziuKonteneris, string trumpiausiasSakinys1, int trumpiausioSakinioVieta1, string trumpiausiasSakinys2, int trumpiausioSakinioVieta2, int zodziuSkaicius1, int zodziuSkaicius2)
        {
            Console.WriteLine("Žodžiai, jų pasikartojimų skaičius ir ilgis:");
            Console.WriteLine("");
            for (int i = 0; i < IlgiausiuZodziuKonteneris.ZodziuSkaicius; i++)
            {
                Console.WriteLine("{0}{1}", i + 1, IlgiausiuZodziuKonteneris.GautiZodi(i).ToString() + "\n");
            }

            Console.WriteLine("");
            Console.WriteLine("Trumpiausias pirmo duomenų failo sakinys: " + "\n" + '"' + trumpiausiasSakinys1 + '"' + "\n" + "\n" + "Sakinios pradžios eilutės Nr.: " + trumpiausioSakinioVieta1 + "\n" + "Sakinio ilgis simboliais: " + trumpiausiasSakinys1.Length + "\n" + "Sakinio žodžių skaičius: " + zodziuSkaicius1);
            Console.WriteLine("");
            Console.WriteLine("Trumpiausias antro duomenų failo sakinys: " + "\n" + '"' + trumpiausiasSakinys2 + '"' + "\n" + "\n" + "Sakinios pradžios eilutės Nr.: " + trumpiausioSakinioVieta2 + "\n" + "Sakinio ilgis simboliais: " + trumpiausiasSakinys2.Length + "\n" + "Sakinio žodžių skaičius: " + zodziuSkaicius2);
        }

        /// <summary>
        /// Spausdinama atlikta 1 ir 2 užduotys į nurodytą duomenų failą
        /// </summary>
        /// <param name="rodikliai">Failo pavadinimas į kurį spausdinama</param>
        /// <param name="IlgiausiuZodziuKonteneris">Ilgiausi 10 žodžių</param>
        /// <param name="trumpiausiasSakinys1">Pirmo duomenų failo trumpiausias sakinys</param>
        /// <param name="trumpiausioSakinioVieta1">Pirmo duomenų failo trumpiausio sakinio vieta</param>
        /// <param name="trumpiausiasSakinys2">Antro duomenų failo trumpiausias sakinys</param>
        /// <param name="trumpiausioSakinioVieta2">Antro duomenų failo trumpiausio sakinio vieta</param>
        /// <param name="zodziuSkaicius1">Pirmo duomenų failo trumpiausio sakinio žodžių skaičius</param>
        /// <param name="zodziuSkaicius2">Antro duomenų failo trumpiausio sakinio žodžių skaičius</param>
        void SpausdinimasFaile(string rodikliai, ZodziuKonteineris IlgiausiuZodziuKonteneris, string trumpiausiasSakinys1, int trumpiausioSakinioVieta1, string trumpiausiasSakinys2, int trumpiausioSakinioVieta2, int zodziuSkaicius1, int zodziuSkaicius2)
        {
            using (StreamWriter writer = new StreamWriter(rodikliai))
            {
                writer.WriteLine("Žodžiai, jų pasikartojimų skaičius ir ilgis:");
                for (int i = 0; i < IlgiausiuZodziuKonteneris.ZodziuSkaicius; i++)
                {
                    writer.WriteLine(i + 1 + ". " + IlgiausiuZodziuKonteneris.GautiZodi(i).ToString() + "\n");
                }

                writer.WriteLine("");
                writer.WriteLine("Trumpiausias pirmo duomenų failo sakinys: " + "\n" + '"' + trumpiausiasSakinys1 + '"' + "\n" + "\n" + "Sakinios pradžios eilutės Nr.: " + trumpiausioSakinioVieta1 + "\n" + "Sakinio ilgis simboliais: " + trumpiausiasSakinys1.Length + "\n" + "Sakinio žodžių skaičius: " + zodziuSkaicius1);
                writer.WriteLine("");
                writer.WriteLine("Trumpiausias antro duomenų failo sakinys: " + "\n" + '"' + trumpiausiasSakinys2 + '"' + "\n" + "\n" + "Sakinios pradžios eilutės Nr.: " + trumpiausioSakinioVieta2 + "\n" + "Sakinio ilgis simboliais: " + trumpiausiasSakinys2.Length + "\n" + "Sakinio žodžių skaičius: " + zodziuSkaicius2);
            }
        }

        /// <summary>
        /// Spausdina į failą pertvarkytą tekstą
        /// </summary>
        /// <param name="failas">Failas, į kurį spausdinamas pertvarkytas tekstas</param>
        /// <param name="Knygos1ZodziaiSuSkyrikliais">Pirmo failo žodžiai su skyrikliais</param>
        /// <param name="Knygos2ZodziaiSuSkyrikliais">Antro failo žodžiai su skyrikliais</param>
        void PertvarkytoTekstoSpausdinimasFaile(string failas, ZodziuKonteineris Knygos1ZodziaiSuSkyrikliais, ZodziuKonteineris Knygos2ZodziaiSuSkyrikliais)
        {
            using (StreamWriter writer = new StreamWriter(failas, false, Encoding.UTF8))
            {
                if (TekstoPertvarkymas(Knygos1ZodziaiSuSkyrikliais, Knygos2ZodziaiSuSkyrikliais).Length == 0)
                {
                    writer.WriteLine("Duomenų failuose nėra");
                }
                else
                {
                    writer.WriteLine(TekstoPertvarkymas(Knygos1ZodziaiSuSkyrikliais, Knygos2ZodziaiSuSkyrikliais));
                }
            }
        }
    }
}