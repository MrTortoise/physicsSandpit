﻿using System;
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

        protected Dictionary<string, IAmInSpace> mIAmInSpace;
        protected Dictionary<string, IGetEffectedByGravity> mFieldObjects;
        protected Dictionary<string, IEmitPointField> mPointFieldEmitters;
        protected Dictionary<string, IPhysicsObject> mPhysicsObjects;
      //  protected Dictionary<string, IUpdateable> mUpdateableObjects;
        protected Dictionary<string, IDrawable> mDrawableObjects;

        protected Dictionary<string, GameSpaceUnit> mGameSpaceUnits;


        public GameSpaceUnit()
        {
            mIAmInSpace = new Dictionary<string, IAmInSpace>();
            mFieldObjects = new Dictionary<string, IGetEffectedByGravity>();
            mPointFieldEmitters = new Dictionary<string, IEmitPointField>();
            mPhysicsObjects = new Dictionary<string, IPhysicsObject>();
          //  mUpdateableObjects = new Dictionary<string, IUpdateable>();
            mDrawableObjects = new Dictionary<string, IDrawable>();
            mLastUpdateTime = DateTime.Now;
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



        public void UpdateSpace(DateTime UpdateTime)
        {
            TimeSpan UpdatePeriod = UpdateTime - mLastUpdateTime;

            ResetForUpdate();

            PerformGravityUpdate(UpdatePeriod);
          //  PerformAnotherUpdateType(UpdatePeriod);

        }

        protected void ResetForUpdate()
        {
            foreach (IPhysicsObject p in mPhysicsObjects.Values )
            {
                p.ResetDisplacementStructures();
            }


        }

        protected void PerformGravityUpdate(TimeSpan  UpdatePeriod)
        {
            foreach (IGetEffectedByGravity i in mFieldObjects.Values )
            {
                i.ExecuteGravityDisplacement(UpdatePeriod);                
            }
        }        


    }
}
