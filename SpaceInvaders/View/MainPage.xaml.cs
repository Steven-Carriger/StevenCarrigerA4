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
            this.createEventListeners();
        }

        #endregion

        #region Methods

        private void createEventListeners()
        {
            this.gameManager.ScoreUpdated += this.updateScore;
            this.gameManager.GameEnded += this.endGame;
            this.gameManager.PlayerHit += this.updatePlayerLives;
        }

        private void toggleEndOfGameTextBoxes()
        {
            if (this.gameOverTextBox.Visibility == Visibility.Collapsed &&
                this.gameStatTextBlock.Visibility == Visibility.Collapsed)
            {
                this.gameOverTextBox.Visibility = Visibility.Visible;
                this.gameStatTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                this.gameOverTextBox.Visibility = Visibility.Collapsed;
                this.gameStatTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void updateScore(int score)
        {
            this.scoreLabel.Text = $"Your Score: {score}";
        }

        private void updatePlayerLives(int lives)
        {
            this.playerLivesLabel.Text = $"Your Lives: {lives}";
        }

        private void endGame()
        {
            this.toggleGameObjectsVisibility();
            this.toggleEndOfGameTextBoxes();
            this.toggleLabels();
            this.gameStatTextBlock.Text = this.gameManager.DidPlayerLose
                ? $"You Lost, better luck next time!{Environment.NewLine}{this.scoreLabel.Text}"
                : $"Congratulations, you won!{Environment.NewLine}{this.scoreLabel.Text}";
        }

        private void toggleLabels()
        {
            this.scoreLabel.Visibility = Visibility.Collapsed;
            this.playerLivesLabel.Visibility = Visibility.Collapsed;
        }

        private void toggleGameObjectsVisibility()
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