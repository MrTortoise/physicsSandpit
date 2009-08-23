using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using _3dplayground;
using _3dplayground.Maths;

namespace MathsPhysicsTests.mocks
{
    class IAmInSpaceMock : IAmInSpace 
    {

        #region IAmInSpace Members

        public DVector3 Position
        {
            get { return DVector3.UnitX; }
        }

        public Quaternion Rotation
        {
            get { return Quaternion.Identity; }
        }

        public GameSpaceUnit Space
        {
            get { return new GameSpaceUnit(); }
        }

        #endregion

        #region IHasName Members

        public string Name
        {
            get { return "Mock IAmInSpace"; }
        }

        public int ID
        { get { return -1; } }

        #endregion
    }
}
