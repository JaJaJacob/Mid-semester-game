using System;
using System.Collections.Generic;

namespace zadaniena5
{
    public class Rules
    {
        public bool Affiramative = true;
        public List<string> RulesList = new List<string>();
        public List<string> MenuList = new List<string>();
        public static List<string> GameModesList = new List<string>();
        public Rules()
        {
            RulesList.Add("Plansza składa się z n pól wypełnionych znakami wpisanymi przez użytkownika");
            RulesList.Add(" ");
            RulesList.Add("1. Gracze mogą poruszać się tylko po swoich polach.");
            RulesList.Add(" ");
            RulesList.Add("1. 1. Aby wygrać gracz z czerwonymi pionkami musi ustanowić połączenie między górnymi i dolnymi pionkami");
            RulesList.Add(" ");
            RulesList.Add("1. 2. Aby wygrać gracz z niebieskimi pionkami musi ustanowić połączenie między lewymi i prawymi pionkami");
            RulesList.Add(" ");
            RulesList.Add("2. Jeśli gracz ruszy się niezgodnie z przepisami, bądź będzie znajdował się na nie swoim polu traci ruch");
            RulesList.Add(" ");
            RulesList.Add("3. Aby potwierdzić wybranie danego pola wciśnij klawisz Enter");
            RulesList.Add(" ");
            RulesList.Add("4. Aby wylosować pozycję gracze podają dwie liczby z przedziału od 10-99. Gracz będący bliżej liczby wylosowanej zaczyna");
            RulesList.Add(" ");
            MenuList.Add("Menu ruchów:");
            MenuList.Add(" ");
            MenuList.Add("[Strzałka w górę]   -  idź o 1 wzwyż");
            MenuList.Add("[Strzałka w dół]    -  idź o 1 w dół");
            MenuList.Add(" [Strzałka w lewo]   -  idź o 1 w lewo");
            MenuList.Add("   [Strzałka w prawo]  -  idź o 1 w prawo");
            MenuList.Add(" ");
            MenuList.Add("Czy gracze akceptują zasady? [Y/n]");

            GameModesList.Add("1. Granie z graczem");
            GameModesList.Add("2. Granie z botem");
        }
        public void PrepareGame()
        {
            Console.SetCursorPosition((Console.WindowWidth - RulesList[0].Length) / 2, Console.CursorTop);
            foreach (var item in RulesList)
            {
                Console.SetCursorPosition((Console.WindowWidth - item.Length) / 2, Console.CursorTop);
                Console.WriteLine(item);
            }
            foreach (var item in MenuList)
            {
                Console.SetCursorPosition((Console.WindowWidth - item.Length) / 2, Console.CursorTop);
                Console.WriteLine(item);
            }
        }
        public int SwitchingGameTypeMenu()
        {
            int Choice = 1;
            Console.SetCursorPosition((Console.WindowWidth - GameModesList[0].Length) / 2, Console.CursorTop);
            foreach (string item in GameModesList)
            {
                Console.SetCursorPosition((Console.WindowWidth - item.Length) / 2, Console.CursorTop);
                Console.WriteLine(item);
            }

            Console.SetCursorPosition(Console.WindowWidth / 2, Console.CursorTop);
            Choice = int.Parse(Console.ReadLine());

            Console.Clear();

            if (Choice == 1)
                return 1;
            else if (Choice == 2)
                return 2;
            else
                return 1;
        }

    }
}
