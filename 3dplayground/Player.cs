using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using _3dplayground.EventManagement;
using _3dplayground.Physics;
using _3dplayground.Maths;
using _3dplayground.Graphics.D3;
using _3dplayground.Ships;



namespace _3dplayground
{
    class Player  
    {

        protected EventManager mEventManager;
        protected Camera mCamera;
        protected IAmShip mShip;


        public Player(Camera thecamera)
        {
            mCamera=thecamera;

            mEventManager = EventManager.GetInstance();

            mEventManager.KeyPressed += new EventHandler<InputEventArgs>(EventManager_KeyPressed);
            mEventManager.KeyReleased += new EventHandler<InputEventArgs>(EventManager_KeyReleased);
            mEventManager.MouseButtonPressed += new EventHandler<InputEventArgs>(EventManager_MouseButtonPressed);
            mEventManager.MouseButtonReleased += new EventHandler<InputEventArgs>(EventManager_MouseButtonReleased);
            mEventManager.MousePositionChanged += new EventHandler<InputEventArgs>(EventManager_MousePositionChanged);
            
        }



        protected void EventManager_KeyPressed(Object s, InputEventArgs a)
        {
            foreach (Keys k in a.NewDepressedKeys)
            {
                switch (k)
                {
                    case Keys.W:
                        {

                            break;
                        }
                }
                        

            }

        }

        protected void EventManager_KeyReleased(Object s, InputEventArgs a)
        {

        }

        protected void EventManager_MouseButtonReleased(Object s, InputEventArgs a)
        {

        }

        protected void EventManager_MouseButtonPressed(Object s, InputEventArgs a)
        {

        }

        protected void EventManager_MousePositionChanged(Object s, InputEventArgs a)
        {

        }
    }
}
