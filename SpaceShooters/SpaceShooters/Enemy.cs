using System.Drawing;

namespace SpaceShooters
{
    class Enemy
    {
        private int life, maxLife, defense, type, x, y, dx, dy, width, height;
        private bool right, down;
        private Image image;

        public Enemy(int x, int y, int width, int height, bool right, bool down, Image image)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Right = right;
            Down = down;
            Image = image;
        }

        public Enemy(int life, int defense, int type, int x, int y, int dx, int dy, int width, int height, Image image)
        {
            Life = life;
            MaxLife = life;
            Defense = defense;
            Type = type;
            X = x;
            Y = y;
            Dx = dx;
            Dy = dy;
            Width = width;
            Height = height;
            Image = image;
        }

        public int Life { get => life; set => life = value; }
        public int MaxLife { get => maxLife; set => maxLife = value; }
        public int Defense { get => defense; set => defense = value; }
        public int Type { get => type; set => type = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Dx { get => dx; set => dx = value; }
        public int Dy { get => dy; set => dy = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public Image Image { get => image; set => image = value; }
        public bool Right { get => right; set => right = value; }
        public bool Down { get => down; set => down = value; }
    }
}
