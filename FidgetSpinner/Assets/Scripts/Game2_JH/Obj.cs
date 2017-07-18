using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Game2
{

    public class Obj : MonoBehaviour
    {
        public enum Target
        {
            Red,
            Blue,
        }
        public Target TargetType;
        public SpriteRenderer Sprite;

        public void Set()
        {
            int seed = Random.Range(0, 2);
            switch (seed)
            {
                case 0:
                    Sprite.color = Color.red;
                    TargetType = Target.Red;
                    break;

                case 1:
                    Sprite.color = Color.blue;
                    TargetType = Target.Blue;
                    break;
            }
        }
    }
}
