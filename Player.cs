namespace zadaniena5
{
    public class Player : Mother
    {
        public int RandomEntered = 0;
        public string PlayerName = "default_player";
        string UsedPlayerName = "none";
        public int RANDOMENTERED
        {
            get => RandomEntered;
            set
            {
                RandomEntered = value;
            }
        }
        public string USEDPLAYERNAME
        {
            get => UsedPlayerName;
            set
            {
                UsedPlayerName = value;
            }
        }
        public Player() { }
        public void CreateBot()
        {
            PlayerName = "bot";
            Character = '-';
            UsedPlayerName = "player2";
        }
        public bool CheckNickNames(Player pp2)
        {
            if (PlayerName == pp2.PlayerName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckCharacters(Player pp2)
        {
            if (Character == pp2.Character)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
