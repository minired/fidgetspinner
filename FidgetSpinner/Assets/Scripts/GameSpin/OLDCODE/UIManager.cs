using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Game2
{
    public class UIManager : MonoBehaviour
    {
        public void OnClick_Left()
        {
            MainCamera.Instance.Pool.Break();
        }

        public void OnClick_Right()
        {
            MainCamera.Instance.Pool.Break();
        }
    }
}
