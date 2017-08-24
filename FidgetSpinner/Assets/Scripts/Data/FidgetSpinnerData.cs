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
        public int requireLevel;
        public ulong price;

        public FidgetSpinnerItem(string name, string spriteName, int atlasIndex, int requireLevel, ulong price)
        {
            this.name = name;
            this.spriteName = spriteName;
            this.atlasIndex = atlasIndex;
            this.requireLevel = requireLevel;
            this.price = price;
        }

    }

    public struct FidgetSpinnerDetail
    {

        public float speed;
        public float haste;
        public float damping;
        public float coin;
        public ulong upgrade;
       

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
        public static FidgetSpinnerItem[] fidgetSpinnerItems = {
            new FidgetSpinnerItem( "Gaxay", "spinner1", 0, 1, 0),
            new FidgetSpinnerItem( "Bat", "spinner2", 0, 5, 1000),
            new FidgetSpinnerItem( "Bat", "spinner3", 0, 10, 5000),
            new FidgetSpinnerItem( "Bat", "spinner4", 0, 20, 10000),
            new FidgetSpinnerItem( "Bat", "spinner5", 0, 30, 15000),
            new FidgetSpinnerItem( "Bat", "spinner6", 0, 40, 100000),
            new FidgetSpinnerItem( "Bat", "spinner7", 1, 50, 500000),
            new FidgetSpinnerItem( "Bat", "spinner8", 1, 60, 1000000),
            new FidgetSpinnerItem( "Bat", "spinner9", 1, 70, 5000000),
            new FidgetSpinnerItem( "Bat", "spinner10", 1, 80, 10000000),
            new FidgetSpinnerItem( "Bat", "spinner11", 1, 90, 30000000),
            new FidgetSpinnerItem( "Bat", "spinner12", 1, 100, 50000000),
        };


        public static ulong[] coinBonusAmount = {
            100,
            500,
           1000,
           1500,
           2000,
           2500,
           3000,
           3500,
           4000,
           4500,
        };

        public static float[] coinBonusLevel = {
            10f,
            20f,
            30f,
            40f,
            50f,
            60f,
            70f,
            80f,
            90f,
           101f,
        };


        public static FidgetSpinnerDetail[,] fidgetSpinnerDetails = {
            //1
            {
                new FidgetSpinnerDetail( 1f, 1f, 1f, 1f, 100),
                new FidgetSpinnerDetail( 2f, 5f, 5f, 5f, 200),
                new FidgetSpinnerDetail( 4f, 10f, 10f, 10f, 300),
                new FidgetSpinnerDetail( 6f, 15f, 15f, 15f, 400),
                new FidgetSpinnerDetail( 8f, 20f, 20f, 20f, 500),
                new FidgetSpinnerDetail( 9f, 25f, 25f, 25f, 600),
                new FidgetSpinnerDetail( 13f, 30f, 30f, 30f, 700),
                new FidgetSpinnerDetail( 15f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 17f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 18f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 20f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 22f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 24f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 26f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 27f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 29f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 31f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 33f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 35f, 35f, 35f, 35f, 800),
                new FidgetSpinnerDetail( 35f, 40f, 30f, 30f, 20000),
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
            //11
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
            //12
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