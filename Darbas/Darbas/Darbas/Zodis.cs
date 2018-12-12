using System;

namespace L4_U4_11
{
    /// <summary>
    /// Klasė, skirta duomenims apie žodžius aprašyti
    /// </summary>
    class Zodis
    {
        public string ZodzioPavadinimas { get; set; } //Žodis
        public int Pasikartojimai { get; set; } //Jo pasikartojimų skaičius
        public int Ilgis { get; set; } // Žodžio simbolių kiekis
        public int Eilute { get; set; } //Numeris eilutės, kurioje yra žodis

        /// <summary>
        /// Žodžio konstruktorius
        /// </summary>
        /// <param name="zodzioPavadinimas">Žodis</param>
        /// <param name="pasikartojimai">Pasikartojimų skaičius</param>
        /// <param name="ilgis">Eilutės numeris</param>
        public Zodis(string zodzioPavadinimas,int pasikartojimai, int ilgis, int eilute)
        {
            ZodzioPavadinimas = zodzioPavadinimas;
            Pasikartojimai = pasikartojimai;
            Ilgis = ilgis;
            Eilute = eilute;
        }

        /// <summary>
        /// Pakeičia ToString metodą
        /// </summary>
        /// <returns>Pakeistą ToString šabloną</returns>
        public override string ToString()
        {
            return String.Format("{0} {1} {2:d} {3} {4} {5} {6} {7} {8}", ".", "Žodis:", ZodzioPavadinimas, "|", "Pasikartojimai:", Pasikartojimai, "|", "Ilgis:", Ilgis);
        }
    }
}
