﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ColumnHeadersVisible = false;
            int[,] tab = new int[9, 9];
            tab = LosujPlansze();
            dataGridView1 = DodajLosowaneLiczby(tab, dataGridView1);
           // Sprawdz(dataGridView1);
        }

        public static void Sprawdz(DataGridView dataGrid)
        {
           int size = dataGrid.Rows.Count;
        }

        public static DataGridView DodajLosowaneLiczby(int[,] tab, DataGridView dataGrid)
        {
            
            for (int i=0; i<9; i++) // wiersze
            {
                string[] row = new string[9];
                for (int j=0; j<9; j++) // kolumny
                {
                    if( tab[i, j] > 0)
                    {
                        row[j] = tab[i, j].ToString();
                    }
                }
                dataGrid.Rows.Add(row);
            }
            return dataGrid;
        }

        

        public static int[,] LosujPlansze()
        {
            int[,] tab = new int[9, 9];
            Random random = new Random();
            // uzupelniamy tablice z liczbami losowymi cyframi 1-9
            for (int i = 0; i < 9; i++)
            {
                int x = random.Next(0, 8);
                int y = random.Next(0, 8);

                tab[x, y] = random.Next(1, 9);
                //if (SprawdzPlanszeR() > 0)
                //{
                //    int aktualna = tab[x, y];
                //    tab[x, y] = random.Next(1, 9);
                //    if (aktualna == tab[x, y])
                //    {
                //        tab[x, y] = random.Next(1, 9);
                //    }
                //}
            }
            return tab;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Zmienione(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;
            int y = e.ColumnIndex;

            MessageBox.Show("Zmieniono cos !");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ilKol = dataGridView1.Rows.Count;
            int[,] tab = new int[9, 9];
            int j = 0, k = 0;
            for (int i=0; i < ilKol;i++)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    IEnumerator enumerator = row.Cells.GetEnumerator();
                    while (enumerator.MoveNext() == true)
                    {
                        DataGridViewCell curr = (DataGridViewCell)enumerator.Current;
                        if (curr.Value != null)
                        {
                            string cr = curr.Value.ToString();


                            int cur = Int32.Parse(curr.Value.ToString());
                            tab[j, k] = cur;
                        }
                        enumerator.MoveNext();
                    }
                    k++;
                }
                j++;
                
            }
        }
    }
}

/*
 * 
 * class Program
    {
        static int[,] tab = new int[9, 9]; // Tabelka 9x9

        public static void RysujPlansze()
        { // rysujemy plansze 
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (tab[i, j] == 0)
                    {
                        Console.Write(" - "); // jesli jakis element jest zerem - to rysujemy kreske 
                    }
                    else
                    {
                        Console.Write(" " + tab[i, j] + " "); // jezeli nie, to wypisujemy ten element 
                    }
                }
                Console.WriteLine("");
            }
        }

        public static void LosujPlansze()
        {
            // w przyszlosci zmienic na zwracanie tablicy ewentualnie zwracanie liczb ktore zostały wylosowane tak, by nie mozna bylo zmienic ich podczas wypelniania sudoku
            Random random = new Random();
            // uzupelniamy tablice z liczbami losowymi cyframi 1-9
            for (int i=0; i<9; i++)
            {
                int x = random.Next(0, 8);
                int y = random.Next(0, 8);

                tab[x, y] = random.Next(1, 9);
                if (SprawdzPlanszeR() > 0)
                {
                    int aktualna = tab[x, y];
                    tab[x, y] = random.Next(1, 9);
                    if (aktualna == tab[x, y])
                    {
                        tab[x, y] = random.Next(1, 9);
                    }
                }
            }
        }

        public static void ZmienCos()
        { // zmienanie elementu
            Console.WriteLine("x : ");
            int x = Int32.Parse(Console.ReadLine());
            Console.WriteLine("x : ");
            int y = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Wartosc : ");
            tab[x, y] = Int32.Parse(Console.ReadLine());
            RysujPlansze();
        }

        public static int checkNulls()
        { // sprawdz ile jeszcze zer pozostało do wypelnienia
            int nulls = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (tab[i, j] == 0)
                    {
                        nulls++;
                    }
                }
            }
            return nulls;
        }

        public static int checkCell(int wiersz, int kolumna)
        { // sprawdzanie komorki
            int temp = tab[wiersz, kolumna];
            int err = 0;
            for (int i = 0; i < 9; i++)
            {
                if (tab[i, kolumna] == 0) continue;
                if (wiersz == i) continue;
                if (temp == tab[i, kolumna])
                {
                    err++;
                }
            }

            for (int j = 0; j < 9; j++)
            {
                if (tab[wiersz, j] == 0) continue;
                if (kolumna == j) continue;
                if (temp == tab[wiersz, j])
                {
                    err++;
                }
            }
            return err;
        }

        public static void SprawdzPlansze()
        { // sprawdzanie planszy w poszukiwaniu bledow
            int errors = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int err = checkCell(i, j);
                    errors = errors + err;
                }
            }
            if (errors > 0)
            {
                Console.WriteLine("Sa bledy");
            }
            else
            {
                Console.WriteLine("Nie ma bledow");
            }
            int nulls = checkNulls(); // pobieramy ilosc zer
            if (nulls == 0 && errors == 0)
            { // jezeli nie ma juz pustych pol i nie ma bledow to rozwiazane sudoku
                Console.WriteLine("GRATULACJE !!!! ROZWIĄZAŁEŚ SUDOKU.");
            }
        }

        public static int SprawdzPlanszeR()
        {
            int errors = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int err = checkCell(i, j);
                    errors = errors + err;
                }
            }
            if (errors > 0)
            {
                return errors;
            }
            else
            {
                return 0;
            }
        }

        public static void Main(String[] args)
        {
            int w = 0;  // zmienna sterujaca, na razie bezuzyteczna
            int m = 0; // ruchy
            LosujPlansze();
            RysujPlansze();
            ZmienCos();
            m++;
            while (w == 0)
            {
                ZmienCos();
                m++;
                if (m > 2)
                {
                    SprawdzPlansze();
                }

            }
        }
    }

*/