using System;
using System.IO;
using System.Threading;

namespace Struktury
{
    class Program
    {
        const string IMIE = "[Imie]";
        const string NAZWISKO = "[Nazwisko]";
        const string WIEK = "[Wiek]";

        public struct Osoba
        {
            public string Imie;
            public string Nazwisko;
            public int Wiek;
        }

        static void ZapiszStrukture(string a_sNazwaPliku,ref Osoba a_oOsoba)
        {
            using (StreamWriter _oPlikDoZapisu = new StreamWriter(a_sNazwaPliku))
            {
                _oPlikDoZapisu.WriteLine(a_oOsoba.Imie);
                _oPlikDoZapisu.WriteLine(a_oOsoba.Nazwisko);
                _oPlikDoZapisu.WriteLine(a_oOsoba.Wiek);
            }
        }

        static bool OdczytajStukture(string a_sNazwaPliku, ref Osoba a_oOsoba)
        {
            try
            {
                using (StreamReader _oPlikDoOdczytu = new StreamReader(a_sNazwaPliku))
                {
                    a_oOsoba.Imie = _oPlikDoOdczytu.ReadLine();
                    a_oOsoba.Nazwisko = _oPlikDoOdczytu.ReadLine();
                    a_oOsoba.Wiek = int.Parse(_oPlikDoOdczytu.ReadLine());
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd! {e.Message}");
            }

            return false;
        }

        static bool ZapiszStrukture2(string a_sNazwaPliku, ref Osoba a_oOsoba)
        {
            try
            {
                using (StreamWriter _oPlikDoZapisu = new StreamWriter(a_sNazwaPliku))
                {
                    _oPlikDoZapisu.WriteLine(IMIE);
                    _oPlikDoZapisu.WriteLine(a_oOsoba.Imie);
                    _oPlikDoZapisu.WriteLine(NAZWISKO);
                    _oPlikDoZapisu.WriteLine(a_oOsoba.Nazwisko);
                    _oPlikDoZapisu.WriteLine(WIEK);
                    _oPlikDoZapisu.WriteLine(a_oOsoba.Wiek);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd! {e.Message}");
            }

            return false;
        }


        static bool OdczytajStukture2(string a_sNazwaPliku, ref Osoba a_oOsoba)
        {
            try
            {
                using (StreamReader _oPlikDoOdczytu = new StreamReader(a_sNazwaPliku))
                {
                    //EndOfStream <- ma wartosc true jak skonczy sie plik
                    //EndOfStream <= ma wartosc false kiedy sa jeszcze dane

                    int _iTag = -1;
                    string _sLiniaTekstowa;

                    //Tag = 0 - Imie
                    //Tag = 1 - Nazwisko
                    //Tag = 2 - Wiek

                    while (!_oPlikDoOdczytu.EndOfStream)
                    {
                        _sLiniaTekstowa = _oPlikDoOdczytu.ReadLine();

                        if (_sLiniaTekstowa == IMIE)
                        {
                            _iTag = 0;
                        }
                        else if (_sLiniaTekstowa == NAZWISKO)
                        {
                            _iTag = 1;
                        }
                        else if (_sLiniaTekstowa == WIEK)
                        {
                            _iTag = 2;
                        }
                        else
                        {
                            switch (_iTag)
                            {
                                case 0:
                                    a_oOsoba.Imie = _sLiniaTekstowa; break;

                                case 1:
                                    a_oOsoba.Nazwisko = _sLiniaTekstowa; break;

                                case 2:
                                    {
                                        int _iWiek = 0;
                                        if (int.TryParse(_sLiniaTekstowa,out _iWiek))
                                        {
                                            a_oOsoba.Wiek = _iWiek;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd! {e.Message}");
            }

            return false;
        }


        static void Main(string[] args)
        {
            Osoba osoba = new Osoba
            {
                Imie = "Jacek",
                Nazwisko = "K.",
                Wiek = 39
            };

            if (ZapiszStrukture2("jacek2.txt", ref osoba) == true)
            {
                Osoba osoba2 = new Osoba();

                if (OdczytajStukture2("jacek2.txt", ref osoba2) == true)
                {
                    Console.WriteLine($"Imie={osoba2.Imie}");
                    Console.WriteLine($"Nazwisko={osoba2.Nazwisko}");
                    Console.WriteLine($"Wiek={osoba2.Wiek}");
                }
            }
        }
    }
}
