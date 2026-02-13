using System;

namespace Games.Model
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeveloperStudio { get; set; }
        public string Style { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string GameMode { get; set; }
        public int SoldCopies { get; set; }

        public override string ToString()
        {
            return $"[ID: {Id}] {Name} ({Style}) | Studio: {DeveloperStudio} | Released: {ReleaseDate:yyyy-MM-dd} | Mode: {GameMode} | Sold: {SoldCopies}";
        }
    }
}