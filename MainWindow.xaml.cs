using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TetrisDotNet.BlocksProperties;
using TetrisDotNet.Controller;
using NAudio;
using NAudio.Wave;
using System.Reflection;
using System.Threading;

namespace TetrisDotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] ImagesDesTuiles = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
        };

        private readonly ImageSource[] BlockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
        };

        private readonly Image[,] controlleurImage;
        private GameStateController gameState = new GameStateController();
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 25;

        public bool gPause = false;

        ManualResetEvent man = new ManualResetEvent(false);

        public MainWindow()
        {
            InitializeComponent();
            controlleurImage = SetupGameCanvas(gameState.Grille);
        }

        private async void WebSocketConn()
        {
            
        }

        private Image[,] SetupGameCanvas(Grille grille)
        {
            Image[,] l_controlleurImages = new Image[grille._Lignes, grille._Colonnes];
            int tailleCellule = 25;

            for (int l=0; l < grille._Lignes; l++)
            {
                for(int c = 0; c < grille._Colonnes; c++)
                {
                    Image l_controlleurImage = new Image
                    {
                        Width = tailleCellule,
                        Height = tailleCellule
                    };
                    Canvas.SetTop(l_controlleurImage, (l - 2) * tailleCellule+10);
                    Canvas.SetLeft(l_controlleurImage, c * tailleCellule);
                    GameCanvas.Children.Add(l_controlleurImage);
                    l_controlleurImages[l,c] = l_controlleurImage;
                }
            }
            return l_controlleurImages;
        }

        private void DessinerGrille(Grille grille)
        {
            for (int l = 0; l < grille._Lignes; l++)
            {
                for (int c = 0; c < grille._Colonnes; c++)
                {
                    int id = grille[l, c];
                    controlleurImage[l, c].Opacity = 1;
                    controlleurImage[l,c].Source = ImagesDesTuiles[id];
                }
            }
        }

        private Task WaitForEscape()
        {

            return Task.Factory.StartNew(() =>
            {
                man.WaitOne();
                man.Reset();
            }
             );
        }

        private void DessinerBlockActuel(Block block)
        {
            foreach(Position p in block.PositionTuile())
            {
                controlleurImage[p.Ligne, p.Colonne].Opacity = 1;
                controlleurImage[p.Ligne, p.Colonne].Source = ImagesDesTuiles[block.Id];
            }
        }

        private void Draw(GameStateController gameState)
        {
            DessinerGrille(gameState.Grille);
            DessinerGhostBlock(gameState.BlockActuel);
            DessinerBlockActuel(gameState.BlockActuel);
            DessinerNextBlockPreview(gameState.FileAttenteBlock);
            DessinerBlockTenu(gameState.BlockTenu);
            TXB_Score.Text = $"Score : {gameState.Score}";
        }

        private async Task BoucleDeJeu()
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                if (!gPause)
                {
                    int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease));
                    if (delay < 350)
                    {
                        delay = 350;
                    }
                    await Task.Delay(delay);
                    gameState.DeplacerBlockBas();
                    Draw(gameState);
                }
                else
                {
                    await WaitForEscape();
                    gPause = false;
                }
            }

            GameOverPanel.Visibility = Visibility.Visible;
            TBX_ScoreFinal.Text = $"Score final : {gameState.Score}";
        }

        private void DessinerNextBlockPreview(FileAttenteBlock fileAttente)
        {
            Block suivant = fileAttente.BlockSuivant;
            NextImage.Source = BlockImages[suivant.Id];
        }

        private void DessinerBlockTenu(Block blockTenu)
        {
            if(blockTenu== null)
            {
                HoldImage.Source = BlockImages[0];
            }
            else
            {
                HoldImage.Source = BlockImages[blockTenu.Id];
            }
        }

        private void DessinerGhostBlock(Block block)
        {
            int distanceADrop = gameState.BlockDropDistance();

            foreach (Position p in block.PositionTuile())
            {
                controlleurImage[p.Ligne + distanceADrop, p.Colonne].Opacity = 0.25;
                controlleurImage[p.Ligne + distanceADrop, p.Colonne].Source = ImagesDesTuiles[block.Id];
            }
        }

        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            gPause = false;
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.DeplacerBlockGauche();
                    break;
                case Key.Right:
                    gameState.DeplacerBlockDroite();
                    break;
                case Key.Down:
                    gameState.DeplacerBlockBas();
                    break;
                case Key.Up:
                    gameState.RotationBlockGauche();
                    break;
                case Key.NumPad0:
                    gameState.RotationBlockDroite();
                    break;
                case Key.Space:
                    gameState.TenirBlock();
                    break;
                case Key.D:
                    gameState.HardDropBlock();
                    break;
                case Key.Escape:
                    if (!gPause)
                    {
                        gPause = true;
                        GameOverPanel.Visibility = Visibility.Visible;
                        TBX_GameOver.Visibility = Visibility.Hidden;
                        TBX_ScoreFinal.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        gPause = false;
                        GameOverPanel.Visibility = Visibility.Hidden;
                        TBX_GameOver.Visibility = Visibility.Visible;
                        TBX_ScoreFinal.Visibility = Visibility.Visible;
                        await BoucleDeJeu();
                    }
                    break;
                default:
                    return;
            }

            Draw(gameState);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await BoucleDeJeu();
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameStateController();
            GameOverPanel.Visibility=Visibility.Hidden;
            gPause=false;
            await BoucleDeJeu();
        }

        private async void BTN_Continue_Click(object sender, RoutedEventArgs e)
        {
            GameOverPanel.Visibility = Visibility.Hidden;
            TBX_GameOver.Visibility = Visibility.Visible;
            TBX_ScoreFinal.Visibility = Visibility.Visible;
            gPause =false;
            await BoucleDeJeu();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
