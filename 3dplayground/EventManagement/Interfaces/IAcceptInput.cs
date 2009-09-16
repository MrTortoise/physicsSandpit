using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground.EventManagement
{
    /// <summary>
    /// This is the interface for accepting input, it will also be what the ai use as their basis for input.
    /// </summary>
    interface IAcceptInput
    {

        void SubscribeToInputEvents();
        void UnSubscribeFromInputEvents();

        void OnMouseButtonPressed(object s, InputEventArgs args);
        void OnMouseButtonReleased(object s, InputEventArgs args);
        void OnMouseWheelScrolled(object s, InputEventArgs args);
        void OnMousePositionChanged(object s, InputEventArgs args);
        void OnKeyPressed(object s, InputEventArgs args);
        void OnKeyReleased(object s, InputEventArgs args);

    }
}
