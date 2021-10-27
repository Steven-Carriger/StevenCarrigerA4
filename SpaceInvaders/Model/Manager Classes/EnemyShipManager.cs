using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.Model.EnemyShips;
using SpaceInvaders.Model.Enum_Classes;

namespace SpaceInvaders.Model.Manager_Classes
{
    /// <summary> Manages all of the enemy ships in the game, including their placement & movement</summary>
    public class EnemyShipManager
    {
        #region Data members
        private const int MaxNumberOfEnemyShips = 20;

        private const int limitScaler = 2;

        private const int ChanceToFireTheGun = 100;
        private const int RangeOfProbability = 0;

        private const int SpaceBetweenShips = 5;

        private const int MaxSteps = 40;
        private const int MxStepsInOneDirection = MaxSteps / 2;
        private int stepsTaken;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the score value of all destroyed ships.
        /// </summary>
        /// <value>
        ///     The score value of all destroyed ships.
        /// </value>
        public int ScoreValueOfDestroyedShips { get; set; }

        private Canvas BackgroundCanvas { get; }

        private double halfWidthOfCanvas => this.BackgroundCanvas.Width / 2;

        /// <summary> Gets a value indicating whether [are all enemy ships destroyed]. </summary>
        /// <value> <c>true</c> if [all ships are destroyed]; otherwise, <c>false</c>. </value>
        public bool AreAllShipsDestroyed => this.EnemyShips.Count == 0;

        /// <summary>
        ///     Gets the enemy ships.
        /// </summary>
        /// <value>
        ///     The enemy ships.
        /// </value>
        public Collection<EnemyShip> EnemyShips { get; }

        private int firstRowLimit { get; set; }

        private int secondRowLimit { get; set; }

        private int thirdRowLimit { get; set; }

        private int fourthRowLimit { get; set; }

        private int currentRowLimit { get; set; }

        /// <summary>
        /// a function that handles when the score is updated.
        /// </summary>
        /// <param name="score">The score.</param>
        public delegate void ScoreUpdatedHandler(int score);

        /// <summary>
        /// Occurs when [score updated].
        /// </summary>
        public event ScoreUpdatedHandler ScoreUpdated;
        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyShipManager" /> class.
        ///     precondition: none
        ///     post condition: enemy ships are added and placed on the Canvas.
        /// </summary>
        public EnemyShipManager(Canvas background)
        {
            this.EnemyShips = new Collection<EnemyShip>();
            this.ScoreValueOfDestroyedShips = 0;
            this.BackgroundCanvas = background;
            this.addLimits();
            this.addEnemies();
            this.placeEnemyShips();
        }

        #endregion

        #region Methods

        private void addLimits()
        {
            this.firstRowLimit = (int)ShipLevel.LevelOne;
            this.secondRowLimit = (int)ShipLevel.LevelTwo;
            this.thirdRowLimit = (int)ShipLevel.LevelThree;
            this.fourthRowLimit = (int)ShipLevel.LevelFour;

            this.currentRowLimit = limitScaler;
        }

        /// <summary>
        ///     Moves the ships left.
        ///     precondition: none
        ///     post condition: all enemy ships have shifted left
        /// </summary>
        public void MoveShipsLeft()
        {
            foreach (var ship in this.EnemyShips)
            {
                ship.MoveLeft();
            }
        }

        /// <summary>
        ///     Moves all the enemy ships to the right.
        ///     precondition: none
        ///     post condition: all enemy ships have shifted right
        /// </summary>
        public void MoveShipsRight()
        {
            foreach (var ship in this.EnemyShips)
            {
                ship.MoveRight();
            }
        }

        /// <summary>
        ///     Moves the ships down.
        /// </summary>
        public void MoveShipsDown()
        {
            foreach (var ship in this.EnemyShips)
            {
                ship.MoveDown();
            }
        }

        /// <summary>
        ///     Makes all of the enemy ships take a step based on how far the ships have traveled
        ///     precondition: none
        ///     post condition: enemyShips have taken a step.
        /// </summary>
        public void TakeAStep()
        {
            if (this.areShipsDoneMoving())
            {
                this.MoveShipsDown();
                this.stepsTaken = 0;
            }
            else if (this.areShipsDoneMovingToTheRight())
            {
                this.MoveShipsLeft();
                this.stepsTaken++;
            }
            else
            {
                this.MoveShipsRight();
                this.stepsTaken++;
            }

            this.removeDestroyedShips();
        }

        private bool areShipsDoneMoving()
        {
            return this.stepsTaken == MaxSteps;
        }

        private bool areShipsDoneMovingToTheRight()
        {
            return this.stepsTaken >= MxStepsInOneDirection;
        }

        private void addEnemies()
        {
            var shipsAdded = 0;
            var shipsAddedOnRow = 0;
            var currentRow = Row.FirstRow;

            while (shipsAdded < MaxNumberOfEnemyShips)
            {
                this.addEnemy(currentRow);
                shipsAdded++;
                shipsAddedOnRow++;
                if (!this.hasAddedMaximumShipsForCurrentRow(shipsAddedOnRow))
                { 
                    currentRow = this.changeRow(currentRow);
                    shipsAddedOnRow = 0;
                    this.currentRowLimit += limitScaler;
                }
            }
        }

        private Row changeRow(Row currentRow)
        {
            switch (currentRow)
            {
                default:
                    return Row.SecondRow;
                case Row.SecondRow:
                    return Row.ThirdRow;
                case Row.ThirdRow:
                    return Row.FourthRow;
            }
        }

        /// <summary>
        ///     Fires the enemy ships.
        /// </summary>
        /// <returns>A bullet if one of the enemy ships fire a bullet, null otherwise</returns>
        public Bullet fireEnemyShips()
        {
            var randomNumberGenerator = new Random();
            foreach (var ship in this.EnemyShips)
            {
                var chanceNumber = randomNumberGenerator.Next(0, ChanceToFireTheGun);
                if (chanceNumber <= RangeOfProbability)
                {
                    return ship.Fire();
                }
            }

            return null;
        }

        private bool hasAddedMaximumShipsForCurrentRow(int totalAdded)
        {
            return totalAdded < this.currentRowLimit;
        }

        private void addEnemy(Row enemyRow)
        {
            switch (enemyRow)
            {
                case Row.FirstRow:
                    this.EnemyShips.Add(EnemyShipFactory.MakeEnemyShip(ShipLevel.LevelOne));
                    break;
                case Row.SecondRow:
                    this.EnemyShips.Add(EnemyShipFactory.MakeEnemyShip(ShipLevel.LevelTwo));
                    break;
                case Row.ThirdRow:
                    this.EnemyShips.Add(EnemyShipFactory.MakeEnemyShip(ShipLevel.LevelThree));
                    break;
                case Row.FourthRow:
                    this.EnemyShips.Add(EnemyShipFactory.MakeEnemyShip(ShipLevel.LevelFour));
                    break;
                default:
                    break;
            }
        }

        private void placeEnemyShips()
        {
            foreach (var ship in this.EnemyShips)
            {
                var startingXCord = this.BackgroundCanvas.Width / (int)ship.ShipLevel;
                switch (ship.ShipRow)
                {
                    case Row.FirstRow:
                        this.firstRowLimit--;
                        ship.X = this.findNextFirstRowX(ship, startingXCord);
                        break;
                    case Row.SecondRow:
                        this.secondRowLimit--;
                        ship.X = this.findNextSecondRowX(ship, startingXCord);
                        break;
                    case Row.ThirdRow:
                        this.thirdRowLimit--;
                        ship.X = this.findNextThirdRowX(ship, startingXCord);
                        break;
                    case Row.FourthRow:
                        this.fourthRowLimit--;
                        ship.X = this.findNextFourthRowX(ship, startingXCord);
                        break;
                    default:
                        break;
                }
                this.BackgroundCanvas.Children.Add(ship.Sprite);
                ship.Y = (int)ship.ShipRow;
            }
        }

        private void removeDestroyedShips()
        {
            for (var shipIndex = 0; shipIndex < this.EnemyShips.Count; shipIndex++)
            {
                if (this.EnemyShips[shipIndex].IsDestroyed)
                {
                    this.ScoreValueOfDestroyedShips += (int)this.EnemyShips[shipIndex].ScoreValue;
                    this.EnemyShips.Remove(this.EnemyShips[shipIndex]);
                    this.onScoreUpdated();
                }
            }
        }

        private void onScoreUpdated()
        {
            if (this.ScoreUpdated != null)
            {
                this.ScoreUpdated(this.ScoreValueOfDestroyedShips);
            }
        }

        private double findNextFourthRowX(EnemyShip ship, double startingCord)
        {
            startingCord -= ship.Width / limitScaler;
            return startingCord + (ship.Width + SpaceBetweenShips) * this.fourthRowLimit;
        }

        private double findNextThirdRowX(EnemyShip ship, double startingCord)
        {
            return startingCord + (ship.Width + SpaceBetweenShips) * this.thirdRowLimit;
        }

        private double findNextSecondRowX(EnemyShip ship, double startingCord)
        {
            return startingCord + (ship.Width + SpaceBetweenShips) * this.secondRowLimit;
        }

        private double findNextFirstRowX(EnemyShip ship, double startingCord)
        {
            startingCord -= ship.Width * limitScaler;
            return startingCord + (ship.Width + SpaceBetweenShips) * this.firstRowLimit;
        }
        #endregion
    }
}