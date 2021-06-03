using System;
using System.Collections.Generic;

namespace zadaniena5
{
    public class Battlefiled
    {
        int Size = 0;
        Piece[,] Battlefield1;
        char[,] Battlefield2;
        public int[] tab = new int[2];
        public int XD, YD;
        public bool PlayerOneWon = false;
        public bool PlayerTwoWon = false;
        public int XP1 = 0, YP1 = 1;
        public int XP2 = 1, YP2 = 0;
        string PlayerStartedPosition;
        int StartRightBottomX = 1;
        int StartRightBottomY; // przypisz do size 
        int StartRightTopX; //przypisz do size - 1
        int StartRightTopY; //przypisz do size
        int StartLeftBottomX = 1;
        int StartLeftBottomY = 0;
        int StartLeftTopX; //przypisz do size - 1
        int StartLeftTopY = 0;
        int StartFirstX; //przypisz do size - 1
        int StartFirstY = 0; //przypisz do 0
        int ifStartFirstBlockedX = 1;
        int ifStartFirstBlockedY; //przypisz do size
        int BotWantLeftBlocked = 0;
        int BotWantRightBlocked = 0;
        int hop = 0;
        int[] Abandoned = new int[2];
        public Battlefiled(int Size)
        {
            this.Size = Size;
            Battlefield1 = new Piece[Size + 1, Size + 1];
            Battlefield2 = new char[Size + 1, Size + 1];
            this.StartRightBottomY = Size;
            this.StartRightTopX = Size - 1;
            this.StartRightTopY = Size;
            this.StartLeftTopX = Size - 1;
            this.StartFirstX = Size - 1;
            this.ifStartFirstBlockedY = Size;
        }
        public bool RandomNumberLotteryMechanism(Player p1, Player p2)
        {
            Random R = new Random();
            int ChanceNumber = R.Next(10, 1000);

            if (p2.PlayerName != "bot")
            {
                do
                {
                    Console.SetCursorPosition((Console.WindowWidth - 27) / 2, Console.CursorTop / 2);
                    Console.WriteLine("Wprowadź liczbę od 10 do 999:");
                    Console.SetCursorPosition((Console.WindowWidth - 1) / 2, Console.CursorTop + 1 / 2);
                    p1.RandomEntered = int.Parse(Console.ReadLine());
                    Console.SetCursorPosition((Console.WindowWidth - 27) / 2, Console.CursorTop + 1 / 2);
                    Console.WriteLine("Wprowadź liczbę od 10 do 999:");
                    Console.SetCursorPosition((Console.WindowWidth - 2) / 2, Console.CursorTop + 1 / 2);
                    p2.RandomEntered = int.Parse(Console.ReadLine());
                    Console.Clear();
                }
                while (!((p1.RandomEntered >= 10 && p1.RandomEntered <= 999) && (p2.RandomEntered >= 10 && p2.RandomEntered <= 999)));
            }
            else
            {
                do
                {
                    Console.SetCursorPosition((Console.WindowWidth - 27) / 2, Console.CursorTop / 2);
                    Console.WriteLine("Wprowadź liczbę od 10 do 999:");
                    Console.SetCursorPosition((Console.WindowWidth - 1) / 2, Console.CursorTop + 1 / 2);
                    p1.RandomEntered = int.Parse(Console.ReadLine());
                    p2.RandomEntered = R.Next(10, 1000);
                    Console.Clear();
                }
                while (!((p1.RandomEntered >= 10 && p1.RandomEntered <= 999) && (p2.RandomEntered >= 10 && p2.RandomEntered <= 999)));
            }

            int DifferencePl1 = ChanceNumber - p1.RandomEntered;
            int DifferencePl2 = ChanceNumber - p2.RandomEntered;

            if (DifferencePl2 > DifferencePl1) // zaczyna gracz p1
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void FillTheBattlefield(Player p1, Player p2)
        {
            for (int i = 0; i < Size + 1; i++)
            {
                for (int j = 0; j < Size + 1; j++)
                {
                    if (i % 2 == 0 && j % 2 == 1)
                    {
                        if (i == 0)
                        {
                            Battlefield1[i, j] = new Piece(p1, true, false);
                            Battlefield2[i, j] = p1.Character;
                        }
                        else if (i == Size)
                        {
                            Battlefield1[i, j] = new Piece(p1, false, true);
                            Battlefield2[i, j] = p1.Character;
                        }
                        else
                        {
                            Battlefield1[i, j] = new Piece(p1);
                            Battlefield2[i, j] = p1.Character;
                        }
                    }
                    else if (i % 2 == 1 && j % 2 == 0)
                    {
                        if (j == 0)
                        {
                            Battlefield1[i, j] = new Piece(p2, true, false);
                            Battlefield2[i, j] = p2.Character;
                        }
                        else if (j == Size)
                        {
                            Battlefield1[i, j] = new Piece(p2, false, true);
                            Battlefield2[i, j] = p2.Character;
                        }
                        else
                        {
                            Battlefield1[i, j] = new Piece(p2);
                            Battlefield2[i, j] = p2.Character;
                        }
                    }
                    else
                    {
                        Battlefield2[i, j] = ' ';
                    }
                }
            }
        }
        public void DisplayBattlefield(Player p1, Player p2)
        {
            Console.SetCursorPosition((Console.WindowWidth - Size) / 2, Console.CursorTop);

            for (int i = 0; i < Size + 1; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - Size) / 2, Console.CursorTop);

                for (int j = 0; j < Size + 1; j++)
                {
                    if (Battlefield2[i, j] == p1.Character)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(p1.Character);
                    }
                    else if (Battlefield2[i, j] == p2.Character)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(p2.Character);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(' ');
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
            }
        }
        public bool IsFieldAvailable(int X, int Y, Player Pl)
        {
            if (X < Size + 1 && Y < Size + 1 && X >= 0 && Y >= 0)
                if (Battlefield1[X, Y].PieceAuthority == Pl.PlayerName)
                    return true;
                else
                    return false;
            else
            {
                Console.SetCursorPosition((Console.WindowWidth - 24) / 2, Console.CursorTop);
                Console.WriteLine("Poza skalą! Tracisz turę");
                return false;
            }
        }
        public void Move(Player Pl, Player p1, Player p2)
        {
            ConsoleKeyInfo k;
            Console.SetCursorPosition((Console.WindowWidth - 13) / 2, Console.CursorTop);
            Console.WriteLine("Runda gracza " + Pl.PlayerName);
            Console.WriteLine(" ");
            DisplayBattlefield(p1, p2);
            do
            {
                if (Pl.PlayerName == p1.PlayerName)
                {
                    k = Console.ReadKey(true);
                    if (k.Key == ConsoleKey.DownArrow && XP1 < Size - 1)
                    {
                        XP1 += 2;
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - 13) / 2, Console.CursorTop);
                        Console.WriteLine("Runda gracza " + Pl.PlayerName);
                        Console.WriteLine(" ");
                        ColorBattlefieldField(p1, p1, p2);
                        //Console.WriteLine("X: " + XP1 + " Y: " + YP1);
                    }
                    if (k.Key == ConsoleKey.UpArrow && XP1 > 0)
                    {
                        XP1 -= 2;
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - 13) / 2, Console.CursorTop);
                        Console.WriteLine("Runda gracza " + Pl.PlayerName);
                        Console.WriteLine(" ");
                        ColorBattlefieldField(p1, p1, p2);
                        //Console.WriteLine("X: " + XP1 + " Y: " + YP1);
                    }
                    if (k.Key == ConsoleKey.LeftArrow && YP1 >= 2)
                    {
                        YP1 -= 2;
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - 13) / 2, Console.CursorTop);
                        Console.WriteLine("Runda gracza " + Pl.PlayerName);
                        Console.WriteLine(" ");
                        ColorBattlefieldField(p1, p1, p2);
                        //Console.WriteLine("X: " + XP1 + " Y: " + YP1);
                    }
                    if (k.Key == ConsoleKey.RightArrow && YP1 <= Size - 2)
                    {
                        YP1 += 2;
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - 13) / 2, Console.CursorTop);
                        Console.WriteLine("Runda gracza " + Pl.PlayerName);
                        Console.WriteLine(" ");
                        ColorBattlefieldField(p1, p1, p2);
                        //Console.WriteLine("X: " + XP1 + " Y: " + YP1);
                    }
                }
                else
                {
                    k = Console.ReadKey(true);
                    if (k.Key == ConsoleKey.DownArrow && XP2 < Size - 2)
                    {
                        XP2 += 2;
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - 13) / 2, Console.CursorTop);
                        Console.WriteLine("Runda gracza " + Pl.PlayerName);
                        Console.WriteLine(" ");
                        ColorBattlefieldField(p2, p1, p2);
                        //Console.WriteLine("X: " + XP2 + " Y: " + YP2);
                    }
                    if (k.Key == ConsoleKey.UpArrow && XP2 >= 2)
                    {
                        XP2 -= 2;
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - 13) / 2, Console.CursorTop);
                        Console.WriteLine("Runda gracza " + Pl.PlayerName);
                        Console.WriteLine(" ");
                        ColorBattlefieldField(p2, p1, p2);
                        //Console.WriteLine("X: " + XP2 + " Y: " + YP2);
                    }
                    if (k.Key == ConsoleKey.LeftArrow && YP2 > 0)
                    {
                        YP2 -= 2;
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - 13) / 2, Console.CursorTop);
                        Console.WriteLine("Runda gracza " + Pl.PlayerName);
                        Console.WriteLine(" ");
                        ColorBattlefieldField(p2, p1, p2);
                        //Console.WriteLine("X: " + XP2 + " Y: " + YP2);
                    }
                    if (k.Key == ConsoleKey.RightArrow && YP2 < Size)
                    {
                        YP2 += 2;
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - 13) / 2, Console.CursorTop);
                        Console.WriteLine("Runda gracza " + Pl.PlayerName);
                        Console.WriteLine(" ");
                        ColorBattlefieldField(p2, p1, p2);
                        //Console.WriteLine("X: " + XP2 + " Y: " + YP2);
                    }
                }
            }
            while (!(k.Key == ConsoleKey.Enter));
        }
        public void SetMovePlayer(int[] tab, Player Pl)
        {
            Player Tmp = Pl;
            Piece Pi = new Piece(Pl);
            if (Pl.USEDPLAYERNAME == "player1")
            {
                XD = XP1;
                YD = YP1;
            }
            else
            {
                XD = XP2;
                YD = YP2;
            }

            var Input = Console.ReadKey();

            switch (Input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (IsFieldAvailable(XD - 2, YD, Pl))
                    {
                        if (Battlefield1[XD - 1, YD] == null)
                        {
                            Battlefield1[XD - 1, YD] = Pi;
                            Battlefield2[XD - 1, YD] = Pi.Character;
                            Battlefield1[XD, YD].ConnectUpperPiece(Battlefield1[XD - 2, YD]);
                            Battlefield1[XD - 2, YD].ConnectLowerPiece(Battlefield1[XD, YD]);

                            Battlefield1[XD, YD].ConnectEdges(Battlefield1[XD - 2, YD].IsEdge1, Battlefield1[XD - 2, YD].IsEdge2);
                            Battlefield1[XD - 2, YD].ConnectEdges(Battlefield1[XD, YD].IsEdge1, Battlefield1[XD, YD].IsEdge2);
                        }
                        else
                        {
                            Console.WriteLine("Cannot place a piece at this point!");
                            Console.WriteLine("Round lost");
                        }
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (IsFieldAvailable(XD + 2, YD, Pl))
                    {
                        if (Battlefield1[XD + 1, YD] == null)
                        {
                            Battlefield1[XD + 1, YD] = Pi;
                            Battlefield2[XD + 1, YD] = Pi.Character;
                            Battlefield1[XD, YD].ConnectLowerPiece(Battlefield1[XD + 2, YD]);
                            Battlefield1[XD + 2, YD].ConnectUpperPiece(Battlefield1[XD, YD]);

                            Battlefield1[XD, YD].ConnectEdges(Battlefield1[XD + 2, YD].IsEdge1, Battlefield1[XD + 2, YD].IsEdge2);
                            Battlefield1[XD + 2, YD].ConnectEdges(Battlefield1[XD, YD].IsEdge1, Battlefield1[XD, YD].IsEdge2);
                        }
                        else
                        {
                            Console.WriteLine("Cannot place a piece at this point!");
                            Console.WriteLine("Round lost");
                        }
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (IsFieldAvailable(XD, YD - 2, Pl))
                    {
                        if (Battlefield1[XD, YD - 1] == null)
                        {
                            Battlefield1[XD, YD - 1] = Pi;
                            Battlefield2[XD, YD - 1] = Pi.Character;
                            Battlefield1[XD, YD].ConnectLeftPiece(Battlefield1[XD, YD - 2]);
                            Battlefield1[XD, YD - 2].ConnectRightPiece(Battlefield1[XD, YD]);

                            Battlefield1[XD, YD].ConnectEdges(Battlefield1[XD, YD - 2].IsEdge1, Battlefield1[XD, YD - 2].IsEdge2);
                            Battlefield1[XD, YD - 2].ConnectEdges(Battlefield1[XD, YD].IsEdge1, Battlefield1[XD, YD].IsEdge2);
                        }
                        else
                        {
                            Console.WriteLine("Cannot place a piece at this point!");
                            Console.WriteLine("Round lost");
                        }
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (IsFieldAvailable(XD, YD + 2, Pl))
                    {
                        if (Battlefield1[XD, YD + 1] == null)
                        {
                            Battlefield1[XD, YD + 1] = Pi;
                            Battlefield2[XD, YD + 1] = Pi.Character;
                            Battlefield1[XD, YD].ConnectRightPiece(Battlefield1[XD, YD + 2]);
                            Battlefield1[XD, YD + 2].ConnectLeftPiece(Battlefield1[XD, YD]);

                            Battlefield1[XD, YD].ConnectEdges(Battlefield1[XD, YD + 2].IsEdge1, Battlefield1[XD, YD + 2].IsEdge2);
                            Battlefield1[XD, YD + 2].ConnectEdges(Battlefield1[XD, YD].IsEdge1, Battlefield1[XD, YD].IsEdge2);
                        }
                        else
                        {
                            Console.WriteLine("Cannot place a piece at this point!");
                            Console.WriteLine("Round lost");
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        public bool CheckWin()
        {
            if (Battlefield1[XD, YD].CheckWinPlayer()) return true;
            else return false;
        }
        public void CheckPlayerStartedPoint(int i)
        {
            if (i == 0)
            {
                if (Battlefield1[1, 1] != null) //jesli zajął lewo góra
                    PlayerStartedPosition = "LU";
                if (Battlefield1[Size - 1, 1] != null) //jeśli zajął lewo dół
                    PlayerStartedPosition = "LD";
                if (Battlefield1[1, Size - 1] != null) //jeśli zajął prawo góre
                    PlayerStartedPosition = "RU";
                if (Battlefield1[Size - 1, Size - 1] != null) //jeśli zajął prawo dół
                    PlayerStartedPosition = "RD";
                if (Battlefield1[1, 1] != null && Battlefield1[Size - 1, 1] != null && Battlefield1[1, Size - 1] != null && Battlefield1[Size - 1, Size - 1] != null)
                    PlayerStartedPosition = "LU"; //lub zrób uniwersal np. U i obmyśl mniej więcej jaki algorytm by był      
            }
        }
        public void SetBotMove(Player p1, Player p2, int WhosNext)
        {
            Piece Pi = new Piece(p2);

            if (WhosNext == 0) // jesli gracz zaczął
            {
                if (PlayerStartedPosition == "LU") //jesli zajął lewo góra
                {
                    if (StartLeftTopY < Size)
                    {
                        if (Battlefield1[StartLeftTopX, StartLeftTopY + 1] == null)
                        {
                            XD = StartLeftTopX;
                            YD = StartLeftTopY;

                            Battlefield1[StartLeftTopX, StartLeftTopY + 1] = Pi;
                            Battlefield2[StartLeftTopX, StartLeftTopY + 1] = Pi.Character;
                            Battlefield1[StartLeftTopX, StartLeftTopY].ConnectRightPiece(Battlefield1[StartLeftTopX, StartLeftTopY + 2]);
                            Battlefield1[StartLeftTopX, StartLeftTopY + 2].ConnectLeftPiece(Battlefield1[StartLeftTopX, StartLeftTopY]);
                            Battlefield1[StartLeftTopX, StartLeftTopY].ConnectEdges(Battlefield1[StartLeftTopX, StartLeftTopY + 2].IsEdge1, Battlefield1[StartLeftTopX, StartLeftTopY + 2].IsEdge2);
                            Battlefield1[StartLeftTopX, StartLeftTopY + 2].ConnectEdges(Battlefield1[StartLeftTopX, StartLeftTopY].IsEdge1, Battlefield1[StartLeftTopX, StartLeftTopY].IsEdge2);
                            StartLeftTopY += 2;
                            BotWantRightBlocked = 0;
                        }
                        else if (Battlefield1[StartLeftTopX, StartLeftTopY + 1] != null && hop == 0)
                        {
                            BotWantRightBlocked++;

                            if (BotWantRightBlocked < 2)
                            {
                                if (Battlefield1[StartLeftTopX - 1, StartLeftTopY] == null)
                                {
                                    XD = StartLeftTopX;
                                    YD = StartLeftTopY;

                                    Battlefield1[StartLeftTopX - 1, StartLeftTopY] = Pi;
                                    Battlefield2[StartLeftTopX - 1, StartLeftTopY] = Pi.Character;
                                    Battlefield1[StartLeftTopX, StartLeftTopY].ConnectUpperPiece(Battlefield1[StartLeftTopX - 2, StartLeftTopY]);
                                    Battlefield1[StartLeftTopX - 2, StartLeftTopY].ConnectLowerPiece(Battlefield1[StartLeftTopX, StartLeftTopY]);

                                    Battlefield1[StartLeftTopX, StartLeftTopY].ConnectEdges(Battlefield1[StartLeftTopX - 2, StartLeftTopY].IsEdge1, Battlefield1[StartLeftTopX - 2, StartLeftTopY].IsEdge2);
                                    Battlefield1[StartLeftTopX - 2, StartLeftTopY].ConnectEdges(Battlefield1[StartLeftTopX, StartLeftTopY].IsEdge1, Battlefield1[StartLeftTopX, StartLeftTopY].IsEdge2);

                                    StartLeftTopX -= 2;
                                }
                            }
                            else
                            {
                                if (Battlefield1[StartLeftTopX - 2, StartLeftTopY + 1] == null)
                                {
                                    Abandoned[0] = StartLeftTopX;
                                    Abandoned[1] = StartLeftTopY;

                                    StartLeftTopX -= 2;
                                    XD = StartLeftTopX;
                                    YD = StartLeftTopY;

                                    Battlefield1[StartLeftTopX, StartLeftTopY + 1] = Pi;
                                    Battlefield2[StartLeftTopX, StartLeftTopY + 1] = Pi.Character;
                                    Battlefield1[StartLeftTopX, StartLeftTopY].ConnectLeftPiece(Battlefield1[StartLeftTopX, StartLeftTopY + 2]);
                                    Battlefield1[StartLeftTopX, StartLeftTopY + 2].ConnectRightPiece(Battlefield1[StartLeftTopX, StartLeftTopY]);

                                    BotWantRightBlocked = 0;
                                    StartLeftTopY += 2;
                                    hop++;
                                }
                            }
                        }
                        else
                        {
                            if (Battlefield1[StartRightTopX, StartRightTopY - 1] == null)
                            {
                                XD = StartRightTopX;
                                YD = StartRightTopY;

                                Battlefield1[StartRightTopX, StartRightTopY - 1] = Pi;
                                Battlefield2[StartRightTopX, StartRightTopY - 1] = Pi.Character;
                                Battlefield1[StartRightTopX, StartRightTopY].ConnectRightPiece(Battlefield1[StartRightTopX, StartRightTopY - 2]);
                                Battlefield1[StartRightTopX, StartRightTopY - 2].ConnectLeftPiece(Battlefield1[StartRightTopX, StartRightTopY]);

                                Battlefield1[StartRightTopX, StartRightTopY].ConnectEdges(Battlefield1[StartRightTopX, StartRightTopY - 2].IsEdge1, Battlefield1[StartRightTopX, StartRightTopY - 2].IsEdge2);
                                Battlefield1[StartRightTopX, StartRightTopY - 2].ConnectEdges(Battlefield1[StartRightTopX, StartRightTopY].IsEdge1, Battlefield1[StartRightTopX, StartRightTopY].IsEdge2);
                                StartRightTopY -= 2;
                            }
                            else if (Battlefield1[StartRightTopX, StartRightTopY - 1] != null)
                            {
                                if (hop == 1 && Battlefield1[StartRightTopX, StartRightTopY - 1].PieceAuthority == "bot")
                                {
                                    if (Battlefield1[StartLeftTopX - 1, StartLeftTopY] == null)
                                    {
                                        XD = Abandoned[0];
                                        YD = Abandoned[1];

                                        Battlefield1[Abandoned[0] - 1, Abandoned[1]] = Pi;
                                        Battlefield2[Abandoned[0] - 1, Abandoned[1]] = Pi.Character;
                                        Battlefield1[Abandoned[0], Abandoned[1]].ConnectUpperPiece(Battlefield1[Abandoned[0] - 2, Abandoned[1]]);
                                        Battlefield1[Abandoned[0] - 2, Abandoned[1]].ConnectLowerPiece(Battlefield1[Abandoned[0], Abandoned[1]]);

                                        Battlefield1[Abandoned[0], Abandoned[1]].ConnectEdges(Battlefield1[Abandoned[0] - 2, Abandoned[1]].IsEdge1, Battlefield1[Abandoned[0] - 2, Abandoned[1]].IsEdge2);
                                        Battlefield1[Abandoned[0] - 2, Abandoned[1]].ConnectEdges(Battlefield1[Abandoned[0], Abandoned[1]].IsEdge1, Battlefield1[Abandoned[0], Abandoned[1]].IsEdge2);

                                        StartLeftTopX -= 2;
                                        hop = 0;
                                    }
                                    else
                                    {
                                        XD = StartLeftTopX;
                                        YD = StartLeftTopY;

                                        Battlefield1[StartLeftTopX, StartLeftTopY + 1] = Pi;
                                        Battlefield2[StartLeftTopX, StartLeftTopY + 1] = Pi.Character;
                                        Battlefield1[StartLeftTopX, StartLeftTopY].ConnectLeftPiece(Battlefield1[StartLeftTopX, StartLeftTopY + 2]);
                                        Battlefield1[StartLeftTopX, StartLeftTopY + 2].ConnectRightPiece(Battlefield1[StartLeftTopX, StartLeftTopY]);

                                        Battlefield1[StartLeftTopX, StartLeftTopY].ConnectEdges(Battlefield1[StartLeftTopX, StartLeftTopY - 2].IsEdge1, Battlefield1[StartLeftTopX, StartLeftTopY - 2].IsEdge2);
                                        Battlefield1[StartLeftTopX, StartLeftTopY - 2].ConnectEdges(Battlefield1[StartLeftTopX, StartLeftTopY].IsEdge1, Battlefield1[StartLeftTopX, StartLeftTopY].IsEdge2);

                                        StartLeftTopY += 2;
                                        hop = 0;
                                    }
                                }
                                else
                                {
                                    if (Battlefield1[StartRightTopX - 1, StartRightTopY] == null && Battlefield1[StartRightTopX - 2, StartRightTopY].PieceAuthority == "bot")
                                    {
                                        XD = StartRightTopX;
                                        YD = StartRightTopY;

                                        Battlefield1[StartRightTopX - 1, StartRightTopY] = Pi;
                                        Battlefield2[StartRightTopX - 1, StartRightTopY] = Pi.Character;
                                        Battlefield1[StartRightTopX, StartRightTopY].ConnectUpperPiece(Battlefield1[StartRightTopX - 2, StartRightTopY]);
                                        Battlefield1[StartRightTopX - 2, StartRightTopY].ConnectLowerPiece(Battlefield1[StartRightTopX, StartRightTopY]);

                                        Battlefield1[StartRightTopX, StartRightTopY].ConnectEdges(Battlefield1[StartRightTopX - 2, StartRightTopY].IsEdge1, Battlefield1[StartRightTopX - 2, StartRightTopY].IsEdge2);
                                        Battlefield1[StartRightTopX - 2, StartRightTopY].ConnectEdges(Battlefield1[StartRightTopX, StartRightTopY].IsEdge1, Battlefield1[StartRightTopX, StartRightTopY].IsEdge2);
                                        StartRightTopX -= 2;
                                    }
                                }
                            }
                        }
                    }               
                }
                if (PlayerStartedPosition == "LD") //jeśli zajął lewo dół
                {
                    if (StartLeftBottomY < Size)
                    {
                        if (Battlefield1[StartLeftBottomX, StartLeftBottomY + 1] == null)
                        {
                            XD = StartLeftBottomX;
                            YD = StartLeftBottomY;

                            Battlefield1[StartLeftBottomX, StartLeftBottomY + 1] = Pi;
                            Battlefield2[StartLeftBottomX, StartLeftBottomY + 1] = Pi.Character;
                            Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectRightPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY + 2]);
                            Battlefield1[StartLeftBottomX, StartLeftBottomY + 2].ConnectLeftPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY]);

                            Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectEdges(Battlefield1[StartLeftBottomX, StartLeftBottomY + 2].IsEdge1, Battlefield1[StartLeftBottomX, StartLeftBottomY + 2].IsEdge2);
                            Battlefield1[StartLeftBottomX, StartLeftBottomY + 2].ConnectEdges(Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge1, Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge2);

                            StartLeftBottomY += 2;
                            BotWantRightBlocked = 0;
                        }
                        else if (Battlefield1[StartLeftBottomX, StartLeftBottomY + 1] != null && hop == 0)
                        {
                            BotWantRightBlocked++;

                            if (BotWantRightBlocked < 2)
                            {
                                if (Battlefield1[StartLeftBottomX + 1, StartLeftBottomY] == null)
                                {
                                    XD = StartLeftBottomX;
                                    YD = StartLeftBottomX;

                                    Battlefield1[StartLeftBottomX + 1, StartLeftBottomY] = Pi;
                                    Battlefield2[StartLeftBottomX + 1, StartLeftBottomY] = Pi.Character;
                                    Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectLowerPiece(Battlefield1[StartLeftBottomX + 2, StartLeftBottomY]);
                                    Battlefield1[StartLeftBottomX + 2, StartLeftBottomY].ConnectUpperPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY]);

                                    Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectEdges(Battlefield1[StartLeftBottomX + 2, StartLeftBottomY].IsEdge1, Battlefield1[StartLeftBottomX + 2, StartLeftBottomY].IsEdge2);
                                    Battlefield1[StartLeftBottomX + 2, StartLeftBottomY].ConnectEdges(Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge1, Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge2);

                                    StartLeftBottomX += 2;
                                }
                            }
                            else
                            {
                                if (Battlefield1[StartLeftBottomX + 2, StartLeftBottomY + 1] == null)
                                {
                                    Abandoned[0] = StartLeftBottomX;
                                    Abandoned[1] = StartLeftBottomY;

                                    StartLeftBottomX += 2;
                                    XD = StartLeftBottomX;
                                    YD = StartLeftBottomY;

                                    Battlefield1[StartLeftBottomX, StartLeftBottomY + 1] = Pi;
                                    Battlefield2[StartLeftBottomX, StartLeftBottomY + 1] = Pi.Character;
                                    Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectLeftPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY + 2]);
                                    Battlefield1[StartLeftBottomX, StartLeftBottomY + 2].ConnectRightPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY]);

                                    BotWantRightBlocked = 0;
                                    StartLeftBottomY += 2;
                                    hop++;
                                }
                            }
                        }
                        else
                        {
                            if (Battlefield1[StartRightBottomX, StartRightBottomY - 1] == null)
                            {
                                XD = StartRightBottomX;
                                YD = StartRightBottomX;

                                Battlefield1[StartRightBottomX, StartRightBottomX - 1] = Pi;
                                Battlefield2[StartRightBottomX, StartRightBottomY - 1] = Pi.Character;
                                Battlefield1[StartRightBottomX, StartRightBottomY].ConnectRightPiece(Battlefield1[StartRightBottomX, StartRightBottomY - 2]);
                                Battlefield1[StartRightBottomX, StartRightBottomY - 2].ConnectLeftPiece(Battlefield1[StartRightBottomX, StartRightBottomY]);

                                Battlefield1[StartRightBottomX, StartRightBottomY].ConnectEdges(Battlefield1[StartRightBottomX, StartRightBottomY - 2].IsEdge1, Battlefield1[StartRightBottomX, StartRightBottomY - 2].IsEdge2);
                                Battlefield1[StartRightBottomX, StartRightBottomY - 2].ConnectEdges(Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge1, Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge2);
                                StartRightBottomY -= 2;
                            }
                            else if (Battlefield1[StartRightBottomX, StartRightBottomY - 1] != null)
                            {
                                if (hop == 1 && Battlefield1[StartRightBottomX, StartRightBottomY - 1].PieceAuthority == "bot")
                                {
                                    if (Battlefield1[StartLeftBottomX + 1, StartLeftBottomY] == null)
                                    {
                                        XD = Abandoned[0];
                                        YD = Abandoned[1];

                                        Battlefield1[Abandoned[0] + 1, Abandoned[1]] = Pi;
                                        Battlefield2[Abandoned[0] + 1, Abandoned[1]] = Pi.Character;
                                        Battlefield1[Abandoned[0], Abandoned[1]].ConnectUpperPiece(Battlefield1[Abandoned[0] + 2, Abandoned[1]]);
                                        Battlefield1[Abandoned[0] + 2, Abandoned[1]].ConnectLowerPiece(Battlefield1[Abandoned[0], Abandoned[1]]);

                                        Battlefield1[Abandoned[0], Abandoned[1]].ConnectEdges(Battlefield1[Abandoned[0] + 2, Abandoned[1]].IsEdge1, Battlefield1[Abandoned[0] + 2, Abandoned[1]].IsEdge2);
                                        Battlefield1[Abandoned[0] + 2, Abandoned[1]].ConnectEdges(Battlefield1[Abandoned[0], Abandoned[1]].IsEdge1, Battlefield1[Abandoned[0], Abandoned[1]].IsEdge2);

                                        StartLeftBottomX += 2;
                                        hop = 0;
                                    }
                                    else
                                    {
                                        XD = StartLeftBottomX;
                                        YD = StartLeftBottomY;

                                        Battlefield1[StartLeftBottomX, StartLeftBottomY + 1] = Pi;
                                        Battlefield2[StartLeftBottomX, StartLeftBottomY + 1] = Pi.Character;
                                        Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectLeftPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY + 2]);
                                        Battlefield1[StartLeftBottomX, StartLeftBottomY + 2].ConnectRightPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY]);

                                        Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectEdges(Battlefield1[StartLeftBottomX, StartLeftBottomY - 2].IsEdge1, Battlefield1[StartLeftBottomX, StartLeftBottomY - 2].IsEdge2);
                                        Battlefield1[StartLeftBottomX, StartLeftBottomY - 2].ConnectEdges(Battlefield1[StartLeftBottomY, StartLeftBottomY].IsEdge1, Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge2);

                                        StartLeftBottomY += 2;
                                        hop = 0;
                                    }
                                }
                                else
                                {
                                    if (Battlefield1[StartRightBottomX + 1, StartRightBottomY] == null && Battlefield1[StartRightBottomX + 2, StartRightBottomY].PieceAuthority == "bot")
                                    {
                                        XD = StartRightBottomX;
                                        YD = StartRightBottomY;

                                        Battlefield1[StartRightBottomX + 1, StartRightBottomY] = Pi;
                                        Battlefield2[StartRightBottomX + 1, StartRightBottomY] = Pi.Character;
                                        Battlefield1[StartRightBottomX, StartRightBottomY].ConnectLowerPiece(Battlefield1[StartRightBottomX + 2, StartRightBottomY]);
                                        Battlefield1[StartRightBottomX + 2, StartRightBottomY].ConnectUpperPiece(Battlefield1[StartRightBottomX, StartRightBottomY]);

                                        Battlefield1[StartRightBottomX, StartRightBottomY].ConnectEdges(Battlefield1[StartRightBottomX + 2, StartRightBottomY].IsEdge1, Battlefield1[StartRightBottomX + 2, StartRightBottomY].IsEdge2);
                                        Battlefield1[StartRightBottomX + 2, StartRightBottomY].ConnectEdges(Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge1, Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge2);
                                        StartRightBottomX += 2;
                                    }
                                }
                            }
                        }
                    }
                }
                if (PlayerStartedPosition == "RU") //jeśli zajął prawo góre
                {
                    if (StartRightTopY > 0)
                    {
                        if (Battlefield1[StartRightTopX, StartRightTopY - 1] == null)
                        {
                            XD = StartRightTopX;
                            YD = StartRightTopY;

                            Battlefield1[StartRightTopX, StartRightTopY - 1] = Pi;
                            Battlefield2[StartRightTopX, StartRightTopY - 1] = Pi.Character;
                            Battlefield1[StartRightTopX, StartRightTopY].ConnectLeftPiece(Battlefield1[StartRightTopX, StartRightTopY - 2]);
                            Battlefield1[StartRightTopX, StartRightTopY - 2].ConnectRightPiece(Battlefield1[StartRightTopX, StartRightTopY]);

                            Battlefield1[StartRightTopX, StartRightTopY].ConnectEdges(Battlefield1[StartRightTopX, StartRightTopY - 2].IsEdge1, Battlefield1[StartRightTopX, StartRightTopY - 2].IsEdge2);
                            Battlefield1[StartRightTopX, StartRightTopY - 2].ConnectEdges(Battlefield1[StartRightTopX, StartRightTopY].IsEdge1, Battlefield1[StartRightTopX, StartRightTopY].IsEdge2);
                            StartRightTopY -= 2;
                            BotWantLeftBlocked = 0;
                        }
                        else if (Battlefield1[StartRightTopX, StartRightTopY - 1] != null && hop == 0)
                        {
                            BotWantLeftBlocked++;

                            if (BotWantLeftBlocked < 2)
                            {
                                if (Battlefield1[StartRightTopX - 1, StartRightTopY] == null)
                                {
                                    XD = StartRightTopX;
                                    YD = StartRightTopY;

                                    Battlefield1[StartRightTopX - 1, StartRightTopY] = Pi;
                                    Battlefield2[StartRightTopX - 1, StartRightTopY] = Pi.Character;
                                    Battlefield1[StartRightTopX, StartRightTopY].ConnectUpperPiece(Battlefield1[StartRightTopX - 2, StartRightTopY]);
                                    Battlefield1[StartRightTopX - 2, StartRightTopY].ConnectLowerPiece(Battlefield1[StartRightTopX, StartRightTopY]);

                                    Battlefield1[StartRightTopX, StartRightTopY].ConnectEdges(Battlefield1[StartRightTopX - 2, StartRightTopY].IsEdge1, Battlefield1[StartRightTopX - 2, StartRightTopY].IsEdge2);
                                    Battlefield1[StartRightTopX - 2, StartRightTopY].ConnectEdges(Battlefield1[StartRightTopX, StartRightTopY].IsEdge1, Battlefield1[StartRightTopX, StartRightTopY].IsEdge2);

                                    StartRightTopX -= 2;
                                }
                            }
                            else
                            {
                                if (Battlefield1[StartRightTopX - 2, StartRightTopY - 1] == null)
                                {
                                    Abandoned[0] = StartRightTopX;
                                    Abandoned[1] = StartRightTopY;

                                    StartRightTopX -= 2;
                                    XD = StartRightTopX;
                                    YD = StartRightTopY;

                                    Battlefield1[StartRightTopX, StartRightTopY - 1] = Pi;
                                    Battlefield2[StartRightTopX, StartRightTopY - 1] = Pi.Character;
                                    Battlefield1[StartRightTopX, StartRightTopY].ConnectLeftPiece(Battlefield1[StartRightTopX, StartRightTopY - 2]);
                                    Battlefield1[StartRightTopX, StartRightTopY - 2].ConnectRightPiece(Battlefield1[StartRightTopX, StartRightTopY]);

                                    BotWantLeftBlocked = 0;
                                    StartRightTopY -= 2;
                                    hop++;
                                }
                            }
                        }
                        else
                        {
                            if (Battlefield1[StartLeftTopX, StartLeftTopY + 1] == null)
                            {
                                XD = StartLeftTopX;
                                YD = StartLeftTopY;

                                Battlefield1[StartLeftTopX, StartLeftTopY + 1] = Pi;
                                Battlefield2[StartLeftTopX, StartLeftTopY + 1] = Pi.Character;
                                Battlefield1[StartLeftTopX, StartLeftTopY].ConnectRightPiece(Battlefield1[StartLeftTopX, StartLeftTopY + 2]);
                                Battlefield1[StartLeftTopX, StartLeftTopY + 2].ConnectLeftPiece(Battlefield1[StartLeftTopX, StartLeftTopY]);

                                Battlefield1[StartLeftTopX, StartLeftTopY].ConnectEdges(Battlefield1[StartLeftTopX, StartLeftTopY + 2].IsEdge1, Battlefield1[StartLeftTopX, StartLeftTopY + 2].IsEdge2);
                                Battlefield1[StartLeftTopX, StartLeftTopY + 2].ConnectEdges(Battlefield1[StartLeftTopX, StartLeftTopY].IsEdge1, Battlefield1[StartLeftTopX, StartLeftTopY].IsEdge2);
                                StartLeftTopY += 2;
                            }
                            else if (Battlefield1[StartLeftTopX, StartLeftTopY + 1] != null)
                            {
                                if (hop == 1 && Battlefield1[StartLeftTopX, StartLeftTopY + 1].PieceAuthority == "bot")
                                {
                                    if (Battlefield1[StartRightTopX - 1, StartRightTopY] == null)
                                    {
                                        StartRightTopX = Abandoned[0];
                                        StartRightTopY = Abandoned[1];
                                        XD = Abandoned[0];
                                        YD = Abandoned[1];

                                        Battlefield1[Abandoned[0] - 1, Abandoned[1]] = Pi;
                                        Battlefield2[Abandoned[0] - 1, Abandoned[1]] = Pi.Character;
                                        Battlefield1[Abandoned[0], Abandoned[1]].ConnectUpperPiece(Battlefield1[Abandoned[0] - 2, Abandoned[1]]);
                                        Battlefield1[Abandoned[0] - 2, Abandoned[1]].ConnectLowerPiece(Battlefield1[Abandoned[0], Abandoned[1]]);

                                        Battlefield1[Abandoned[0], Abandoned[1]].ConnectEdges(Battlefield1[Abandoned[0] - 2, Abandoned[1]].IsEdge1, Battlefield1[Abandoned[0] - 2, Abandoned[1]].IsEdge2);
                                        Battlefield1[Abandoned[0] - 2, Abandoned[1]].ConnectEdges(Battlefield1[Abandoned[0], Abandoned[1]].IsEdge1, Battlefield1[Abandoned[0], Abandoned[1]].IsEdge2);

                                        hop = 0;
                                    }
                                    else
                                    {
                                        XD = StartRightTopX;
                                        YD = StartRightTopX;

                                        Battlefield1[StartRightTopX, StartRightTopY - 1] = Pi;
                                        Battlefield2[StartRightTopX, StartRightTopY - 1] = Pi.Character;
                                        Battlefield1[StartRightTopX, StartRightTopY].ConnectLeftPiece(Battlefield1[StartRightTopX, StartRightTopY - 2]);
                                        Battlefield1[StartRightTopX, StartRightTopY - 2].ConnectRightPiece(Battlefield1[StartRightTopX, StartRightTopY]);

                                        Battlefield1[StartRightTopX, StartRightTopY].ConnectEdges(Battlefield1[StartRightTopX, StartRightTopY - 2].IsEdge1, Battlefield1[StartRightTopX, StartRightTopY - 2].IsEdge2);
                                        Battlefield1[StartRightTopX, StartRightTopY - 2].ConnectEdges(Battlefield1[StartRightTopX, StartRightTopY].IsEdge1, Battlefield1[StartRightTopX, StartRightTopY].IsEdge2);

                                        StartRightTopY -= 2;
                                        hop = 0;
                                    }
                                }
                                else
                                {
                                    if (Battlefield1[StartLeftTopX - 1, StartLeftTopY] == null && Battlefield1[StartLeftTopX - 2, StartLeftTopY].PieceAuthority == "bot")
                                    {
                                        XD = StartLeftTopX;
                                        YD = StartLeftTopY;

                                        Battlefield1[StartLeftTopX - 1, StartLeftTopY] = Pi;
                                        Battlefield2[StartLeftTopX - 1, StartLeftTopY] = Pi.Character;
                                        Battlefield1[StartLeftTopX, StartLeftTopY].ConnectUpperPiece(Battlefield1[StartLeftTopX - 2, StartLeftTopY]);
                                        Battlefield1[StartLeftTopX - 2, StartLeftTopY].ConnectLowerPiece(Battlefield1[StartLeftTopX, StartLeftTopY]);

                                        Battlefield1[StartLeftTopX, StartLeftTopY].ConnectEdges(Battlefield1[StartLeftTopX - 2, StartLeftTopY].IsEdge1, Battlefield1[StartLeftTopX - 2, StartLeftTopY].IsEdge2);
                                        Battlefield1[StartLeftTopX - 2, StartLeftTopY].ConnectEdges(Battlefield1[StartLeftTopX, StartLeftTopY].IsEdge1, Battlefield1[StartLeftTopX, StartLeftTopY].IsEdge2);
                                        StartLeftTopX -= 2;
                                    }
                                }
                            }
                        }

                    }
                }  
                if (PlayerStartedPosition == "RD") //jeśli zajął prawo dół
                {
                    if (StartRightBottomY > 0)
                    {
                        if (Battlefield1[StartRightBottomX, StartRightBottomY - 1] == null)
                        {
                            XD = StartRightBottomX;
                            YD = StartRightBottomY;

                            Battlefield1[StartRightBottomX, StartRightBottomY - 1] = Pi;
                            Battlefield2[StartRightBottomX, StartRightBottomY - 1] = Pi.Character;
                            Battlefield1[StartRightBottomX, StartRightBottomY].ConnectLeftPiece(Battlefield1[StartRightBottomX, StartRightBottomY - 2]);
                            Battlefield1[StartRightBottomX, StartRightBottomY - 2].ConnectRightPiece(Battlefield1[StartRightBottomX, StartRightBottomY]);

                            Battlefield1[StartRightBottomX, StartRightBottomY].ConnectEdges(Battlefield1[StartRightBottomX, StartRightBottomY - 2].IsEdge1, Battlefield1[StartRightBottomX, StartRightBottomY - 2].IsEdge2);
                            Battlefield1[StartRightBottomX, StartRightBottomY - 2].ConnectEdges(Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge1, Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge2);

                            StartRightBottomY -= 2;
                            BotWantLeftBlocked = 0;
                        }
                        else if (Battlefield1[StartRightBottomX, StartRightBottomY - 1] != null && hop == 0)
                        {
                            BotWantLeftBlocked++;

                            if (BotWantLeftBlocked < 2)
                            {
                                if (Battlefield1[StartRightBottomX + 1, StartRightBottomY] == null)
                                {
                                    XD = StartRightBottomX;
                                    YD = StartRightBottomY;

                                    Battlefield1[StartRightBottomX + 1, StartRightBottomY] = Pi;
                                    Battlefield2[StartRightBottomX + 1, StartRightBottomY] = Pi.Character;
                                    Battlefield1[StartRightBottomX, StartRightBottomY].ConnectLowerPiece(Battlefield1[StartRightBottomX + 2, StartRightBottomY]);
                                    Battlefield1[StartRightBottomX + 2, StartRightBottomY].ConnectUpperPiece(Battlefield1[StartRightBottomX, StartRightBottomY]);

                                    Battlefield1[StartRightBottomX, StartRightBottomY].ConnectEdges(Battlefield1[StartRightBottomX + 2, StartRightBottomY].IsEdge1, Battlefield1[StartRightBottomX + 2, StartRightBottomY].IsEdge2);
                                    Battlefield1[StartRightBottomX + 2, StartRightBottomY].ConnectEdges(Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge1, Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge2);
 
                                    StartRightBottomX += 2;
                                }
                            }
                            else
                            {
                                if (Battlefield1[StartRightBottomX + 2, StartRightBottomY - 1] == null)
                                {
                                    Abandoned[0] = StartRightBottomX;
                                    Abandoned[1] = StartRightBottomY;

                                    StartRightBottomX += 2;
                                    XD = StartRightBottomX;
                                    YD = StartRightBottomY;

                                    Battlefield1[StartRightBottomX, StartRightBottomY - 1] = Pi;
                                    Battlefield2[StartRightBottomX, StartRightBottomY - 1] = Pi.Character;
                                    Battlefield1[StartRightBottomX, StartRightBottomY].ConnectLeftPiece(Battlefield1[StartRightBottomX, StartRightBottomY - 2]);
                                    Battlefield1[StartRightBottomX, StartRightBottomY - 2].ConnectRightPiece(Battlefield1[StartRightBottomX, StartRightBottomY]);

                                    BotWantLeftBlocked = 0;
                                    StartRightBottomY -= 2;
                                    hop++;
                                }                               
                            }
                        }
                        else
                        {
                            if (Battlefield1[StartLeftBottomX, StartLeftBottomY + 1] == null)
                            {
                                XD = StartLeftBottomX;
                                YD = StartLeftBottomY;

                                Battlefield1[StartLeftBottomX , StartLeftBottomY + 1] = Pi;
                                Battlefield2[StartLeftBottomX, StartLeftBottomY + 1] = Pi.Character;
                                Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectRightPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY + 2]);
                                Battlefield1[StartLeftBottomX, StartLeftBottomY + 2].ConnectLeftPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY]);

                                Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectEdges(Battlefield1[StartLeftBottomX, StartLeftBottomY + 2].IsEdge1, Battlefield1[StartLeftBottomX, StartLeftBottomY + 2].IsEdge2);
                                Battlefield1[StartLeftBottomX, StartLeftBottomY + 2].ConnectEdges(Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge1, Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge2);
                                StartLeftBottomY += 2;
                            }
                            else if (Battlefield1[StartLeftBottomX, StartLeftBottomY + 1] != null)
                            {
                                if (hop == 1 && Battlefield1[StartLeftBottomX, StartLeftBottomY + 1].PieceAuthority == "bot")
                                {
                                    if (Battlefield1[StartRightBottomX + 1, StartRightBottomY] == null)
                                    {
                                        StartRightBottomX = Abandoned[0];
                                        StartRightBottomY = Abandoned[1];
                                        XD = StartRightBottomX;
                                        YD = StartRightBottomY;

                                        Battlefield1[StartRightBottomX + 1, StartRightBottomY] = Pi;
                                        Battlefield2[StartRightBottomX + 1, StartRightBottomY] = Pi.Character;
                                        Battlefield1[StartRightBottomX, StartRightBottomY].ConnectLowerPiece(Battlefield1[StartRightBottomX + 2, StartRightBottomY]);
                                        Battlefield1[StartRightBottomX + 2, StartRightBottomY].ConnectUpperPiece(Battlefield1[StartRightBottomX, StartRightBottomY]);

                                        Battlefield1[StartRightBottomX, StartRightBottomY].ConnectEdges(Battlefield1[StartRightBottomX + 2, StartRightBottomY].IsEdge1, Battlefield1[StartRightBottomX + 2, StartRightBottomY].IsEdge2);
                                        Battlefield1[StartRightBottomX + 2, StartRightBottomY].ConnectEdges(Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge1, Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge2);

                                        StartRightBottomX += 2;
                                        hop = 0;
                                    }
                                    else
                                    {
                                        XD = StartRightBottomX;
                                        YD = StartRightBottomY;

                                        Battlefield1[StartRightBottomX, StartRightBottomY - 1] = Pi;
                                        Battlefield2[StartRightBottomX, StartRightBottomY - 1] = Pi.Character;
                                        Battlefield1[StartRightBottomX, StartRightBottomY].ConnectLeftPiece(Battlefield1[StartRightBottomX, StartRightBottomY - 2]);
                                        Battlefield1[StartRightBottomX, StartRightBottomY - 2].ConnectRightPiece(Battlefield1[StartRightBottomX, StartRightBottomY]);

                                        Battlefield1[StartRightBottomX, StartRightBottomY].ConnectEdges(Battlefield1[StartRightBottomX, StartRightBottomY - 2].IsEdge1, Battlefield1[StartRightBottomX, StartRightBottomY - 2].IsEdge2);
                                        Battlefield1[StartRightBottomX, StartRightBottomY - 2].ConnectEdges(Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge1, Battlefield1[StartRightBottomX, StartRightBottomY].IsEdge2);

                                        StartRightBottomY -= 2;
                                        hop = 0;
                                    }
                                }
                                else
                                {
                                    if (StartLeftBottomX + 2 <= Size)
                                    {
                                        if (Battlefield1[StartLeftBottomX + 1, StartLeftBottomY] == null && Battlefield1[StartLeftBottomX + 2, StartLeftBottomY].PieceAuthority == "bot")
                                        {
                                            XD = StartLeftBottomX;
                                            YD = StartLeftBottomY;

                                            Battlefield1[StartLeftBottomX + 1, StartLeftBottomY] = Pi;
                                            Battlefield2[StartLeftBottomX + 1, StartLeftBottomY] = Pi.Character;
                                            Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectLowerPiece(Battlefield1[StartLeftBottomX + 2, StartLeftBottomY]);
                                            Battlefield1[StartLeftBottomX + 2, StartLeftBottomY].ConnectUpperPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY]);

                                            Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectEdges(Battlefield1[StartLeftBottomX + 2, StartLeftBottomY].IsEdge1, Battlefield1[StartLeftBottomX + 2, StartLeftBottomY].IsEdge2);
                                            Battlefield1[StartLeftBottomX + 2, StartLeftBottomY].ConnectEdges(Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge1, Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge2);
                                            StartLeftBottomX += 2;
                                        }
                                    }
                                    else
                                    {
                                        if (Battlefield1[StartLeftBottomX - 1, StartLeftBottomY] == null)
                                        {
                                            XD = StartLeftBottomX;
                                            YD = StartLeftBottomY;

                                            Battlefield1[StartLeftBottomX - 1, StartLeftBottomY] = Pi;
                                            Battlefield2[StartLeftBottomX - 1, StartLeftBottomY] = Pi.Character;
                                            Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectLowerPiece(Battlefield1[StartLeftBottomX - 2, StartLeftBottomY]);
                                            Battlefield1[StartLeftBottomX - 2, StartLeftBottomY].ConnectUpperPiece(Battlefield1[StartLeftBottomX, StartLeftBottomY]);

                                            Battlefield1[StartLeftBottomX, StartLeftBottomY].ConnectEdges(Battlefield1[StartLeftBottomX - 2, StartLeftBottomY].IsEdge1, Battlefield1[StartLeftBottomX - 2, StartLeftBottomY].IsEdge2);
                                            Battlefield1[StartLeftBottomX - 2, StartLeftBottomY].ConnectEdges(Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge1, Battlefield1[StartLeftBottomX, StartLeftBottomY].IsEdge2);
                                            StartLeftBottomX -= 2;
                                        }
                                    }
                                }
                            }
                            
                        }
                        
                    }                
                }
            }
            else
            {
                if (StartFirstY < Size)
                {
                    if (Battlefield1[StartFirstX, StartFirstY + 1] == null)
                    {
                        XD = StartFirstX;
                        YD = StartFirstY;

                        Battlefield1[StartFirstX, StartFirstY + 1] = Pi;
                        Battlefield2[StartFirstX, StartFirstY + 1] = Pi.Character;

                        Battlefield1[StartFirstX, StartFirstY].ConnectRightPiece(Battlefield1[StartFirstX, StartFirstY + 2]);
                        Battlefield1[StartFirstX, StartFirstY + 2].ConnectLeftPiece(Battlefield1[StartFirstX, StartFirstY]);

                        Battlefield1[StartFirstX, StartFirstY].ConnectEdges(Battlefield1[StartFirstX, StartFirstY + 2].IsEdge1, Battlefield1[StartFirstX, StartFirstY + 2].IsEdge2);
                        Battlefield1[StartFirstX, StartFirstY + 2].ConnectEdges(Battlefield1[StartFirstX, StartFirstY].IsEdge1, Battlefield1[StartFirstX, StartFirstY].IsEdge2);

                        StartFirstY += 2;
                    }
                    else
                    {
                        if (Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY - 1] == null)
                        {
                            XD = ifStartFirstBlockedX;
                            YD = ifStartFirstBlockedY;

                            Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY - 1] = Pi;
                            Battlefield2[ifStartFirstBlockedX, ifStartFirstBlockedY - 1] = Pi.Character;

                            Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY].ConnectRightPiece(Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY - 2]);
                            Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY - 2].ConnectLeftPiece(Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY]);

                            Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY].ConnectEdges(Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY - 2].IsEdge1, Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY - 2].IsEdge2);
                            Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY - 2].ConnectEdges(Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY].IsEdge1, Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY].IsEdge2);

                            ifStartFirstBlockedY -= 2;
                        }
                        else
                        {
                            if (Battlefield1[ifStartFirstBlockedX + 1, ifStartFirstBlockedY] == null)
                            {
                                XD = ifStartFirstBlockedX;
                                YD = ifStartFirstBlockedY;

                                Battlefield1[ifStartFirstBlockedX + 1, ifStartFirstBlockedY] = Pi;
                                Battlefield2[ifStartFirstBlockedX + 1, ifStartFirstBlockedY] = Pi.Character;

                                Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY].ConnectRightPiece(Battlefield1[ifStartFirstBlockedX + 2, ifStartFirstBlockedY]);
                                Battlefield1[ifStartFirstBlockedX + 2, ifStartFirstBlockedY].ConnectLeftPiece(Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY]);

                                Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY].ConnectEdges(Battlefield1[ifStartFirstBlockedX + 2, ifStartFirstBlockedY].IsEdge1, Battlefield1[ifStartFirstBlockedX + 2, ifStartFirstBlockedY].IsEdge2);
                                Battlefield1[ifStartFirstBlockedX + 2, ifStartFirstBlockedY].ConnectEdges(Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY].IsEdge1, Battlefield1[ifStartFirstBlockedX, ifStartFirstBlockedY].IsEdge2);

                                ifStartFirstBlockedX += 2;
                            }

                        }
                    }
                }
            }
        }       
        public void ColorBattlefieldField(Player Pl, Player p1, Player p2)
        {
            if (Pl.PlayerName == p1.PlayerName)
            {
                XD = XP1;
                YD = YP1;
            }
            else
            {
                XD = XP2;
                YD = YP2;
            }

            Console.SetCursorPosition((Console.WindowWidth - Size) / 2, Console.CursorTop);

            for (int i = 0; i < Size + 1; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - Size) / 2, Console.CursorTop);

                for (int j = 0; j < Size + 1; j++)
                {
                    if (Battlefield2[i, j] == p1.Character)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (i == XD && j == YD)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write(p1.Character);
                        }
                        else
                        {
                            Console.Write(p1.Character);
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (Battlefield2[i, j] == p2.Character)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        if (i == XD && j == YD)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write(p2.Character);
                        }
                        else
                        {
                            Console.Write(p2.Character);
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();   
            }
        }          
    }
}
