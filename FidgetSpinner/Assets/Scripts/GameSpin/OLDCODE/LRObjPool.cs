using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Game2
{
    public class LRObjPool : MonoBehaviour
    {
        public GameObject Instantiate;
        public List<LRObj> LRObjList = new List<LRObj>();
        public LRObj TargetLR;
        public UIGrid Grid;

        public void Set()
        {
            for (int i = 0; i < 10; i++)
                Create();
            SetTarget();
        }

        public void SetTarget()
        {
            if (LRObjList.Count > 0)
                TargetLR = LRObjList[0];
        }

        public void Create()
        {
            LRObj temp = Object.Instantiate(Instantiate).GetComponent<LRObj>();
            LRObjList.Add(temp);
            temp.transform.parent = Grid.transform;
            temp.transform.SetAsFirstSibling();
            temp.gameObject.SetActive(true);
            Grid.Reposition();
        }

        public void Break()
        {
            LRObjList.RemoveAt(0);
            TargetLR.Break();
            Create();
            SetTarget();
        }
    }

}