using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _3dplayground.Physics;

namespace _3dplayground
{

    /*
     *   Parts of the update loop will be multithreaded.
     *   the main Division will be Physics and the rest. 
     *   
     *   Current PC's and laptops are dual core - IE only 2 threads will have good performance
     *   Otherwise context switching dominates
     *   
     *   However multithreading everything has to remain a possibility. 
     *   Maybe one day it will run on a server box with 16 cores.
     *   
     *   Processing should be seperated by independant phases Eg gravity, Player / input movement
     *   the assumpti is that combinatino of such operations will be 
     *   both associative and comutative in its combination
     *   
     *   However caluculating world forces prior to the forces internal - i know its wrong but cant 
     *   think of a better way to say it -  to the objects will allow
     *   the availability of the world forces on each object for use in their calculations.
     * 
     * 
     * So for now just write the operations a seperate and assume that the threading will be handled higher up.
     * * */

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
        CollisionComponent mCollisionComponent;

        protected Dictionary<string, IAmInSpace> mIAmInSpace;
        protected Dictionary<string, IGetEffectedByGravity> mFieldObjects;
        protected Dictionary<string, IEmitPointField> mPointFieldEmitters;
        protected Dictionary<string, IPhysicsObject> mPhysicsObjects;
      //  protected Dictionary<string, IUpdateable> mUpdateableObjects;
        protected Dictionary<string, IDrawable> mDrawableObjects;

        protected Dictionary<string, GameSpaceUnit> mGameSpaceUnits;


        protected List<DisplacementStructure> mUpdateStructures;
      //  protected List<DisplacementStructure> mDrawStructures;


        public GameSpaceUnit()
        {
            mIAmInSpace = new Dictionary<string, IAmInSpace>();
            mFieldObjects = new Dictionary<string, IGetEffectedByGravity>();
            mPointFieldEmitters = new Dictionary<string, IEmitPointField>();
            mPhysicsObjects = new Dictionary<string, IPhysicsObject>();
          //  mUpdateableObjects = new Dictionary<string, IUpdateable>();
            mDrawableObjects = new Dictionary<string, IDrawable>();
            mLastUpdateTime = DateTime.Now;
            mUpdateStructures = new List<DisplacementStructure>();
            CollisionComponent mCollisionComponent = new CollisionComponent();
           
        }
        #region Properties
       public string Name
       {get{return mName;}}

        public Dictionary<string, IAmInSpace> InSpaceObjects
        { get { return mIAmInSpace; } }

        public Dictionary<string, IEmitPointField> PointFieldEmitters
        { get { return mPointFieldEmitters; } }

        //public Dictionary<string, IUpdateable> UpdateableObjects
        //{ get { return mUpdateableObjects; } }

        public Dictionary<string, IPhysicsObject> PhysicsObjects
        { get { return mPhysicsObjects; } }

        public Dictionary<string, IDrawable> DrawableObjects
        { get { return mDrawableObjects; } }
        #endregion  
       
        public IAmInSpace GetGameObject(string name)
        { return mIAmInSpace[name]; }

        /// <summary>
        /// Adds the object to he collection and any other relevant lists.
        /// </summary>
        /// <param name="theGameObject"></param>
        public void AddGameObject(IAmInSpace theGameObject)
        {              

            if (!mIAmInSpace.ContainsKey(theGameObject.Name))
            {
                mIAmInSpace.Add(theGameObject.Name, theGameObject);
                // This casts the object, but sets it to null if the cast fails rather than throwing an exception
                IGetEffectedByGravity effect = theGameObject as IGetEffectedByGravity;
                if (effect != null)
                {
                    if (!mFieldObjects.ContainsKey(effect.Name))
                    {
                        mFieldObjects.Add(effect.Name, effect);
                    }
                }

                IEmitPointField emitter = theGameObject as IEmitPointField;
                if (emitter != null)
                {
                    if (!mPointFieldEmitters.ContainsKey(emitter.Name))
                    {
                        mPointFieldEmitters.Add(emitter.Name, emitter);

                    }
                }

                IPhysicsObject phys = theGameObject as IPhysicsObject;
                if (phys != null)
                {
                    if (!mPhysicsObjects.ContainsKey(phys.Name))
                    {
                        mPhysicsObjects.Add(phys.Name, phys);

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

                IDrawable drawable = theGameObject as IDrawable;
                if (drawable != null)
                {
                    if (!mDrawableObjects.ContainsKey(drawable.Name))
                    {
                        mDrawableObjects.Add(drawable.Name, drawable);
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

        public void UpdateSpace(DateTime UpdateTime)
        {
            TimeSpan UpdatePeriod = UpdateTime - mLastUpdateTime;

           
            // Perform updates in order of dependance - objects that use propulsion require knowledge of gravity?             
            // how is ai going to work with gravity path finding lol?
            //Can we use your technique to sample many points on a path to get an average? - easier solution would be to make them navigate by planets
            PerformGravityUpdate(UpdatePeriod);             
            PerformCompoundUpdates(UpdatePeriod);

            //objects that get updated raise their own events handled by  phys_RequestMove

            PerformCollisionCalculation(UpdatePeriod);

        }

        protected void PerformGravityUpdate(TimeSpan  UpdatePeriod)
        {
            foreach (IGetEffectedByGravity i in mFieldObjects.Values )
            {
                i.ExecuteGravityDisplacement(UpdatePeriod);
                
            }
        } 
        protected void PerformCompoundUpdates(TimeSpan UpdatePeriod)
        {

            // This is a loop for multithreading
            foreach (IPhysicsObject p in mPhysicsObjects.Values)
            {
                p.ResetDisplacementStructures();
                p.Update(UpdatePeriod);
               
            }
        }


        protected void AddDisplacementStructure(DisplacementStructure theStructure)
        {
            int theIndex = mUpdateStructures.IndexOf(theStructure);
            if (theIndex > 0)
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


        protected void PerformCollisionCalculation(TimeSpan UpdatePeriod)
        {
            //no collisions atm.

            foreach (DisplacementStructure d in mUpdateStructures)
            {
              
            }

        }

        #endregion

        #region Drawing Stuff

        public void Draw(Camera theCamera)
        {
            foreach (IPhysicsObject p in mDrawableObjects.Values )
            {
                p.Draw(theCamera, p.Position.ToVector3(), Quaternion.Identity);
            }


        }


        #endregion



    }
}
