namespace Battleship
{
    internal class Ship
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int[][] Position { get; set; }
        public Alignment Alignment { get; set; }
        public bool Destroyed { get; set; }

        public Ship(string name, int size, Alignment alignment)
        {
            Name = name;
            Size = size;
            Alignment = alignment;
            Position = new int[size][];
            Destroyed = false;
        }
    }
}
