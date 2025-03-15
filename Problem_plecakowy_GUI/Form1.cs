using System;
using System.Windows.Forms;
using ConsoleApp1;

namespace Problem_plecakowy_GUI
{
    public partial class Form1 : Form
    {
        private Label Opis_liczba_przedmiotow;    private TextBox Input_liczba_przedmiotow;
        private Label Opis_seed;                  private TextBox Input_seed;
        private Label Opis_pojemnosc_plecaka;     private TextBox Input_pojemnosc_plecaka;

        private Button przycisk_start;
        private ListBox Pole_textowe_zawartosc;
        private ListBox Pole_textowe_parametry;



        public Form1()
        {
            this.Text = "[.NET][CZ 15:15][LAB 1] \"Problem Plecakowy\"";
            this.Size = new System.Drawing.Size(620, 420);

            Opis_liczba_przedmiotow  = new Label {Text="Ilość przedmiotów:", Left=20, Top=20, Width=150};
            Opis_seed                = new Label {Text="Podaj seed:",        Left=20, Top=45, Width=150};
            Opis_pojemnosc_plecaka   = new Label {Text="Pojemność plecaka:", Left=20, Top=70, Width=150};
            Input_liczba_przedmiotow = new TextBox {Left=180, Top=20, Width=100, Text="10"};
            Input_seed               = new TextBox {Left=180, Top=45, Width=100, Text= "0"};
            Input_pojemnosc_plecaka  = new TextBox {Left=180, Top=70, Width=100, Text= "8"};

            przycisk_start = new Button {Text="Uruchom", Left=20, Top=95, Width=260};
            przycisk_start.Click += przycisk_start_funkcja!;

            Pole_textowe_parametry = new ListBox {Text="Parametry", Left= 20, Top=120, Width=260, Height=140};
            Pole_textowe_zawartosc = new ListBox {Text="Parametry", Left=320, Top=20,  Width=260, Height=360};

            this.Controls.Add(Opis_liczba_przedmiotow);  this.Controls.Add(Input_liczba_przedmiotow);
            this.Controls.Add(Opis_seed);                this.Controls.Add(Input_seed);
            this.Controls.Add(Opis_pojemnosc_plecaka);   this.Controls.Add(Input_pojemnosc_plecaka);
            this.Controls.Add(przycisk_start);
            this.Controls.Add(Pole_textowe_zawartosc);
            this.Controls.Add(Pole_textowe_parametry);
        }

        private void przycisk_start_funkcja(object sender, EventArgs e)
        {
            try
            {
                int iloscPrzedmiotow = int.Parse(Input_liczba_przedmiotow.Text);
                int seed             = int.Parse(              Input_seed.Text);
                int pojemnosc        = int.Parse( Input_pojemnosc_plecaka.Text);
                
                Problem plecakowy = new Problem(seed, iloscPrzedmiotow);
                Pole_textowe_parametry.Items.Clear();
                Pole_textowe_parametry.Items.Add($"Liczba wszystkich przedmiotów: {iloscPrzedmiotow}");
                Pole_textowe_parametry.Items.Add($"Seed generatora liczb losowych: {seed}");
                Pole_textowe_parametry.Items.Add($"Całkowita pojemność plecaka: {pojemnosc}");
                Pole_textowe_parametry.Items.Add($"");
                string[] tmp = plecakowy.Solve(pojemnosc).ToThreeStrings();
                Pole_textowe_parametry.Items.Add(tmp[0]);
                Pole_textowe_parametry.Items.Add(tmp[1]);
                Pole_textowe_parametry.Items.Add(tmp[2]);
                Pole_textowe_parametry.Items.Add(tmp[3]);

                Pole_textowe_zawartosc.Items.Clear();
                foreach(string str in plecakowy.ToFewStrings()) Pole_textowe_zawartosc.Items.Add(str);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Zły format wprowadzonych danych\nProgram przyjmuje tylko int!\nBłąd: " 
                            + ex.Message, "Złe dane!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }
    }
}

