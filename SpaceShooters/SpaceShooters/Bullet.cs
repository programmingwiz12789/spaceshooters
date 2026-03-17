using System.Drawing;

namespace SpaceShooters
{
    class Bullet
    {
        private int x, y, width, height, damage, type;
        private Color color;
        private Image image;

        public Bullet(int x, int y, int width, int height, int damage, int type, Color color, Image image)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Damage = damage;
            Type = type;
            Color = color;
            Image = image;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Type { get => type; set => type = value; }
        public Color Color { get => color; set => color = value; }
        public Image Image { get => image; set => image = value; }
    }
}
