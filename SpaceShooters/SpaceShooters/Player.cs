using System.Drawing;

namespace SpaceShooters
{
    class Player
    {
        private int score, life, maxLife, defense, x, y, width, height;
        private Image image;

        public Player(int score, int life, int defense, int x, int y, int width, int height, Image image)
        {
            Score = score;
            Life = life;
            MaxLife = life;
            Defense = defense;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Image = image;
        }

        public int Score { get => score; set => score = value; }
        public int Life { get => life; set => life = value; }
        public int MaxLife { get => maxLife; set => maxLife = value; }
        public int Defense { get => defense; set => defense = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public Image Image { get => image; set => image = value; }
    }
}
