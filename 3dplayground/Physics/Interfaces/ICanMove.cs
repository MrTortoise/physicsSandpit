using System;

using Microsoft.Xna.Framework;

using _3dplayground.Maths;

namespace _3dplayground.Physics
{
    /// <summary>
    /// If an object wants to be moved around then it needs to implement this interface.
    /// </summary>
    public interface ICanMove : IAmInSpace 
    {
        DVector3 Velocity
        { get; }

        Quaternion AngularVelocity
        { get; }

        bool IsCanMoveActive
        { get; }

        event EventHandler<DisplacementArgs> RequestMove;

        /// <summary>
        /// This value is only valid when all updates have completed for the frame
        /// </summary>
        DisplacementStructure GetDisplacementStructure
        { get; }
        void RaiseRequestMove(DisplacementArgs theArgs);
        void ExecuteDisplacementStructure(DisplacementStructure theStructure);
        void ResetDisplacementStructures();

        void Update(float UpdateTime);
    }
}
