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
            new FidgetSpinnerItem( "Galaxy", "spinner1", 0, 1, 0),
            new FidgetSpinnerItem( "Bat", "spinner2", 0, 5, 10000),
            new FidgetSpinnerItem( "Andromeda", "spinner3", 0, 10, 50000),
            new FidgetSpinnerItem( "Flower", "spinner4", 0, 20, 100000),
            new FidgetSpinnerItem( "Dagger", "spinner5", 0, 30, 150000),
            new FidgetSpinnerItem( "AngelWing", "spinner6", 0, 40, 1000000),
            new FidgetSpinnerItem( "Gold", "spinner7", 1, 50, 5000000),
            new FidgetSpinnerItem( "Crystal", "spinner8", 1, 60, 10000000),
            new FidgetSpinnerItem( "Ocean", "spinner9", 1, 70, 50000000),
            new FidgetSpinnerItem( "Volcano", "spinner10", 1, 80, 70000000),
            new FidgetSpinnerItem( "Redvelvet", "spinner11", 1, 90, 80000000),
            new FidgetSpinnerItem( "DarthVader", "spinner12", 1, 100, 99000000),
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
                new FidgetSpinnerDetail(1f,1f,1f,1f,100),
                new FidgetSpinnerDetail(2.79f,3.05f,2.52f,2.52f,200),
                new FidgetSpinnerDetail(4.58f,5.1f,4.04f,4.04f,300),
                new FidgetSpinnerDetail(6.37f,7.15f,5.56f,5.56f,400),
                new FidgetSpinnerDetail(8.16f,9.2f,7.08f,7.08f,500),
                new FidgetSpinnerDetail(9.95f,11.25f,8.6f,8.6f,600),
                new FidgetSpinnerDetail(11.74f,13.3f,10.12f,10.12f,700),
                new FidgetSpinnerDetail(13.53f,15.35f,11.64f,11.64f,800),
                new FidgetSpinnerDetail(15.32f,17.4f,13.16f,13.16f,900),
                new FidgetSpinnerDetail(17.11f,19.45f,14.68f,14.68f,1000),
                new FidgetSpinnerDetail(18.9f,21.5f,16.2f,16.2f,1100),
                new FidgetSpinnerDetail(20.69f,23.55f,17.72f,17.72f,1200),
                new FidgetSpinnerDetail(22.48f,25.6f,19.24f,19.24f,1300),
                new FidgetSpinnerDetail(24.27f,27.65f,20.76f,20.76f,1400),
                new FidgetSpinnerDetail(26.06f,29.7f,22.28f,22.28f,1500),
                new FidgetSpinnerDetail(27.85f,31.75f,23.8f,23.8f,1600),
                new FidgetSpinnerDetail(29.64f,33.8f,25.32f,25.32f,1700),
                new FidgetSpinnerDetail(31.43f,35.85f,26.84f,26.84f,1800),
                new FidgetSpinnerDetail(33.22f,37.9f,28.36f,28.36f,1900),
                new FidgetSpinnerDetail(35f,40f,30f,30f,2000),
            },
            //2
            {
                new FidgetSpinnerDetail(8f,15f,1f,5f,100),
                new FidgetSpinnerDetail(9.68f,17.2f,2f,6.73f,200),
                new FidgetSpinnerDetail(11.36f,19.4f,3f,8.46f,300),
                new FidgetSpinnerDetail(13.04f,21.6f,4f,10.19f,400),
                new FidgetSpinnerDetail(14.72f,23.8f,5f,11.92f,500),
                new FidgetSpinnerDetail(16.4f,26f,6f,13.65f,600),
                new FidgetSpinnerDetail(18.08f,28.2f,7f,15.38f,700),
                new FidgetSpinnerDetail(19.76f,30.4f,8f,17.11f,800),
                new FidgetSpinnerDetail(21.44f,32.6f,9f,18.84f,900),
                new FidgetSpinnerDetail(23.12f,34.8f,10f,20.57f,1000),
                new FidgetSpinnerDetail(24.8f,37f,11f,22.3f,1100),
                new FidgetSpinnerDetail(26.48f,39.2f,12f,24.03f,1200),
                new FidgetSpinnerDetail(28.16f,41.4f,13f,25.76f,1300),
                new FidgetSpinnerDetail(29.84f,43.6f,14f,27.49f,1400),
                new FidgetSpinnerDetail(31.52f,45.8f,15f,29.22f,1500),
                new FidgetSpinnerDetail(33.2f,48f,16f,30.95f,1600),
                new FidgetSpinnerDetail(34.88f,50.2f,17f,32.68f,1700),
                new FidgetSpinnerDetail(36.56f,52.4f,18f,34.41f,1800),
                new FidgetSpinnerDetail(38.24f,54.6f,19f,36.14f,1900),
                new FidgetSpinnerDetail(40f,55f,20f,38f,2000),
            },
           //3
            {
                new FidgetSpinnerDetail(15f,10f,16f,14f,100),
                new FidgetSpinnerDetail(16.58f,11.9f,17.73f,15.37f,200),
                new FidgetSpinnerDetail(18.16f,13.8f,19.46f,16.74f,300),
                new FidgetSpinnerDetail(19.74f,15.7f,21.19f,18.11f,400),
                new FidgetSpinnerDetail(21.32f,17.6f,22.92f,19.48f,500),
                new FidgetSpinnerDetail(22.9f,19.5f,24.65f,20.85f,600),
                new FidgetSpinnerDetail(24.48f,21.4f,26.38f,22.22f,700),
                new FidgetSpinnerDetail(26.06f,23.3f,28.11f,23.59f,800),
                new FidgetSpinnerDetail(27.64f,25.2f,29.84f,24.96f,900),
                new FidgetSpinnerDetail(29.22f,27.1f,31.57f,26.33f,1000),
                new FidgetSpinnerDetail(30.8f,29f,33.3f,27.7f,1100),
                new FidgetSpinnerDetail(32.38f,30.9f,35.03f,29.07f,1200),
                new FidgetSpinnerDetail(33.96f,32.8f,36.76f,30.44f,1300),
                new FidgetSpinnerDetail(35.54f,34.7f,38.49f,31.81f,1400),
                new FidgetSpinnerDetail(37.12f,36.6f,40.22f,33.18f,1500),
                new FidgetSpinnerDetail(38.7f,38.5f,41.95f,34.55f,1600),
                new FidgetSpinnerDetail(40.28f,40.4f,43.68f,35.92f,1700),
                new FidgetSpinnerDetail(41.86f,42.3f,45.41f,37.29f,1800),
                new FidgetSpinnerDetail(43.44f,44.2f,47.14f,38.66f,1900),
                new FidgetSpinnerDetail(45f,46f,49f,40f,2000),
            },
           //4
            {
                new FidgetSpinnerDetail(20f,24f,11f,26f,100),
                new FidgetSpinnerDetail(21.63f,25.52f,12.52f,27.63f,200),
                new FidgetSpinnerDetail(23.26f,27.04f,14.04f,29.26f,300),
                new FidgetSpinnerDetail(24.89f,28.56f,15.56f,30.89f,400),
                new FidgetSpinnerDetail(26.52f,30.08f,17.08f,32.52f,500),
                new FidgetSpinnerDetail(28.15f,31.6f,18.6f,34.15f,600),
                new FidgetSpinnerDetail(29.78f,33.12f,20.12f,35.78f,700),
                new FidgetSpinnerDetail(31.41f,34.64f,21.64f,37.41f,800),
                new FidgetSpinnerDetail(33.04f,36.16f,23.16f,39.04f,900),
                new FidgetSpinnerDetail(34.67f,37.68f,24.68f,40.67f,1000),
                new FidgetSpinnerDetail(36.3f,39.2f,26.2f,42.3f,1100),
                new FidgetSpinnerDetail(37.93f,40.72f,27.72f,43.93f,1200),
                new FidgetSpinnerDetail(39.56f,42.24f,29.24f,45.56f,1300),
                new FidgetSpinnerDetail(41.19f,43.76f,30.76f,47.19f,1400),
                new FidgetSpinnerDetail(42.82f,45.28f,32.28f,48.82f,1500),
                new FidgetSpinnerDetail(44.45f,46.8f,33.8f,50.45f,1600),
                new FidgetSpinnerDetail(46.08f,48.32f,35.32f,52.08f,1700),
                new FidgetSpinnerDetail(47.71f,49.84f,36.84f,53.71f,1800),
                new FidgetSpinnerDetail(49.34f,51.36f,38.36f,55.34f,1900),
                new FidgetSpinnerDetail(51f,53f,40f,57f,2000),
            },
           //5
            {
                new FidgetSpinnerDetail(26f,30f,10f,36f,100),
                new FidgetSpinnerDetail(27.63f,31.68f,11.47f,37.52f,200),
                new FidgetSpinnerDetail(29.26f,33.36f,12.94f,39.04f,300),
                new FidgetSpinnerDetail(30.89f,35.04f,14.41f,40.56f,400),
                new FidgetSpinnerDetail(32.52f,36.72f,15.88f,42.08f,500),
                new FidgetSpinnerDetail(34.15f,38.4f,17.35f,43.6f,600),
                new FidgetSpinnerDetail(35.78f,40.08f,18.82f,45.12f,700),
                new FidgetSpinnerDetail(37.41f,41.76f,20.29f,46.64f,800),
                new FidgetSpinnerDetail(39.04f,43.44f,21.76f,48.16f,900),
                new FidgetSpinnerDetail(40.67f,45.12f,23.23f,49.68f,1000),
                new FidgetSpinnerDetail(42.3f,46.8f,24.7f,51.2f,1100),
                new FidgetSpinnerDetail(43.93f,48.48f,26.17f,52.72f,1200),
                new FidgetSpinnerDetail(45.56f,50.16f,27.64f,54.24f,1300),
                new FidgetSpinnerDetail(47.19f,51.84f,29.11f,55.76f,1400),
                new FidgetSpinnerDetail(48.82f,53.52f,30.58f,57.28f,1500),
                new FidgetSpinnerDetail(50.45f,55.2f,32.05f,58.8f,1600),
                new FidgetSpinnerDetail(52.08f,56.88f,33.52f,60.32f,1700),
                new FidgetSpinnerDetail(53.71f,58.56f,34.99f,61.84f,1800),
                new FidgetSpinnerDetail(55.34f,60.24f,36.46f,63.36f,1900),
                new FidgetSpinnerDetail(57f,62f,38f,65f,2000),
            },
           //6
            {
                new FidgetSpinnerDetail(31f,23f,41f,30f,100),
                new FidgetSpinnerDetail(32.68f,24.47f,42.47f,31.52f,200),
                new FidgetSpinnerDetail(34.36f,25.94f,43.94f,33.04f,300),
                new FidgetSpinnerDetail(36.04f,27.41f,45.41f,34.56f,400),
                new FidgetSpinnerDetail(37.72f,28.88f,46.88f,36.08f,500),
                new FidgetSpinnerDetail(39.4f,30.35f,48.35f,37.6f,600),
                new FidgetSpinnerDetail(41.08f,31.82f,49.82f,39.12f,700),
                new FidgetSpinnerDetail(42.76f,33.29f,51.29f,40.64f,800),
                new FidgetSpinnerDetail(44.44f,34.76f,52.76f,42.16f,900),
                new FidgetSpinnerDetail(46.12f,36.23f,54.23f,43.68f,1000),
                new FidgetSpinnerDetail(47.8f,37.7f,55.7f,45.2f,1100),
                new FidgetSpinnerDetail(49.48f,39.17f,57.17f,46.72f,1200),
                new FidgetSpinnerDetail(51.16f,40.64f,58.64f,48.24f,1300),
                new FidgetSpinnerDetail(52.84f,42.11f,60.11f,49.76f,1400),
                new FidgetSpinnerDetail(54.52f,43.58f,61.58f,51.28f,1500),
                new FidgetSpinnerDetail(56.2f,45.05f,63.05f,52.8f,1600),
                new FidgetSpinnerDetail(57.88f,46.52f,64.52f,54.32f,1700),
                new FidgetSpinnerDetail(59.56f,47.99f,65.99f,55.84f,1800),
                new FidgetSpinnerDetail(61.24f,49.46f,67.46f,57.36f,1900),
                new FidgetSpinnerDetail(63f,51f,69f,59f,2000),
            },
            //7
            {
                new FidgetSpinnerDetail(37f,38f,35f,40f,100),
                new FidgetSpinnerDetail(38.73f,39.68f,36.58f,41.31f,200),
                new FidgetSpinnerDetail(40.46f,41.36f,38.16f,42.62f,300),
                new FidgetSpinnerDetail(42.19f,43.04f,39.74f,43.93f,400),
                new FidgetSpinnerDetail(43.92f,44.72f,41.32f,45.24f,500),
                new FidgetSpinnerDetail(45.65f,46.4f,42.9f,46.55f,600),
                new FidgetSpinnerDetail(47.38f,48.08f,44.48f,47.86f,700),
                new FidgetSpinnerDetail(49.11f,49.76f,46.06f,49.17f,800),
                new FidgetSpinnerDetail(50.84f,51.44f,47.64f,50.48f,900),
                new FidgetSpinnerDetail(52.57f,53.12f,49.22f,51.79f,1000),
                new FidgetSpinnerDetail(54.3f,54.8f,50.8f,53.1f,1100),
                new FidgetSpinnerDetail(56.03f,56.48f,52.38f,54.41f,1200),
                new FidgetSpinnerDetail(57.76f,58.16f,53.96f,55.72f,1300),
                new FidgetSpinnerDetail(59.49f,59.84f,55.54f,57.03f,1400),
                new FidgetSpinnerDetail(61.22f,61.52f,57.12f,58.34f,1500),
                new FidgetSpinnerDetail(62.95f,63.2f,58.7f,59.65f,1600),
                new FidgetSpinnerDetail(64.67f,64.88f,60.28f,60.96f,1700),
                new FidgetSpinnerDetail(66.40f,66.56f,61.86f,62.27f,1800),
                new FidgetSpinnerDetail(68.13f,68.24f,63.44f,63.58f,1900),
                new FidgetSpinnerDetail(70f,70f,65f,65f,2000),
            },
            //8
            {
                new FidgetSpinnerDetail(43f,30f,34f,68f,100),
                new FidgetSpinnerDetail(44.68f,31.52f,35.1f,69.68f,200),
                new FidgetSpinnerDetail(46.36f,33.04f,36.2f,71.36f,300),
                new FidgetSpinnerDetail(48.04f,34.56f,37.3f,73.04f,400),
                new FidgetSpinnerDetail(49.72f,36.08f,38.4f,74.72f,500),
                new FidgetSpinnerDetail(51.4f,37.6f,39.5f,76.4f,600),
                new FidgetSpinnerDetail(53.08f,39.12f,40.6f,78.08f,700),
                new FidgetSpinnerDetail(54.76f,40.64f,41.7f,79.76f,800),
                new FidgetSpinnerDetail(56.44f,42.16f,42.8f,81.44f,900),
                new FidgetSpinnerDetail(58.12f,43.68f,43.9f,83.12f,1000),
                new FidgetSpinnerDetail(59.8f,45.2f,45f,84.8f,1100),
                new FidgetSpinnerDetail(61.48f,46.72f,46.1f,86.48f,1200),
                new FidgetSpinnerDetail(63.16f,48.24f,47.2f,88.16f,1300),
                new FidgetSpinnerDetail(64.84f,49.76f,48.3f,89.84f,1400),
                new FidgetSpinnerDetail(66.52f,51.28f,49.4f,91.52f,1500),
                new FidgetSpinnerDetail(68.2f,52.8f,50.5f,93.2f,1600),
                new FidgetSpinnerDetail(69.88f,54.32f,51.6f,94.88f,1700),
                new FidgetSpinnerDetail(71.56f,55.84f,52.7f,96.56f,1800),
                new FidgetSpinnerDetail(73.24f,57.36f,53.8f,98.24f,1900),
                new FidgetSpinnerDetail(75f,59f,55f,100f,2000),
            },
            //9
            {
                new FidgetSpinnerDetail(48f,53f,42f,50f,100),
                new FidgetSpinnerDetail(49.73f,54.68f,43.73f,51.52f,200),
                new FidgetSpinnerDetail(51.46f,56.36f,45.46f,53.04f,300),
                new FidgetSpinnerDetail(53.19f,58.04f,47.19f,54.56f,400),
                new FidgetSpinnerDetail(54.92f,59.72f,48.92f,56.08f,500),
                new FidgetSpinnerDetail(56.65f,61.4f,50.65f,57.6f,600),
                new FidgetSpinnerDetail(58.38f,63.08f,52.38f,59.12f,700),
                new FidgetSpinnerDetail(60.11f,64.76f,54.11f,60.64f,800),
                new FidgetSpinnerDetail(61.84f,66.44f,55.84f,62.16f,900),
                new FidgetSpinnerDetail(63.57f,68.12f,57.57f,63.68f,1000),
                new FidgetSpinnerDetail(65.3f,69.8f,59.3f,65.2f,1100),
                new FidgetSpinnerDetail(67.03f,71.48f,61.03f,66.72f,1200),
                new FidgetSpinnerDetail(68.76f,73.16f,62.76f,68.24f,1300),
                new FidgetSpinnerDetail(70.49f,74.84f,64.49f,69.76f,1400),
                new FidgetSpinnerDetail(72.22f,76.52f,66.22f,71.28f,1500),
                new FidgetSpinnerDetail(73.95f,78.2f,67.95f,72.8f,1600),
                new FidgetSpinnerDetail(75.67f,79.88f,69.67f,74.32f,1700),
                new FidgetSpinnerDetail(77.4f,81.56f,71.4f,75.84f,1800),
                new FidgetSpinnerDetail(79.13f,83.24f,73.13f,77.36f,1900),
                new FidgetSpinnerDetail(81f,85f,75f,79f,2000),
            },
            //10
            {
                new FidgetSpinnerDetail( 53f, 47f, 59f, 60f, 100),
                new FidgetSpinnerDetail( 54.84f, 48.36f, 60.78f, 61.84f, 200),
                new FidgetSpinnerDetail( 56.68f, 49.72f, 62.56f, 63.68f, 300),
                new FidgetSpinnerDetail( 58.52f, 51.08f, 64.34f, 65.52f, 400),
                new FidgetSpinnerDetail( 60.36f, 52.44f, 66.12f, 67.36f, 500),
                new FidgetSpinnerDetail( 62.2f, 53.8f, 67.9f, 69.2f, 600),
                new FidgetSpinnerDetail( 64.04f, 55.16f, 69.68f, 71.04f, 700),
                new FidgetSpinnerDetail( 65.88f, 56.52f, 71.46f, 72.88f, 800),
                new FidgetSpinnerDetail( 67.72f, 57.88f, 73.24f, 74.72f, 900),
                new FidgetSpinnerDetail( 69.56f, 59.24f, 75.02f, 76.56f, 1000),
                new FidgetSpinnerDetail( 71.4f, 60.6f, 76.8f, 78.4f, 1100),
                new FidgetSpinnerDetail( 73.24f, 61.96f, 78.58f, 80.24f, 1200),
                new FidgetSpinnerDetail( 75.08f, 63.32f, 80.36f, 82.08f, 1300),
                new FidgetSpinnerDetail( 76.92f, 64.68f, 82.14f, 83.92f, 1400),
                new FidgetSpinnerDetail( 78.76f, 66.04f, 83.92f, 85.76f, 1500),
                new FidgetSpinnerDetail( 80.60f, 67.4f, 85.7f, 87.60f, 1600),
                new FidgetSpinnerDetail( 82.44f, 68.76f, 87.48f, 89.44f, 1700),
                new FidgetSpinnerDetail( 84.28f, 70.12f, 89.26f, 91.28f, 1800),
                new FidgetSpinnerDetail( 86.12f, 71.48f, 91.04f, 93.12f, 1900),
                new FidgetSpinnerDetail( 88f, 73f, 93f, 95f, 2000),
            },
            //11
            {
                new FidgetSpinnerDetail( 58f, 52f, 63f, 60f, 100),
                new FidgetSpinnerDetail( 59.89f, 53.63f, 64.68f, 61.73f, 200),
                new FidgetSpinnerDetail( 61.78f, 55.26f, 66.36f, 63.46f, 300),
                new FidgetSpinnerDetail( 63.67f, 56.89f, 68.04f, 65.19f, 400),
                new FidgetSpinnerDetail( 65.56f, 58.52f, 69.72f, 66.92f, 500),
                new FidgetSpinnerDetail( 67.45f, 60.15f, 71.4f, 68.65f, 600),
                new FidgetSpinnerDetail( 69.34f, 61.78f, 73.08f, 70.38f, 700),
                new FidgetSpinnerDetail( 71.23f, 63.41f, 74.76f, 72.11f, 800),
                new FidgetSpinnerDetail( 73.12f, 65.04f, 76.44f, 73.84f, 900),
                new FidgetSpinnerDetail( 75.01f, 66.67f, 78.12f, 75.57f, 1000),
                new FidgetSpinnerDetail( 76.9f, 68.3f, 79.8f, 77.3f, 1100),
                new FidgetSpinnerDetail( 78.79f, 69.93f, 81.48f, 79.03f, 1200),
                new FidgetSpinnerDetail( 80.68f, 71.56f, 83.16f, 80.76f, 1300),
                new FidgetSpinnerDetail( 82.57f, 73.19f, 84.84f, 82.49f, 1400),
                new FidgetSpinnerDetail( 84.46f, 74.82f, 86.52f, 84.22f, 1500),
                new FidgetSpinnerDetail( 86.35f, 76.45f, 88.2f, 85.95f, 1600),
                new FidgetSpinnerDetail( 88.24f, 78.08f, 89.88f, 87.67f, 1700),
                new FidgetSpinnerDetail( 90.13f, 79.71f, 91.56f, 89.4f, 1800),
                new FidgetSpinnerDetail( 92.02f, 81.34f, 93.24f, 91.13f, 1900),
                new FidgetSpinnerDetail( 94f, 83f, 95f, 93f, 2000),
            },
            //12
            {
                new FidgetSpinnerDetail( 65f, 65f, 65f, 65f, 100),
                new FidgetSpinnerDetail( 66.84f, 66.84f, 66.84f, 66.84f, 200),
                new FidgetSpinnerDetail( 68.68f, 68.68f, 68.68f, 68.68f, 300),
                new FidgetSpinnerDetail( 70.52f, 70.52f, 70.52f, 70.52f, 400),
                new FidgetSpinnerDetail( 72.36f, 72.36f, 72.36f, 72.36f, 500),
                new FidgetSpinnerDetail( 74.2f, 74.2f, 74.2f, 74.2f, 600),
                new FidgetSpinnerDetail( 76.04f, 76.04f, 76.04f, 76.04f, 700),
                new FidgetSpinnerDetail( 77.88f, 77.88f, 77.88f, 77.88f, 800),
                new FidgetSpinnerDetail( 79.72f, 79.72f, 79.72f, 79.72f, 900),
                new FidgetSpinnerDetail( 81.56f, 81.56f, 81.56f, 81.56f, 1000),
                new FidgetSpinnerDetail( 83.4f, 83.4f, 83.4f, 83.4f, 1100),
                new FidgetSpinnerDetail( 85.24f, 85.24f, 85.24f, 85.24f, 1200),
                new FidgetSpinnerDetail( 87.08f, 87.08f, 87.08f, 87.08f, 1300),
                new FidgetSpinnerDetail( 88.92f, 88.92f, 88.92f, 88.92f, 1400),
                new FidgetSpinnerDetail( 90.76f, 90.76f, 90.76f, 90.76f, 1500),
                new FidgetSpinnerDetail( 92.60f, 92.60f, 92.60f, 92.60f, 1600),
                new FidgetSpinnerDetail( 94.44f, 94.44f, 94.44f, 94.44f, 1700),
                new FidgetSpinnerDetail( 96.28f, 96.28f, 96.28f, 96.28f, 1800),
                new FidgetSpinnerDetail( 98.12f, 98.12f, 98.12f, 98.12f, 1900),
                new FidgetSpinnerDetail( 100f, 100f, 100f, 100f, 2000),
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