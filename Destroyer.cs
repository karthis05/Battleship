namespace Battleship
{
    internal class Destroyer
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int[][] Position { get; set; }
        public Alignment Alignment { get; set; }
        public bool Destroyed { get; set; }

        public Destroyer(string name, int size, Alignment alignment)
        {
            Name = name;
            Size = size;
            Alignment = alignment;
            Position = new int[size][];
            Destroyed = false;
        }
    }
}
