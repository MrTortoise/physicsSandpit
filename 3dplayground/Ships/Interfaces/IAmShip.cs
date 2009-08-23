using System;
using System.Collections.Generic;
using _3dplayground.Physics;
using _3dplayground.Maths;

namespace _3dplayground.Ships
{
    interface IAmShip : IPhysicsObject,IGetEffectedByGravity 
    {

        void SetEngine(IAmShipEngine theEngine);

        void TurnEngineOff();
        void TurnEngineOn();

        /// <summary>
        /// Sets the direction the ship will start to rotationally accelerate towards.
        /// </summary>
        /// <param name="theRotation"></param>
        /// <param name="magnitude">A value between 0 and 1 inclusive</param>                                          
        void SetRotationUnitVector(DVector3 theRotation,float magnitude);

        


    }
}
