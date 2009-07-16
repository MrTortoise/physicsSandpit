using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{

    class Moon : IGameObject, IGetEffectedByField  
    {
        private string mName;

        protected  IFieldPhysics mFieldPhysics;
        protected  Vector3 mPosition;
        protected Vector3 mVelocity;
        protected Vector3 mRotation;
        

        public Moon()
        {
            mFieldPhysics = new FieldPhysicsComponent();

        }

        #region IGetEffectedByField Members
        
        /// <summary>
        /// DO NOT USE IN CODE - constructors should set the mFieldPhysics
        /// </summary>
        public IFieldPhysics FieldPhysicsComponent
        {
            get
            {
                return mFieldPhysics;
            }
            set
            {
                if (value != null)
                {
                    mFieldPhysics = value;
                }
            }
        }

        #endregion

        #region IAmInSpace Members

        public Vector3 Position
        {
            get { return mPosition; }
        }

        public Vector3 Velocity
        {
            get { return mVelocity; }
        }

        public Microsoft.Xna.Framework.Vector3 Rotation
        {
            get { return mRotation; }
        }

        public string Name
        {
            get { return mName; }
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
