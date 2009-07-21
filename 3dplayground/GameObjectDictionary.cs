using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _3dplayground.Physics;

namespace _3dplayground
{
    /// <summary>
    /// Singleton class to hold the list of objects.
    /// This will need adapting in the future, but you get the gist.
    /// There will be garbage collection issues.
    /// </summary>
    class GameObjectDictionary
    {
        protected static GameObjectDictionary mInstance;

        protected Dictionary<string, IAmInSpace> mIAmInSpace;
        protected Dictionary<string, IGetEffectedByGravity> mFieldObjects;
        protected Dictionary<string, IEmitPointField> mPointFieldEmitters;
        protected Dictionary<string, IUpdateable> mUpdateableObjects;
        protected Dictionary<string, IDrawable> mDrawableObjects;


        /// <summary>
        /// Cannot be instantiated externally
        /// </summary>
        protected GameObjectDictionary()
        {
            mIAmInSpace = new Dictionary<string, IAmInSpace>();
            mFieldObjects = new Dictionary<string, IGetEffectedByGravity>();
            mPointFieldEmitters = new Dictionary<string, IEmitPointField>();
            mUpdateableObjects = new Dictionary<string, IUpdateable>();
            mDrawableObjects = new Dictionary<string, IDrawable>();
        }

        /// <summary>
        /// Returns the instance of the class.
        /// </summary>
        /// <returns></returns>
        public static  GameObjectDictionary GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new GameObjectDictionary();
            }
            return mInstance;
        }

        public Dictionary<string, IAmInSpace> InSpaceObjects
        { get { return mIAmInSpace; } }

        public Dictionary<string, IEmitPointField> PointFieldEmitters
        { get { return mPointFieldEmitters; } }

        public Dictionary<string, IUpdateable> UpdateableObjects
        { get { return mUpdateableObjects; } }

        public Dictionary<string, IDrawable> DrawableObjects
        { get { return mDrawableObjects; } }


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

                IUpdateable updateable = theGameObject as IUpdateable;
                if (updateable != null)
                {
                    if (!mUpdateableObjects.ContainsKey(updateable.Name))
                    {
                        mUpdateableObjects.Add(updateable.Name, updateable);
                    }
                }

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




    }
}
