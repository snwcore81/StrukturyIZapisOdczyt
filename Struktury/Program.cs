using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Struktury
{
    class Program
    {
        const string OSOBA = "[Osoba]";
        const string IMIE = "[Imie]";
        const string NAZWISKO = "[Nazwisko]";
        const string WIEK = "[Wiek]";
        const string PLEC = "[Plec]";

        public enum Plec
        {
            Mezczyzna,
            Kobieta,
            Pozostale
        }

        public struct Osoba
        {
            public string Imie;
            public string Nazwisko;
            public int Wiek;
            public Plec Plec;  
        }

        static void ZapiszStrukture(string a_sNazwaPliku,ref Osoba a_oOsoba)
        {
            using (StreamWriter _oPlikDoZapisu = new StreamWriter(a_sNazwaPliku))
            {
                _oPlikDoZapisu.WriteLine(a_oOsoba.Imie);
                _oPlikDoZapisu.WriteLine(a_oOsoba.Nazwisko);
                _oPlikDoZapisu.WriteLine(a_oOsoba.Wiek);
                _oPlikDoZapisu.WriteLine(a_oOsoba.Plec);
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
                    a_oOsoba.Plec = Enum.Parse<Plec>(_oPlikDoOdczytu.ReadLine());
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
                    _oPlikDoZapisu.WriteLine(PLEC);
                    _oPlikDoZapisu.WriteLine(a_oOsoba.Plec);
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
                    //Tag = 3 - Plec

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
                        else if (_sLiniaTekstowa == PLEC)
                        {
                            _iTag = 3;
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

                                case 3:
                                    {
                                        Plec _ePlec = Plec.Pozostale;

                                        if (Enum.TryParse<Plec>(_sLiniaTekstowa, out _ePlec))
                                        {
                                            a_oOsoba.Plec = _ePlec;
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

        static bool OdczytajStuktury(string a_sNazwaPliku, out Osoba[] a_oOsoby)
        {
            a_oOsoby = null;

            try
            {
                using (StreamReader _oPlikDoOdczytu = new StreamReader(a_sNazwaPliku))
                {
                    //EndOfStream <- ma wartosc true jak skonczy sie plik
                    //EndOfStream <= ma wartosc false kiedy sa jeszcze dane

                    int _iRozmiarTablicy = int.Parse(_oPlikDoOdczytu.ReadLine());

                    if (_iRozmiarTablicy < 1)
                        throw new Exception("Niepoprawny rozmiar tablicy!");

                    int _iIndeksWTablicy = -1;

                    a_oOsoby = new Osoba[_iRozmiarTablicy];

                    int _iTag = -1;
                    string _sLiniaTekstowa;

                    //Tag = 0 - Imie
                    //Tag = 1 - Nazwisko
                    //Tag = 2 - Wiek
                    //Tag = 3 - Plec

                    while (!_oPlikDoOdczytu.EndOfStream)
                    {
                        _sLiniaTekstowa = _oPlikDoOdczytu.ReadLine();

                        if (_sLiniaTekstowa == OSOBA)
                        {
                            _iIndeksWTablicy++;

                            if (_iIndeksWTablicy> _iRozmiarTablicy-1)
                            {
                                return true;
                            }

                            a_oOsoby[_iIndeksWTablicy] = new Osoba();
                            _iTag = -1;
                        }
                        else if (_sLiniaTekstowa == IMIE)
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
                        else if (_sLiniaTekstowa == PLEC)
                        {
                            _iTag = 3;
                        }
                        else
                        {
                            switch (_iTag)
                            {
                                case 0:
                                    a_oOsoby[_iIndeksWTablicy].Imie = _sLiniaTekstowa; break;

                                case 1:
                                    a_oOsoby[_iIndeksWTablicy].Nazwisko = _sLiniaTekstowa; break;

                                case 2:
                                    {
                                        int _iWiek = 0;
                                        if (int.TryParse(_sLiniaTekstowa, out _iWiek))
                                        {
                                            a_oOsoby[_iIndeksWTablicy].Wiek = _iWiek;
                                        }
                                    }
                                    break;

                                case 3:
                                    {
                                        Plec _ePlec = Plec.Pozostale;

                                        if (Enum.TryParse<Plec>(_sLiniaTekstowa, out _ePlec))
                                        {
                                            a_oOsoby[_iIndeksWTablicy].Plec = _ePlec;
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

        static bool ZapiszStruktury(string a_sNazwaPliku, Osoba[] a_oOsoby)
        {
            if (a_oOsoby == null)
                return false;

            try
            {
                using (StreamWriter _oPlikDoZapisu = new StreamWriter(a_sNazwaPliku))
                {
                    _oPlikDoZapisu.WriteLine(a_oOsoby.Length);

                    for (int i=0;i<a_oOsoby.Length;i++)
                    {
                        _oPlikDoZapisu.WriteLine(OSOBA);
                        _oPlikDoZapisu.WriteLine(IMIE);
                        _oPlikDoZapisu.WriteLine(a_oOsoby[i].Imie);
                        _oPlikDoZapisu.WriteLine(NAZWISKO);
                        _oPlikDoZapisu.WriteLine(a_oOsoby[i].Nazwisko);
                        _oPlikDoZapisu.WriteLine(WIEK);
                        _oPlikDoZapisu.WriteLine(a_oOsoby[i].Wiek);
                        _oPlikDoZapisu.WriteLine(PLEC);
                        _oPlikDoZapisu.WriteLine(a_oOsoby[i].Plec);
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
            /*
            Osoba osoba = new Osoba
            {
                Imie = "Jacek",
                Nazwisko = "K.",
                Wiek = 39,
                Plec = Plec.Mezczyzna
            };

            ZapiszStrukture2("osoba1.txt", ref osoba);

            */

            /*
            Osoba[] osoby;

            if (OdczytajStuktury("osoba1.txt", out osoby))
            {
                for (int i=0;i<osoby.Length;i++)
                {
                    Console.WriteLine($"Imie={osoby[i].Imie}");
                    Console.WriteLine($"Nazwisko={osoby[i].Nazwisko}");
                    Console.WriteLine($"Wiek={osoby[i].Wiek}");
                    Console.WriteLine($"Plec={osoby[i].Plec}");
                }
            }
            */

            /*
            Osoba[] osoby = new Osoba[3];

            osoby[0] = new Osoba
            {
                Imie = "Jacek",
                Nazwisko = "K.",
                Wiek = 39,
                Plec = Plec.Mezczyzna
            };

            osoby[1] = new Osoba
            {
                Imie = "Andrzej",
                Nazwisko = "F.",
                Wiek = 28,
                Plec = Plec.Mezczyzna
            };

            osoby[2] = new Osoba
            {
                Imie = "Anna",
                Nazwisko = "M.",
                Wiek = 40,
                Plec = Plec.Kobieta
            };

            ZapiszStruktury("osoby.txt", osoby);
            */

            List<Osoba> listaOsob = new List<Osoba>();

            listaOsob.Add(new Osoba
            {
                Imie = "Jacek",
                Nazwisko = "K.",
                Wiek = 39,
                Plec = Plec.Mezczyzna
            });

            listaOsob.Add(new Osoba
            {
                Imie = "Anna",
                Nazwisko = "M.",
                Wiek = 40,
                Plec = Plec.Kobieta
            });

            listaOsob.Add(new Osoba
            {
                Imie = "Andrzej",
                Nazwisko = "F.",
                Wiek = 28,
                Plec = Plec.Mezczyzna
            });

            listaOsob.RemoveAt(0);

            foreach (Osoba osoba in listaOsob)
            {
                Console.WriteLine($"Imie={osoba.Imie}");
                Console.WriteLine($"Nazwisko={osoba.Nazwisko}");
                Console.WriteLine($"Wiek={osoba.Wiek}");
                Console.WriteLine($"Plec={osoba.Plec}");
            }

            /*
            Osoba[] osoby;

            if (OdczytajStuktury("osoby.txt", out osoby))
            {
                for (int i = 0; i < osoby.Length; i++)
                {
                    Console.WriteLine($"Imie={osoby[i].Imie}");
                    Console.WriteLine($"Nazwisko={osoby[i].Nazwisko}");
                    Console.WriteLine($"Wiek={osoby[i].Wiek}");
                    Console.WriteLine($"Plec={osoby[i].Plec}");
                }
            }
            */

        }
    }
}
