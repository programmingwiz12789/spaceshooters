using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace SpaceShooters
{
    public partial class Menu : Form
    {
        private const int ENEMY_SPEED = 10;
        private int MENU_AREA_WIDTH, MENU_AREA_HEIGHT;
        private Menu form;
        private Dictionary<string, Image> images;
        private Enemy enemy;
        
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            form = this;
            MENU_AREA_WIDTH = form.ClientSize.Width;
            MENU_AREA_HEIGHT = form.ClientSize.Height;
            images = Utils.GetAllImages();
            enemy = null;
            Random rn = new Random();
            int prob = rn.Next(100) + 1;
            if (prob >= 1 && prob <= 40)
            {
                int posX = rn.Next(MENU_AREA_WIDTH - images["enemy1"].Width);
                enemy = new Enemy(posX, 0, images["enemy1"].Width, images["enemy1"].Height, true, false, images["enemy1"]);
            }
            else if (prob >= 41 && prob <= 60)
            {
                int posX = rn.Next(MENU_AREA_WIDTH - images["enemy2"].Width);
                enemy = new Enemy(posX, 0, images["enemy2"].Width, images["enemy2"].Height, true, false, images["enemy2"]);
            }
            else if (prob >= 61 && prob <= 70)
            {
                int posX = rn.Next(MENU_AREA_WIDTH - images["enemy3"].Width);
                enemy = new Enemy(posX, 0, images["enemy3"].Width, images["enemy3"].Height, true, false, images["enemy3"]);
            }
            else if (prob >= 71 && prob <= 79)
            {
                int posX = rn.Next(MENU_AREA_WIDTH - images["enemy4"].Width);
                enemy = new Enemy(posX, 0, images["enemy4"].Width, images["enemy4"].Height, true, false, images["enemy4"]);
            }
            else if (prob >= 80 && prob <= 87)
            {
                int posX = rn.Next(MENU_AREA_WIDTH - images["enemy5"].Width);
                enemy = new Enemy(posX, 0, images["enemy5"].Width, images["enemy5"].Height, true, false, images["enemy5"]);
            }
            else if (prob >= 88 && prob <= 94)
            {
                int posX = rn.Next(MENU_AREA_WIDTH - images["enemy6"].Width);
                enemy = new Enemy(posX, 0, images["enemy6"].Width, images["enemy6"].Height, true, false, images["enemy6"]);
            }
            else if (prob >= 95 && prob <= 97)
            {
                int posX = rn.Next(MENU_AREA_WIDTH - images["boss1"].Width);
                enemy = new Enemy(posX, 0, images["boss1"].Width, images["boss1"].Height, true, false, images["boss1"]);
            }
            else if (prob >= 98 && prob <= 99)
            {
                int posX = rn.Next(MENU_AREA_WIDTH - images["boss2"].Width);
                enemy = new Enemy(posX, 0, images["boss2"].Width, images["boss2"].Height, true, false, images["boss2"]);
            }
            else
            {
                int posX = rn.Next(MENU_AREA_WIDTH - images["boss3"].Width);
                enemy = new Enemy(posX, 0, images["boss3"].Width, images["boss3"].Height, true, false, images["boss3"]);
            }
            timer1.Enabled = true;
            CreateGraphics();
            Invalidate();
            DoubleBuffered = true;
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            form.Hide();
            SpaceShooters spaceShooters = new SpaceShooters();
            spaceShooters.ShowDialog();
            form.Close();
        }

        private void leaderboardBtn_Click(object sender, EventArgs e)
        {
            form.Hide();
            Leaderboard leaderboard = new Leaderboard();
            leaderboard.ShowDialog();
            form.Close();
        }

        private void controlsBtn_Click(object sender, EventArgs e)
        {
            form.Hide();
            Controls controls = new Controls();
            controls.ShowDialog();
            form.Close();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            form.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int LIMIT_X = MENU_AREA_WIDTH - enemy.Width, LIMIT_Y = MENU_AREA_HEIGHT - enemy.Height;
            if (enemy.Right)
            {
                enemy.Dx = ENEMY_SPEED;
                enemy.Dy = 0;
                if (enemy.X + enemy.Dx >= LIMIT_X)
                {
                    enemy.Dx = -ENEMY_SPEED;
                    enemy.Right = false;
                    if (enemy.Down)
                    {
                        if (enemy.Y + enemy.Dy >= LIMIT_Y)
                        {
                            enemy.Dy = -ENEMY_SPEED * 2;
                            enemy.Down = false;
                        }
                        else
                        {
                            enemy.Dy = ENEMY_SPEED * 2;
                        }
                    }
                    else
                    {
                        if (enemy.Y + enemy.Dy <= 0)
                        {
                            enemy.Dy = ENEMY_SPEED * 2;
                            enemy.Down = true;
                        }
                        else
                        {
                            enemy.Dy = -ENEMY_SPEED * 2;
                        }
                    }
                }
            }
            else
            {
                enemy.Dx = -ENEMY_SPEED;
                enemy.Dy = 0;
                if (enemy.X + enemy.Dx <= 0)
                {
                    enemy.Dx = ENEMY_SPEED;
                    enemy.Right = true;
                    if (enemy.Down)
                    {
                        if (enemy.Y + enemy.Dy >= LIMIT_Y)
                        {
                            enemy.Dy = -ENEMY_SPEED * 2;
                            enemy.Down = false;
                        }
                        else
                        {
                            enemy.Dy = ENEMY_SPEED * 2;
                        }
                    }
                    else
                    {
                        if (enemy.Y + enemy.Dy <= 0)
                        {
                            enemy.Dy = ENEMY_SPEED * 2;
                            enemy.Down = true;
                        }
                        else
                        {
                            enemy.Dy = -ENEMY_SPEED * 2;
                        }
                    }
                }
            }
            enemy.X = Math.Min(Math.Max(0, enemy.X + enemy.Dx), LIMIT_X);
            enemy.Y = Math.Min(Math.Max(0, enemy.Y + enemy.Dy), LIMIT_Y);
            form.Invalidate();
        }

        private void SpaceShooters_onPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(enemy.Image, enemy.X, enemy.Y, enemy.Width, enemy.Height);
        }
    }
}
