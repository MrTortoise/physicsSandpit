using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using _3dplayground;
using _3dplayground.Maths;
using _3dplayground.Physics ;

namespace MathsPhysicsTests.mocks
{
    class ICanMoveMock : ICanMove 
        
    {

        protected IAmInSpace mim = new IAmInSpaceMock();

        #region ICanMove Members

        public DVector3 Velocity
        {
            get { return DVector3.UnitY; }
        }

        public Quaternion AngularVelocity
        {
            get { return Quaternion.Identity; }
        }

        public event EventHandler<DisplacementArgs> RequestMove;

        public DisplacementStructure GetDisplacementStructure
        {
            get { return new DisplacementStructure(); }
        }

        public void RaiseRequestMove(DisplacementArgs theArgs)
        {
            RequestMove(this, null); 
        }

        public void ExecuteDisplacementStructure(DisplacementStructure theStructure)
        {
            
        }

        public void ResetDisplacementStructures()
        {
            
        }

        public void Update(TimeSpan UpdateTime)
        {
            
        }

        #endregion

        #region IAmInSpace Members

        public DVector3 Position
        {
            get { return mim.Position;}
        }

        public Quaternion Rotation
        {
            get { return mim.Rotation;}
        }

        public GameSpaceUnit Space
        {
            get { return mim.Space; }
        }

        #endregion

        #region IHasName Members

        public string Name
        {
            get { return mim.Name; }
        }

        #endregion
    }
}
