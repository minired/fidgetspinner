using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class CoinAnimation : MonoBehaviour
    {
        public DG.Tweening.DOTweenPath[] dotweenPathList;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnCompleteAnimation(GameObject obj)
        {
            obj.SetActive(false);
        }

        public void OnPlayAnimation()
        {
            foreach (DG.Tweening.DOTweenPath p in dotweenPathList)
            {
                p.gameObject.SetActive(true);
                p.DORewind();
                p.DOPlay();
            }
        }
        

    }
}