using System;


using _3dplayground.Graphics;

namespace _3dplayground.Physics
{
    /// <summary>
    /// This class forms the basis of the physics and positional thread
    /// </summary>
    sealed  class GameSpacePhysics
    {

       // private Dictionary<int, ICanMove> mItems = new Dictionary<int,ICanMove>();
        private GameSpaceUnit mSpace;


        public GameSpacePhysics(GameSpaceUnit theGameSpace)
        {
            mSpace = theGameSpace;
        }
        

        public void Update(float TimePeriod, DrawingBufferManager theBuffer)
        {
            //Preform the update on the gamespace unit
            mSpace.UpdateSpace(TimePeriod,theBuffer);
            // take any drawingBufferItems and add them to the DrawingBufferManager to provbide updated data to the draw loop.          

        }





    }
}
