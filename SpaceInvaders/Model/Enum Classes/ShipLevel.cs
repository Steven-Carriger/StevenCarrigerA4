namespace SpaceInvaders.Model.Enum_Classes
{
    /// <summary> the levels and limit for each ship, the higher the level the more challenging to fight. </summary>
    public enum ShipLevel
    {
        /// <summary> level one, the most basic level for the enemy ship the value is the amount of times it can be placed</summary>
        LevelOne = 2,

        /// <summary> level two, an average level for the enemy ship </summary>
        LevelTwo = 4,

        /// <summary> level three, a challenging level for the enemy ship </summary>
        LevelThree = 6,

        /// <summary> level four, one of the most difficult enemy level ships to go against</summary>
        LevelFour = 8
    }
}