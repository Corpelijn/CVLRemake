using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Input.Interfaces
{
    interface IMouseClick : IClickableObject
    {
        void OnMouseClick(int button, Vector3 hitPoint);
    }
}
