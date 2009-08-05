using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using _3dplayground;
using _3dplayground.Maths;
using _3dplayground.Physics;

namespace MathsPhysicsTests.mocks
{
    class IPhysicsObjectMock : IPhysicsObject          
    {

        IAmInSpace mIam = new IAmInSpaceMock();
        ICanMove mICan = new ICanMoveMock();

        #region IAmInSpace Members

        public DVector3 Position
        {
            get { return mIam.Position; }
        }

        public Quaternion Rotation
        {
            get { return mIam.Rotation; }
        }

        public GameSpaceUnit Space
        {
            get { return mIam.Space; }
        }

        #endregion

        #region IHasName Members

        public string Name
        {
            get { return mIam.Name; }
        }

        #endregion

        #region IDrawable Members

        public void Draw(Camera theCamera, Vector3 thePosition, Quaternion theRotation)
        {
            
        }

        public bool IsDrawActive
        {
            get { return true ;}
        }

        #endregion

        #region IHasMass Members

        public int Mass
        {
            get { return 1;}
        }

        #endregion

        #region ICanMove Members

        public DVector3 Velocity
        {
            get { return mICan.Velocity; }
        }

        public Quaternion AngularVelocity
        {
            get { return mICan.AngularVelocity; }
        }

        public event EventHandler<DisplacementArgs> RequestMove;

        public DisplacementStructure GetDisplacementStructure
        {
            get { return mICan.GetDisplacementStructure; }
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
    }
}
