using System.Linq;

namespace L4_U4_11
{
    /// <summary>
    /// Žodžių konteineris
    /// </summary>
    class ZodziuKonteineris
    {
        private Zodis[] Zodziai { get; set; }
        public int ZodziuSkaicius { get; private set; }

        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="size">Masyvo dydis</param>
        public ZodziuKonteineris(int size)
        {
            Zodziai = new Zodis[size];
            ZodziuSkaicius = 0;
        }

        /// <summary>
        ///  Paimamas žodis iš masyvo pagal nurodytą indeksą 
        /// </summary>
        /// <param name="indeksas">Elemento vieta masyve</param>
        /// <returns></returns> 
        public Zodis GautiZodi(int indeksas)
        {
            return Zodziai[indeksas];
        }

        /// <summary>
        ///  Prideda Žodžius į masyvą 
        /// </summary>
        /// <param name="zodis">Pagal klasės Zodis šabloną aprašytas žodis</param>
        public void PridetiZodi(Zodis zodis)
        {
            Zodziai[ZodziuSkaicius++] = zodis;
        }

        /// <summary>
        /// Ieško ar masyve yra nurodytas žodis
        /// </summary>
        /// <param name="zodis">Pagal klasės Zodis šabloną aprašytas žodis</param>
        /// <returns></returns>
        public bool Contains(Zodis zodis)
        {
            return Zodziai.Contains(zodis);
        }

        /// <summary>
        /// Apkeičiami žodžiai vietomis
        /// </summary>
        /// <param name="pirmas">Pirmas žodis</param>
        /// <param name="antras">Antras žodis</param>
        public void Swap(int pirmas, int antras)
        {
            Zodis temp = Zodziai[pirmas];
            Zodziai[pirmas] = Zodziai[antras];
            Zodziai[antras] = temp;

        }

        /// <summary>
        /// Suranda pasikartojančio žodžio indeksą
        /// </summary>
        /// <param name="zodis">Žodis</param>
        /// <returns>Pasikartojančio indeksa arba -1</returns>
        public int PasikartojancioIndexas(Zodis zodis)
        {
            for (int i = 0; i < ZodziuSkaicius; i++)
            {
                if (Zodziai[i].ZodzioPavadinimas.ToLower() == zodis.ZodzioPavadinimas.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Patikrina ar yra tokio pavadinimo žodis masyve
        /// </summary>
        /// <param name="Pavadinimas">Pavadinimas</param>
        /// <returns>True or false, ar yra toks pavadinimas</returns>
        public bool ArYraToksPavadinimas(string Pavadinimas)
        {
            for (int i = 0; i < ZodziuSkaicius; i++)
            {
                if (Zodziai[i].ZodzioPavadinimas == Pavadinimas)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
