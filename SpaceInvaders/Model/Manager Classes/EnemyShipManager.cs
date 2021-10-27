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

        private const int LimitPerRow = 4;
        private const int NumberOfRows = 3;
        private const int HalfOfRowLimit = LimitPerRow / 2;

        private const int ChanceToFireTheGun = 100;
        private const int RangeOfProbability = 0;

        private const int EnemyShipWidth = 50;
        private const int SpaceBetweenShips = 10;

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

        private int MaxNumberOfEnemyShips => LimitPerRow * NumberOfRows;

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
            this.addEnemies();
            this.placeEnemyShips();
        }

        #endregion

        #region Methods

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
            var currentRow = Row.BottomRow;

            while (shipsAdded < this.MaxNumberOfEnemyShips)
            {
                this.addEnemy(currentRow);
                shipsAdded++;

                if (this.hasAddedMaximumShipsForCurrentRow(shipsAdded))
                {
                    switch (currentRow)
                    {
                        case Row.BottomRow:
                            currentRow = Row.MiddleRow;
                            break;
                        case Row.MiddleRow:
                            currentRow = Row.TopRow;
                            break;
                    }
                }
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
            return totalAdded % LimitPerRow == 0;
        }

        private void addEnemy(Row enemyRow)
        {
            if (enemyRow == Row.BottomRow)
            {
                this.EnemyShips.Add(new EnemyShipLevel1());
            }
            else if (enemyRow == Row.MiddleRow)
            {
                this.EnemyShips.Add(new EnemyShipLevel2());
            }
            else if (enemyRow == Row.TopRow)
            {
                this.EnemyShips.Add(new EnemyShipLevel3());
            }
        }

        private void placeEnemyShips()
        {
            var startingXCord = this.BackgroundCanvas.Width / HalfOfRowLimit - EnemyShipWidth * HalfOfRowLimit;
            var shipsPlaced = 0;

            foreach (var ship in this.EnemyShips)
            {
                this.BackgroundCanvas.Children.Add(ship.Sprite);

                ship.X = startingXCord + (ship.Width + SpaceBetweenShips) * shipsPlaced;
                ship.Y = (int)ship.ShipRow;

                shipsPlaced++;

                if (shipsPlaced == LimitPerRow)
                {
                    shipsPlaced = 0;
                }
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

        #endregion
    }
}