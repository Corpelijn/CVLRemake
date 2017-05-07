using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    interface IDrawable
    {
        void Draw(Transform parent, Vector3 position);

        void Destroy();
    }
}
