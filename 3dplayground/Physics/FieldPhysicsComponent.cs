using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground.Physics
{
    /// <summary>
    /// The class that will implement the methods of the mFieldPhysics physics.
    /// </summary>      
    class FieldPhysicsComponent : IFieldPhysics 
    {  
        #region IFieldPhysics Members        

        public New_pos_and_vel dothe_phys(float step,IGetEffectedByGravity  i)
        {
            float hstep = step / 20f;
            step = step / 10;
            Vector3 force = Vector3.Zero;            

            GameObjectDictionary instance = GameObjectDictionary.GetInstance(); 

                Vector3 K1pos = i.Velocity;
                foreach (IEmitPointField j in instance.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.Acceleration(i.Position, 1234); }
                }
                Vector3 K1vel = force;

                Vector3 K2pos = K1vel * (float)hstep + i.Velocity;

                force = Vector3.Zero;

                foreach (IEmitPointField j in instance.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.Acceleration(i.Position + K1pos * hstep, 1234); }
                }

                Vector3 K2vel = force;

                Vector3 K3pos = K2vel * (float)hstep + i.Velocity;

                force = Vector3.Zero;

                foreach (IEmitPointField j in instance.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.Acceleration(i.Position + K2pos * hstep, 1234); }
                }
                Vector3 K3vel = force;

                Vector3 k4pos = K3vel * (float)step + i.Velocity;
                force = Vector3.Zero;
                foreach (IEmitPointField j in instance.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.Acceleration(i.Position + K3pos, 1234); }
                }
                Vector3 k4vel = force;


             New_pos_and_vel meow;
            meow.position = (step / 6f) * (K1pos + K2pos * 2f + K3pos * 2f + k4pos);
            meow.velocity =  (step / 6f) * (K1vel + K2vel * 2f + K3vel * 2f + k4vel);
            return (meow);
        }

        #endregion
    }
}
