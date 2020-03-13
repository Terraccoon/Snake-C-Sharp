using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Timers;


namespace Übung_7_Snake
{
    public enum Direction
    {
        right,
        up,
        left,
        down,
        sleep
    }// Komponente für Richtung mmm

    public partial class Form1 : Form
    {
        #region Variablen
        Graphics paper; //grafic in form1 übertragen
        Snake snake = new Snake(); //neue snake erstellt
        Random randomfood = new Random();
        Food food;
        int Point_Counter = 0; // Startwert für Punkte
        int Lenght_Counter = 3;
        public static int worth;
        #endregion

        public Direction direction = Direction.right;
        public Direction newDirection = Direction.right;
        
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            #region Geschwindigkeit by Lenght
            if (Lenght_Counter == 25)
            {
                gameTimer.Stop();
                gameTimer.Interval = 100;
                gameTimer.Start();
            }
            else if (Lenght_Counter == 50)
            {
                gameTimer.Stop();
                gameTimer.Interval = 75;
                gameTimer.Start();
            }
            else if (Lenght_Counter == 75)
            {
                gameTimer.Stop();
                gameTimer.Interval = 50;
                gameTimer.Start();
            }
            else if (Lenght_Counter >= 100)
            {
                gameTimer.Stop();
                gameTimer.Interval = 25;
                gameTimer.Start();
            }
            #endregion// Schwierigkeit erhöhen

            

            direction = newDirection;
            // Bestimmt die Richtung 
            if (direction == Direction.down) { snake.MoveDown(); }
            if (direction == Direction.up) { snake.MoveUp(); }
            if (direction == Direction.left) { snake.MoveLeft(); }
            if (direction == Direction.right) { snake.MoveRight(); }

            for (int i = 0; i < snake.SnakeRec.Count; i++)
            {
                if (snake.SnakeRec[i].IntersectsWith(food.foodrectangle))
                {
                    #region WennSnakefrisst

                    backgroundWorker2.RunWorkerAsync();// Esssound

                    switch (worth)
                    {
                        case 1:
                            Point_Counter += 10;
                            break;

                        case 2:
                            Point_Counter += 20;
                            break;

                        case 3:
                            Point_Counter += 50;
                            break;

                        case 4:
                            Point_Counter += 100;
                            break;

                        case 5:
                            Point_Counter += 500;
                            break;

                        default:
                            Point_Counter += 10; // Falls worth gleich 0
                            break;
                    }

                    //Point_Counter += 10;
                    Point_Counter_label.Text = Point_Counter.ToString(); // gibt Punkte aus
                    Lenght_Counter += 1;
                    Lenght_Counter_label.Text = Lenght_Counter.ToString(); // gibt Länge aus
                    snake.growsnake();
                    #endregion

                    while (foodIntersects()) // Wenn Food in Snake spawnt, suche neue stelle
                    {
                        food.Foodlocation(randomfood); // Neue Stelle für Food suchen

                        Random rdn = new Random();

                        if (rdn.Next(0, 1000) <= 600)
                        {
                            worth = 1;
                        }
                        else if (rdn.Next(1, 1000) <= 800 && rdn.Next(1, 1000) >= 601)
                        {
                            worth = 2;
                        }
                        else if (rdn.Next(1, 1000) <= 900 && rdn.Next(1, 1000) >= 801)
                        {
                            worth = 3;
                        }
                        else if (rdn.Next(1, 1000) <= 960 && rdn.Next(1, 1000) >= 901)
                        {
                            worth = 4;
                        }
                        else if (rdn.Next(1, 1000) <= 1000 && rdn.Next(1, 1000) >= 961)
                        {
                            worth = 5;
                        }

                        
                    }

                }

                if (snake.SnakeRec[i].IntersectsWith(Item.itemrectangle))
                {
                    // coin sound

                    //Effekte

                    //Effekt in Lable
                }

                //while ()



            }//Wenn mit Food kollidiert

            Collision(); //Waende

            this.Invalidate();
        }// Spielablauf

        public void Collision()
        {
            for (int i = 1; i < snake.SnakeRec.Count; i++)
            {
                if (snake.SnakeRec[0].IntersectsWith(snake.SnakeRec[i]))//Wenn Snake sich selbst beist
                {
                    reset();
                }
            }

            if (snake.SnakeRec[0].X < 0 || snake.SnakeRec[0].X > 950)//Bewgungsfläche für Snake
            {
                reset();
            }

            if (snake.SnakeRec[0].Y < 0 || snake.SnakeRec[0].Y > 740)//Bewgungsfläche für Snake
            {
                reset();
            }
        }// Interaktion mit Objekten

        public void reset()
        {
            gameTimer.Enabled = false; //Timer stoppt

            LoadXmL(Application.StartupPath + "\\Highscore.xml");// Lädt den Highscore
            Vergleiche_Punkte_Highscore();// vergleicht Punkte mit Highscore
            SaveHighscore(Application.StartupPath + "\\Highscore.xml");// Speichert den Highscore
            Highscoreoverview();// Zeigt aktuellen Highscore an

            Point_Counter = 0;// Punktevariable zurück gesetzt
            Lenght_Counter = 3;// Längenvariable zurückgesetzt

            Point_Counter_label.Text = "00";//Punkteanzeige zurückgesetzt
            Lenght_Counter_label.Text = "3";//Längenanzeige zurückgesetzt

            snake = new Snake();// Neue Schlange erstellt
            direction = Direction.right;// Wartet auf eingabe
            newDirection = Direction.right;// Wartet auf eingabe
        }//Reset nach Spielende


        public Form1()
        {
            InitializeComponent();
            food = new Food(randomfood);
            if (gameTimer.Enabled == true)// Verhindert das Snake sofort losläuft (teil des Links debugs)
            {
                gameTimer.Enabled = false;
            }
        }// Spawnt erstes Food

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;
            snake.drawSnake(paper);
            food.drawFood(paper);
        }//Pinselt die Objekte

        public bool foodIntersects()
        {
            for (int i = 0; i < snake.SnakeRec.Count; i++)
            {
                if (snake.SnakeRec[i].IntersectsWith(food.foodrectangle))
                {
                    return true;
                }
            }
            return false;
        }//wenn Food in Snake spawnt, sucht sich neue stelle

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameTimer.Enabled == false)
            {
                gameTimer.Enabled = true;
            }

            if (e.KeyData == Keys.Down && direction != Direction.up)
            {
                newDirection = Direction.down;
            }

            if (e.KeyData == Keys.Up && direction != Direction.down)
            {
                newDirection = Direction.up;
            }

            if (e.KeyData == Keys.Right && direction != Direction.left)
            {
                newDirection = Direction.right;
            }

            if (e.KeyData == Keys.Left && direction != Direction.right)
            {
                newDirection = Direction.left;
            }
        }//Movement / verhindert das Snake durch sich selbst geht


        #region Highscore

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadXmL(Application.StartupPath + "\\Highscore.xml");// Nimmt Datei da raus
            
            Show_Highscore();
        }//Load 

        public void Show_Highscore()
        {
            var DerHighscore = Highscorelist[0].Score;// Save the first entry of the list in a variable
            Highscore_label.Text = DerHighscore.ToString();// save the first entry in the Lable
        }// gibt den aktuellen Highscore ins lable


        [Serializable]
        public class Highscore_obj
        {
            public string Name { get; set; }
            public int Score { get; set; }
        }//Serialisierte Klasse 

        public List<Highscore_obj> Highscorelist = new List<Highscore_obj>()
        {
            new Highscore_obj(),
            new Highscore_obj(),
            new Highscore_obj(),
            new Highscore_obj(),
            new Highscore_obj()
        };// Die Liste vom Highscore


        public void LoadXmL(string path)
        {
            var serializer = new XmlSerializer(typeof(List<Highscore_obj>));

            using (var stream = File.OpenRead(path))
            {
                Highscorelist = (List<Highscore_obj>)serializer.Deserialize(stream);
            }

        }// Laden und zeige Highscore

        public void Vergleiche_Punkte_Highscore()
        {
            int aktuelle_points = Point_Counter;
            if (aktuelle_points > Highscorelist.Last().Score)
            {
                int newpos = Highscorelist.FindIndex(c => c.Score < aktuelle_points);
                Ranglistenfenster Scorefenster = new Ranglistenfenster(newpos+1, aktuelle_points);
                Scorefenster.ShowDialog();
                string spielername = Scorefenster.namedesspielers;
                SpeicherinListe(newpos, spielername, aktuelle_points);
            }
            else
            {
                MessageBox.Show("Points: " + Point_Counter_label.Text);
            }
        }// Nimm Punkte und vergleiche sie mit der XmL (nur wenn hoch genug)

        public void SpeicherinListe(int Position, string Namezumspeichern, int Punkte)
        {            
            Highscorelist.Insert(Position, new Highscore_obj { Name = Namezumspeichern, Score = Punkte });
            Highscorelist.Remove(Highscorelist.Last());

            #region Lambda Beispiel
            // lambda variante, gü fragen
            //Highscorelist.ForEach(c => c.Name = "gü");

            //for(int i = Highscorelist.Count-1; (i > -1) /*|| (aktuelle_points > Highscorelist[i].Score)*/; i--)
            //{                
            //    if(Highscorelist[i].Score < aktuelle_points)
            //    {
            //        newpos = i;
            //    }                                                            
            //}

            //for (int i = 0; i < Highscorelist.Count; i++)
            //{
            //    if (Highscorelist[i].Score > aktuelle_points) newpos = i + 1;
            //    else break;
            //}
            //if (newpos >= Highscorelist.Count) return;
            #endregion
        }// Vergleiche Punkte mit Tabelle (Ein bisschen blöd das so zu machen aber nagut. Wenn's funktioniert erstmal.)

        public void SaveHighscore(string path)
        {
            var serializer = new XmlSerializer(typeof(List<Highscore_obj>));

            using (var stream = File.OpenWrite(path))
            {
                stream.SetLength(0);// Löscht quasi die Liste beim überschreiben um fehler zu vermeiden
                serializer.Serialize(stream, Highscorelist);
            }
        }// Speichere neue Highscoreliste


        public void Highscoreoverview()
        {
            Übersicht Übersichtfenster = new Übersicht(Highscorelist);
            Übersichtfenster.ShowDialog();
        }// erstellt das Übersichtsfenster
        #endregion

        #region split for items
        Random randomitem = new Random();
        Item item;
        

        public void Itemsystem()
        {
            int rndE; // Variable für Random effekt
            int rndS; // Variable für Random spride


            Random randomnumber = new Random();
            rndE = randomnumber.Next(1, 1000);
            rndS = randomnumber.Next(1, 3);

            if(rndE <= 300 && rndE > 0)
            {
                // Lade Effekt
                // slow auf 200 für 10 sek
                //nicht vergessen, dass per länge die intervalle verändert werden
                // Wahrscheinlichkeit 30%
                //dem Feld zuweisen

                int aktinterval = gameTimer.Interval;

                gameTimer.Stop();
                gameTimer.Interval = 200;
                gameTimer.Start();

                var aTimer = new System.Timers.Timer(10000);
                MessageBox.Show("Bis hier hinn funktioniert es.");

                gameTimer.Stop();
                gameTimer.Interval = aktinterval;
                gameTimer.Start();
            }
            else if(rndE <=600 && rndE > 300)
            {
                // Lade Effekt
                // fast auf 20 für 2 - 5 sek
                //nicht vergessen, dass per länge die intervalle verändert werden
                // Wahrscheinlichkeit 30%
                //dem Feld zuweisen
            }

            else if (rndE <= 800 && rndE > 600)
            {
                // Lade Effekt
                // verringere die Punkte um 10 %
                // Wahrscheinlichkeit 20
                //dem Feld zuweisen
            }

            else if (rndE <= 960 && rndE > 800)
            {
                // Lade Effekt
                // verdopple die Punkte die eingesammelt werden für 20 - 30 sek
                // Wahrscheinlichkeit 16
                //dem Feld zuweisen
            }

            else if (rndE <= 990 && rndE > 960)
            {
                // Lade Effekt
                // aktuelle punkte halbieren
                // Wahrscheinlichkeit 3
                //dem Feld zuweisen
            }

            else if (rndE <= 1000 && rndE > 990)
            {
                // Lade Effekt
                // verdopple die aktuelle punktzahl
                // Wahrscheinlichkeit 1
                //dem Feld zuweisen
            }
            
            switch (rndS)
            {
                case 1:
                    // Lade spride
                    Image Candy_Drops = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\tranzcandyklein.png");
                    break;

                case 2:
                    // Lade spride
                    Image Candy_Chocolate = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\tranzchocolatebarklein.png");
                    break;

                case 3:
                    // Lade spride
                    Image Candy_Cookie = Image.FromFile(@"C:\Users\A.Krapp\Desktop\WinForms\Übung 7 Snake\Übung 7 Snake\sprides\tranzcookieklein.png");
                    break;

                default:
                    //Fehlermeldung
                    MessageBox.Show("Error! Could not load Item sprides!");
                    break;
            }






        }






















        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //for (int i = 0; i < 
            //Backgroundworker ist für das laden der Snake sprides













        }//Backgroundworker ist für das laden der Snake sprides

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.Beep(300, 100);
        }// Eatsound
    }
}


