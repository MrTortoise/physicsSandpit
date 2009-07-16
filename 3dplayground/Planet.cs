using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground
{
    class Planet : IGameObject, IEmitPointField 
    {    



        public Planet()
        {
            mFieldPhys = new FieldPhysicsComponent();

        }

        #region IEmitPointField Members

        public int ScalarValue
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Microsoft.Xna.Framework.Vector3 Force(Microsoft.Xna.Framework.Vector3 thePosition, int magnitude)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAmInSpace Members

        public Microsoft.Xna.Framework.Vector3 Position
        {
            get { throw new NotImplementedException(); }
        }

        public Microsoft.Xna.Framework.Vector3 Velocity
        {
            get { throw new NotImplementedException(); }
        }

        public Microsoft.Xna.Framework.Vector3 Rotation
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDrawable Members

        public void Draw(Camera theCamera)
        {
            throw new NotImplementedException();
        }

        public bool IsDrawActive
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IUpdateable Members

        public void Update(Microsoft.Xna.Framework.GameTime timeInterval)
        {
            throw new NotImplementedException();
        }

        public bool IsUpdateActive
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
