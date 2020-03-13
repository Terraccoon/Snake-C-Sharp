using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;



namespace Übung_7_Snake
{

    class Food
    {
        #region Variables
        private int width = 20, height = 20;
        //private SolidBrush brush;
        public Rectangle foodrectangle;

        int groesedesFeldes_X = 47;// Feld in dem das Food spawnen soll
        int groesedesFeldes_Y = 38;// Feld in dem das Food spawnen soll
        int groesederKaestchen = 20;// Länge und Breite des Kästchens

        Image Apfel = Image.FromFile(Application.StartupPath + "\\sprides\\Kleinappletranz.png");
        Image Orange = Image.FromFile(Application.StartupPath + "\\sprides\\kleinOrangetranz.png");
        Image Banane = Image.FromFile(Application.StartupPath + "\\sprides\\kleinBananatranz.png");
        Image Pfirsich = Image.FromFile(Application.StartupPath + "\\sprides\\kleinPeachestranz.png");
        Image Maus = Image.FromFile(Application.StartupPath + "\\sprides\\kleinMousetranz.png");

        #endregion

        //public void changecolor()
        //{
        //    switch (Form1.worth)
        //    {
        //        case 1:
        //            brush = new SolidBrush(Color.Red); // Farbe des Food
        //            break;

        //        case 2:
        //            brush = new SolidBrush(Color.Orange); // Farbe des Food
        //            break;

        //        case 3:
        //            brush = new SolidBrush(Color.Yellow); // Farbe des Food
        //            break;

        //        case 4:
        //            brush = new SolidBrush(Color.Blue); // Farbe des Food
        //            break;

        //        case 5:
        //            brush = new SolidBrush(Color.White); // Farbe des Food
        //            break;
        //        default: brush = new SolidBrush(Color.Red); break;
        //    }
        //}

        public Food(Random randomfood)
        {
            //changecolor();

            int x = randomfood.Next(0, groesedesFeldes_X) * groesederKaestchen;
            int y = randomfood.Next(0, groesedesFeldes_Y) * groesederKaestchen;

            foodrectangle = new Rectangle(x, y, width, height);

        }//Startwerte für Normal Food

        public void Foodlocation(Random randomfood)
        {
            //changecolor();


            int x = randomfood.Next(0, groesedesFeldes_X) * groesederKaestchen;// Umkreis vom nächsten Spawn, Range des Gesamtbereichs, Kästchengröße
            int y = randomfood.Next(0, groesedesFeldes_Y) * groesederKaestchen;

            foodrectangle.X = x; //X.Max = 48 * 20 = 960
            foodrectangle.Y = y; //Y.Max = 38 * 20 = 760

            foodrectangle = new Rectangle(x, y, width, height);//Spawn des ersten Food

        }//Spawnbereich für Food

        public void drawFood(Graphics paper)
        {
            //paper.FillRectangle(brush, foodrectangle);

            switch (Form1.worth)
            {
                case 1:
                    paper.DrawImage(Apfel, foodrectangle.X, foodrectangle.Y, new Rectangle(0, 0, groesedesFeldes_X, groesedesFeldes_Y), GraphicsUnit.Pixel);
                    break;

                case 2:
                    paper.DrawImage(Orange, foodrectangle.X, foodrectangle.Y, new Rectangle(0, 0, groesedesFeldes_X, groesedesFeldes_Y), GraphicsUnit.Pixel);
                    break;

                case 3:
                    paper.DrawImage(Banane, foodrectangle.X, foodrectangle.Y, new Rectangle(0, 0, groesedesFeldes_X, groesedesFeldes_Y), GraphicsUnit.Pixel);
                    break;

                case 4:
                    paper.DrawImage(Pfirsich, foodrectangle.X, foodrectangle.Y, new Rectangle(0, 0, groesedesFeldes_X, groesedesFeldes_Y), GraphicsUnit.Pixel);
                    break;

                case 5:
                    paper.DrawImage(Maus, foodrectangle.X, foodrectangle.Y, new Rectangle(0, 0, groesedesFeldes_X, groesedesFeldes_Y), GraphicsUnit.Pixel);
                    break;
                default:
                    paper.DrawImage(Apfel, foodrectangle.X, foodrectangle.Y, new Rectangle(0, 0, groesedesFeldes_X, groesedesFeldes_Y), GraphicsUnit.Pixel);
                    break;
            }
        }
        
    }

    public class Item
    {

        #region Variables spetial items
        public static Rectangle itemrectangle;
        int groesedesFeldes_X = 47;// Feld in dem das Food spawnen soll
        int groesedesFeldes_Y = 38;// Feld in dem das Food spawnen soll
        int groesederKaestchen = 20;// Länge und Breite des Kästchens
        private int width = 20, height = 20;
        #endregion

        public Item(Random randomitem)
        {

            int x = randomitem.Next(0, groesedesFeldes_X) * groesederKaestchen;
            int y = randomitem.Next(0, groesedesFeldes_Y) * groesederKaestchen;

            itemrectangle = new Rectangle(x, y, width, height);
        }

        public void Itemlocation(Random randomitem)
        {
            //changecolor();


            int x = randomitem.Next(0, groesedesFeldes_X) * groesederKaestchen;// Umkreis vom nächsten Spawn, Range des Gesamtbereichs, Kästchengröße
            int y = randomitem.Next(0, groesedesFeldes_Y) * groesederKaestchen;

            itemrectangle.X = x; //X.Max = 48 * 20 = 960
            itemrectangle.Y = y; //Y.Max = 38 * 20 = 760

            itemrectangle = new Rectangle(x, y, width, height);//Spawn point

        }//Spawnbereich für items 
    }  
}
