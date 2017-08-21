using UnityEngine;
using System.Collections;
using System;
namespace Fidget.Common
{
    public class GameData
    {

        TimeSpan playingTime;
        DateTime loadedTime;
        string data;
        string state;

        static readonly string HEADER = "GDv1";



        public GameData(string _initData)
        {

            data = _initData;
            state = "Initialized, modified";
            playingTime = new TimeSpan();
            loadedTime = DateTime.Now;

        }

        public TimeSpan TotalPlayingTime
        {
            get
            {
                TimeSpan delta = DateTime.Now.Subtract(loadedTime);
                return playingTime.Add(delta);
            }
        }

        public override string ToString()
        {
            GameDataSave datasave = new GameDataSave();
            return datasave.LoadString();
        }

        public byte[] ToBytes()
        {
            return System.Text.ASCIIEncoding.Default.GetBytes(ToString());
        }

        public static GameData FromBytes(byte[] bytes)
        {
            return FromString(System.Text.ASCIIEncoding.Default.GetString(bytes));
        }

        public static GameData FromString(string _s)
        {
            GameData _gd = new GameData("initializing from string");
            GameDataSave datasave = new GameDataSave();
            bool result = datasave.Save(_s);

            if (!result)
                return _gd;

            _gd.data = _s;
            double val = 0;
            _gd.playingTime = TimeSpan.FromMilliseconds(val > 0f ? val : 0f);
            _gd.loadedTime = DateTime.Now;
            _gd.state = "Loaded successfully";
            return _gd;
        }

        public string Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
                state += ", modified";
            }

        }

        public string State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }
    }
}