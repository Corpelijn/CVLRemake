using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Input.Interfaces
{
    interface IMouseHoverLeave : IClickableObject
    {
        void OnMouseHoverLeave();
    }
}
