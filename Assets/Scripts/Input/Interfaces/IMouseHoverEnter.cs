using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Input.Interfaces
{
    interface IMouseHoverEnter : IClickableObject
    {
        void OnMouseHoverEnter();
    }
}
