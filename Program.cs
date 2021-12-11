using System;
using System.IO;

namespace Kolos2
{
    public class Program
    {
        static void Main(string[] args)
        {
            CRUD crud = new CRUD();
            crud.MainMenu();

        }
        public class CRUD
        {
            bool Menuloop = true;
            public int IndexArray = 0;
            public string[] baza;

            public CRUD()
            {
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                string database = projectDirectory + @"\baza.dat";
                if (!File.Exists(database))
                    File.Create(database);

                this.baza = File.ReadAllLines(database);
            }

            public void AddAlbum()
            {
                Console.Clear();
                string[] wpis = new string[3];
                Console.WriteLine("Dane nie mogą zawierać średnika");
                for (int i = 0; i < wpis.Length; i++)
                {
                    if (i == 0)
                        Console.WriteLine("Podaj wykonawcę");
                    if (i == 1)
                        Console.WriteLine("Podaj nazwę albumu");
                    if (i == 2)
                        Console.WriteLine("Podaj rok wydania");
                    wpis[i] = Console.ReadLine();
                    if (wpis[i].Contains(';'))
                    {
                        Console.WriteLine("Zawiera średnik!");
                        i--;
                    }
                    if (i == 2 && (!int.TryParse(wpis[i], out int si) || wpis[i].Length > 4))
                    {
                        Console.WriteLine("Rok musi być cyfrą i nie dłuższą niż 4 znaki");
                        i--;
                    }

                }
                this.baza = new string[] { String.Join(';', wpis) };
            }

            public void SaveDB()
            {

            }

            public void MainMenu()
            {
                string tekst = "Wybierz opcję używając strzałek w górę i w dół";
                string[] MainMenuOpt = { "Dodaj album", "Znajdź album", "Popraw album", "Usuń album" };
                this.Menu(tekst, MainMenuOpt);
                switch (this.IndexArray)
                {
                    case 0:
                        AddAlbum();
                        break;
                    case 1: break;
                    case 2: break;
                    case 3: break;
                    default: break;
                }
            }
            public void Menu(string tekst, string[] MenuD)
            {
                Menuloop = true;
                while (this.Menuloop)
                {
                    Console.Clear();
                    Console.WriteLine(tekst);
                    for (int i = 0; i < MenuD.Length; i++)
                    {

                        if (this.IndexArray == i)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine($"* << {MenuD[i]} >> *");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.WriteLine($"  << {MenuD[i]} >>  ");
                        }

                    }
                    ConsoleKeyInfo KP = Console.ReadKey(true);
                    switch (KP.Key)
                    {
                        case ConsoleKey.DownArrow:
                            this.IndexArray++;
                            if (this.IndexArray >= MenuD.Length)
                            {
                                this.IndexArray = 0;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            this.IndexArray--;
                            if (this.IndexArray < 0)
                            {
                                this.IndexArray = MenuD.Length - 1;
                            }
                            break;
                        case ConsoleKey.Enter:
                            this.Menuloop = false;
                            break;
                    }
                }
            }
        }
    }
}
