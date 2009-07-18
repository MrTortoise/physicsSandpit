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
    struct New_pos_and_vel{
        public Vector3 position;
        public Vector3 velocity;

    }
    
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


        

        public New_pos_and_vel dothe_phys(float step,IEmitPointField i)
        {
            float hstep = step / 2;
            Vector3 force = Vector3.Zero;

            GameObjectDictionary instance = GameObjectDictionary.GetInstance();

 

                Vector3 K1pos = i.Velocity;
                foreach (IEmitPointField j in instance.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.Force(i.Position, 1234); }
                }
                Vector3 K1vel = force;

                Vector3 K2pos = K1vel * (float)hstep + i.Velocity;

                force = Vector3.Zero;

                foreach (IEmitPointField j in instance.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.Force(i.Position + K1pos*hstep, 1234); }
                }

                Vector3 K2vel = force;

                Vector3 K3pos = K2vel * (float)hstep + i.Velocity;

                force = Vector3.Zero;

                foreach (IEmitPointField j in instance.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.Force(i.Position + K2pos * hstep, 1234); }
                }
                Vector3 K3vel = force;

                Vector3 k4pos = K3vel * (float)step + i.Velocity;
                force = Vector3.Zero;
                foreach (IEmitPointField j in instance.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.Force(i.Position + K3pos, 1234); }
                }
                Vector3 k4vel = force;


             New_pos_and_vel meow;
            meow.position = i.Position + (float)(step / 6) * (K1pos + K2pos / 2 + K3pos / 2 + k4pos);
            meow.velocity = i.Velocity + (float)(step / 6) * (K1vel + K2vel / 2 + K3vel / 2 + k4vel);
            return (meow);
        }

        #endregion
    }
}
