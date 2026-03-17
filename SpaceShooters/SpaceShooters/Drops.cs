using System.Drawing;

namespace SpaceShooters
{
    class Drops
    {
        private string name;
        private int x, y, width, height, type, duration;
        private Image image;

        public Drops(string name, int x, int y, int width, int height, int type, int duration, Image image)
        {
            Name = name;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Type = type;
            Duration = duration;
            Image = image;
        }

        public string Name { get => name; set => name = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int Type { get => type; set => type = value; }
        public int Duration { get => duration; set => duration = value; }
        public Image Image { get => image; set => image = value; }
    }
}
