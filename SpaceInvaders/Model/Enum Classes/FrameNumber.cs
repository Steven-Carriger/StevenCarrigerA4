using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model.Enum_Classes
{
    /// <summary>
    /// Used to determine what the current frame is for animating the ships.
    /// </summary>
    public enum FrameNumber
    {
        /// <summary>
        /// The first frame. Should be the default appearance if no other variation.
        /// </summary>
        FrameOne,

        /// <summary>
        /// The second frame. the other frame to switch to and from.
        /// </summary>
        FrameTwo
    }
}
