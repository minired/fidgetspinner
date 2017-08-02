using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class BackgroundSelector : MonoBehaviour
    {
        public List<Material> materialList;

        // Use this for initialization
        void Start()
        {
            
        }

        public void SetBackground(int index)
        {
            gameObject.GetComponent<SpriteRenderer>().material = materialList[index];
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}