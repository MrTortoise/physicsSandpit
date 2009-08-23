using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground.Graphics
{
    public interface IGameDrawable : IHasName 
    {
    
        void Draw(float theTimeSpan, Camera theCamera);

        bool IsDrawActive
        { get; }

        /// <summary>
        /// This takes the drawing structure that it generated earlier in its update loop and applies it to the object
        /// </summary>
        /// <param name="theBufferItem"></param>
        void ApplyDrawingstructure(DrawingStructure theBufferItem);

        /// <summary>
        /// thi smethod constructs the Updated info for the draw thread from the update thread
        /// </summary>
        /// <returns></returns>
        DrawingStructure ConstructDrawingStuct(DisplacementStructure theSource);

    }
}
