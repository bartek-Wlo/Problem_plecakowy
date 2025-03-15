using System;
using System.Collections.Generic;
using System.Globalization; // LISTA
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Test_Programu_plecakoego"), InternalsVisibleTo("Problem_plecakowy_GUI")]


namespace ConsoleApp1
{
    class Przedmiot
    {
        public int nr_przedmiotu {get;}
        public int waga {get;}
        public int wartosc {get;}
        public decimal Wartosc_Masa {get;}

        public Przedmiot(int nr, int masa, int cena)
        {
            nr_przedmiotu = nr;
            waga = masa;
            wartosc = cena;
            Wartosc_Masa = (decimal)cena / masa;
        }
        public override string ToString()
        {
            return $"{nr_przedmiotu,2}.     {wartosc,2} [zł],     {waga,2} [kg],    {Wartosc_Masa,8:F4} [zł/kg]";
        }

    }










    class Result
    {
        public List<int> ListaPrzedmiotow {get;}
        private int SumaWartosci;
        private int SumaMas; public int get_SumaMas() {return SumaMas;}
        private int Pojemnosc;

        public Result(int capacity)
        {
            ListaPrzedmiotow = new List<int>();
            SumaWartosci = 0;
            SumaMas = 0;
            Pojemnosc = capacity;
        }

        public bool DodajPrzedmiot(int nr, int wartosc, int masa)
        {
            if (SumaMas + masa > Pojemnosc) return false;
            ListaPrzedmiotow.Add(nr);
            SumaWartosci += wartosc;
            SumaMas += masa;
            return true;
        }

        public override string ToString()
        {
            string przedmioty = ListaPrzedmiotow.Count > 0 ? string.Join(", ", ListaPrzedmiotow) : "Brak przedmiotów";
            return $"Items: {przedmioty}\nTotal value: {SumaWartosci}\nTotal weight: {SumaMas}";
        }

        public string[] ToThreeStrings()
        {
            string[] ret_string = new string[4];
            ret_string[0] = $"Suma wartości przedmiotów: {SumaWartosci}";
            ret_string[1] = $"Suma mas przedmiotów: {SumaMas}";
            ret_string[2] = "ID, przedmitów: ";
            ret_string[3] = ListaPrzedmiotow.Count > 0 ? string.Join(", ", ListaPrzedmiotow) : "Brak przedmiotów";
            return ret_string;
        }
    }









    class Problem
    {
        
        private int liczba_przedmiotow;
        public List<Przedmiot> ListaPrzedmiotow { get; }
        public List<Przedmiot> lista_sort { get; set; }

        public Result Solve(int capacity)
        {
            if (capacity <= 0) { Console.Write("ERROR, za mały plecak!\n"); }
            Result rozwiazanie = new Result(capacity);
            /* lista_sort = new List<Przedmiot>(ListaPrzedmiotow);
            Skrócona inicjalizacja:                             */
            lista_sort = [.. ListaPrzedmiotow];
            lista_sort.Sort((a, b) => b.Wartosc_Masa.CompareTo(a.Wartosc_Masa));
            foreach (Przedmiot tmp in lista_sort) {
                //Console.WriteLine(tmp); /* DISPLAY posortowane */
                rozwiazanie.DodajPrzedmiot(tmp.nr_przedmiotu, tmp.wartosc, tmp.waga);
            }
            return rozwiazanie;
        }

        public Problem(int seed, int ilosc_przedmiotow)
        {
            Random ran = new Random(seed);
            if (ilosc_przedmiotow == 0) liczba_przedmiotow = ran.Next(5, 15);
            else liczba_przedmiotow = ilosc_przedmiotow;
            ListaPrzedmiotow = new List<Przedmiot>();
            lista_sort = new List<Przedmiot>();
            for (int i = 0; i < liczba_przedmiotow; ++i) ListaPrzedmiotow.Add(new Przedmiot(i, ran.Next(1, 20), ran.Next(1, 20)));
        }

        public override string ToString()
        {
            string ret = $"Liczba przedmiotów: {liczba_przedmiotow}\n";
            foreach (Przedmiot p in ListaPrzedmiotow) ret += $"{p}\n";
            return ret;
        }

        public string[] ToFewStrings()
        {
            string[] ret_string = new string[ListaPrzedmiotow.Count];
            for (int i = 0; i < ListaPrzedmiotow.Count; ++i) ret_string[i] =  ListaPrzedmiotow[i].ToString();
            return ret_string;
        }
    }








    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj ilosc_przedmiotow: ");
            int ilosc_przedmiotow = int.Parse(Console.ReadLine() ?? "10");
            Console.WriteLine($"Całkowita liczba przedmiotów do zabrania: {ilosc_przedmiotow}");

            Console.Write("Podaj seed: ");
            int seed = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine($"Podany seed do Random({seed}).");


            Problem plecakowy = new Problem(seed, ilosc_przedmiotow);
            Console.Write(plecakowy);
            Console.Write("Podaj pojemność plecaka: ");
            int Pojemnosc = int.Parse(Console.ReadLine() ?? "8");
            Console.Write(plecakowy.Solve(Pojemnosc));
        }
    }
}
