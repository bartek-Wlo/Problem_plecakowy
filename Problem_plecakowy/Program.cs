using System;
using System.Collections.Generic;
using System.Globalization; // LISTA


namespace ConsoleApp1
{
    class Przedmiot
    {
        private int nr_przedmiotu { get; }
        private int waga { get; }
        private int wartosc { get; }
        private decimal Wartosc_Masa;
        public int getNrPrzedmiotu() { return nr_przedmiotu; }
        public int getWaga() { return waga; }
        public int getWartosc() { return wartosc; }
        public decimal get_WartoscMasa() { return Wartosc_Masa; }

        public Przedmiot(int nr, int masa, int cena)
        {
            nr_przedmiotu = nr;
            waga = masa;
            wartosc = cena;
            Wartosc_Masa = (decimal)cena / masa;
        }
        public override string ToString()
        {
            return $"{nr_przedmiotu}.      {wartosc} [zł],      {waga} [kg],      {Wartosc_Masa} [zł/kg]";
        }

    }










    class Result
    {
        private List<int> ListaPrzedmiotow { get; }
        private int SumaWartosci;
        private int SumaMas;
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
    }









    class Problem
    {
        public Result Solve(int capacity)
        {
            if (capacity <= 0) { Console.Write("ERROR, za mały plecak!\n"); }
            Result rozwiazanie = new Result(capacity);
            List<Przedmiot> lista_sort = new List<Przedmiot>(lista_przedmiotow);
            lista_sort.Sort((a, b) => b.get_WartoscMasa().CompareTo(a.get_WartoscMasa()));
            foreach (Przedmiot tmp in lista_sort) {
                //Console.WriteLine(tmp); /* DISPLAY posortowane */
                rozwiazanie.DodajPrzedmiot(tmp.getNrPrzedmiotu(), tmp.getWartosc(), tmp.getWaga());
            }
            return rozwiazanie;
        }
        private int liczba_przedmiotow { get; }
        private List<Przedmiot> lista_przedmiotow { get; }
        public Problem(int seed, int ilosc_przedmiotow)
        {
            Random ran = new Random(seed);
            if (ilosc_przedmiotow == 0) liczba_przedmiotow = ran.Next(5, 15);
            else liczba_przedmiotow = ilosc_przedmiotow;
            lista_przedmiotow = new List<Przedmiot>();
            for (int i = 0; i < liczba_przedmiotow; ++i) lista_przedmiotow.Add(new Przedmiot(i, ran.Next(1, 10), ran.Next(1, 10)));
        }
        public void Display()
        {
            Console.WriteLine($"Liczba przedmiotów: {liczba_przedmiotow}\n");
            foreach (Przedmiot p in lista_przedmiotow) Console.WriteLine(p);
        }









    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj ilosc_przedmiotow: ");
            int ilosc_przedmiotow = int.Parse(Console.ReadLine());
            Console.WriteLine($"Całkowita liczba przedmiotów do zabrania= {ilosc_przedmiotow}");

            Console.Write("Podaj seed: ");
            int seed = int.Parse(Console.ReadLine());
            Console.WriteLine($"Podany seed do rand() = {seed}");


            Problem plecakowy = new Problem(seed, ilosc_przedmiotow);
            plecakowy.Display();
            Console.Write("Podaj pojemność plecaka: ");
            int Pojemnosc = int.Parse(Console.ReadLine());
            Console.Write(plecakowy.Solve(Pojemnosc));
        }
    }
}

