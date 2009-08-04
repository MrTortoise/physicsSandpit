using System;

using Microsoft.Xna.Framework;
using _3dplayground.Maths;

namespace _3dplayground.Physics
{
    /// <summary>
    /// The class that will implement the methods of the mFieldPhysics physics.
    /// </summary>      
    class FieldPhysicsComponent : IFieldPhysics 
    {  
        #region IFieldPhysics Members        

        /// <summary>
        /// please fill me in as john is a dumbass and forgot. :D
        /// Im also rapidly trying to do the same for you
        /// </summary>
        /// <param name="step"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public New_pos_and_vel dothe_phys(double  step,IGetEffectedByGravity  i)
        {
            //ToDo can you use the xml comment to tell me what the return values are
            // I think they are the change in position and velocity, but am not sure 
            // ...   just type /// above a method to do the comment thang btw
            
            double hstep = step / 20f;
            step = step / 10;
            DVector3 force = DVector3.Zero;

            GameSpaceUnit space = i.Space ;

            DVector3 K1pos = i.Velocity;
                foreach (IEmitPointField j in space.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.PointFieldAcceleration(i.Position); }
                }
                DVector3 K1vel = force;

                DVector3 K2pos = K1vel * hstep + i.Velocity;

                force = DVector3.Zero;

                foreach (IEmitPointField j in space.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.PointFieldAcceleration(i.Position + K1pos * hstep); }
                }

                DVector3 K2vel = force;

                DVector3 K3pos = K2vel * hstep + i.Velocity;

                force = DVector3.Zero;

                foreach (IEmitPointField j in space.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.PointFieldAcceleration(i.Position + K2pos * hstep); }
                }
                DVector3 K3vel = force;

                DVector3 k4pos = K3vel * step + i.Velocity;
                force = DVector3.Zero;
                foreach (IEmitPointField j in space.PointFieldEmitters.Values)
                {
                    if (i == j) { } else { force = force + j.PointFieldAcceleration(i.Position + K3pos); }
                }
                DVector3 k4vel = force;


             New_pos_and_vel meow;
            meow.position = (step / 6f) * (K1pos + K2pos * 2f + K3pos * 2f + k4pos);
            meow.velocity =  (step / 6f) * (K1vel + K2vel * 2f + K3vel * 2f + k4vel);
            return (meow);
        }

        #endregion
    }
}
