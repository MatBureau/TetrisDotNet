namespace TetrisDotNet
{
    public class Grille
    {
        private readonly int[,] _Grille;
        public int _Lignes { get; }
        public int _Colonnes { get; }

        public int this[int l, int c]
        {
            get => _Grille[l, c];
            set => _Grille[l, c] = value;
        }
        
        public Grille(int pLignes, int pColonnes)
        {
            _Lignes = pLignes;
            _Colonnes = pColonnes;
            _Grille = new int[pLignes, pColonnes];
        }
    
        public bool EstDansLaGrille(int l, int c)
        {
            return l >= 0 && l < _Lignes && c >= 0 && c < _Colonnes;
        }

        public bool EstCelluleVide(int l, int c)
        {
            return EstDansLaGrille(l, c) && _Grille[l,c] == 0;
        }

        public bool EstLigneComplete(int l)
        {
            for (int c=0; c<_Colonnes; c++)
            {
                if(_Grille[l,c] == 0)
                {
                    return false;
                }

            }
            return true;
        }

        public bool EstLigneVide(int l)
        {
            for (int c = 0; c < _Colonnes; c++)
            {
                if (_Grille[l, c] != 0)
                {
                    return false;
                }

            }
            return true;
        }

        private void EffacerLigneComplete(int l)
        {
            for (int c = 0; c < _Colonnes; c++)
            {
                _Grille[l, c] = 0;
            }
        }

        private void DeplacerLigneBas(int l, int NombreDeplacement)
        {
            for (int c=0; c < _Colonnes; c++)
            {
                _Grille[l + NombreDeplacement, c] = _Grille[l, c];
                _Grille[l, c] = 0;
            }
        }

        public int SupprimerTouteLignesComplete()
        {
            int lignesEffacee = 0;

            for (int l = _Lignes-1; l >= 0; l--)
            {
                if (EstLigneComplete(l))
                {
                    EffacerLigneComplete(l);
                    lignesEffacee++;
                }
                else if (lignesEffacee>0)
                {
                    DeplacerLigneBas(l, lignesEffacee);
                }
            }

            return lignesEffacee;
        }
    }
}
