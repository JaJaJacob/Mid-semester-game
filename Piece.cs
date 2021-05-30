namespace zadaniena5
{
    public class Piece : Mother
    {
        public bool IsEdge1;
        public bool IsEdge2;
        public int Xcoordinate, Ycoordinate;
        public Piece UpperPiece;
        public Piece LowerPiece;
        public Piece RightPiece;
        public Piece LeftPiece;
        public string PieceDefaultAuthority = "default_player";
        public string PieceAuthority = "default_player";
        public bool wartos_test = false;
        public Piece(Player Pl)
        {
            this.PieceAuthority = Pl.PlayerName;
            Character = Pl.Character;
        }
        public Piece(Player Pl, bool IsEdge1, bool IsEdge2)
        {
            this.PieceAuthority = Pl.PlayerName;
            Character = Pl.Character;
            this.IsEdge1 = IsEdge1;
            this.IsEdge2 = IsEdge2;
        }
        public void ConnectUpperPiece(Piece Pi)
        {
            UpperPiece = Pi;
        }
        public void ConnectLowerPiece(Piece Pi)
        {
            LowerPiece = Pi;
        }
        public void ConnectRightPiece(Piece Pi)
        {
            RightPiece = Pi;
        }
        public void ConnectLeftPiece(Piece Pi)
        {
            LeftPiece = Pi;
        }
        public void ConnectEdges(bool Edge1, bool Edge2)
        {
            if (Edge1 == IsEdge1 && Edge2 == IsEdge2)
                return;

            if (Edge1) IsEdge1 = true;
            if (Edge2) IsEdge2 = true;
            if (Edge1 && Edge2) return;

            if (UpperPiece != null) UpperPiece.ConnectEdges(IsEdge1, IsEdge2);
            if (LowerPiece != null) LowerPiece.ConnectEdges(IsEdge1, IsEdge2);
            if (LeftPiece != null) LeftPiece.ConnectEdges(IsEdge1, IsEdge2);
            if (RightPiece != null) RightPiece.ConnectEdges(IsEdge1, IsEdge2);
        }      
        public bool CheckWinPlayer()
        {
            if (IsEdge1 && IsEdge2) return true;
            else return false;
        }  
        
        public bool IsConnected()
        {
            if (IsEdge1 && IsEdge2)
                return true;

            return false;
        }
    }
}
