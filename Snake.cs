using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Übung_7_Snake
{

    public class Snake
    {
        private List<Rectangle> SnakeRectangle; //erstellt ein rechteck
        private SolidBrush brush; //Bemalt formen
        private int x, y, width = 20, hight = 20;    //Variablen

        #region Snakespride

        //Image Snakehead_up = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snakehead_upklein.png");
        //Image Snakehead_down = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snakehead_downklein.png");
        //Image Snakehead_left = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snakehead_leftklein.png");
        //Image Snakehead_right = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snakehead_rightklein.png");

        //Image Snakechest_up = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snakechest_upklein.png");
        //Image Snakechest_down = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snakechest_downklein.png");
        //Image Snakechest_left = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snakechest_leftklein.png");
        //Image Snakechest_right = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snakechest_rightklein.png");

        //Image Snaketail_up = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snaketail_upklein.png");
        //Image Snaketail_down = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snaketail_downklein.png");
        //Image Snaketail_left = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snaketail_leftklein.png");
        //Image Snaketail_right = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\Snake\Snaketail_rightklein.png");

        #endregion

        //private TextureBrush Texture;

        public List<Rectangle> SnakeRec
        {
            get { return SnakeRectangle; }
        }// Snake läuft über eine Liste

        public Snake()
        {
            SnakeRectangle = new List<Rectangle>(); // erzeuge 3 neue rechtecke
            //Texture = new TextureBrush();
            brush = new SolidBrush(Color.ForestGreen); // bestimme farbe
            x = 60; //position der Rechtecke
            y = 20;//position der Rechtecke
            
            for (int i = 0; i < 3; i++)
            {
                SnakeRectangle.Add(new Rectangle(x, y, width, hight));
                x -= 20;               
            }
        }//Form der Schlange

        public void drawSnake(Graphics paper)
        {
            foreach (Rectangle rect in SnakeRectangle)
            {
                paper.FillRectangle(brush, rect);
                //paper.DrawImage(Snakechest_right, SnakeRec[SnakeRec.Count - 1].X, SnakeRec[SnakeRec.Count - 1].Y, new Rectangle(0, 0, 20, 20), GraphicsUnit.Pixel);
            }
        }//Zeichne Schlange

        public void drawSnake()
        {
            for (int i = SnakeRectangle.Count - 1; i > 0; i--)
            {
                SnakeRectangle[i] = SnakeRectangle[i - 1];
            }
        }// Der Arsch verschwindet

        public void growsnake()
        {
            SnakeRec.Add(new Rectangle(SnakeRec[SnakeRec.Count - 1].X, SnakeRec[SnakeRec.Count - 1].Y, width, hight));
        }// Der Kopf wächst / Vorwärtsbewegung

        #region Movement
        public void MoveDown()
        {
            drawSnake();

            var temp = SnakeRectangle[0];
            temp.Y += 20;
            SnakeRectangle[0] = temp;
        }
        public void MoveUp()
        {
            drawSnake();

            var temp = SnakeRectangle[0];
            temp.Y -= 20;
            SnakeRectangle[0] = temp;
        }
        public void MoveRight()
        {
            drawSnake();

            var temp = SnakeRectangle[0];
            temp.X += 20;
            SnakeRectangle[0] = temp;
        }
        public void MoveLeft()
        {
            drawSnake();

            var temp = SnakeRectangle[0];
            temp.X -= 20;
            SnakeRectangle[0] = temp;
        }
        #endregion

    }
}
