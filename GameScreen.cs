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

        List<Point> displayValidMoveSquares = new List<Point>();
        int currentPiece = -1;
        int currentMove = 0;

        #region Brushes & Font:
        SolidBrush kingChecksOverlayBrush1 = new SolidBrush(Color.FromArgb(170, 155, 20, 155));
        SolidBrush kingChecksOverlayBrush2 = new SolidBrush(Color.FromArgb(170, 20, 155, 155));
        SolidBrush overlayBrush = new SolidBrush(Color.FromArgb(70, 155, 155, 155));
        SolidBrush whiteBrush = new SolidBrush(Color.FromArgb(255, 255, 230, 255));
        SolidBrush blackBrush = new SolidBrush(Color.FromArgb(255, 80, 50, 80));
        SolidBrush whiteTextBrush = new SolidBrush(Color.Red);
        SolidBrush blackTextBrush = new SolidBrush(Color.LimeGreen);
        Font font = new Font("Arial", 20, FontStyle.Bold);
        #endregion
        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //King whiteKing = pieceList.Find(x => x.name == "King" & x.color == "White") as King;
            //whitePotentialMoves = kingChecks(whiteKing);
            //King blackKing = pieceList.Find(x => x.name == "King" & x.color == "Black") as King;
            //blackPotentialMoves = kingChecks(blackKing);
            //debugLabel.Text = $"{kingChecks(king)}";
            //if (currentPiece != -1)
            //{
            //    debugLabel.Text = $"{pieceList[currentPiece].position}\n{currentPiece}";
            //}
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
                    //Checker-Board Effect:
                    SolidBrush currentSquareBrush = blackBrush;
                    if ((x + y) % 2 != 0) { currentSquareBrush = whiteBrush; }

                    e.Graphics.FillRectangle(currentSquareBrush, currentSquareRectangle);
                }
            }
            //Show places you can move a piece, I know this is redrawing over what I just did before
            foreach (Point point in displayValidMoveSquares)
            {
                Rectangle currentSquareRectangle = new Rectangle(point.X * 100, point.Y * 100, 100, 100);
                e.Graphics.FillEllipse(overlayBrush, currentSquareRectangle);
            }
            //King whiteKing = pieceList.Find(x => x.name == "King" & x.color == "White") as King;
            //foreach (Point point in whiteKing.enemyMoves)
            //{
            //    Rectangle currentSquareRectangle = new Rectangle(point.X * 100, point.Y * 100, 100, 100);
            //    e.Graphics.DrawEllipse(new Pen(kingChecksOverlayBrush2, 4), currentSquareRectangle);
            //}
            //King blackKing = pieceList.Find(x => x.name == "King" & x.color == "Black") as King;
            //foreach (Point point in blackKing.enemyMoves)
            //{
            //    Rectangle currentSquareRectangle = new Rectangle(point.X * 100, point.Y * 100, 100, 100);
            //    e.Graphics.DrawEllipse(new Pen(kingChecksOverlayBrush1, 4), currentSquareRectangle);
            //}
            #endregion
            #region Draw ALL Pieces:
            foreach (Chesspiece piece in pieceList)
            {
                SolidBrush textBrush = blackTextBrush;
                if (piece.color == "White") { textBrush = whiteTextBrush; }
                e.Graphics.DrawString(piece.name, font, textBrush, new Point((piece.position.X * 100), (piece.position.Y * 100) + 50 - 20));
                e.Graphics.DrawEllipse(new Pen(textBrush), new Rectangle(piece.position.X * 100, piece.position.Y * 100, 100, 100));
            }
            #endregion
        }

        #region Choosing & Moving Pieces:
        private void GameScreen_MouseClick(object sender, MouseEventArgs e)
        {
            Point clickPoint = PointToClient(Cursor.Position);
            clickPoint.X /= 100;
            clickPoint.Y /= 100;

            //Select Piece With Right Click
            if (e.Button == MouseButtons.Right)
            {
                currentPiece = -1;
                for (int i = 0; i < pieceList.Count; i++)
                {
                    if (pieceList[i].position == clickPoint)
                    {
                        currentPiece = i;
                    }
                }

                displayValidMoveSquares.Clear();
                if (currentPiece != -1) //If the piece is not empty space, but an actual piece in the list:
                {
                    string currentColor = (currentMove % 2 == 0) ? "White" : "Black";
                    if (pieceList[currentPiece].color == currentColor) //If the piece belongs to the correct team (has to be a real piece before checking)
                    {
                        pieceList[currentPiece].boardPieceInfo = pieceList;
                        displayValidMoveSquares = pieceList[currentPiece].validMoveSquares();
                    }
                }
            }
            //Piece Action With Left Click
            else if (e.Button == MouseButtons.Left && currentPiece != -1)
            {
                if (displayValidMoveSquares.Contains(clickPoint))
                {
                    pieceList[currentPiece].position = clickPoint;
                    currentMove++;

                    if (pieceList[currentPiece].name == "Pawn")
                    {
                        if (clickPoint.Y == 0 || clickPoint.Y == 7) //Pawn Promotions: 
                        {
                            pieceList[currentPiece] = new Queen(pieceList[currentPiece].color, pieceList[currentPiece].position.X, pieceList[currentPiece].position.Y);
                        }
                        else
                        {
                            Pawn ghostPawn = (Pawn)pieceList[currentPiece];
                            ghostPawn.hasNotMoved = false;
                            pieceList[currentPiece] = ghostPawn;
                        }
                    }

                    for (int i = 0; i < pieceList.Count; i++)
                    {
                        if (pieceList[i].position == clickPoint && i != currentPiece)
                        {
                            if (pieceList[i].name == "King")
                            {
                                Form1.scoreTracker[(pieceList[i].color == "White") ? 0 : 1]++;
                                Form1.ChangeScreen(this, new MainMenu($"{pieceList[i].color} Was Checkmated!\n Score: {Form1.scoreTracker[0]} | {Form1.scoreTracker[1]}"));
                            }
                            pieceList.RemoveAt(i); break;
                        }
                    }
                    currentPiece = -1;
                    updateKingsChecks();
                }

                displayValidMoveSquares.Clear();
            }
        }
        #endregion

        #region Checks & Checkmate
        private List<Point> kingChecks(King king)
        {
            List<Chesspiece> enemyPieces = pieceList.FindAll(x => x.color != king.color);
            List<Point> enemyMovePositions = new List<Point>();
            foreach (Chesspiece enemy in enemyPieces)
            {
                if (enemy is Pawn) 
                {
                    Pawn ghostPawn = (Pawn)enemy;
                    enemyMovePositions.Add(new Point(ghostPawn.position.X + 1, ghostPawn.position.Y + (ghostPawn.direction)));
                    enemyMovePositions.Add(new Point(ghostPawn.position.X - 1, ghostPawn.position.Y + (ghostPawn.direction)));
                }
                else
                {
                    enemy.boardPieceInfo = pieceList;
                    enemyMovePositions.AddRange(enemy.validMoveSquares());
                }
            }
            return enemyMovePositions;
        }

        void updateKingsChecks()
        {
            //Update and check for potential checks.
            List<Chesspiece> kings = pieceList.FindAll(x => x.name == "King");
            foreach (King king in kings)
            {
                List<Point> tempPointList = kingChecks(king);
                king.enemyMoves.Clear();
                king.enemyMoves.AddRange(tempPointList);
            }
        }
        #endregion
        private void GameScreen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //ASK HOW TO REMOVE THIS
        }

        private void GameScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                Application.Exit();
            }
        }
    }
    #region pieceClasses
    public class Chesspiece
    {
        public string color, name;
        public Point position;
        public List<Point> validSquares = new List<Point>();
        public List<Chesspiece> boardPieceInfo = new List<Chesspiece>();
        public List<Point> validMoveSquares()
        {
            validSquares.Clear();
            updateValidSquares();
            return validSquares;
        }

        public List<Point> checkLine(int range, int offsetOne, int offsetTwo)
        {
            List<Point> ghostPointList = new List<Point>();
            for (int i = 1; i <= range; i++)
            {
                Point nextPoint = new Point(position.X + (offsetOne * i), position.Y + (offsetTwo * i));
                Chesspiece nextPiece = boardPieceInfo.Find(x => x.position == nextPoint);
                if (nextPiece != null)
                {
                    if (nextPiece.color == color) { break; }

                    ghostPointList.Add(nextPoint);
                    if (nextPiece.color != color) { break; }
                }
                else { ghostPointList.Add(nextPoint); }
            }
            return ghostPointList;
        }

        public virtual void updateValidSquares() { }
    }
    public class Pawn : Chesspiece
    {
        public bool hasNotMoved = true;
        public int direction;
        public Pawn(string _color, int x, int y)
        {
            color = _color;
            position = new Point(x, y);
            name = "Pawn";
            direction = -1;
            if (color == "White") { direction = 1; }
        }
        public override void updateValidSquares()
        {
            validSquares.AddRange(checkLine(((hasNotMoved) ? 2 : 1), 0, direction, false));
            validSquares.AddRange(checkLine(1, 1, direction, true));
            validSquares.AddRange(checkLine(1, -1, direction, true));
        }

        private List<Point> checkLine(int range, int offsetOne, int offsetTwo, bool diagonal)
        {
            List<Point> ghostPointList = new List<Point>();
            for (int i = 1; i <= range; i++)
            {
                Point nextPoint = new Point(position.X + (offsetOne * i), position.Y + (offsetTwo * i));
                Chesspiece nextPiece = boardPieceInfo.Find(x => x.position == nextPoint);
                if (nextPiece != null)
                {
                    if (nextPiece.color == color) { break; } //if the piece is its own color
                    if (nextPiece.color != color && diagonal == false) { break; } // if the piece is its enemy color, but its moving not attacking
                    ghostPointList.Add(nextPoint);
                }
                else if (diagonal == false) { ghostPointList.Add(nextPoint); }
            }
            return ghostPointList;
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
        public override void updateValidSquares()
        {
            validSquares.AddRange(checkLine(8, 1, 0));
            validSquares.AddRange(checkLine(8, 0, 1));
            validSquares.AddRange(checkLine(8, -1, 0));
            validSquares.AddRange(checkLine(8, 0, -1));
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
        public override void updateValidSquares()
        {
            validSquares.AddRange(checkLine(1, 2, 1));
            validSquares.AddRange(checkLine(1, 1, 2));
            validSquares.AddRange(checkLine(1, -2, 1));
            validSquares.AddRange(checkLine(1, 1, -2));
            validSquares.AddRange(checkLine(1, 2, -1));
            validSquares.AddRange(checkLine(1, -1, 2));
            validSquares.AddRange(checkLine(1, -2, -1));
            validSquares.AddRange(checkLine(1, -1, -2));
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
        public override void updateValidSquares()
        {
            validSquares.AddRange(checkLine(8, 1, 1));
            validSquares.AddRange(checkLine(8, -1, 1));
            validSquares.AddRange(checkLine(8, 1, -1));
            validSquares.AddRange(checkLine(8, -1, -1));
        }
    }
    public class King : Chesspiece
    {
        public List<Point> enemyMoves = new List<Point>();
        public King(string _color, int x, int y)
        {
            color = _color;
            position = new Point(x, y);
            name = "King";
        }
        public override void updateValidSquares()
        {
            validSquares.AddRange(checkLine(1, 1, 0));
            validSquares.AddRange(checkLine(1, 0, 1));
            validSquares.AddRange(checkLine(1, -1, 0));
            validSquares.AddRange(checkLine(1, 0, -1));

            validSquares.AddRange(checkLine(1, 1, 1));
            validSquares.AddRange(checkLine(1, 1, -1));
            validSquares.AddRange(checkLine(1, -1, 1));
            validSquares.AddRange(checkLine(1, -1, -1));
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
        public override void updateValidSquares()
        {
            validSquares.AddRange(checkLine(8, 1, 0));
            validSquares.AddRange(checkLine(8, 0, 1));
            validSquares.AddRange(checkLine(8, -1, 0));
            validSquares.AddRange(checkLine(8, 0, -1));

            validSquares.AddRange(checkLine(8, 1, 1));
            validSquares.AddRange(checkLine(8, 1, -1));
            validSquares.AddRange(checkLine(8, -1, 1));
            validSquares.AddRange(checkLine(8, -1, -1));
        }
    }
    #endregion
}
