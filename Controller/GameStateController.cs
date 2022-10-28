using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisDotNet.BlocksProperties;

namespace TetrisDotNet.Controller
{
    public class GameStateController
    {
        private Block blockActuel;

        public Block BlockActuel
        {
            get => blockActuel;
            private set
            {
                blockActuel = value;
                blockActuel.RestaurerPosition();

                for(int i = 0; i < 2; i++)
                {
                    blockActuel.Mouvement(1, 0);
                    if (!PositionValide())
                    {
                        blockActuel.Mouvement(-1, 0);
                    }
                }
            }
        }

        public Grille Grille { get; }
        public FileAttenteBlock FileAttenteBlock { get; }
        public bool GameOver { get; private set; }
        public int Score { get; set; }
        public Block BlockTenu { get; private set; }
        public bool PeutTenir { get; private set; }

        public GameStateController()
        {
            Grille = new Grille(22, 10);
            FileAttenteBlock = new FileAttenteBlock();
            BlockActuel = FileAttenteBlock.GetEtUpdate();
            PeutTenir = true;
        }

        private bool PositionValide()
        {
            foreach(Position p in BlockActuel.PositionTuile())
            {
                if (!Grille.EstCelluleVide(p.Ligne, p.Colonne))
                {
                    return false;
                }
            }
            return true;
        }

        public void TenirBlock()
        {
            if (!PeutTenir)
            {
                return;
            }

            if(BlockTenu == null)
            {
                BlockTenu = blockActuel;
                blockActuel = FileAttenteBlock.GetEtUpdate();
            }
            else
            {
                Block temporaire = blockActuel;
                BlockActuel = BlockTenu;
                BlockTenu = temporaire;
            }

            PeutTenir = false;
        }

        public void RotationBlockDroite()
        {
            BlockActuel.RotationDroite();

            if (!PositionValide())
            {
                BlockActuel.RotationGauche();
            }
        }

        public void RotationBlockGauche()
        {
            BlockActuel.RotationGauche();

            if (!PositionValide())
            {
                blockActuel.RotationDroite();
            }
        }

        public void DeplacerBlockGauche()
        {
            BlockActuel.Mouvement(0, -1);

            if (!PositionValide())
            {
                blockActuel.Mouvement(0,1);
            }
        }

        public void DeplacerBlockDroite()
        {
            BlockActuel.Mouvement(0, 1);

            if (!PositionValide())
            {
                blockActuel.Mouvement(0, -1);
            }
        }

        private bool EstGameOver()
        {
            return !(Grille.EstLigneVide(0) && Grille.EstLigneVide(1));
        }

        private void PlaceBlock()
        {
            foreach(Position p in blockActuel.PositionTuile())
            {
                Grille[p.Ligne, p.Colonne] = blockActuel.Id;
            }
            
            int ScoreTemp = Grille.SupprimerTouteLignesComplete();
            Score += ScoreTemp * ScoreTemp;
            ScoreTemp = 0;
            //if (ScoreTemp > 1)
            //{
            //    Score += ScoreTemp * ScoreTemp;
            //}
            //else
            //{
            //    Score += ScoreTemp;
            //}
            

            if (EstGameOver())
            {
                GameOver = true;
            }
            else
            {
                BlockActuel = FileAttenteBlock.GetEtUpdate();
                PeutTenir = true;
            }
        }

        public void DeplacerBlockBas()
        {
            BlockActuel.Mouvement(1, 0);

            if (!PositionValide())
            {
                BlockActuel.Mouvement(-1, 0);
                PlaceBlock();
            }
        }

        private int DistanceVersLeBas(Position p)
        {
            int drop = 0;

            while(Grille.EstCelluleVide(p.Ligne+drop+1, p.Colonne))
            {
                drop++;
            }
            return drop;
        }

        public int BlockDropDistance()
        {
            int drop = Grille._Lignes;

            foreach(Position p in blockActuel.PositionTuile())
            {
                drop = System.Math.Min(drop, DistanceVersLeBas(p));
            }

            return drop;
        }

        public void HardDropBlock()
        {
            blockActuel.Mouvement(BlockDropDistance(), 0);
            PlaceBlock();
        }
    }
}
