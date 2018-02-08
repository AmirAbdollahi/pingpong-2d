using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPong2D
{
    public partial class Form1 : Form
    {
        Graphics g;

        Bitmap bmpMain;
        Bitmap bmpPad;
        Bitmap bmpBall;

        RectangleF recRightPad;
        RectangleF recLeftPad;
        RectangleF recBall;

        enum Menus { START, LEVEL, HUMAN, COMPUTER };
        Menus menu;

        enum StartMenuItems { HUMAN_VS_COMPUTER, HUMAN_VS_HUMAN, EXIT };
        StartMenuItems startMenuSelectedItem;

        enum LevelMenuItems {EASY, MEDIUM, HARD};
        LevelMenuItems levelMenuSelectedItem;

        Font fntMenu = new Font("Arial", 15);
        Font fntText = new Font("Arial", 20);
        Font fntScores = new Font("Arial", 40);

        PointF pntDirection = new PointF(1.0f, 0.1f);
        float initialSpeedFactorBall = 3f;
        float speedFactorBall;
        float speedFactorPad = 6f;
        float levelFactor;

        int leftScore = 0;
        int rightScore = 0;

        Random random = new Random(); // used for select starter player

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            speedFactorBall = initialSpeedFactorBall;

            bmpMain = new Bitmap(picMain.Width, picMain.Height);
            bmpPad = new Bitmap(Properties.Resources.Pad);
            bmpBall = new Bitmap(Properties.Resources.Ball);

            // draw right pad at center of right side:
            recRightPad = new RectangleF(picMain.Right - bmpPad.Width, picMain.Height / 2 - bmpPad.Height / 2, bmpPad.Width, bmpPad.Height);
            // draw left pad at center of left side:
            recLeftPad = new RectangleF(picMain.Left, picMain.Height / 2 - bmpPad.Height / 2, bmpPad.Width, bmpPad.Height);

            // select starter player randomly:
            int randomNumber = random.Next(0, 2);
            switch (randomNumber)
            {
                case 0:
                    // draw ball at center of left pad:
                    recBall = new RectangleF(recLeftPad.Right + 0.1f, recLeftPad.Top + recLeftPad.Height / 2 - bmpBall.Height / 2, bmpBall.Width, bmpBall.Height);
                    break;

                case 1:
                    // draw ball at center of right pad:
                    recBall = new RectangleF(recRightPad.Left - bmpBall.Width - 0.1f, recRightPad.Top + recRightPad.Height / 2 - bmpBall.Height / 2, bmpBall.Width, bmpBall.Height);
                    break;
            }

            g = Graphics.FromImage(bmpMain);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            DisplayStartMenuItems(StartMenuItems.HUMAN_VS_COMPUTER);
        }

        private void DisplayStartMenuItems(StartMenuItems selectedItem)
        {
            g.Clear(picMain.BackColor);
            g = Graphics.FromImage(bmpMain);

            switch (selectedItem)
            {
                case StartMenuItems.HUMAN_VS_COMPUTER:
                    g.DrawString("Human vs. Computer", fntMenu, Brushes.Gold, 250, picMain.Top + 150);
                    g.DrawString("Human vs. Human", fntMenu, Brushes.White, 250, picMain.Top + 190);
                    g.DrawString("Exit", fntMenu, Brushes.White, 250, picMain.Top + 230);

                    startMenuSelectedItem = StartMenuItems.HUMAN_VS_COMPUTER;
                    break;

                case StartMenuItems.HUMAN_VS_HUMAN:
                    g.DrawString("Human vs. Computer", fntMenu, Brushes.White, 250, picMain.Top + 150);
                    g.DrawString("Human vs. Human", fntMenu, Brushes.Gold, 250, picMain.Top + 190);
                    g.DrawString("Exit", fntMenu, Brushes.White, 250, picMain.Top + 230);

                    startMenuSelectedItem = StartMenuItems.HUMAN_VS_HUMAN;
                    break;

                case StartMenuItems.EXIT:
                    g.DrawString("Human vs. Computer", fntMenu, Brushes.White, 250, picMain.Top + 150);
                    g.DrawString("Human vs. Human", fntMenu, Brushes.White, 250, picMain.Top + 190);
                    g.DrawString("Exit", fntMenu, Brushes.Gold, 250, picMain.Top + 230);

                    startMenuSelectedItem = StartMenuItems.EXIT;
                    break;
            }

            picMain.Refresh();
            menu = Menus.START;
        }

        private void DisplayLevelMenuItems(LevelMenuItems selectedItem)
        {
            g.Clear(picMain.BackColor);
            g = Graphics.FromImage(bmpMain);

            g.DrawString("Select Level:", fntMenu, Brushes.Gray, 300, picMain.Top + 100);
            switch (selectedItem)
            {
                case LevelMenuItems.EASY:
                    g.DrawString("Easy", fntMenu, Brushes.Gold, 300, picMain.Top + 150);
                    g.DrawString("Medium", fntMenu, Brushes.White, 300, picMain.Top + 190);
                    g.DrawString("Hard", fntMenu, Brushes.White, 300, picMain.Top + 230);

                    levelMenuSelectedItem = LevelMenuItems.EASY;
                    break;

                case LevelMenuItems.MEDIUM:
                    g.DrawString("Easy", fntMenu, Brushes.White, 300, picMain.Top + 150);
                    g.DrawString("Medium", fntMenu, Brushes.Gold, 300, picMain.Top + 190);
                    g.DrawString("Hard", fntMenu, Brushes.White, 300, picMain.Top + 230);

                    levelMenuSelectedItem = LevelMenuItems.MEDIUM;
                    break;

                case LevelMenuItems.HARD:
                    g.DrawString("Easy", fntMenu, Brushes.White, 300, picMain.Top + 150);
                    g.DrawString("Medium", fntMenu, Brushes.White, 300, picMain.Top + 190);
                    g.DrawString("Hard", fntMenu, Brushes.Gold, 300, picMain.Top + 230);

                    levelMenuSelectedItem = LevelMenuItems.HARD;
                    break;
            }

            picMain.Refresh();
            menu = Menus.LEVEL;
        }

        private void StartHumanMode()
        {
            g.Clear(picMain.BackColor);
            g = Graphics.FromImage(bmpMain);

            DisplayScores();
            DisplayStartMessage();
            // draw pad at right side:
            g.DrawImage(bmpPad, recRightPad);
            // draw pad at left side:
            g.DrawImage(bmpPad, recLeftPad);
            // draw ball at center:
            g.DrawImage(bmpBall, recBall);

            picMain.Refresh();

            menu = Menus.HUMAN;
        }

        private void StartComputerMode()
        {
            g.Clear(picMain.BackColor);
            g = Graphics.FromImage(bmpMain);

            DisplayScores();
            DisplayStartMessage();
            // draw pad at right side:
            g.DrawImage(bmpPad, recRightPad);
            // draw pad at left side:
            g.DrawImage(bmpPad, recLeftPad);
            // draw ball at center:
            g.DrawImage(bmpBall, recBall);

            picMain.Refresh();

            menu = Menus.COMPUTER;
        }

        private void DisplayScores()
        {
            // display left score:
            g.DrawString(leftScore.ToString(), fntScores, Brushes.Gray, 70, picMain.Top + 50);
            // display right score:
            g.DrawString(rightScore.ToString(), fntScores, Brushes.Gray, picMain.Width - 120, picMain.Top + 50);
        }

        private void DisplayStartMessage()
        {
            g.DrawString("Press Enter to start", fntText, Brushes.White, picMain.Width / 2 - 130, picMain.Top + 100);
        }

        private void picMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmpMain, 0, 0);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (menu)
            {
                case Menus.START:
                    {
                        if (e.KeyCode == Keys.Down)
                        {
                            switch (startMenuSelectedItem)
                            {
                                case StartMenuItems.HUMAN_VS_COMPUTER:
                                    DisplayStartMenuItems(StartMenuItems.HUMAN_VS_HUMAN);
                                    break;

                                case StartMenuItems.HUMAN_VS_HUMAN:
                                    DisplayStartMenuItems(StartMenuItems.EXIT);
                                    break;
                            }
                        }
                        else if (e.KeyCode == Keys.Up)
                        {
                            switch (startMenuSelectedItem)
                            {
                                case StartMenuItems.EXIT:
                                    DisplayStartMenuItems(StartMenuItems.HUMAN_VS_HUMAN);
                                    break;

                                case StartMenuItems.HUMAN_VS_HUMAN:
                                    DisplayStartMenuItems(StartMenuItems.HUMAN_VS_COMPUTER);
                                    break;
                            }
                        }
                        else if (e.KeyCode == Keys.Enter)
                        {
                            switch (startMenuSelectedItem)
                            {
                                case StartMenuItems.HUMAN_VS_COMPUTER:
                                    DisplayLevelMenuItems(LevelMenuItems.EASY);
                                    break;

                                case StartMenuItems.HUMAN_VS_HUMAN:
                                    levelFactor = 1.0f;
                                    StartHumanMode();
                                    break;

                                case StartMenuItems.EXIT:
                                    Application.Exit();
                                    break;
                            }
                        }
                        break;
                    }

                case Menus.LEVEL:
                    {
                        if (e.KeyCode == Keys.Down)
                        {
                            switch (levelMenuSelectedItem)
                            {
                                case LevelMenuItems.EASY:
                                    DisplayLevelMenuItems(LevelMenuItems.MEDIUM);
                                    break;

                                case LevelMenuItems.MEDIUM:
                                    DisplayLevelMenuItems(LevelMenuItems.HARD);
                                    break;
                            }
                        }
                        else if (e.KeyCode == Keys.Up)
                        {
                            switch (levelMenuSelectedItem)
                            {
                                case LevelMenuItems.HARD:
                                    DisplayLevelMenuItems(LevelMenuItems.MEDIUM);
                                    break;

                                case LevelMenuItems.MEDIUM:
                                    DisplayLevelMenuItems(LevelMenuItems.EASY);
                                    break;
                            }
                        }
                        else if (e.KeyCode == Keys.Enter)
                        {
                            switch (levelMenuSelectedItem)
                            {
                                case LevelMenuItems.EASY:
                                    levelFactor = 1.0f;
                                    StartComputerMode();
                                    break;

                                case LevelMenuItems.MEDIUM:
                                    levelFactor = 1.5f;
                                    StartComputerMode();
                                    break;

                                case LevelMenuItems.HARD:
                                    levelFactor = 2.0f;
                                    StartComputerMode();
                                    break;
                            }
                        }
                        break;
                    }

                case Menus.COMPUTER:
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            tmrBall.Enabled = true;
                        }

                        if (e.KeyCode == Keys.Escape)
                        {
                            tmrBall.Stop();
                            DisplayStartMenuItems(StartMenuItems.HUMAN_VS_COMPUTER);
                        }

                        if (tmrBall.Enabled == true)
                        {
                            if (e.KeyCode == Keys.Up)
                            {
                                tmrRightPadDown.Stop();
                                tmrRightPadUp.Start();
                            }
                            else if (e.KeyCode == Keys.Down)
                            {
                                tmrRightPadUp.Stop();
                                tmrRightPadDown.Start();
                            }
                        }
                        break;
                    }

                case Menus.HUMAN:
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            tmrBall.Enabled = true;
                        }

                        if (e.KeyCode == Keys.Escape)
                        {
                            tmrBall.Stop();
                            DisplayStartMenuItems(StartMenuItems.HUMAN_VS_COMPUTER);
                        }

                        if (tmrBall.Enabled == true)
                        {
                            if (e.KeyCode == Keys.Up)
                            {
                                tmrRightPadDown.Stop();
                                tmrRightPadUp.Start();
                            }
                            else if (e.KeyCode == Keys.Down)
                            {
                                tmrRightPadUp.Stop();
                                tmrRightPadDown.Start();
                            }

                            if (e.KeyCode == Keys.W)
                            {
                                tmrLeftPadDown.Stop();
                                tmrLeftPadUp.Start();
                            }
                            else if (e.KeyCode == Keys.S)
                            {
                                tmrLeftPadUp.Stop();
                                tmrLeftPadDown.Start();
                            }
                        }
                        break;
                    }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (menu)
            {
                case Menus.COMPUTER:
                    {
                        if (e.KeyCode == Keys.Up)
                        {
                            tmrRightPadUp.Stop();
                        }
                        else if (e.KeyCode == Keys.Down)
                        {
                            tmrRightPadDown.Stop();
                        }

                        break;
                    }

                case Menus.HUMAN:
                    {
                        if (e.KeyCode == Keys.Up)
                        {
                            tmrRightPadUp.Stop();
                        }
                        else if (e.KeyCode == Keys.Down)
                        {
                            tmrRightPadDown.Stop();
                        }

                        if (e.KeyCode == Keys.W)
                        {
                            tmrLeftPadUp.Stop();
                        }
                        else if (e.KeyCode == Keys.S)
                        {
                            tmrLeftPadDown.Stop();
                        }
                        break;
                    }
            }
        }

        private void tmrBall_Tick(object sender, EventArgs e)
        {
            if (menu == Menus.COMPUTER)
            {
                if (recBall.Y - recLeftPad.Height / 2 + recBall.Height / 2 < 0)
                {
                    recLeftPad = new RectangleF(0, 0, recLeftPad.Width, recLeftPad.Height);
                }
                else if (recBall.Y - recLeftPad.Height / 2 + recBall.Height / 2 >= picMain.Bottom - recLeftPad.Height)
                {
                    recLeftPad = new RectangleF(0, picMain.Bottom - recLeftPad.Height, recLeftPad.Width, recLeftPad.Height);
                }
                else
                {
                    recLeftPad = new RectangleF(0, recBall.Y - recLeftPad.Height / 2 + recBall.Height / 2, recLeftPad.Width, recLeftPad.Height);
                }
            }

            if (recBall.Right >= recRightPad.Left)
            {
                if (recBall.Bottom >= recRightPad.Top && recBall.Top <= recRightPad.Bottom) // if ball encountered right pad
                {
                    speedFactorBall += 0.3f;

                    if (recBall.Bottom >= recRightPad.Top && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 0.1f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (-0.6f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recRightPad.Top + recRightPad.Height * 0.1f && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 0.2f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (-0.5f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recRightPad.Top + recRightPad.Height * 0.2f && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 0.3f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (-0.4f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recRightPad.Top + recRightPad.Height * 0.3f && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 0.4f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (-0.3f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recRightPad.Top + recRightPad.Height * 0.4f && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 0.5f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (-0.2f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recRightPad.Top + recRightPad.Height * 0.5f && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 0.6f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.1f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recRightPad.Top + recRightPad.Height * 0.6f && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 0.7f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.2f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recRightPad.Top + recRightPad.Height * 0.7f && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 0.8f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.3f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recRightPad.Top + recRightPad.Height * 0.8f && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 0.9f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.4f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recRightPad.Top + recRightPad.Height * 0.9f && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 1.0f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.5f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recRightPad.Top + recRightPad.Height * 1.0f && recBall.Bottom <= recRightPad.Top + recRightPad.Height * 1.1f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.6f) * levelFactor);
                    }
                }
                else if (recBall.Left > picMain.Right) // if ball passes right side
                {
                    leftScore++;
                    speedFactorBall = initialSpeedFactorBall;

                    // draw ball at center of left pad:
                    recBall = new RectangleF(recLeftPad.Right + 0.1f, recLeftPad.Top + recLeftPad.Height / 2 - bmpBall.Height / 2, bmpBall.Width, bmpBall.Height);

                    g.Clear(picMain.BackColor);
                    g = Graphics.FromImage(bmpMain);

                    DisplayScores();
                    DisplayStartMessage();
                    g.DrawImage(bmpPad, recRightPad);
                    g.DrawImage(bmpPad, recLeftPad);
                    g.DrawImage(bmpBall, recBall);
                    picMain.Refresh();

                    tmrBall.Stop();
                    tmrLeftPadUp.Stop();
                    tmrLeftPadDown.Stop();
                    tmrRightPadUp.Stop();
                    tmrRightPadDown.Stop();
                    return;
                }
            }
            else if (recBall.Left <= recLeftPad.Right)
            {
                if (recBall.Bottom >= recLeftPad.Top && recBall.Top <= recLeftPad.Bottom) // if ball encountered left pad
                {
                    speedFactorBall += 0.3f;

                    if (recBall.Bottom >= recLeftPad.Top && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 0.1f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (-0.6f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recLeftPad.Top + recLeftPad.Height * 0.1f && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 0.2f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (-0.5f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recLeftPad.Top + recLeftPad.Height * 0.2f && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 0.3f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (-0.4f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recLeftPad.Top + recLeftPad.Height * 0.3f && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 0.4f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (-0.3f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recLeftPad.Top + recLeftPad.Height * 0.4f && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 0.5f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (-0.2f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recLeftPad.Top + recLeftPad.Height * 0.5f && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 0.6f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.1f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recLeftPad.Top + recLeftPad.Height * 0.6f && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 0.7f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.2f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recLeftPad.Top + recLeftPad.Height * 0.7f && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 0.8f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.3f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recLeftPad.Top + recLeftPad.Height * 0.8f && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 0.9f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.4f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recLeftPad.Top + recLeftPad.Height * 0.9f && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 1.0f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.5f) * levelFactor);
                    }
                    else if (recBall.Bottom >= recLeftPad.Top + recLeftPad.Height * 1.0f && recBall.Bottom <= recLeftPad.Top + recLeftPad.Height * 1.1f)
                    {
                        pntDirection = new PointF(pntDirection.X * (-1f), (0.6f) * levelFactor);
                    }
                }
                else if (recBall.Right < picMain.Left) // if ball passes left side
                {
                    rightScore++;
                    speedFactorBall = initialSpeedFactorBall;

                    // draw ball at center of right pad:
                    recBall = new RectangleF(recRightPad.Left - recBall.Width - 0.1f, recRightPad.Top + recRightPad.Height / 2 - bmpBall.Height / 2, bmpBall.Width, bmpBall.Height);

                    g.Clear(picMain.BackColor);
                    g = Graphics.FromImage(bmpMain);

                    DisplayScores();
                    DisplayStartMessage();
                    g.DrawImage(bmpPad, recRightPad);
                    g.DrawImage(bmpPad, recLeftPad);
                    g.DrawImage(bmpBall, recBall);
                    picMain.Refresh();

                    tmrBall.Stop();
                    tmrLeftPadUp.Stop();
                    tmrLeftPadDown.Stop();
                    tmrRightPadUp.Stop();
                    tmrRightPadDown.Stop();
                    return;
                }
            }
            else if (recBall.Top <= picMain.Top || // if ball encountered top side
                     recBall.Bottom >= picMain.Bottom) // if ball encountered bottom side
            {
                pntDirection = new PointF(pntDirection.X, pntDirection.Y * (-1f));
            }

            if (recBall.Y + pntDirection.Y * speedFactorBall < 0)
            {
                recBall = new RectangleF(recBall.X + pntDirection.X * speedFactorBall, 0, recBall.Width, recBall.Height);
            }
            else if (recBall.Y + pntDirection.Y * speedFactorBall + bmpBall.Height >= picMain.Bottom)
            {
                recBall = new RectangleF(recBall.X + pntDirection.X * speedFactorBall, picMain.Bottom - bmpBall.Height, recBall.Width, recBall.Height);
            }
            else
            {
                recBall = new RectangleF(recBall.X + pntDirection.X * speedFactorBall, recBall.Y + pntDirection.Y * speedFactorBall, recBall.Width, recBall.Height);
            }

            g.Clear(picMain.BackColor);
            g = Graphics.FromImage(bmpMain);

            DisplayScores();
            g.DrawImage(bmpPad, recRightPad);
            g.DrawImage(bmpPad, recLeftPad);
            g.DrawImage(bmpBall, recBall);

            picMain.Refresh();
        }

        private void tmrRightPadUp_Tick(object sender, EventArgs e)
        {
            if (recRightPad.Y - speedFactorPad >= 0)
            {
                recRightPad = new RectangleF(recRightPad.X, recRightPad.Y - speedFactorPad, recRightPad.Width, recRightPad.Height);
            }
            else
            {
                recRightPad = new RectangleF(recRightPad.X, 0, recRightPad.Width, recRightPad.Height);

                g.Clear(picMain.BackColor);
                g = Graphics.FromImage(bmpMain);

                DisplayScores();
                g.DrawImage(bmpPad, recRightPad);
                g.DrawImage(bmpPad, recLeftPad);
                g.DrawImage(bmpBall, recBall);

                picMain.Refresh();

                tmrRightPadUp.Stop();
                return;
            }

            g.Clear(picMain.BackColor);
            g = Graphics.FromImage(bmpMain);

            DisplayScores();
            g.DrawImage(bmpPad, recRightPad);
            g.DrawImage(bmpPad, recLeftPad);
            g.DrawImage(bmpBall, recBall);

            picMain.Refresh();
        }

        private void tmrRightPadDown_Tick(object sender, EventArgs e)
        {
            if (recRightPad.Y + speedFactorPad < picMain.Bottom - recRightPad.Height)
            {
                recRightPad = new RectangleF(recRightPad.X, recRightPad.Y + speedFactorPad, recRightPad.Width, recRightPad.Height);
            }
            else
            {
                recRightPad = new RectangleF(recRightPad.X, picMain.Bottom - recRightPad.Height, recRightPad.Width, recRightPad.Height);

                g.Clear(picMain.BackColor);
                g = Graphics.FromImage(bmpMain);

                DisplayScores();
                g.DrawImage(bmpPad, recRightPad);
                g.DrawImage(bmpPad, recLeftPad);
                g.DrawImage(bmpBall, recBall);

                picMain.Refresh();

                tmrRightPadUp.Stop();
                return;
            }

            g.Clear(picMain.BackColor);
            g = Graphics.FromImage(bmpMain);

            DisplayScores();
            g.DrawImage(bmpPad, recRightPad);
            g.DrawImage(bmpPad, recLeftPad);
            g.DrawImage(bmpBall, recBall);

            picMain.Refresh();
        }

        private void tmrLeftPadUp_Tick(object sender, EventArgs e)
        {
            if (recLeftPad.Y - speedFactorPad >= 0)
            {
                recLeftPad = new RectangleF(recLeftPad.X, recLeftPad.Y - speedFactorPad, recLeftPad.Width, recLeftPad.Height);
            }
            else
            {
                recLeftPad = new RectangleF(recLeftPad.X, 0, recLeftPad.Width, recLeftPad.Height);

                g.Clear(picMain.BackColor);
                g = Graphics.FromImage(bmpMain);

                DisplayScores();
                g.DrawImage(bmpPad, recRightPad);
                g.DrawImage(bmpPad, recLeftPad);
                g.DrawImage(bmpBall, recBall);

                picMain.Refresh();

                tmrRightPadUp.Stop();
                return;
            }

            g.Clear(picMain.BackColor);
            g = Graphics.FromImage(bmpMain);

            DisplayScores();
            g.DrawImage(bmpPad, recRightPad);
            g.DrawImage(bmpPad, recLeftPad);
            g.DrawImage(bmpBall, recBall);

            picMain.Refresh();
        }

        private void tmrLeftPadDown_Tick(object sender, EventArgs e)
        {
            if (recLeftPad.Y + speedFactorPad < picMain.Bottom - recLeftPad.Height)
            {
                recLeftPad = new RectangleF(recLeftPad.X, recLeftPad.Y + speedFactorPad, recLeftPad.Width, recLeftPad.Height);
            }
            else
            {
                recLeftPad = new RectangleF(recLeftPad.X, picMain.Height - recLeftPad.Height, recLeftPad.Width, recLeftPad.Height);

                g.Clear(picMain.BackColor);
                g = Graphics.FromImage(bmpMain);

                DisplayScores();
                g.DrawImage(bmpPad, recRightPad);
                g.DrawImage(bmpPad, recLeftPad);
                g.DrawImage(bmpBall, recBall);

                picMain.Refresh();

                tmrRightPadUp.Stop();
                return;
            }

            g.Clear(picMain.BackColor);
            g = Graphics.FromImage(bmpMain);

            DisplayScores();
            g.DrawImage(bmpPad, recRightPad);
            g.DrawImage(bmpPad, recLeftPad);
            g.DrawImage(bmpBall, recBall);

            picMain.Refresh();
        }

    }
}
