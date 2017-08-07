using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Data
{
    public struct FidgetSpinnerItem
    {
        public string name;
        public string spriteName;
        public int atlasIndex;

        public FidgetSpinnerItem(string name, string spriteName, int atlasIndex)
        {
            this.name = name;
            this.spriteName = spriteName;
            this.atlasIndex = atlasIndex;
        }

    }

    public struct FidgetSpinnerDetail
    {

        float speed;
        float haste;
        float damping;
        float coin;
        ulong upgrade;
        public FidgetSpinnerDetail(float speed, float haste, float damping, float coin, ulong upgrade)
        {
            this.speed = speed;
            this.haste = haste;
            this.damping = damping;
            this.coin = coin;
            this.upgrade = upgrade;
        }
    }


    public class FidgetSpinnerData
    {
        public static FidgetSpinnerItem[]  fidgetSpinnerItems = {
            new FidgetSpinnerItem( "Gaxay", "1", 0),
            new FidgetSpinnerItem( "Bat", "2", 0),
            new FidgetSpinnerItem( "Bat", "3", 0),
            new FidgetSpinnerItem( "Bat", "4", 0),
            new FidgetSpinnerItem( "Bat", "5", 0),
            new FidgetSpinnerItem( "Bat", "6", 0),
            new FidgetSpinnerItem( "Bat", "7", 1),
            new FidgetSpinnerItem( "Bat", "8", 1),
            new FidgetSpinnerItem( "Bat", "9", 1),
            new FidgetSpinnerItem( "Bat", "10", 1),
        };


        public static FidgetSpinnerDetail[,] fidgetSpinnerDetails = {
            //1
            {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 5f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 10f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 15f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 20f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 25f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 30f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 40f, 40f, 40f, 40f, 900),
                new FidgetSpinnerDetail( 45f, 45f, 45f, 45f, 1000),
                new FidgetSpinnerDetail( 50f, 50f, 50f, 50f, 1100),
                new FidgetSpinnerDetail( 55f, 55f, 55f, 55f, 1200),
                new FidgetSpinnerDetail( 60f, 60f, 60f, 60f, 1300),
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 1400),
                new FidgetSpinnerDetail( 70f, 70f, 70f, 70f, 1500),
                new FidgetSpinnerDetail( 75f, 75f, 75f, 75f, 1600),
                new FidgetSpinnerDetail( 80f, 80f, 80f, 80f, 1700),
                new FidgetSpinnerDetail( 85f, 85f, 85f, 85f, 1800),
                new FidgetSpinnerDetail( 90f, 90f, 90f, 90f, 1900),
                new FidgetSpinnerDetail( 95f, 95f, 95f, 95f, 2000),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 20000),
            },
            //2
         {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 5f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 10f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 15f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 20f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 25f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 30f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 40f, 40f, 40f, 40f, 900),
                new FidgetSpinnerDetail( 45f, 45f, 45f, 45f, 1000),
                new FidgetSpinnerDetail( 50f, 50f, 50f, 50f, 1100),
                new FidgetSpinnerDetail( 55f, 55f, 55f, 55f, 1200),
                new FidgetSpinnerDetail( 60f, 60f, 60f, 60f, 1300),
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 1400),
                new FidgetSpinnerDetail( 70f, 70f, 70f, 70f, 1500),
                new FidgetSpinnerDetail( 75f, 75f, 75f, 75f, 1600),
                new FidgetSpinnerDetail( 80f, 80f, 80f, 80f, 1700),
                new FidgetSpinnerDetail( 85f, 85f, 85f, 85f, 1800),
                new FidgetSpinnerDetail( 90f, 90f, 90f, 90f, 1900),
                new FidgetSpinnerDetail( 95f, 95f, 95f, 95f, 2000),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 20000),
            },
           //3
         {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 5f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 10f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 15f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 20f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 25f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 30f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 40f, 40f, 40f, 40f, 900),
                new FidgetSpinnerDetail( 45f, 45f, 45f, 45f, 1000),
                new FidgetSpinnerDetail( 50f, 50f, 50f, 50f, 1100),
                new FidgetSpinnerDetail( 55f, 55f, 55f, 55f, 1200),
                new FidgetSpinnerDetail( 60f, 60f, 60f, 60f, 1300),
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 1400),
                new FidgetSpinnerDetail( 70f, 70f, 70f, 70f, 1500),
                new FidgetSpinnerDetail( 75f, 75f, 75f, 75f, 1600),
                new FidgetSpinnerDetail( 80f, 80f, 80f, 80f, 1700),
                new FidgetSpinnerDetail( 85f, 85f, 85f, 85f, 1800),
                new FidgetSpinnerDetail( 90f, 90f, 90f, 90f, 1900),
                new FidgetSpinnerDetail( 95f, 95f, 95f, 95f, 2000),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 20000),
            },
           //4
         {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 5f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 10f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 15f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 20f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 25f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 30f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 40f, 40f, 40f, 40f, 900),
                new FidgetSpinnerDetail( 45f, 45f, 45f, 45f, 1000),
                new FidgetSpinnerDetail( 50f, 50f, 50f, 50f, 1100),
                new FidgetSpinnerDetail( 55f, 55f, 55f, 55f, 1200),
                new FidgetSpinnerDetail( 60f, 60f, 60f, 60f, 1300),
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 1400),
                new FidgetSpinnerDetail( 70f, 70f, 70f, 70f, 1500),
                new FidgetSpinnerDetail( 75f, 75f, 75f, 75f, 1600),
                new FidgetSpinnerDetail( 80f, 80f, 80f, 80f, 1700),
                new FidgetSpinnerDetail( 85f, 85f, 85f, 85f, 1800),
                new FidgetSpinnerDetail( 90f, 90f, 90f, 90f, 1900),
                new FidgetSpinnerDetail( 95f, 95f, 95f, 95f, 2000),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 20000),
            },
           //5
         {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 5f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 10f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 15f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 20f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 25f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 30f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 40f, 40f, 40f, 40f, 900),
                new FidgetSpinnerDetail( 45f, 45f, 45f, 45f, 1000),
                new FidgetSpinnerDetail( 50f, 50f, 50f, 50f, 1100),
                new FidgetSpinnerDetail( 55f, 55f, 55f, 55f, 1200),
                new FidgetSpinnerDetail( 60f, 60f, 60f, 60f, 1300),
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 1400),
                new FidgetSpinnerDetail( 70f, 70f, 70f, 70f, 1500),
                new FidgetSpinnerDetail( 75f, 75f, 75f, 75f, 1600),
                new FidgetSpinnerDetail( 80f, 80f, 80f, 80f, 1700),
                new FidgetSpinnerDetail( 85f, 85f, 85f, 85f, 1800),
                new FidgetSpinnerDetail( 90f, 90f, 90f, 90f, 1900),
                new FidgetSpinnerDetail( 95f, 95f, 95f, 95f, 2000),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 20000),
            },
           //6
         {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 5f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 10f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 15f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 20f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 25f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 30f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 40f, 40f, 40f, 40f, 900),
                new FidgetSpinnerDetail( 45f, 45f, 45f, 45f, 1000),
                new FidgetSpinnerDetail( 50f, 50f, 50f, 50f, 1100),
                new FidgetSpinnerDetail( 55f, 55f, 55f, 55f, 1200),
                new FidgetSpinnerDetail( 60f, 60f, 60f, 60f, 1300),
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 1400),
                new FidgetSpinnerDetail( 70f, 70f, 70f, 70f, 1500),
                new FidgetSpinnerDetail( 75f, 75f, 75f, 75f, 1600),
                new FidgetSpinnerDetail( 80f, 80f, 80f, 80f, 1700),
                new FidgetSpinnerDetail( 85f, 85f, 85f, 85f, 1800),
                new FidgetSpinnerDetail( 90f, 90f, 90f, 90f, 1900),
                new FidgetSpinnerDetail( 95f, 95f, 95f, 95f, 2000),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 20000),
            },
            //7
         {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 5f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 10f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 15f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 20f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 25f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 30f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 40f, 40f, 40f, 40f, 900),
                new FidgetSpinnerDetail( 45f, 45f, 45f, 45f, 1000),
                new FidgetSpinnerDetail( 50f, 50f, 50f, 50f, 1100),
                new FidgetSpinnerDetail( 55f, 55f, 55f, 55f, 1200),
                new FidgetSpinnerDetail( 60f, 60f, 60f, 60f, 1300),
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 1400),
                new FidgetSpinnerDetail( 70f, 70f, 70f, 70f, 1500),
                new FidgetSpinnerDetail( 75f, 75f, 75f, 75f, 1600),
                new FidgetSpinnerDetail( 80f, 80f, 80f, 80f, 1700),
                new FidgetSpinnerDetail( 85f, 85f, 85f, 85f, 1800),
                new FidgetSpinnerDetail( 90f, 90f, 90f, 90f, 1900),
                new FidgetSpinnerDetail( 95f, 95f, 95f, 95f, 2000),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 20000),
            },
            //8
         {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 5f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 10f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 15f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 20f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 25f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 30f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 40f, 40f, 40f, 40f, 900),
                new FidgetSpinnerDetail( 45f, 45f, 45f, 45f, 1000),
                new FidgetSpinnerDetail( 50f, 50f, 50f, 50f, 1100),
                new FidgetSpinnerDetail( 55f, 55f, 55f, 55f, 1200),
                new FidgetSpinnerDetail( 60f, 60f, 60f, 60f, 1300),
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 1400),
                new FidgetSpinnerDetail( 70f, 70f, 70f, 70f, 1500),
                new FidgetSpinnerDetail( 75f, 75f, 75f, 75f, 1600),
                new FidgetSpinnerDetail( 80f, 80f, 80f, 80f, 1700),
                new FidgetSpinnerDetail( 85f, 85f, 85f, 85f, 1800),
                new FidgetSpinnerDetail( 90f, 90f, 90f, 90f, 1900),
                new FidgetSpinnerDetail( 95f, 95f, 95f, 95f, 2000),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 20000),
            },
            //9
         {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 5f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 10f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 15f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 20f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 25f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 30f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 40f, 40f, 40f, 40f, 900),
                new FidgetSpinnerDetail( 45f, 45f, 45f, 45f, 1000),
                new FidgetSpinnerDetail( 50f, 50f, 50f, 50f, 1100),
                new FidgetSpinnerDetail( 55f, 55f, 55f, 55f, 1200),
                new FidgetSpinnerDetail( 60f, 60f, 60f, 60f, 1300),
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 1400),
                new FidgetSpinnerDetail( 70f, 70f, 70f, 70f, 1500),
                new FidgetSpinnerDetail( 75f, 75f, 75f, 75f, 1600),
                new FidgetSpinnerDetail( 80f, 80f, 80f, 80f, 1700),
                new FidgetSpinnerDetail( 85f, 85f, 85f, 85f, 1800),
                new FidgetSpinnerDetail( 90f, 90f, 90f, 90f, 1900),
                new FidgetSpinnerDetail( 95f, 95f, 95f, 95f, 2000),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 20000),
            },
            //10
         {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 5f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 10f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 15f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 20f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 25f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 30f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 40f, 40f, 40f, 40f, 900),
                new FidgetSpinnerDetail( 45f, 45f, 45f, 45f, 1000),
                new FidgetSpinnerDetail( 50f, 50f, 50f, 50f, 1100),
                new FidgetSpinnerDetail( 55f, 55f, 55f, 55f, 1200),
                new FidgetSpinnerDetail( 60f, 60f, 60f, 60f, 1300),
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 1400),
                new FidgetSpinnerDetail( 70f, 70f, 70f, 70f, 1500),
                new FidgetSpinnerDetail( 75f, 75f, 75f, 75f, 1600),
                new FidgetSpinnerDetail( 80f, 80f, 80f, 80f, 1700),
                new FidgetSpinnerDetail( 85f, 85f, 85f, 85f, 1800),
                new FidgetSpinnerDetail( 90f, 90f, 90f, 90f, 1900),
                new FidgetSpinnerDetail( 95f, 95f, 95f, 95f, 2000),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 20000),
            },
        };


        public static FidgetSpinnerDetail GetFidgetSpinnerDetail(int fidgetIndex, int level)
        {
            try
            {
                return fidgetSpinnerDetails[fidgetIndex, level];
            }
            catch
            {
                return fidgetSpinnerDetails[0, 0];
            }
        }

        public static FidgetSpinnerItem GetFidgetSpinnerItem(int index)
        {
            return fidgetSpinnerItems[index];
        }

    }
}