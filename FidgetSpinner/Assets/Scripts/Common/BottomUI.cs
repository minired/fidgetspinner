using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
namespace Fidget.Common
{
    public class BottomUI : MonoBehaviour
    {
        public GameObject shopBtnObj;

        public GameObject adBtnObj;


        public GameObject adPopupObj;

        Vector3 shopBtnPos;
        Vector3 adBtnPos;

      

        public void MoveShopScene()
        {
            SceneManager.LoadScene("Shop");
        }

        private void Awake()
        {
            shopBtnPos = shopBtnObj.transform.position;
            adBtnPos = adBtnObj.transform.position;
        }


        // Use this for initialization
        void Start()
        {
            Advertisement.Initialize("1515814", true);
        }
        void MoveEnd()
        {
            shopBtnObj.gameObject.SetActive(false);
            adBtnObj.gameObject.SetActive(false);
        }


        public void InitPosition()
        {
            shopBtnObj.transform.position = shopBtnPos;
            adBtnObj.transform.position = adBtnPos;

            shopBtnObj.gameObject.SetActive(true);
            adBtnObj.gameObject.SetActive(true);
        }

        public void MoveStart()
        {
            if (LeanTween.isTweening(shopBtnObj))
            {
                LeanTween.cancel(shopBtnObj);
            }
            if (LeanTween.isTweening(adBtnObj))
            {
                LeanTween.cancel(adBtnObj);
            }
            LeanTween.moveX(adBtnObj, -0.9f, 1.0f).setOnComplete(MoveEnd);
            LeanTween.moveX(shopBtnObj, 0.9f, 1.0f);
        }


        public void ShowAdPopup()
        {
            if (Advertisement.IsReady())
            {
                adPopupObj.SetActive(true);
            }
        }


        


        // Update is called once per frame
        void Update()
        {

        }
    }
}