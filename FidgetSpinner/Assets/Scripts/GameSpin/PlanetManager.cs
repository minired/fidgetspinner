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
        public Score score;
        public Combo combo;
        public Fever fever;
        public Combo_Circle circle;

        public Vector3[] leftPosition;      // 행성 자리 지정
        public Vector3[] rightPosition;

        GameObject[] leftPlanets = new GameObject[10];      // 행성들
        GameObject[] rightPlanets = new GameObject[10];

        bool isFever = false;

        int leftNum;
        int rightNum;
        int buttomNum;      // 맨 밑에있는 행성 번호

        public float sizeUp;
        public float sizeDown;
        public float moveTime;
        
        void Start()
        {
            buttomNum = 0;

            for(int i = 0; i < 10; i++)
            {
                leftPlanets[i] = NGUITools.AddChild(parent, planet);        //좌우 행성 생성
                rightPlanets[i] = NGUITools.AddChild(parent, planet);
                leftPlanets[i].transform.localPosition = leftPosition[i];       // 자리 지정
                rightPlanets[i].transform.localPosition = rightPosition[i];
                rightPlanets[i].transform.localScale *= sizeDown;      // 크기 지정
                leftPlanets[i].transform.localScale *= sizeDown;

                leftNum = Random.Range(0, 2);
                if (leftNum == 0)           // 좌우 상반되게 랜덤번호 생성
                {
                    rightNum = Random.Range(1, 3);
                }
                else
                {
                    leftNum = Random.Range(1, 3);
                    rightNum = 0;
                }

                rightPlanets[i].GetComponent<Planet>().setSprite(rightNum);     // 번호대로 행성 sprite 변경
                leftPlanets[i].GetComponent<Planet>().setSprite(leftNum);
            }
            rightPlanets[0].transform.localScale *= sizeUp;      // 크기 지정
            leftPlanets[0].transform.localScale *= sizeUp;

        }

        void ProceedPlanets()       // 버튼 누른 뒤 행성들 이동
        {
            int j = 0;

            leftPlanets[buttomNum].transform.localPosition = leftPosition[9];       // 맨밑 행성 맨위로, 사이즈 조절
            rightPlanets[buttomNum].transform.localPosition = rightPosition[9];
            leftPlanets[buttomNum].transform.localScale *= sizeDown;
            rightPlanets[buttomNum].transform.localScale *= sizeDown;

            leftNum = Random.Range(0, 2);       // 위로 보내면서 랜덤으로 다시 Set
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

            for (int i = buttomNum + 1; i < 10; i++, j++)       // 나머지는 한칸씩 아래로
            {
                LeanTween.moveLocal(leftPlanets[i], leftPosition[j], moveTime);
                LeanTween.moveLocal(rightPlanets[i], rightPosition[j], moveTime);
            }
            for(int i = 0; i < buttomNum; i++, j++)
            {
                LeanTween.moveLocal(leftPlanets[i], leftPosition[j], moveTime);
                LeanTween.moveLocal(rightPlanets[i], rightPosition[j], moveTime);
            }
            
            buttomNum = ++buttomNum % 10;

            leftPlanets[buttomNum].transform.localScale *= sizeUp;
            rightPlanets[buttomNum].transform.localScale *= sizeUp;
        }

        void ProceedPlanets_Fever()
        {
            int j = 0;

            leftPlanets[buttomNum].transform.localPosition = leftPosition[9];       // 맨밑 행성 맨위로, 사이즈 조절
            rightPlanets[buttomNum].transform.localPosition = rightPosition[9];
            leftPlanets[buttomNum].transform.localScale *= sizeDown;
            rightPlanets[buttomNum].transform.localScale *= sizeDown;

            rightPlanets[buttomNum].GetComponent<Planet>().setSprite(0);
            leftPlanets[buttomNum].GetComponent<Planet>().setSprite(0);

            for (int i = buttomNum + 1; i < 10; i++, j++)       // 나머지는 한칸씩 아래로
            {
                LeanTween.moveLocal(leftPlanets[i], leftPosition[j], moveTime);
                LeanTween.moveLocal(rightPlanets[i], rightPosition[j], moveTime);
            }
            for (int i = 0; i < buttomNum; i++, j++)
            {
                LeanTween.moveLocal(leftPlanets[i], leftPosition[j], moveTime);
                LeanTween.moveLocal(rightPlanets[i], rightPosition[j], moveTime);
            }

            buttomNum = ++buttomNum % 10;

            leftPlanets[buttomNum].transform.localScale *= sizeUp;
            rightPlanets[buttomNum].transform.localScale *= sizeUp;
        }

        public void LeftButton()
        {
            if (!isFever)
            {
                if (leftPlanets[buttomNum].GetComponent<Planet>().isPlanet)
                {
                    timer.Success();
                    score.Success();
                    combo.Success();
                    fever.Success();
                    circle.Success();
                }
                else
                {
                    if (leftPlanets[buttomNum].GetComponent<Planet>().isStone)
                    {
                        //timer.StoneFail();
                        combo.Fail();
                        fever.Fail();
                    }
                    else
                    {
                        timer.BrokenFail();
                        combo.Fail();
                        fever.Fail();
                    }
                }
                ProceedPlanets();
            }
            if(isFever)
            {
                timer.Success();
                score.Success();
                combo.Success();
                circle.Success();

                ProceedPlanets_Fever();
            }

        }

        public void RightButton()
        {
            if (!isFever)
            {
                if (rightPlanets[buttomNum].GetComponent<Planet>().isPlanet)
                {
                    timer.Success();
                    score.Success();
                    combo.Success();
                    fever.Success();
                    circle.Success();
                }
                else
                {
                    if (rightPlanets[buttomNum].GetComponent<Planet>().isStone)
                    {
                        //timer.StoneFail();
                        combo.Fail();
                        fever.Fail();
                    }
                    else
                    {
                        timer.BrokenFail();
                        combo.Fail();
                        fever.Fail();
                    }
                }

                ProceedPlanets();
            }

            if(isFever)
            {
                timer.Success();
                score.Success();
                combo.Success();
                circle.Success();

                ProceedPlanets_Fever();
            }
        }

        public void FeverOn()
        {
            isFever = true;
            for(int i = 0; i < 10; i++)
            {
                leftPlanets[i].GetComponent<Planet>().setSprite(0);
                rightPlanets[i].GetComponent<Planet>().setSprite(0);
            }
        }

        public void FeverOff()
        {
            isFever = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                LeftButton();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                RightButton();
        }
    }
}