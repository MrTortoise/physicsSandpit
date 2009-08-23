using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _3dplayground.Graphics
{
    /// <summary>
    /// A singleton class that represents the DrawigBufferManager. The assumption is tht there is only one.
    /// I want to test this - i want a problem if i need 2 so i can think properly about it.
    /// This class assumes that structures represent abolute values and not differentials that need summing.
    /// </summary>
    public sealed  class DrawingBufferManager
    {
        //ToDo: turn this into a generic class ... buffers for all then :D

        /// <summary>
        /// enum used to identify which buffer is being used for drawing and update.
        /// </summary>
        private enum eBuffer
        {
            first,
            second
        }

        // This class is effectivley a triple buffer. 
        //In reality its like 2.5 because the draw buffer will be created by cloning the available finished frame.
        //If it was to be made triple buffer by explicitly having  the list here there would be concurrency issues

        //This class consists of 2 buffers, one representing the available complete frame nd the othe for the work in progress.
        //The complete frame can be cloned by the Get draw at anytime.
        
        static private  DrawingBufferManager mInstance;

        public EventHandler NewFrameFinished;


        private List<DrawingBufferItem> mItems = new List<DrawingBufferItem>();
        private List<DrawingBufferItem> mItems2 = new List<DrawingBufferItem>(); 

 
        private eBuffer mUpdateBuffer = eBuffer.first;
        private eBuffer mFinishedBuffer = eBuffer.second;

        private object mLockUpdate = new object();
        private object mLockFinished = new object();

        private bool mIsFrameReady = false;
       // private bool mIsSecondFrameReady = false;      


        private DrawingBufferManager()
        { }

        /// <summary>
        /// Gets an instance of the singleton DrawingBufferManager
        /// </summary>
        /// <returns></returns>
        static public DrawingBufferManager GetInstance()
        {
            if (mInstance == null)
            { mInstance = new DrawingBufferManager(); }


            return mInstance;

        }

        /// <summary>
        /// Returns true if there is a complete frame of buffer items
        /// </summary>
        public bool IsFrameReady
        { get { return mIsFrameReady ; } }

        /// <summary>
        /// Adds a drawing buffer item to the list that is being updated.
        /// This function is Thread Safe.
        /// </summary>
        /// <param name="theItem"></param>
        public void Add(DrawingBufferItem theItem)
        {
            Monitor.Enter(mLockUpdate);
            try
            {
                switch (mUpdateBuffer)
                {
                    case eBuffer.first:
                        AddToBuffer(mItems, theItem);
                        break;
                    case eBuffer.second:
                        AddToBuffer(mItems2, theItem);
                        break;
                }
            }
            catch (Exception ex)
            { throw new Exception("Eception whilst Adding a drawing buffer item", ex); }
            finally
            {
                Monitor.Pulse(mLockUpdate);
                Monitor.Exit(mLockUpdate);
            }
        }


        public void ApplyDrawBuffer()
        {
            
            Monitor.Enter(mLockFinished);
            try
            {
                List<DrawingBufferItem> temp = Getbuffer(mFinishedBuffer);
                foreach (DrawingBufferItem i in temp)
                {
                    i.ApplyBuffer();
                }
            }
            catch (Exception ex)
            {
                Exception e = new Exception("Exception whilst getting lock on or setting return value for GetDrawBuffer", ex);
                e.Data.Add("DrawBuffer", mFinishedBuffer.ToString());
                throw e;
            }
            finally
            {
                Monitor.Pulse(mLockFinished);
                Monitor.Exit(mLockFinished);

            }

        }
  

        /// <summary>
        /// Called by update loop once it has finished. this swaps the buffers over and clears the update for new data.
        /// This function is threadsafe.
        /// </summary>
        public  void UpdateFinished()
        {
            Monitor.Enter(mLockUpdate);
            Monitor.Enter(mLockFinished);
            try
            {  
                mIsFrameReady = false;
                eBuffer temp = mFinishedBuffer;
                mFinishedBuffer = mUpdateBuffer;
                mUpdateBuffer = temp;
                ClearBuffer(mUpdateBuffer);
                mIsFrameReady = true;
            }
            catch (Exception ex)
            {
                Exception e = new Exception("Exception whllst executing Updatefinished", ex);
                throw e;
            }
            finally
            {
                Monitor.Pulse(mLockFinished);
                Monitor.Exit(mLockFinished);
                Monitor.Pulse(mLockUpdate);
                Monitor.Exit(mLockUpdate);
            }
            

        }

        /// <summary>
        /// THIS IS NOT THREAD SAFE
        /// </summary>
        /// <param name="theBuffer"></param>
        private void ClearBuffer(eBuffer theBuffer)
        {
            switch (theBuffer)
            {
                case eBuffer.first:
                    mItems.Clear();
                    break;

                case eBuffer.second:
                    mItems2.Clear();
                    break;
            }

        }

        /// <summary>
        /// NOT THREADSAFE  - adds if new, overwrites existing if not
        /// </summary>
        /// <param name="theBuffer"></param>
        /// <param name="theItem"></param>
        private void AddToBuffer(List<DrawingBufferItem> theBuffer, DrawingBufferItem theItem)
        {
            int index = theBuffer.IndexOf(theItem);
            if (index > -1)
            {
                theBuffer[index] = theItem;
            }
            else
            {
                theBuffer.Add(theItem);
            }
        }

        /// <summary>
        /// NOT HTREADSAFE
        /// </summary>
        /// <param name="theBuffer"></param>
        /// <returns></returns>
        private List<DrawingBufferItem> Getbuffer(eBuffer theBuffer)
        {
            List<DrawingBufferItem> retVal = null ;
            switch (theBuffer)
            {
                case eBuffer.first:
                    retVal = mItems;
                    break;
                case eBuffer.second:
                    retVal = mItems2;
                    break;     
            }
            return retVal;
        }
        

    }
}
