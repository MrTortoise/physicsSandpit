using System;
using System.Collections.Generic;
using _3dplayground.Physics;
using _3dplayground.Maths;

namespace _3dplayground.Ships
{
    interface IAmShip : IPhysicsObject,IGetEffectedByGravity 
    {

        void SetEngine(IAmShipEngine theEngine);

        void Accelerate();
        void Decelerate();
        void TurnEngineOff();

        void RotateLaterally(float value);
        void RotateLongitudionally(float value);

        


    }
}
