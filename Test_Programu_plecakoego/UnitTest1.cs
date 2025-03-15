using ConsoleApp1;
namespace Test_Programu_plecakoego
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1() // Czy poprawnie wylicza cenność przedmiotów
        {
            int a = 2, b = 5, c = 7; 
            Przedmiot rzecz = new Przedmiot(a,b,c);
            Assert.AreEqual((decimal)c/b, rzecz.Wartosc_Masa, "TestMethod1"); 
        }

        [ TestMethod ] // I Sprawdzenie, czy jeśli co najmniej jeden przedmiot spełnia ograniczenia, to zwrócono co najmniej jeden element.
        public void TEST01 ()
        {
            Result Rezultat;
            for(int i = 0, capacity = 10; capacity <= 100; ++i, ++capacity)
            {
                Rezultat = new Result(capacity);
                Assert.IsTrue(Rezultat.DodajPrzedmiot(i, 10, capacity/2), " 01 - 1. "); // Udało się zapakować dwa przedmioty
                Assert.IsTrue(Rezultat.DodajPrzedmiot(i, 10, capacity/2), " 01 - 2. "); // o masie == 1/2 ładowności
                Assert.IsFalse(Rezultat.DodajPrzedmiot(i, 10, 10), " 01 - 3. "); // 3 przednmiot już się nie mieści
            }    
        }

        [ TestMethod ] // II Sprawdzenie, czy jeśli żaden przedmiot nie spełnia ograniczeń, to zwrócono puste rozwiązanie.
        public void TEST02 ()
        {
            Result Rezultat;
            for(int capacity = 10; capacity <= 100; ++capacity)
            {
                for(int i = 1; i < 20; i += 1)
                {
                    Rezultat = new Result(capacity);
                    Assert.IsFalse(Rezultat.DodajPrzedmiot(0, 10, capacity+i), $" 02 - {capacity} < {capacity*i}");
                }
            }    
        }



        [ TestMethod ] // III Sprawdzenie poprawności wyniku dla konkretnej instancji.
        /* 1. Czy jest jakiś przedmiot który by się jeszcze zmieścił ale go nie dodano 
           2. Czy poprawnie sumuje
           3. Czy Suma zapakowanych przedniotów się zmieściłą do plecaka */
        public void TEST03 ()
        {
            for(int i = 10; i<100; i += 5)
            {
                int ilosc_przedmiotow = i;
                int seed = new Random().Next();
                Problem plecakowy = new Problem(seed, ilosc_przedmiotow);
                int Pojemnosc = i*3;
                Result res = plecakowy.Solve(Pojemnosc);
                List<int> TestowaPlecak = res.ListaPrzedmiotow;
                List<Przedmiot> TestowaWszystkie = plecakowy.ListaPrzedmiotow;
                //Console.Write(plecakowy);
                //Console.Write(res);

                int masa_pelnego_plecaka = res.get_SumaMas();
                int Wolna_przestrzen = Pojemnosc - masa_pelnego_plecaka;

                int suma_mas = 0;
                foreach (Przedmiot tmp in TestowaWszystkie)
                {
                    if(TestowaPlecak.Contains(tmp.nr_przedmiotu)) suma_mas += tmp.waga;
                    else Assert.IsTrue(tmp.waga > Wolna_przestrzen,$" 03 -> {tmp.waga} <= {Wolna_przestrzen}" );
                }
                // Poprawność sumowania
                Assert.IsTrue(suma_mas == res.get_SumaMas(), $" 03 - sumowanie {suma_mas} != {res.get_SumaMas()}");
                Assert.IsTrue(suma_mas <= Pojemnosc, $" 03 - pojemnosc {suma_mas} > {Pojemnosc}");
            }
        }



        [ TestMethod ] // IV Sprawdzanie czyj są uszeregowane od naj cenniejszych/masa do najmniej
        public void TEST04 ()
        {
            for(int i = 10; i<100; i += 5)
            {
                int ilosc_przedmiotow = i;
                int seed = new Random().Next();
                Problem plecakowy = new Problem(seed, ilosc_przedmiotow);
                int Pojemnosc = i*3;
                plecakowy.Solve(Pojemnosc);
                Assert.AreNotSame(plecakowy.lista_sort, plecakowy.ListaPrzedmiotow, " 04 - porównanie list");
                decimal poprzednia_cennosc = plecakowy.lista_sort.First().Wartosc_Masa;
                foreach (Przedmiot rzecz in plecakowy.lista_sort)
                {
                    Assert.IsTrue(poprzednia_cennosc >= rzecz.Wartosc_Masa, 
                        $"04 - Zła kolejnośc {poprzednia_cennosc} < {rzecz.Wartosc_Masa}");
                    poprzednia_cennosc = rzecz.Wartosc_Masa;
                }
            }
        }



        [ TestMethod ] // V Czy dodaje odpowiednią ilość przedmiotów
        public void TEST05 ()
        {
            for(int i = 10; i<100; i += 5)
            {
                int ilosc_przedmiotow = i;
                int seed = new Random().Next();
                Problem plecakowy = new Problem(seed, ilosc_przedmiotow);
                Assert.AreEqual(plecakowy.ListaPrzedmiotow.Count, ilosc_przedmiotow, " 05 liczba przedmiotów");
            }
            
        }
    }
}