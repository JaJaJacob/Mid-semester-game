using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace zadaniena5
{
    //LINK DO YT

    class Program
    {
        public static int Size = 0;
        public static int WhoStarted = 0; //0 - gracz  1 - bot
        public static Player p1 = new Player();
        public static Player p2 = new Player();
        public static Player bot = new Player();
        public static Battlefiled BattlefiledOmega;
        public static int[] tab = new int[2];
        public static int XP1 = 0, YP1 = 1;
        public static int XP2 = 1, YP2 = 0;
        public static bool IsWonPlayerOne= false;
        public static bool IsWonPlayerTwo = false;
        public static bool IsWon = false;
        public static Rules r1 = new Rules();
        public static char ReadyToPlay = 'n';
        public void HomePage()
        {
            r1.PrepareGame();
            Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
        }
        public bool Ready()
        {
            ReadyToPlay = char.Parse(Console.ReadLine());

            if (ReadyToPlay == 'n' || ReadyToPlay == 'N')
                return false;
            else
            {
                Console.Clear();
                Console.SetCursorPosition((Console.WindowWidth - 40) / 2, (Console.WindowHeight - 8) / 2);
                Console.WriteLine("$$$$$  $$  $$$$$  $$$$    $$  $    $$$  ");
                Console.WriteLine("                                             $$     $$  $$     $$  $$  $$ $    $$ $$ ");
                Console.WriteLine("                                             $$ $$  $$  $$$$   $$$$    $$$    $$   $$");
                Console.WriteLine("                                             $$  $  $$  $$     $$  $   $$ $   $$$$$$$");
                Console.WriteLine("                                             $$$$$  $$  $$$$$  $$  $   $$  $  $$   $$");
                Console.WriteLine("");
                Console.WriteLine("                                             Projekt: Jakub Piotrowski, D401, 05-2021");
                Thread.Sleep(2000);
                Console.Clear();

                return true;
            }
        }
        public bool CheckWin()
        {
            if (IsWonPlayerOne == true)
                return false;
            else if (IsWonPlayerTwo == true)
                return false;
            else if (IsWonPlayerOne == false)
                return true;
            else if (IsWonPlayerTwo == false)
                return true;
            else
                return true;
        }
        public void SetNicknamesAndSizeGameMode1()
        {
            Console.SetCursorPosition((Console.WindowWidth - 22) / 2, Console.CursorTop);
            Console.WriteLine("Wpisz nazwę gracza 1: ");
            Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
            p1.PlayerName = Console.ReadLine();
            p1.USEDPLAYERNAME = "player1";
            Console.SetCursorPosition((Console.WindowWidth - 20) / 2, Console.CursorTop);
            Console.WriteLine("Wprowadź swój znak: ");
            Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
            p1.Character = char.Parse(Console.ReadLine());

            do
            {
                Console.SetCursorPosition((Console.WindowWidth - 22) / 2, Console.CursorTop);
                Console.WriteLine("Wpisz nazwę gracza 2: ");
                Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
                p2.PlayerName = Console.ReadLine();
            }
            while (p1.CheckNickNames(p2));

            do
            {
                Console.SetCursorPosition((Console.WindowWidth - 20) / 2, Console.CursorTop);
                Console.WriteLine("Wprowadź swój znak");
                Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
                p2.Character = char.Parse(Console.ReadLine());
            }
            while (p1.CheckCharacters(p2));

            p2.USEDPLAYERNAME = "player2";
            Console.SetCursorPosition((Console.WindowWidth - 24) / 2, Console.CursorTop);
            Console.WriteLine("Podaj rozmiar pola gry: ");
            Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
            Size = int.Parse(Console.ReadLine());
            Size += Size;
            Size -= 2;
            BattlefiledOmega = new Battlefiled(Size);
            BattlefiledOmega.FillTheBattlefield(p1, p2);
            Console.Clear();
        }
        public void SetNicknamesAndSizeGameMode2()
        {
            p2.CreateBot();

            do
            {
                Console.SetCursorPosition((Console.WindowWidth - 22) / 2, Console.CursorTop);
                Console.WriteLine("Wpisz nazwę gracza 1: ");
                Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
                p1.PlayerName = Console.ReadLine();
            }
            while (p1.CheckNickNames(p2));

            do
            {
                Console.SetCursorPosition((Console.WindowWidth - 20) / 2, Console.CursorTop);
                Console.WriteLine("Wprowadź swój znak");
                Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
                p1.Character = char.Parse(Console.ReadLine());
            }
            while (p1.CheckCharacters(p2));

            p1.USEDPLAYERNAME = "player1";
            Console.SetCursorPosition((Console.WindowWidth - 24) / 2, Console.CursorTop);
            Console.WriteLine("Podaj rozmiar pola gry: ");
            Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
            Size = int.Parse(Console.ReadLine());
            Size += Size;
            Size -= 2;
            BattlefiledOmega = new Battlefiled(Size);
            BattlefiledOmega.FillTheBattlefield(p1, p2);
            Console.Clear();
        }
        public void WinningAlert()
        {
            Console.SetCursorPosition((Console.WindowWidth - 13) / 2, Console.CursorTop);

            if (IsWonPlayerOne == true)
            {
                Console.SetCursorPosition((Console.WindowWidth - 15) / 2, (Console.WindowHeight - 8) / 2);
                Console.WriteLine("Wygrywa gracz " + p1.PlayerName);
            }
            else if (IsWonPlayerTwo)
            {
                Console.SetCursorPosition((Console.WindowWidth - 15) / 2, (Console.WindowHeight - 8) / 2);
                Console.WriteLine("Wygrywa gracz " + p2.PlayerName);
            }
        }
        static void Main(string[] args)
        {
            Console.SetWindowSize(130, 30);
            Program p = new Program();
            p.HomePage();

            if (p.Ready())
            {
                Console.SetCursorPosition((Console.WindowWidth - 16) / 2, (Console.WindowHeight - 8) / 2);
                Console.WriteLine("Wybierz typ gry:");

                if (r1.SwitchingGameTypeMenu() == 1)
                {
                    p.SetNicknamesAndSizeGameMode1();

                    if (BattlefiledOmega.RandomNumberLotteryMechanism(p1, p2))
                    {
                        while (p.CheckWin())
                        {
                            BattlefiledOmega.Move(p1, p1, p2);
                            BattlefiledOmega.SetMovePlayer(tab, p1);
                            BattlefiledOmega.DisplayBattlefield(p1, p2);
                            IsWonPlayerOne = BattlefiledOmega.CheckWin();
                            Console.Clear();

                            if (!IsWonPlayerOne)
                            {
                                BattlefiledOmega.Move(p2, p1, p2);
                                BattlefiledOmega.SetMovePlayer(tab, p2);
                                BattlefiledOmega.DisplayBattlefield(p1, p2);
                                IsWonPlayerTwo = BattlefiledOmega.CheckWin();
                                Console.Clear();
                            }
                        }
                    }
                    else
                    {
                        while (p.CheckWin())
                        {
                            BattlefiledOmega.Move(p2, p1, p2);
                            BattlefiledOmega.SetMovePlayer(tab, p2);
                            BattlefiledOmega.DisplayBattlefield(p1, p2);
                            IsWonPlayerTwo = BattlefiledOmega.CheckWin();
                            Console.Clear();

                            if (!IsWonPlayerTwo)
                            {
                                BattlefiledOmega.Move(p1, p1, p2);
                                BattlefiledOmega.SetMovePlayer(tab, p1);
                                BattlefiledOmega.DisplayBattlefield(p1, p2);
                                IsWonPlayerOne = BattlefiledOmega.CheckWin();
                                Console.Clear();
                            }
                        }
                    }

                    p.WinningAlert();

                }    
                else
                {
                    p.SetNicknamesAndSizeGameMode2();

                    if (BattlefiledOmega.RandomNumberLotteryMechanism(p1, p2))
                    {
                        int i = 0;
                        while (p.CheckWin())
                        {
                            BattlefiledOmega.Move(p1, p1, p2);
                            BattlefiledOmega.SetMovePlayer(tab, p1);
                            BattlefiledOmega.DisplayBattlefield(p1, p2);
                            IsWonPlayerOne = BattlefiledOmega.CheckWin();
                            BattlefiledOmega.CheckPlayerStartedPoint(i);
                            i++;
                            Console.Clear();

                            if (!IsWonPlayerOne)
                            { 
                                BattlefiledOmega.SetBotMove(p1, p2, WhoStarted);
                                BattlefiledOmega.DisplayBattlefield(p1, p2);
                                IsWonPlayerTwo = BattlefiledOmega.CheckWin();
                                Console.Clear();
                            }
                        }
                    }
                    else
                    {
                        WhoStarted = 1;

                        while (p.CheckWin())
                        {
                            BattlefiledOmega.SetBotMove(p1, p2, WhoStarted);
                            BattlefiledOmega.DisplayBattlefield(p1, p2);
                            IsWonPlayerTwo = BattlefiledOmega.CheckWin();
                            Console.Clear();

                            if (!IsWonPlayerTwo)
                            {
                                BattlefiledOmega.Move(p1, p1, p2);
                                BattlefiledOmega.SetMovePlayer(tab, p1);
                                BattlefiledOmega.DisplayBattlefield(p1, p2);
                                IsWonPlayerOne = BattlefiledOmega.CheckWin();
                                Console.Clear();
                            }
                        }
                    }

                    p.WinningAlert();

                }
            }
            else
            {
                Console.WriteLine("Brak zgody graczy. Gra wstrzymana!");
            }
        }
        
    }
}
