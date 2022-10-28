namespace TetrisDotNet
{
    public class Position
    {
        public int Ligne { get; set; }
        public int Colonne { get; set; }

        public Position(int pligne, int pcolonne)
        {
            Ligne = pligne;
            Colonne = pcolonne;
        }
    }
}
