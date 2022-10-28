using System.Collections.Generic;

namespace TetrisDotNet
{
    public abstract class Block
    {
        protected abstract Position[][] Tuiles { get; }
        protected abstract Position DecalageDeDepart { get; }
        public abstract int Id { get; }

        private int etatDeRotation;
        private Position decalage;


        public Block()
        {
            decalage = new Position(DecalageDeDepart.Ligne, DecalageDeDepart.Colonne);
        }

        public IEnumerable<Position> PositionTuile()
        {
            foreach(Position p in Tuiles[etatDeRotation])
            {
                yield return new Position(p.Ligne + decalage.Ligne, p.Colonne + decalage.Colonne);
            }
        }

        public void RotationDroite()
        {
            etatDeRotation = (etatDeRotation+1)%Tuiles.Length;
        }

        public void RotationGauche()
        {
            if (etatDeRotation == 0)
            {
                etatDeRotation= Tuiles.Length-1;
            }
            else
            {
                etatDeRotation--;
            }
        }

        public void Mouvement(int lignes, int colonnes)
        {
            decalage.Ligne += lignes;
            decalage.Colonne += colonnes;
        }

        public void RestaurerPosition()
        {
            etatDeRotation = 0;
            decalage.Ligne = DecalageDeDepart.Ligne;
            decalage.Colonne = DecalageDeDepart.Colonne;
        }
    }
}
