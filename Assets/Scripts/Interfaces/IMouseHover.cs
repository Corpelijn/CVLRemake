using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    interface IMouseHover : IClickableObject
    {
        void OnMouseHover(Vector3 mousePosition);
    }
}
