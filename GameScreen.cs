using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessExperiment
{
    public partial class GameScreen : UserControl
    {
        #region Chess Pieces at the Start of the Game:
        List<Chesspiece> pieceList = new List<Chesspiece>() {
                new Pawn("White", 0, 1), new Pawn("White", 1, 1),  new Pawn("White", 2, 1),  new Pawn("White", 3, 1), new Pawn("White", 4, 1), new Pawn("White", 5, 1),  new Pawn("White", 6, 1),  new Pawn("White", 7, 1),
                new Rook("White", 0,0),  new Knight("White", 1,0), new Bishop("White", 2,0), new King("White", 3,0),  new Queen("White", 4,0), new Bishop("White", 5,0), new Knight("White", 6,0), new Rook("White", 7,0),
                new Rook("Black", 0,7),  new Knight("Black", 1,7), new Bishop("Black", 2,7), new King("Black", 3,7),  new Queen("Black", 4,7), new Bishop("Black", 5,7), new Knight("Black", 6,7), new Rook("Black", 7,7),
                new Pawn("Black", 0, 6), new Pawn("Black", 1, 6),  new Pawn("Black", 2, 6),  new Pawn("Black", 3, 6), new Pawn("Black", 4, 6), new Pawn("Black", 5, 6),  new Pawn("Black", 6, 6),  new Pawn("Black", 7, 6) };
        #endregion
        #region Brushes & Font:
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteTextBrush = new SolidBrush(Color.Red);
        SolidBrush blackTextBrush = new SolidBrush(Color.Blue);
        Font font = new Font("Arial", 20, FontStyle.Bold);
        #endregion
        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            #region Draw Game Board:
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Rectangle currentSquareRectangle = new Rectangle(x * 100, y * 100, 100, 100);
                    SolidBrush currentSquareBrush = blackBrush;
                    if ((x + y) % 2 != 0) { currentSquareBrush = whiteBrush; }

                    e.Graphics.FillRectangle(currentSquareBrush, currentSquareRectangle);
                }
            }
            #endregion
            #region Draw ALL Pieces:
            foreach (Chesspiece piece in pieceList)
            {
                SolidBrush textBrush = blackTextBrush;
                if (piece.color == "White") { textBrush = whiteTextBrush; }
                e.Graphics.DrawString(piece.name, font, textBrush, new Point((piece.position.X * 100), (piece.position.Y * 100)));
            }
            #endregion
        }
    }
    #region pieceClasses
    public class Chesspiece
    {
        public string color, name;
        public Point position;
    }
    public class Pawn : Chesspiece
    {
        public Pawn(string _color, int x, int y)
        {
            color = _color;
            position = new Point(x, y);
            name = "Pawn";
        }
    }
    public class Rook : Chesspiece
    {
        public Rook(string _color, int x, int y)
        {
            color = _color;
            position = new Point(x, y);
            name = "Rook";
        }
    }
    public class Knight : Chesspiece
    {
        public Knight(string _color, int x, int y)
        {
            color = _color;
            position = new Point(x, y);
            name = "Knight";
        }
    }
    public class Bishop : Chesspiece
    {
        public Bishop(string _color, int x, int y)
        {
            color = _color;
            position = new Point(x, y);
            name = "Bishop";
        }
    }
    public class King : Chesspiece
    {
        public King(string _color, int x, int y)
        {
            color = _color;
            position = new Point(x, y);
            name = "King";
        }
    }
    public class Queen : Chesspiece
    {
        public Queen(string _color, int x, int y)
        {
            color = _color;
            position = new Point(x, y);
            name = "Queen";
        }
    }
    #endregion
}
