using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using _3dplayground.EventManagement;
using _3dplayground.Maths;
using _3dplayground.Graphics.D3;
using _3dplayground.Ships;



namespace _3dplayground
{
    class Player : IAcceptInput
    {

      
        protected Camera mCamera;
        protected IAmShip mShip;


        public Player(Camera thecamera)
        {
            mCamera=thecamera;             
        }

        public IAmShip Ship
        {
            get { return mShip; }
        }

        public Camera Camera
        { get { return mCamera; } }

        public void SetShip(IAmShip theShip)
        {
            mShip = theShip;
            mCamera.CameraMode = CameraMode.Attached;
            mCamera.AttachToObject(mShip);            
        }

        #region IAcceptInput Members

        public void SubscribeToInputEvents()
        {
            EventManager e = EventManager.GetInstance();

            e.KeyPressed += new EventHandler<InputEventArgs>(OnKeyPressed);
            e.KeyReleased += new EventHandler<InputEventArgs>(OnKeyReleased);
           // e.MouseButtonPressed += new EventHandler<InputEventArgs>(OnMouseButtonPressed);
           // e.MouseButtonReleased += new EventHandler<InputEventArgs>(OnMouseButtonReleased);
            e.MousePositionChanged += new EventHandler<InputEventArgs>(OnMousePositionChanged);
          //  e.MouseWheelScrolled += new EventHandler<InputEventArgs>(OnMouseWheelScrolled);
        }

        public void UnSubscribeFromInputEvents()
        {
            EventManager e = EventManager.GetInstance();

            e.KeyReleased -= OnKeyReleased;
            e.KeyPressed -= OnKeyPressed;
          //  e.MouseButtonPressed -= OnMouseButtonPressed;
          //  e.MouseButtonReleased -= OnMouseButtonReleased;
            e.MousePositionChanged -= OnMousePositionChanged;
          //  e.MouseWheelScrolled -= OnMouseWheelScrolled;
        }

        public void OnMouseButtonPressed(object s, InputEventArgs args)
        {
           
        }
        public void OnMouseButtonReleased(object s, InputEventArgs args)
        {
           
        }
        public void OnMouseWheelScrolled(object s, InputEventArgs args)
        {
           
        }

        public void OnMousePositionChanged(object s, InputEventArgs args)
        {
            //deal with rotation later
        }

        public void OnKeyPressed(object s, InputEventArgs args)
        {
            foreach (Keys k in args.NewPressedKeys)
            {
                if (k == Config.Accelerate)
                {
                    mShip.Accelerate();
                }
                else if (k == Config.Decelerate)
                {
                    mShip.Decelerate();
                }
            }
        }

        public void OnKeyReleased(object s, InputEventArgs args)
        {
            foreach (Keys k in args.NewDepressedKeys )
            {
                if (k == Config.Accelerate)
                {
                    mShip.TurnEngineOff();
                }
                else if (k == Config.Decelerate)
                {
                    mShip.TurnEngineOff();
                }
            }
        }

        #endregion
    }
}
