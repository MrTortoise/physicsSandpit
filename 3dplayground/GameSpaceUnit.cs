using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using _3dplayground.Physics;
using _3dplayground.Graphics.D3;
using _3dplayground.Graphics;


namespace _3dplayground
{

    /// <summary>
    /// This class represents a unit of gamespace containing all the objects in that space.
    /// This will need adapting in the future, but you get the gist.
    /// There will be garbage collection issues.
    /// IE we can have high density regions so that collision lists do not get too large
    /// We do this by breaking them down into isolated and insulated areas. 
    /// Ie we can loosley control the density of the resolution/definition/complexity of space
    /// </summary>
   public  class GameSpaceUnit
    {
       protected string mName;    

        DateTime mLastUpdateTime;
        CollisionComponent mCollisionComponent = new CollisionComponent();

        protected Dictionary<int, IAmInSpace> mIAmInSpace = new Dictionary<int,IAmInSpace>();
        protected Dictionary<int, IGetEffectedByGravity> mFieldObjects = new Dictionary<int,IGetEffectedByGravity>();
        protected Dictionary<int, IEmitPointField> mPointFieldEmitters = new Dictionary<int,IEmitPointField>();
        protected Dictionary<int, IPhysicsObject> mPhysicsObjects = new Dictionary<int,IPhysicsObject>();

        protected Dictionary<int, I3DDrawable> mDrawableObjects = new Dictionary<int,I3DDrawable>();

        protected Dictionary<int, GameSpaceUnit> mGameSpaceUnits = new Dictionary<int,GameSpaceUnit>();


        protected List<DisplacementStructure> mUpdateStructures = new List<DisplacementStructure>();  

       // protected Dictionary<int, DrawingBufferItem> mDrawingItems;

        



        public GameSpaceUnit()
        {
            mLastUpdateTime = DateTime.Now;           
        }
        #region Properties
       public string Name
       {get{return mName;}}

       public Dictionary<int, IAmInSpace> InSpaceObjects
        { get { return mIAmInSpace; } }

       public Dictionary<int, IEmitPointField> PointFieldEmitters
        { get { return mPointFieldEmitters; } }

        //public Dictionary<string, IUpdateable> UpdateableObjects
        //{ get { return mUpdateableObjects; } }

       public Dictionary<int, IPhysicsObject> PhysicsObjects
        { get { return mPhysicsObjects; } }

       public Dictionary<int, I3DDrawable> DrawableObjects
        { get { return mDrawableObjects; } }
        #endregion  
       
        public IAmInSpace GetGameObject(int id)
        { return mIAmInSpace[id]; }  
   

        /// <summary>
        /// Adds the object to he collection and any other relevant lists.
        /// </summary>
        /// <param name="theGameObject"></param>
        public void AddGameObject(IAmInSpace theGameObject)
        {              

            if (!mIAmInSpace.ContainsKey(theGameObject.ID))
            {
                mIAmInSpace.Add(theGameObject.ID, theGameObject);
                // This casts the object, but sets it to null if the cast fails rather than throwing an exception
                IGetEffectedByGravity effect = theGameObject as IGetEffectedByGravity;
                if (effect != null)
                {
                    if (!mFieldObjects.ContainsKey(effect.ID ))
                    {
                        mFieldObjects.Add(effect.ID, effect);
                    }
                }

                IEmitPointField emitter = theGameObject as IEmitPointField;
                if (emitter != null)
                {
                    if (!mPointFieldEmitters.ContainsKey(emitter.ID))
                    {
                        mPointFieldEmitters.Add(emitter.ID, emitter);

                    }
                }

                IPhysicsObject phys = theGameObject as IPhysicsObject;
                if (phys != null)
                {
                    if (!mPhysicsObjects.ContainsKey(phys.ID))
                    {
                        mPhysicsObjects.Add(phys.ID, phys);

                    }
                }

                phys.RequestMove += new EventHandler<DisplacementArgs>(phys_RequestMove);

                //IUpdateable updateable = theGameObject as IUpdateable;
                //if (updateable != null)
                //{
                //    if (!mUpdateableObjects.ContainsKey(updateable.Name))
                //    {
                //        mUpdateableObjects.Add(updateable.Name, updateable);
                //    }
                //}

                I3DDrawable drawable = theGameObject as I3DDrawable;
                if (drawable != null)
                {
                    if (!mDrawableObjects.ContainsKey(drawable.ID))
                    {
                        mDrawableObjects.Add(drawable.ID, drawable);
                    }
                }

                
            }
        }

       

        void phys_RequestMove(object sender, DisplacementArgs e)
        {
            // add the movement to the list to be processed
            AddDisplacementStructure(e.Displacement);
        }

        #region Update Stuff

       /// <summary>
       /// Performs the upate on the GameSoaceUnit. This updates all objects recursivley and publishes their changes in 
        /// DrawingBufferItems. It also adds these DrawingBufferItems to the buffer.
       /// </summary>
       /// <param name="UpdateTime"></param>
        public void UpdateSpace(float UpdateTime, DrawingBufferManager theBuffer)
        {
            // ToDo: when multi GameSpaceUnits are implemented then this will need to solve the boundary collisions
            foreach (GameSpaceUnit g in mGameSpaceUnits.Values)
            {
                g.UpdateSpace(UpdateTime,theBuffer );
            }
            // Clear the buffer lists
          
            mUpdateStructures.Clear();

            // Perform updates in order of dependance - objects that use propulsion require knowledge of gravity?             
            // how is ai going to work with gravity path finding lol?
            // Can we use your technique to sample many points on a path to get an average? - easier solution would be to make them navigate by planets
         
           PerformGravityUpdate(UpdateTime);
            
            
            PerformCompoundUpdates(UpdateTime);

            //objects that get updated raise their own events handled by  phys_RequestMove which constructs the list of displacement structures to be resolved
            PerformCollisionCalculation(UpdateTime);
            //the displacement structures have now been updated to reflect their collision evalutated state
      
        
            foreach (DisplacementStructure d in mUpdateStructures)
            {
                // for each object that needs to be updated, execute its update args.
                d.IPhysicsObject.ExecuteDisplacementStructure(d);

                // If object is drawable 
                // Generate the DrawingStructures - this is done so that the update can get on with 
                // changing whatever it wants whilst the draw uses this structure
                I3DDrawable draw = d.IPhysicsObject  as I3DDrawable;
                if (draw != null)
                {
                    // now we need to add it to the drawing buffer.
                    // We can use the IphysicsObject because we know that the updates have taken place
                    DrawingBufferItem i = new DrawingBufferItem(draw,d.IPhysicsObject.IsDrawActive, 
                        d.IPhysicsObject.Position.ToVector3(), d.IPhysicsObject.Rotation);
                    
                    theBuffer.Add(i);
                }
            }  
        }

        protected void PerformGravityUpdate(float   UpdatePeriod)
        {
            foreach (IGetEffectedByGravity i in mFieldObjects.Values )
            {
                i.ExecuteGravityDisplacement(UpdatePeriod);
                
            }
        } 
        protected void PerformCompoundUpdates(float  UpdatePeriod)
        {
            // This is a loop for multithreading
            foreach (IPhysicsObject   p in mPhysicsObjects.Values)
            {            
                p.Update(UpdatePeriod);                   
            }
        }
        protected void PerformCollisionCalculation(float UpdatePeriod)
        {
            //no collisions atm. 
        }



        protected void AddDisplacementStructure(DisplacementStructure theStructure)
        {
            int theIndex = mUpdateStructures.IndexOf(theStructure);
            if (theIndex > -1)
            {
                //ToDo: Implement +=
                mUpdateStructures[theIndex]= mUpdateStructures[theIndex] + theStructure;
            }
            else
            {
                mUpdateStructures.Add(theStructure);
            }

        }

        protected DisplacementStructure GetNext()
        {
            DisplacementStructure retVal = mUpdateStructures[0];
            mUpdateStructures.RemoveAt(0);
            return retVal;
        }          

        #endregion





    }
}
