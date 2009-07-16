using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{
    /// <summary>
    /// The class that will implement the methods of the mFieldPhysics physics.
    /// </summary>
    class FieldPhysicsComponent : IFieldPhysics 
    {

        protected  Vector3 old_position = Vector3.Zero;

        protected Vector3 K1pos = Vector3.Zero;
        protected Vector3 K2pos = Vector3.Zero;
        protected Vector3 K3pos = Vector3.Zero;
        protected Vector3 k4pos = Vector3.Zero;

        protected Vector3 K1vel = Vector3.Zero;
        protected Vector3 K2vel = Vector3.Zero;
        protected Vector3 K3vel = Vector3.Zero;
        protected Vector3 k4vel = Vector3.Zero;


        #region IFieldPhysics Members


        

        public Vector3 NewPosition(Vector3 inputargs, GameTime here)
        {
            
            throw new NotImplementedException();
        }

        #endregion
    }
}
