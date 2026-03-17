using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpaceShooters
{
    public partial class SpaceShooters : Form
    {
        private const int DEFAULT_BULLET_WIDTH = 3, DEFAULT_BULLET_HEIGHT = 9, DEFAULT_PLAYER_ATTACK = 3, DEFAULT_PLAYER_DEFENSE = 1, BULLET_SPEED = 10, INTERVAL = 50, OFFSET = 5, MULTIPLIER = 12, DELAY = 9, SMALL_TEXT_SIZE = 10, BIG_TEXT_SIZE = 30;
        private SpaceShooters form;
        private int GAME_AREA_WIDTH, GAME_AREA_HEIGHT;
        private Dictionary<string, Image> images;
        private Dictionary<string, Drops> powerUps;
        private List<Drops> dropss;
        private bool cheat, gameOver, start, pause;
        private Player player, prevPlayer;
        private Enemy enemy, prevEnemy;
        private List<Bullet> playerBullets, prevPlayerBullets, enemyBullets, prevEnemyBullets;
        private Image playerExplosionImg, enemyExplosionImg;
        private int diffX, playerDamagedValTxtShownCtr, playerHealCtr, playerExtraLifeValTxtShownCtr, playerExplosionCtr, enemyCnt, enemySpeed, enemyMoveCtr, enemyDamagedValTxtShownCtr, enemyExplosionCtr, bossFightDelayCtr;
        private string playerDamagedValTxt, playerExtraLifeValTxt, enemyDamagedValTxt, bossFightTxt;
        private Dictionary<string, Timer> timers;
        private EnemyAI enemyAI;
        private int enemyAction;

        public SpaceShooters()
        {
            InitializeComponent();
        }

        private void SpaceShooters_Load(object sender, EventArgs e)
        {
            LoadGame();
        }

        private void LoadGame()
        {
            form = this;
            GAME_AREA_WIDTH = form.ClientSize.Width;
            GAME_AREA_HEIGHT = form.ClientSize.Height;
            enemyAI = new EnemyAI(GAME_AREA_WIDTH, GAME_AREA_HEIGHT, 100);
            enemyAction = -1;
            images = Utils.GetAllImages();
            powerUps = new Dictionary<string, Drops>()
            {
                {"swarm", null},
                {"laser", null},
                {"nuke", null},
                {"shield", null}
            };
            dropss = new List<Drops>();
            start = false;
            pause = false;
            pauseLbl.Text = "PRESS S TO START";
            pauseLbl.Visible = true;
            cheat = false;
            diffX = -1;
            player = new Player(0, 20, DEFAULT_PLAYER_DEFENSE, GAME_AREA_WIDTH / 2 - images["player"].Width / 2, GAME_AREA_HEIGHT - images["player"].Height, images["player"].Width, images["player"].Height, images["player"]);
            prevPlayer = null;
            playerScoreLbl.Text = $"Score: {player.Score}";
            playerLifeBar.Maximum = player.MaxLife;
            playerLifeBar.Value = playerLifeBar.Maximum;
            playerBullets = new List<Bullet>();
            prevPlayerBullets = new List<Bullet>();
            playerDamagedValTxtShownCtr = -1;
            playerDamagedValTxt = "";
            playerHealCtr = 0;
            playerExtraLifeValTxtShownCtr = -1;
            playerExtraLifeValTxt = "";
            playerExplosionCtr = -1;
            playerExplosionImg = null;
            enemy = null;
            prevEnemy = null;
            enemyCnt = 0;
            enemySpeed = 10;
            enemyMoveCtr = -1;
            enemyBullets = new List<Bullet>();
            prevEnemyBullets = new List<Bullet>();
            enemyDamagedValTxtShownCtr = -1;
            enemyDamagedValTxt = "";
            enemyExplosionCtr = -1;
            enemyExplosionImg = null;
            bossFightDelayCtr = -1;
            bossFightTxt = "";
            bossLifeLbl.Visible = false;
            bossLifeBar.Visible = false;
            gameOver = false;
            timers = new Dictionary<string, Timer>();
            Timer timer = new Timer();
            timer.Tick += PlayerShoot;
            timers.Add("PlayerShoot", timer);
            timer = new Timer();
            timer.Tick += PlayerBulletsMove;
            timers.Add("PlayerBulletsMove", timer);
            timer = new Timer();
            timer.Tick += PlayerBulletsOutOfArea;
            timers.Add("PlayerBulletsOutOfArea", timer);
            timer = new Timer();
            timer.Tick += PlayerBulletsDamage;
            timers.Add("PlayerBulletsDamage", timer);
            timer = new Timer();
            timer.Tick += PlayerDamagedCtr;
            timers.Add("PlayerDamagedCtr", timer);
            timer = new Timer();
            timer.Tick += PlayerHeal;
            timers.Add("PlayerHeal", timer);
            timer = new Timer();
            timer.Tick += PlayerExtraLifeCtr;
            timers.Add("PlayerExtraLifeCtr", timer);
            timer = new Timer();
            timer.Tick += PlayerExplosion;
            timers.Add("PlayerExplosion", timer);
            timer = new Timer();
            timer.Tick += EnemySpawn;
            timers.Add("EnemySpawn", timer);
            timer = new Timer();
            timer.Tick += EnemyMove;
            timers.Add("EnemyMove", timer);
            timer = new Timer();
            timer.Tick += EnemyShoot;
            timers.Add("EnemyShoot", timer);
            timer = new Timer();
            timer.Tick += EnemyBulletsMove;
            timers.Add("EnemyBulletsMove", timer);
            timer = new Timer();
            timer.Tick += EnemyBulletsOutOfArea;
            timers.Add("EnemyBulletsOutOfArea", timer);
            timer = new Timer();
            timer.Tick += EnemyBulletsDamage;
            timers.Add("EnemyBulletsDamage", timer);
            timer.Tick += EnemyDamagedCtr;
            timers.Add("EnemyDamagedCtr", timer);
            timer = new Timer();
            timer = new Timer();
            timer.Tick += EnemyExplosion;
            timers.Add("EnemyExplosion", timer);
            timer = new Timer();
            timer.Tick += BossFightDelayCtr;
            timers.Add("BossFightDelayCtr", timer);
            timer = new Timer();
            timer.Tick += DropsMove;
            timers.Add("DropsMove", timer);
            timer = new Timer();
            timer.Tick += DropsOutOfArea;
            timers.Add("DropsOutOfArea", timer);
            timer = new Timer();
            timer.Tick += DropsReceived;
            timers.Add("DropsReceived", timer);
            timer = new Timer();
            timer.Tick += PowerUpDuration;
            timers.Add("PowerUpDuration", timer);
            timer = new Timer();
            timer.Tick += RefreshScreen;
            timers.Add("RefreshScreen", timer);
            foreach (string key in timers.Keys)
            {
                if (key.Contains("Shoot"))
                {
                    timers[key].Interval = INTERVAL * MULTIPLIER;
                }
                else
                {
                    timers[key].Interval = INTERVAL;
                }
                timers[key].Enabled = false;
            }
            form.CreateGraphics();
            form.DoubleBuffered = true;
            form.Invalidate();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            if (start)
            {
                if (!gameOver)
                {
                    if (pause)
                    {
                        DialogResult res = MessageBox.Show("Back to main menu? Your progress will not be saved.", "Back to main menu", MessageBoxButtons.YesNo);
                        if (res == DialogResult.Yes)
                        {
                            form.Hide();
                            Menu menu = new Menu();
                            menu.ShowDialog();
                            form.Close();
                        }
                    }
                }
            }
            else
            {
                DialogResult res = MessageBox.Show("Back to main menu?", "Back to main menu", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    form.Hide();
                    Menu menu = new Menu();
                    menu.ShowDialog();
                    form.Close();
                }
            }
        }

        private void SpaceShooters_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.X)
            {
                if (cheat)
                {
                    player.Defense = DEFAULT_PLAYER_DEFENSE;
                    cheat = false;
                }
                else
                {
                    player.Life = player.MaxLife;
                    playerLifeBar.Value = player.Life;
                    cheat = true;
                }
            }
            else
            {
                if (start)
                {
                    if (!gameOver)
                    {
                        if (e.KeyData == Keys.P)
                        {
                            if (!pause)
                            {
                                pause = true;
                                pauseLbl.Text = "PRESS R TO RESUME";
                                pauseLbl.Visible = true;
                                SetTimersState(false);
                            }
                        }
                        else if (e.KeyData == Keys.R)
                        {
                            if (pause)
                            {
                                pause = false;
                                pauseLbl.Visible = false;
                                SetTimersState(true);
                            }
                        }
                    }
                }
                else
                {
                    if (e.KeyData == Keys.S)
                    {
                        start = true;
                        pauseLbl.Visible = false;
                        SetTimersState(true);
                    }
                }
            }
        }

        private void SpaceShooters_mouseDown(object sender, MouseEventArgs e)
        {
            if (start && !gameOver && !pause)
            {
                Rectangle touchRect = new Rectangle(e.X, e.Y, OFFSET, OFFSET);
                Rectangle playerRect = new Rectangle(player.X, player.Y, player.Width, player.Height);
                if (touchRect.IntersectsWith(playerRect))
                {
                    diffX = e.X - player.X;
                }
            }
        }

        private void SpaceShooters_mouseMove(object sender, MouseEventArgs e)
        {

            if (start && !gameOver && !pause)
            {
                if (diffX != -1)
                {
                    player.X = Math.Min(Math.Max(0, e.X - diffX), GAME_AREA_WIDTH - player.Width);
                }
            }
        }

        private void SpaceShooters_mouseUp(object sender, MouseEventArgs e)
        {
            if (start && !gameOver && !pause)
            {
                if (diffX != -1)
                {
                    diffX = -1;
                    RemoveLasers();
                }
            }
        }

        private void SetTimersState(bool state)
        {
            foreach (string key in timers.Keys)
            {
                timers[key].Enabled = state;
            }
        }

        private void RemoveLasers()
        {
            foreach (Bullet bullet in playerBullets.ToList())
            {
                if (bullet.Type == 1)
                {
                    playerBullets.Remove(bullet);
                }
            }
        }

        private void RefreshScreen(object sender, EventArgs e)
        {
            form.Invalidate();
        }

        private void PlayerShoot(object sender, EventArgs e)
        {
            if (start && !gameOver && player.Life > 0)
            {
                if (diffX != -1)
                {
                    RemoveLasers();
                    if (powerUps["swarm"] != null)
                    {
                        Bullet bullet1 = new Bullet(player.X + (player.Width / 2) - (DEFAULT_BULLET_WIDTH / 2), player.Y - DEFAULT_BULLET_HEIGHT, DEFAULT_BULLET_WIDTH, DEFAULT_BULLET_HEIGHT, DEFAULT_PLAYER_ATTACK, 0, Color.FromArgb(173, 216, 230), null);
                        Bullet bullet2 = new Bullet(player.X - (DEFAULT_BULLET_WIDTH / 2), player.Y - DEFAULT_BULLET_HEIGHT, DEFAULT_BULLET_WIDTH, DEFAULT_BULLET_HEIGHT, DEFAULT_PLAYER_ATTACK, 0, Color.FromArgb(173, 216, 230), null);
                        Bullet bullet3 = new Bullet(player.X + player.Width - (DEFAULT_BULLET_WIDTH / 2), player.Y - DEFAULT_BULLET_HEIGHT, DEFAULT_BULLET_WIDTH, DEFAULT_BULLET_HEIGHT, DEFAULT_PLAYER_ATTACK, 0, Color.FromArgb(173, 216, 230), null);
                        playerBullets.Add(bullet1);
                        playerBullets.Add(bullet2);
                        playerBullets.Add(bullet3);
                    }
                    else if (powerUps["laser"] != null)
                    {
                        Bullet bullet = new Bullet(player.X + player.Width / 2, 0, 1, GAME_AREA_HEIGHT - player.Height, 15, 1, Color.FromArgb(173, 216, 230), null);
                        playerBullets.Add(bullet);
                    }
                    else if (powerUps["nuke"] != null)
                    {
                        Bullet bullet = new Bullet(player.X + (player.Width / 2) - (images["nuke_bullet"].Width / 2), player.Y - images["nuke_bullet"].Height, images["nuke_bullet"].Width, images["nuke_bullet"].Height, 30, 2, Color.Transparent, images["nuke_bullet"]);
                        playerBullets.Add(bullet);
                    }
                    else
                    {
                        Bullet bullet = new Bullet(player.X + (player.Width / 2) - (DEFAULT_BULLET_WIDTH / 2), player.Y - DEFAULT_BULLET_HEIGHT, DEFAULT_BULLET_WIDTH, DEFAULT_BULLET_HEIGHT, DEFAULT_PLAYER_ATTACK, 0, Color.FromArgb(173, 216, 230), null);
                        playerBullets.Add(bullet);
                    }
                }
            }
        }

        private void PlayerBulletsMove(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                foreach (Bullet bullet in playerBullets.ToList())
                {
                    if (bullet.Type != 1)
                    {
                        bullet.Y -= BULLET_SPEED;
                    }
                }
            }
        }

        private void PlayerBulletsOutOfArea(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                foreach (Bullet bullet in playerBullets.ToList())
                {
                    if (bullet.Y + bullet.Height < 0)
                    {
                        playerBullets.Remove(bullet);
                    }
                }
            }
        }

        private void PlayerBulletsDamage(object sender, EventArgs e)
        {
            if (start && !gameOver && enemy != null && enemy.Life > 0)
            {
                Rectangle enemyRect = new Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height);
                foreach (Bullet bullet in playerBullets.ToList())
                {
                    Rectangle bulletRect = new Rectangle(bullet.X, bullet.Y, bullet.Width, bullet.Height);
                    if (bulletRect.IntersectsWith(enemyRect))
                    {
                        if (!cheat)
                        {
                            int damage = Math.Max(1, bullet.Damage - enemy.Defense);
                            enemy.Life = Math.Max(0, enemy.Life - damage);
                            if (bossFightDelayCtr == -2)
                            {
                                bossLifeBar.Value = enemy.Life;
                            }
                            enemyDamagedValTxtShownCtr = 0;
                            enemyDamagedValTxt = $"-{damage}";
                        }
                        else
                        {
                            enemy.Life = 0;
                            if (bossFightDelayCtr == -2)
                            {
                                bossLifeBar.Value = enemy.Life;
                            }
                            enemyDamagedValTxtShownCtr = 0;
                            enemyDamagedValTxt = $"-ꝏ";
                        }
                        if (bullet.Type != 1)
                        {
                            playerBullets.Remove(bullet);
                        }
                    }
                }
                if (enemy.Life <= 0)
                {
                    Random rn = new Random();
                    int prob = rn.Next(100) + 1;
                    if (enemy.Type == -1)
                    {
                        player.Score++;
                    }
                    else if (enemy.Type == -2)
                    {
                        player.Score += 3;
                    }
                    else if (enemy.Type == -3)
                    {
                        player.Score += 5;
                    }
                    else if (enemy.Type == -4)
                    {
                        player.Score += 7;
                    }
                    else if (enemy.Type == -5)
                    {
                        player.Score += 9;
                    }
                    else if (enemy.Type == -6)
                    {
                        player.Score += 13;
                    }
                    else if (enemy.Type == 1)
                    {
                        player.Score += 100;
                    }
                    else if (enemy.Type == 2)
                    {
                        player.Score += 200;
                    }
                    else
                    {
                        player.Score += 500;
                    }
                    enemyAI.UpdateQTable(enemyAction, prevEnemy, prevPlayer, prevEnemyBullets, prevPlayerBullets, enemy, player, enemyBullets, playerBullets, -1000);
                    enemyAI.NextEpoch();
                    if (bossFightDelayCtr == -2)
                    {
                        bossFightDelayCtr = -1;
                        bossLifeBar.Visible = false;
                        bossLifeLbl.Visible = false;
                        // enemySpeed += 3;
                        timers["EnemyShoot"].Interval = INTERVAL * MULTIPLIER;
                        //enemyAI.NextEpoch();
                    }
                    playerScoreLbl.Text = $"Score: {player.Score}";
                    if (prob >= 1 && prob <= 10)
                    {
                        string name = "life";
                        int left = enemy.X, right = enemy.X + enemy.Width;
                        int posX = ((left + right) / 2) - (images[name].Width / 2), posY = enemy.Y + enemy.Height;
                        dropss.Add(new Drops(name, posX, posY, images[name].Width, images[name].Height, 0, -1, images[name]));
                    }
                    else if (prob >= 11 && prob <= 17)
                    {
                        string name = "swarm";
                        int left = enemy.X, right = enemy.X + enemy.Width;
                        int posX = ((left + right) / 2) - (images[name].Width / 2), posY = enemy.Y + enemy.Height;
                        dropss.Add(new Drops(name, posX, posY, images[name].Width, images[name].Height, 1, 140, images[name]));
                    }
                    else if (prob >= 18 && prob <= 22)
                    {
                        string name = "laser";
                        int left = enemy.X, right = enemy.X + enemy.Width;
                        int posX = ((left + right) / 2) - (images[name].Width / 2), posY = enemy.Y + enemy.Height;
                        dropss.Add(new Drops(name, posX, posY, images[name].Width, images[name].Height, 2, 120, images[name]));
                    }
                    else if (prob >= 23 && prob <= 25)
                    {
                        string name = "nuke";
                        int left = enemy.X, right = enemy.X + enemy.Width;
                        int posX = ((left + right) / 2) - (images[name].Width / 2), posY = enemy.Y + enemy.Height;
                        dropss.Add(new Drops(name, posX, posY, images[name].Width, images[name].Height, 4, 80, images[name]));
                    }
                    else if (prob == 26)
                    {
                        string name = "shield";
                        int left = enemy.X, right = enemy.X + enemy.Width;
                        int posX = ((left + right) / 2) - (images[name].Width / 2), posY = enemy.Y + enemy.Height;
                        dropss.Add(new Drops(name, posX, posY, images[name].Width, images[name].Height, 5, 160, images[name]));
                    }
                    enemyMoveCtr = -1;
                    if (enemyExplosionCtr == -2)
                    {
                        if (enemy.Type < 0)
                        {
                            enemyExplosionImg = images["small_explosion1"];
                        }
                        else
                        {
                            enemyExplosionImg = images["big_explosion1"];
                        }
                        enemyExplosionCtr = 0;
                    }
                    //if (enemyCnt >= 10)
                    //{
                    //    bossFightDelayCtr = 0;
                    //    bossFightTxt = "BOSS FIGHT";
                    //    enemyCnt = 0;
                    //    timers["EnemyShoot"].Interval = INTERVAL * 5;
                    //}
                }
            }
        }

        private void PlayerDamagedCtr(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                if (playerDamagedValTxtShownCtr > DELAY)
                {
                    playerDamagedValTxtShownCtr = -1;
                    playerDamagedValTxt = "";
                }
                else if (playerDamagedValTxtShownCtr != -1)
                {
                    playerDamagedValTxtShownCtr++;
                }
            }
        }

        private void PlayerExtraLifeCtr(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                if (playerExtraLifeValTxtShownCtr > DELAY)
                {
                    playerExtraLifeValTxtShownCtr = -1;
                    playerExtraLifeValTxt = "";
                }
                else if (playerExtraLifeValTxtShownCtr != -1)
                {
                    playerExtraLifeValTxtShownCtr++;
                }
            }
        }

        private void PlayerHeal(object sender, EventArgs e)
        {
            if (start && !gameOver && player.Life > 0)
            {
                if (playerHealCtr > 600)
                {
                    playerHealCtr = 0;
                    player.Life = Math.Min(player.Life + 1, player.MaxLife);
                    playerLifeBar.Value = player.Life;
                }
            }
        }

        private void PlayerExplosion(object sender, EventArgs e)
        {
            if (start && !gameOver && player.Life <= 0)
            {
                if (playerExplosionCtr > DELAY)
                {
                    playerExplosionCtr = -1;
                    playerExplosionImg = null;
                    gameOver = true;
                    SetTimersState(false);
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(highscore), 0) FROM leaderboard", DB.conn);
                    DB.conn.Open();
                    int highscore = int.Parse(cmd.ExecuteScalar().ToString());
                    DB.conn.Close();
                    if (player.Score > highscore)
                    {
                        string name = "";
                        do
                        {
                            name = Microsoft.VisualBasic.Interaction.InputBox("Enter your name", "New Highscore !", "", GAME_AREA_WIDTH / 2, GAME_AREA_HEIGHT / 2);
                            if (name.Trim(' ') == "")
                            {
                                MessageBox.Show("Name cannot be empty !");
                            }
                        }
                        while (name.Trim(' ') == "");
                        cmd = new SqlCommand("SELECT ISNULL(MAX(id), 0) + 1 FROM leaderboard", DB.conn);
                        DB.conn.Open();
                        decimal id = decimal.Parse(cmd.ExecuteScalar().ToString());
                        DB.conn.Close();
                        cmd = new SqlCommand("INSERT INTO leaderboard (name, highscore, date) VALUES (@name, @highscore, @date)", DB.conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@highscore", decimal.Parse(player.Score.ToString()));
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                        DB.conn.Open();
                        cmd.ExecuteNonQuery();
                        DB.conn.Close();
                    }
                    DialogResult res = MessageBox.Show("Play again?", "Game Over", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        LoadGame();
                    }
                    else
                    {
                        form.Hide();
                        Menu menu = new Menu();
                        menu.ShowDialog();
                        form.Close();
                    }
                }
                else if (playerExplosionCtr != -1)
                {
                    playerExplosionCtr++;
                    playerExplosionImg = images[$"small_explosion{playerExplosionCtr}"];
                }
            }
        }

        private void EnemySpawn(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                if (enemyExplosionCtr == -1)
                {
                    Random rn = new Random();
                    int[] dirX = { 0, 1, 1, 1, 0, -1, -1, -1 };
                    int[] dirY = { -1, -1, 0, 1, 1, 1, 0, -1 };
                    int index = rn.Next(8), dx = dirX[index] * enemySpeed, dy = dirY[index] * enemySpeed;
                    int prob = rn.Next(100) + 1;
                    enemyMoveCtr = 0;
                    if (bossFightDelayCtr == -1)
                    {
                        enemyExplosionCtr = -2;
                        if (prob >= 1 && prob <= 50)
                        {
                            int posX = rn.Next(GAME_AREA_WIDTH - images["enemy1"].Width - SMALL_TEXT_SIZE * 2);
                            enemy = new Enemy(10, 0, -1, posX, playerLifeBar.Height, dx, dy, images["enemy1"].Width, images["enemy1"].Height, images["enemy1"]);
                        }
                        else if (prob >= 51 && prob <= 70)
                        {
                            int posX = rn.Next(GAME_AREA_WIDTH - images["enemy2"].Width - SMALL_TEXT_SIZE * 2);
                            enemy = new Enemy(20, 1, -2, posX, playerLifeBar.Height, dx, dy, images["enemy2"].Width, images["enemy2"].Height, images["enemy2"]);
                        }
                        else if (prob >= 71 && prob <= 80)
                        {
                            int posX = rn.Next(GAME_AREA_WIDTH - images["enemy3"].Width - SMALL_TEXT_SIZE * 2);
                            enemy = new Enemy(20, 2, -3, posX, playerLifeBar.Height, dx, dy, images["enemy3"].Width, images["enemy3"].Height, images["enemy3"]);
                        }
                        else if (prob >= 81 && prob <= 89)
                        {
                            int posX = rn.Next(GAME_AREA_WIDTH - images["enemy4"].Width - SMALL_TEXT_SIZE * 2);
                            enemy = new Enemy(30, 4, -4, posX, playerLifeBar.Height, dx, dy, images["enemy4"].Width, images["enemy4"].Height, images["enemy4"]);
                        }
                        else if (prob >= 90 && prob <= 97)
                        {
                            int posX = rn.Next(GAME_AREA_WIDTH - images["enemy5"].Width - SMALL_TEXT_SIZE * 2);
                            enemy = new Enemy(30, 5, -5, posX, playerLifeBar.Height, dx, dy, images["enemy5"].Width, images["enemy5"].Height, images["enemy5"]);
                        }
                        else
                        {
                            int posX = rn.Next(GAME_AREA_WIDTH - images["enemy6"].Width - SMALL_TEXT_SIZE * 2);
                            enemy = new Enemy(40, 6, -6, posX, playerLifeBar.Height, dx, dy, images["enemy6"].Width, images["enemy6"].Height, images["enemy6"]);
                        }
                        enemyCnt++;
                    }
                    else if (bossFightDelayCtr == -2)
                    {
                        if (enemy == null || enemy.Life <= 0)
                        {
                            enemyExplosionCtr = -2;
                            bossFightDelayCtr = -2;
                            bossFightTxt = "";
                            if (prob >= 1 && prob <= 70)
                            {
                                int posX = rn.Next(GAME_AREA_WIDTH - images["boss1"].Width - SMALL_TEXT_SIZE * 2);
                                enemy = new Enemy(200, 10, 1, posX, 0, dx, dy, images["boss1"].Width, images["boss1"].Height, images["boss1"]);
                            }
                            else if (prob >= 71 && prob <= 90)
                            {
                                int posX = rn.Next(GAME_AREA_WIDTH - images["boss2"].Width - SMALL_TEXT_SIZE * 2);
                                enemy = new Enemy(300, 15, 2, posX, 0, dx, dy, images["boss2"].Width, images["boss2"].Height, images["boss2"]);
                            }
                            else
                            {
                                int posX = rn.Next(GAME_AREA_WIDTH - images["boss3"].Width - SMALL_TEXT_SIZE * 2);
                                enemy = new Enemy(500, 20, 3, posX, 0, dx, dy, images["boss3"].Width, images["boss3"].Height, images["boss3"]);
                            }
                            bossLifeLbl.Visible = true;
                            bossLifeBar.Maximum = enemy.MaxLife;
                            bossLifeBar.Value = bossLifeBar.Maximum;
                            bossLifeBar.Visible = true;
                        }
                    }
                }
            }
        }

        private void EnemyMove(object sender, EventArgs e)
        {
            if (start && !gameOver && enemy != null && enemy.Life > 0)
            {
                int MAX_X = GAME_AREA_WIDTH - enemy.Width - SMALL_TEXT_SIZE * 2, MAX_Y = GAME_AREA_HEIGHT - enemy.Height - player.Height - OFFSET * 10;
                if (enemyMoveCtr > 20)
                {
                    //Random rn = new Random();
                    //int[] dirX = { 0, 1, 1, 1, 0, -1, -1, -1 };
                    //int[] dirY = { -1, -1, 0, 1, 1, 1, 0, -1 };
                    if (enemyAction != -1 && prevEnemy != null && prevPlayer != null)
                    {
                        enemyAI.UpdateQTable(enemyAction, prevEnemy, prevPlayer, prevEnemyBullets, prevPlayerBullets, enemy, player, enemyBullets, playerBullets);
                    }
                    prevEnemy = new Enemy(enemy.Life, enemy.Defense, enemy.Type, enemy.X, enemy.Y, enemy.Dx, enemy.Dy, enemy.Width, enemy.Height, enemy.Image);
                    prevPlayer = new Player(player.Score, player.Life, player.Defense, player.X, player.Y, player.Width, player.Height, player.Image);
                    prevEnemyBullets.Clear();
                    foreach (Bullet bullet in enemyBullets)
                    {
                        prevEnemyBullets.Add(new Bullet(bullet.X, bullet.Y, bullet.Width, bullet.Height, bullet.Damage, bullet.Type, bullet.Color, bullet.Image));
                    }
                    prevPlayerBullets.Clear();
                    foreach (Bullet bullet in playerBullets)
                    {
                        prevPlayerBullets.Add(new Bullet(bullet.X, bullet.Y, bullet.Width, bullet.Height, bullet.Damage, bullet.Type, bullet.Color, bullet.Image));
                    }
                    enemyAction = enemyAI.Action(enemy, player, enemyBullets, playerBullets);
                    //enemy.Dx = dirX[enemyAction] * enemySpeed;
                    //enemy.Dy = dirY[enemyAction] * enemySpeed;
                    Point enemyAct = enemyAI.GetAction(enemyAction);
                    enemy.Dx = enemyAct.X;
                    enemy.Dy = enemyAct.Y;
                    enemyMoveCtr = 0;
                }
                else if (enemyMoveCtr != -1)
                {
                    int MIN_Y = 0;
                    //if (enemy.X + enemy.Dx < 0)
                    //{
                    //    enemy.Dx = enemySpeed;
                    //}
                    //if (enemy.X + enemy.Dx > MAX_X)
                    //{
                    //    enemy.Dx = -enemySpeed;
                    //}
                    if (enemy.X + enemy.Dx < 0 || enemy.X + enemy.Dx > MAX_X)
                    {
                        enemy.Dx = -enemy.Dx;
                    }
                    if (enemy.Type < 0)
                    {
                        MIN_Y = playerLifeBar.Height;
                    }
                    //if (enemy.Y + enemy.Dy < MIN_Y)
                    //{
                    //    enemy.Dy = enemySpeed;
                    //}
                    //if (enemy.Y + enemy.Dy > MAX_Y)
                    //{
                    //    enemy.Dy = -enemySpeed;
                    //}
                    if (enemy.Y + enemy.Dy < MIN_Y || enemy.Y + enemy.Dy > MAX_Y)
                    {
                        enemy.Dy = -enemy.Dy;
                    }
                    enemy.X = Math.Min(Math.Max(0, enemy.X + enemy.Dx), MAX_X);
                    enemy.Y = Math.Min(Math.Max(MIN_Y, enemy.Y + enemy.Dy), MAX_Y);
                    enemyMoveCtr++;
                }
                //Random rn = new Random();
                //int[] dirX = { 0, 1, 1, 1, 0, -1, -1, -1 };
                //int[] dirY = { -1, -1, 0, 1, 1, 1, 0, -1 };
                //prevEnemy = new Enemy(enemy.Life, enemy.Defense, enemy.Type, enemy.X, enemy.Y, enemy.Dx, enemy.Dy, enemy.Width, enemy.Height, enemy.Image);
                //prevPlayer = new Player(player.Score, player.Life, player.Defense, player.X, player.Y, player.Width, player.Height, player.Image);
                //prevEnemyBullets.Clear();
                //foreach (Bullet bullet in enemyBullets)
                //{
                //    prevEnemyBullets.Add(new Bullet(bullet.X, bullet.Y, bullet.Width, bullet.Height, bullet.Damage, bullet.Type, bullet.Color, bullet.Image));
                //}
                //prevPlayerBullets.Clear();
                //foreach (Bullet bullet in playerBullets)
                //{
                //    prevPlayerBullets.Add(new Bullet(bullet.X, bullet.Y, bullet.Width, bullet.Height, bullet.Damage, bullet.Type, bullet.Color, bullet.Image));
                //}
                //enemyAction = enemyAI.Action(enemy, player, enemyBullets, playerBullets);
                ////enemy.Dx = dirX[enemyAction] * enemySpeed;
                ////enemy.Dy = dirY[enemyAction] * enemySpeed;
                //Point enemyAct = enemyAI.GetAction(enemyAction);
                //enemy.Dx = enemyAct.X;
                //enemy.Dy = enemyAct.Y;
                //enemy.X = Math.Min(Math.Max(0, enemy.X + enemy.Dx), MAX_X);
                //int MIN_Y = 0;
                //if (enemy.Type < 0)
                //{
                //    MIN_Y = playerLifeBar.Height;
                //}
                //enemy.Y = Math.Min(Math.Max(MIN_Y, enemy.Y + enemy.Dy), MAX_Y);
                //enemyAI.UpdateQTable(enemyAction, prevEnemy, prevPlayer, prevEnemyBullets, prevPlayerBullets, enemy, player, enemyBullets, playerBullets);
            }
        }

        private void EnemyShoot(object sender, EventArgs e)
        {
            if (start && !gameOver && enemy != null && enemy.Life > 0)
            {
                Bullet bullet;
                int posX = enemy.X + enemy.Width / 2;
                if (enemy.Type == -1)
                {
                    bullet = new Bullet(posX - DEFAULT_BULLET_WIDTH / 2, enemy.Y + enemy.Height, DEFAULT_BULLET_WIDTH, DEFAULT_BULLET_HEIGHT, 1, 3, Color.FromArgb(255, 215, 0), null);
                }
                else if (enemy.Type == -2)
                {
                    bullet = new Bullet(posX - DEFAULT_BULLET_WIDTH / 2, enemy.Y + enemy.Height, DEFAULT_BULLET_WIDTH, DEFAULT_BULLET_HEIGHT, 3, 4, Color.LightGray, null);
                }
                else if (enemy.Type == -3)
                {
                    bullet = new Bullet(posX - DEFAULT_BULLET_WIDTH / 2, enemy.Y + enemy.Height, DEFAULT_BULLET_WIDTH, DEFAULT_BULLET_HEIGHT, 4, 5, Color.FromArgb(192, 192, 192), null);
                }
                else if (enemy.Type == -4)
                {
                    bullet = new Bullet(posX - DEFAULT_BULLET_WIDTH / 2, enemy.Y + enemy.Height, DEFAULT_BULLET_WIDTH, DEFAULT_BULLET_HEIGHT, 5, 6, Color.FromArgb(200, 69, 19), null);
                }
                else if (enemy.Type == -5)
                {
                    bullet = new Bullet(posX - DEFAULT_BULLET_WIDTH / 2, enemy.Y + enemy.Height, DEFAULT_BULLET_WIDTH, DEFAULT_BULLET_HEIGHT, 6, 7, Color.Blue, null);
                }
                else if (enemy.Type == -6)
                {
                    bullet = new Bullet(posX - DEFAULT_BULLET_HEIGHT / 2, enemy.Y + enemy.Height, DEFAULT_BULLET_HEIGHT, DEFAULT_BULLET_WIDTH, 9, 8, Color.Red, null);
                }
                else if (enemy.Type == 1)
                {
                    bullet = new Bullet(posX - images["boss1_bullet"].Width / 2, enemy.Y + enemy.Height, images["boss1_bullet"].Width, images["boss1_bullet"].Width, 30, 9, Color.Transparent, images["boss1_bullet"]);
                }
                else if (enemy.Type == 2)
                {
                    bullet = new Bullet(posX - images["boss2_bullet"].Width / 2, enemy.Y + enemy.Height, images["boss2_bullet"].Width, images["boss2_bullet"].Width, 50, 10, Color.Transparent, images["boss2_bullet"]);
                }
                else
                {
                    bullet = new Bullet(posX - images["boss3_bullet"].Width / 2, enemy.Y + enemy.Height, images["boss3_bullet"].Width, images["boss3_bullet"].Width, 80, 11, Color.Transparent, images["boss3_bullet"]);
                }
                enemyBullets.Add(bullet);
            }
        }

        private void EnemyBulletsMove(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                foreach (Bullet bullet in enemyBullets.ToList())
                {
                    bullet.Y += BULLET_SPEED;
                }
            }
        }

        private void EnemyBulletsOutOfArea(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                foreach (Bullet bullet in enemyBullets.ToList())
                {
                    if (bullet.Y > GAME_AREA_HEIGHT)
                    {
                        enemyBullets.Remove(bullet);
                    }
                }
            }
        }

        private void EnemyBulletsDamage(object sender, EventArgs e)
        {
            if (start && !gameOver && player.Life > 0)
            {
                foreach (Bullet bullet in enemyBullets.ToList())
                {
                    Rectangle bulletRect = new Rectangle(bullet.X, bullet.Y, bullet.Width, bullet.Height);
                    Rectangle playerRect = new Rectangle(player.X, player.Y, player.Width, player.Height);
                    if (bulletRect.IntersectsWith(playerRect))
                    {
                        enemyBullets.Remove(bullet);
                        if (powerUps["shield"] == null && !cheat)
                        {
                            int damage = Math.Max(1, bullet.Damage - player.Defense);
                            player.Life = Math.Max(0, player.Life - damage);
                            playerLifeBar.Value = player.Life;
                            playerDamagedValTxtShownCtr = 0;
                            playerDamagedValTxt = $"-{damage}";
                            if (player.Life <= 0)
                            {
                                if (playerExplosionCtr == -1)
                                {
                                    playerExplosionImg = images["small_explosion1"];
                                    playerExplosionCtr = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void EnemyDamagedCtr(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                if (enemyDamagedValTxtShownCtr > DELAY)
                {
                    enemyDamagedValTxtShownCtr = -1;
                    enemyDamagedValTxt = "";
                }
                else if (enemyDamagedValTxtShownCtr != -1)
                {
                    enemyDamagedValTxtShownCtr++;
                }
            }
        }

        private void EnemyExplosion(object sender, EventArgs e)
        {
            if (start && !gameOver && enemy != null && enemy.Life <= 0)
            {
                if (enemyExplosionCtr > DELAY)
                {
                    enemyExplosionCtr = -1;
                    enemyExplosionImg = null;
                }
                else if (enemyExplosionCtr >= 0)
                {
                    enemyExplosionCtr++;
                    if (enemy.Type < 0)
                    {
                        enemyExplosionImg = images[$"small_explosion{enemyExplosionCtr}"];
                    }
                    else
                    {
                        enemyExplosionImg = images[$"big_explosion{enemyExplosionCtr}"];
                    }
                }
            }
        }

        private void BossFightDelayCtr(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                if (bossFightDelayCtr >= 0)
                {
                    if (bossFightDelayCtr <= 56)
                    {
                        bossFightDelayCtr++;
                    }
                    else
                    {
                        bossFightDelayCtr = -2;
                    }
                }
            }
        }

        private void DropsMove(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                foreach (Drops drops in dropss.ToList())
                {
                    drops.Y += 10;
                }
            }
        }

        private void DropsOutOfArea(object sender, EventArgs e)
        {
            if (start && !gameOver)
            {
                foreach (Drops drops in dropss.ToList())
                {
                    if (drops.Y > GAME_AREA_HEIGHT)
                    {
                        dropss.Remove(drops);
                    }
                }
            }
        }

        private void DropsReceived(object sender, EventArgs e)
        {
            if (start && !gameOver && player.Life > 0)
            {
                foreach (Drops drops in dropss.ToList())
                {
                    Rectangle dropsRect = new Rectangle(drops.X, drops.Y, drops.Width, drops.Height);
                    Rectangle playerRect = new Rectangle(player.X, player.Y, player.Width, player.Height);
                    if (dropsRect.IntersectsWith(playerRect))
                    {
                        if (drops.Type == 0)
                        {
                            const int EXTRA_LIFE = 5;
                            player.Life = Math.Min(player.Life + EXTRA_LIFE, player.MaxLife);
                            playerLifeBar.Value = player.Life;
                            playerExtraLifeValTxtShownCtr = 0;
                            playerExtraLifeValTxt = $"+{EXTRA_LIFE}";
                        }
                        else
                        {
                            powerUps[drops.Name] = drops;
                            if (drops.Type >= 1 && drops.Type <= 4)
                            {
                                foreach (string key in powerUps.ToDictionary(x => x.Key, x => x.Value).Keys.ToList())
                                {
                                    if (key != drops.Name && key != "shield" && powerUps[key] != null)
                                    {
                                        powerUps[key] = null;
                                    }
                                }
                            }
                        }
                        dropss.Remove(drops);
                    }
                }
            }
        }

        private void PowerUpDuration(object sender, EventArgs e)
        {
            if (start && !gameOver && player.Life > 0)
            {
                foreach (string key in powerUps.ToDictionary(x => x.Key, x => x.Value).Keys.ToList())
                {
                    if (powerUps[key] != null)
                    {
                        if (powerUps[key].Duration <= 0)
                        {
                            powerUps[key] = null;
                        }
                        else
                        {
                            powerUps[key].Duration--;
                        }
                    }
                }
            }
        }

        private void SpaceShooters_onPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (player.Life > 0)
            {
                g.DrawImage(images["player"], player.X, player.Y, player.Width, player.Height);
                if (powerUps["shield"] != null)
                {
                    g.DrawEllipse(new Pen(Color.LightBlue), player.X - 10, player.Y - 10, player.Width + 20, player.Height + 20);
                }
            }
            foreach (Bullet bullet in playerBullets.ToList())
            {
                if (bullet.Type < 2)
                {
                    g.FillRectangle(new SolidBrush(bullet.Color), bullet.X, bullet.Y, bullet.Width, bullet.Height);
                }
                else
                {
                    g.DrawImage(bullet.Image, bullet.X, bullet.Y, bullet.Width, bullet.Height);
                }
            }
            if (playerDamagedValTxt != "")
            {
                Font font = new Font("OCR A Extended", SMALL_TEXT_SIZE);
                float x = player.X + player.Width, y = player.Y;
                g.DrawString(playerDamagedValTxt, font, new SolidBrush(Color.Red), x, y);
            }
            if (playerExtraLifeValTxt != "")
            {
                Font font = new Font("OCR A Extended", SMALL_TEXT_SIZE);
                float x = player.X + player.Width, y = player.Y;
                g.DrawString(playerExtraLifeValTxt, font, new SolidBrush(Color.FromArgb(144, 238, 144)), x, y);
            }
            if (playerExplosionImg != null)
            {
                g.DrawImage(playerExplosionImg, player.X, player.Y, playerExplosionImg.Width, playerExplosionImg.Height);
            }
            if (enemy != null && enemy.Life > 0)
            {
                if (enemy.Type < 0)
                {
                    int remainingLifeWidth = enemy.Life * enemy.Width / enemy.MaxLife;
                    g.FillRectangle(new SolidBrush(Color.Red), enemy.X, enemy.Y - playerLifeBar.Height, remainingLifeWidth, playerLifeBar.Height);
                    g.FillRectangle(new SolidBrush(Color.DarkGray), enemy.X + remainingLifeWidth, enemy.Y - playerLifeBar.Height, enemy.Width - remainingLifeWidth, playerLifeBar.Height);
                }
                g.DrawImage(enemy.Image, enemy.X, enemy.Y, enemy.Width, enemy.Height);
            }
            foreach (Bullet bullet in enemyBullets.ToList())
            {
                if (bullet.Type < 9)
                {
                    g.FillRectangle(new SolidBrush(bullet.Color), bullet.X, bullet.Y, bullet.Width, bullet.Height);
                }
                else
                {
                    g.DrawImage(bullet.Image, bullet.X, bullet.Y, bullet.Width, bullet.Height);
                }
            }
            if (enemyDamagedValTxt != "")
            {
                Font font = new Font("OCR A Extended", SMALL_TEXT_SIZE);
                float x = enemy.X + enemy.Width, y = enemy.Y;
                g.DrawString(enemyDamagedValTxt, font, new SolidBrush(Color.Red), x, y);
            }
            if (enemyExplosionImg != null)
            {
                g.DrawImage(enemyExplosionImg, enemy.X, enemy.Y, enemyExplosionImg.Width, enemyExplosionImg.Height);
            }
            if (bossFightTxt != "")
            {
                Font font = new Font("Algerian", BIG_TEXT_SIZE, FontStyle.Bold);
                float x = (GAME_AREA_WIDTH / 2) - (20 * bossFightTxt.Length / 2), y = (GAME_AREA_HEIGHT / 2) - (BIG_TEXT_SIZE / 2);
                g.DrawString(bossFightTxt, font, new SolidBrush(Color.Red), x, y);
            }
            foreach (Drops drops in dropss.ToList())
            {
                g.DrawImage(drops.Image, drops.X, drops.Y, drops.Width, drops.Height);
            }
            float imgTop = playerScoreLbl.Bottom + OFFSET * 10;
            foreach (string key in powerUps.ToDictionary(x => x.Key, x => x.Value).Keys.ToList())
            {
                if (powerUps[key] != null)
                {
                    Font font = new Font("OCR A Extended", SMALL_TEXT_SIZE);
                    float imgLeft = playerLifeBar.Right - powerUps[key].Width;
                    float imgRight = imgLeft + powerUps[key].Width;
                    float txtLeft = Math.Max((imgLeft + imgRight) / 2 - 23, imgLeft);
                    float txtTop = imgTop + powerUps[key].Height + OFFSET;
                    string durationTxt = $"{INTERVAL * powerUps[key].Duration / 1000}";
                    g.DrawImage(powerUps[key].Image, imgLeft, imgTop, powerUps[key].Width, powerUps[key].Height);
                    g.DrawString($"00:{durationTxt.PadLeft(2, '0')}", font, new SolidBrush(Color.White), txtLeft, txtTop);
                    imgTop += powerUps[key].Height + OFFSET * 5;
                }
            }
        }
    }
}