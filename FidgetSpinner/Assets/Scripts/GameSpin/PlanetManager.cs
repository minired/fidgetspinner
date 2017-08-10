using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.GameSpin
{
    public class PlanetManager : MonoBehaviour
    {

        public GameObject planet;
        public GameObject parent;
        public Timer timer;

        public Vector3[] leftPosition;
        public Vector3[] rightPosition;

        GameObject[] leftPlanets = new GameObject[10];
        GameObject[] rightPlanets = new GameObject[10];

        int leftNum;
        int rightNum;

        int buttomNum;
        
        void Start()
        {
            buttomNum = 0;

            for(int i = 0; i < 10; i++)
            {
                leftPlanets[i] = NGUITools.AddChild(parent, planet);
                rightPlanets[i] = NGUITools.AddChild(parent, planet);
                leftPlanets[i].transform.localPosition = leftPosition[i];
                rightPlanets[i].transform.localPosition = rightPosition[i];

                leftNum = Random.Range(0, 2);
                if (leftNum == 0)
                {
                    rightNum = Random.Range(1, 3);
                }
                else
                {
                    leftNum = Random.Range(1, 3);
                    rightNum = 0;
                }

                rightPlanets[i].GetComponent<Planet>().setSprite(rightNum);
                leftPlanets[i].GetComponent<Planet>().setSprite(leftNum);

            }
        }

        void ProceedPlanets()
        {
            int j = 0;

            leftPlanets[buttomNum].transform.localPosition = leftPosition[9];
            rightPlanets[buttomNum].transform.localPosition = rightPosition[9];

            leftNum = Random.Range(0, 2);
            if (leftNum == 0)
            {
                rightNum = Random.Range(1, 3);
            }
            else
            {
                leftNum = Random.Range(1, 3);
                rightNum = 0;
            }

            rightPlanets[buttomNum].GetComponent<Planet>().setSprite(rightNum);
            leftPlanets[buttomNum].GetComponent<Planet>().setSprite(leftNum);

            for (int i = buttomNum + 1; i < 10; i++, j++)
            {
                leftPlanets[i].transform.localPosition = leftPosition[j];
                rightPlanets[i].transform.localPosition = rightPosition[j];
            }
            for(int i = 0; i < buttomNum; i++, j++)
            {
                leftPlanets[i].transform.localPosition = leftPosition[j];
                rightPlanets[i].transform.localPosition = rightPosition[j];
            }
            
            buttomNum = ++buttomNum % 10;
        }

        public void LeftButton()
        {
            if(leftPlanets[buttomNum].GetComponent<Planet>().isPlanet)
            {
                timer.Success();
                Debug.Log("맞췄다");
            }
            else
            {
                timer.Fail();
                Debug.Log("틀렸다");
            }

            ProceedPlanets();
        }

        public void RightButton()
        {
            if (rightPlanets[buttomNum].GetComponent<Planet>().isPlanet)
            {
                timer.Success();
                Debug.Log("맞췄다");
            }
            else
            {
                timer.Fail();
                Debug.Log("틀렸다");
            }

            ProceedPlanets();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}