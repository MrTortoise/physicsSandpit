using System;

using _3dplayground.Maths;
using _3dplayground.Physics;

namespace _3dplayground.Ships
{
    interface IAmShipEngine:IHasMass, IHasName 
    {
        //TodO: Implement thrusters ... swappable rotational, lateral and main thrust
        //ToDo: Implement being able to distribute engine power accross them in different ratios.


        /// <summary>
        /// Gets / Sets wether the engine is currently active
        /// </summary>
        bool IsActive
        { get; set; }

        /// <summary>
        /// If not accelerating then decelerating.
        /// </summary>
        bool IsAccelerating
        { get; set; }

        /// <summary>
        /// The current fuel level of the engines
        /// </summary>
        float FuelLevel
        { get; }

        /// <summary>
        /// The capacity of the engines fuel tank
        /// </summary>
        float FuelCapacity
        { get; }

        /// <summary>
        /// The rate of fuel consumption
        /// </summary>
        float FuelConsumption
        { get; }

        /// <summary>
        /// The thrust that the engines produce
        /// </summary>
        double GetThrust
        { get; }

        /// <summary>
        /// Allows the additino of fuel to the fuel tank
        /// </summary>
        void AddFuel(float  Amount);

        /// <summary>
        /// This returns the accfeleration vector it produces
        /// </summary>
        /// <param name="mass">the mass of the ship excluding the engine</param>
        /// <param name="UnitDirection">direction the engines are pointing</param>
        /// <param name="timeSpan">The time the engines burn for</param>
        /// <returns></returns>
        double GetAcceleration(double mass, double timeSpan);

        
    }
}
