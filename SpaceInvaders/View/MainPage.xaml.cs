using System;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using SpaceInvaders.Model.Manager_Classes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpaceInvaders.View
{
    /// <summary>
    ///     The main page for the game.
    /// </summary>
    public sealed partial class MainPage
    {
        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const double ApplicationHeight = 480;

        /// <summary>
        ///     The application width
        /// </summary>
        public const double ApplicationWidth = 640;

        private readonly GameManager gameManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size { Width = ApplicationWidth, Height = ApplicationHeight };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;

            this.gameManager = new GameManager(this.theCanvas);
            this.toggleEndOfGameTextBoxes();

            this.gameManager.ScoreUpdated += new GameManager.UpdateScoreHandler(this.updateScore);
            this.gameManager.GameEnded += new GameManager.GameEndedHandler(this.endGame);
        }

        #endregion

        #region Methods

        private void toggleEndOfGameTextBoxes()
        {
            if (this.GameOverTextBox.Visibility == Visibility.Collapsed &&
                this.GameStatTextBlock.Visibility == Visibility.Collapsed)
            {
                this.GameOverTextBox.Visibility = Visibility.Visible;
                this.GameStatTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                this.GameOverTextBox.Visibility = Visibility.Collapsed;
                this.GameStatTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void updateScore(int score)
        {
            this.ScoreLabel.Text = $"Score: {score}";
        }

        private void endGame()
        {
            this.toggleShipsVisibility();
            this.toggleEndOfGameTextBoxes();
            this.ScoreLabel.Visibility = Visibility.Collapsed;
            this.GameStatTextBlock.Text = this.gameManager.DidPlayerLose
                ? $"You Lost, better luck next time!{Environment.NewLine}Your {this.ScoreLabel.Text}"
                : $"Congratulations, you won!{Environment.NewLine}Your {this.ScoreLabel.Text}";
        }

        private void toggleShipsVisibility()
        {
            foreach (var sprite in this.theCanvas.Children)
            {
                sprite.Visibility = Visibility.Collapsed;
            }
        }

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayerShipLeft();
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayerShipRight();
                    break;
                case VirtualKey.A:
                    this.gameManager.MovePlayerShipLeft();
                    break;
                case VirtualKey.D:
                    this.gameManager.MovePlayerShipRight();
                    break;
                case VirtualKey.Space:
                    this.gameManager.FirePlayerShipsGun();
                    break;
            }
        }

        #endregion
    }
}