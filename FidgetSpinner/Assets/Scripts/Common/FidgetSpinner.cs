using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fidget.Player;
using Fidget.Data;
namespace Fidget.Common
{
    public class FidgetSpinner : MonoBehaviour
    {


        public List<UIAtlas> atlasList;

        Vector3 zAxis = new Vector3(0, 0, 10);

      

        float speed = 0.0f;
        bool isSpin = false;

        bool isLoopMode = false;


        float maxSpeed = 100.0f;
        public float MaxSpeed
        {
            get
            {
                return maxSpeed;
            }
        }


        float damping = 2.0f;
        public float Damping
        {
            get
            {
                return damping;
            }
        }


        float expUpdateTime = 0.0f;

        float haste = 10.0f;

        ulong coin = 5;

        float coinDelay = 0.0f;
        public float CoinDelay
        {
            get
            {
                return coinDelay;
            }
        }

        public ulong Coin
        {
            get
            {
                return coin;
            }
        }



        public float Haste
        {
            get
            {
                return haste;
            }
        }


        public float Speed
        {
            get
            {
                return speed;
            }
        }


        public bool IsSpin
        {
            get
            {
                return isSpin;
            }
        }

        float direction = 1f;

        public bool IsLeftDirection()
        {
            if(direction < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRightDirection()
        {
            if (direction > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        void Awake()
        {
            
        }


        public void SetLoopMode()
        {
            isLoopMode = true;
        }

        public void InitPosition()
        {
            transform.rotation = new Quaternion();
        }


        public void SetSprite(int index)
        {
            if(index < 6)
            {
                GetComponent<UISprite>().atlas = atlasList[FidgetSpinnerData.fidgetSpinnerItems[index].atlasIndex];
                GetComponent<UISprite>().spriteName = FidgetSpinnerData.fidgetSpinnerItems[index].spriteName;
            }
            else
            {
                GetComponent<UISprite>().atlas = atlasList[FidgetSpinnerData.fidgetSpinnerItems[index].atlasIndex];
                GetComponent<UISprite>().spriteName = FidgetSpinnerData.fidgetSpinnerItems[index].spriteName;
            }

        }



        public void SetMaxSpeed(float speedRate)
        {
            maxSpeed = (8.99f * speedRate) + 100.0f;
        }

        public void SetDamping(float dampingRate)
        {
            damping += (2f- (dampingRate * 0.02f));
        }

        public void SetHaste(float hasteRate)
        {
            haste += (haste * 0.5f);
        }


        

        public void SetCoin(float coinRate)
        {
            coin = FidgetSpinnerData.coinBonusAmount[0];
            for (int i=0; i < FidgetSpinnerData.coinBonusLevel.Length; ++i)
            {
                if (coinRate < FidgetSpinnerData.coinBonusLevel[i])
                {
                    coin = FidgetSpinnerData.coinBonusAmount[i];
                    break;
                }
            }
        }

        public void SetCoinDelay(float coinRate)
        {
            if (coinRate < 10.0f)
            {
                coinDelay = 6.0f;
            }
            else if (coinRate < 20.0f)
            {
                coinDelay = 5.5f;
            }
            else if (coinRate < 30.0f)
            {
                coinDelay = 5.0f;
            }
            else if (coinRate < 40.0f)
            {
                coinDelay = 4.5f;
            }
            else if (coinRate < 50.0f)
            {
                coinDelay = 4.0f;
            }
            else if (coinRate < 60.0f)
            {
                coinDelay = 3.5f;
            }
            else if (coinRate < 70.0f)
            {
                coinDelay = 3.0f;
            }
            else if (coinRate < 80.0f)
            {
                coinDelay = 2.5f;
            }
            else if (coinRate < 90.0f)
            {
                coinDelay = 2.0f;
            }
            else
            {
                coinDelay = 1.5f;
            }
        }

        public void SetRightDirection()
        {
            direction = 1;
        }


        public void SetLeftDirection()
        {
            direction = -1;
        }

        public void SpeedUp(float fact)
        { 

            if (speed > maxSpeed)
                return;
            speed += fact;
        }

        public void SpeedDown(float fact)
        {
            if (speed - fact > 0)
            {
                speed -= fact;
            }
            else
            {
                OnSpinStop();
            }
        }



        public void IncreaseCoin()
        {
            User.Instance.Coin += coin;
        }

        public void AlmostStop()
        {
            speed = 30;
        }



        public void OnSpinStart()
        {
            isSpin = true;
        }

        public void OnSpinStop()
        {
            speed = 0;
            isSpin = false;
        }


        // Use this for initialization
        void Start()
        {

        }

        float GetSpeedMinus()
        {
            float result = 0.0f;
            if(speed < 100)
            {
                result = - 0.6f;
            }
            else if (speed < 200)
            {
                result = -0.9f;
            }
            else if(speed < 300)
            {
                result = -1.0f;
            }
            else if (speed < 400)
            {
                result = -1.2f;
            }
            else if(speed < 500)
            {
                result = -1.4f;
            }
            else if (speed < 600)
            {
                result = -1.6f;
            }
            else if (speed < 700)
            {
                result = -1.8f;
            }
            else
            {
                result = -2.0f;
            }


            if (speed > 100)
            {
                result -= damping;
            }
            if (result > 0.0f)
            {
                result = 0.0f;
            }
            return result;

        }


        float GetLoopSpeedMinus()
        {
            float result = 0.0f;
            if (speed < 100)
            {
                result = -0.05f;
            }
            else if (speed < 200)
            {
                result = -0.07f;
            }
            else if (speed < 300)
            {
                result = -0.09f;
            }
            else if (speed < 400)
            {
                result = -0.10f;
            }
            else if (speed < 500)
            {
                result = -0.12f;
            }
            else if (speed < 600)
            {
                result = -0.14f;
            }
            else if (speed < 700)
            {
                result = -0.16f;
            }
            else
            {
                result = -0.18f;
            }



            return result;

        }

        // Update is called once per frame
        void Update()
        {
            if (isSpin)
            {
                transform.Rotate(zAxis * Time.deltaTime * speed * direction, Space.Self);
                float t = 0.0f;
                if (!isLoopMode)
                {
                    t = GetSpeedMinus();
                }
                else
                {
                    t = GetLoopSpeedMinus();
                }
                speed += t;
                if (speed < 0.1f)
                {
                    OnSpinStop();
                }
                else if (speed > 60f)
                {
                    expUpdateTime += Time.deltaTime;
                    if(expUpdateTime > 0.1f)
                    {
                        User.Instance.Exp += (int)(speed*0.1f);
                        User.Instance.Score += (int)(speed);
                        expUpdateTime = 0.0f;

                    }
                }
            }
        }
    }
}