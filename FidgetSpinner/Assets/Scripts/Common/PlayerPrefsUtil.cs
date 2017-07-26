using UnityEngine;
using System.Collections;

namespace Fidget.Common
{
    public class PlayerPrefsUtil
    {

        public PlayerPrefsUtil()
        {
            Init();
        }
        public void Init()
        {
            if (!EncryptedPlayerPrefs.HaveKeys)
            {
                SetPrivateKey();
                EncryptedPlayerPrefs.HaveKeys = true;
            }
        }
        private void SetPrivateKey()
        {
            EncryptedPlayerPrefs.keys = new string[10];
            EncryptedPlayerPrefs.keys[0] = "jlkjfb4a";
            EncryptedPlayerPrefs.keys[1] = "vcxgyA4a";
            EncryptedPlayerPrefs.keys[2] = "ngnrbqAK";
            EncryptedPlayerPrefs.keys[3] = "vcxv1ANZ";
            EncryptedPlayerPrefs.keys[4] = "xawa4AXz";
            EncryptedPlayerPrefs.keys[5] = "xp1dfaAK";
            EncryptedPlayerPrefs.keys[6] = "cxvkjh2D";
            EncryptedPlayerPrefs.keys[7] = "pbnbAzK3";
            EncryptedPlayerPrefs.keys[8] = "nCasdNaZ";
            EncryptedPlayerPrefs.keys[9] = "Hsd4znap";
        }
        public void SetInt(string key, int val)
        {
            EncryptedPlayerPrefs.SetInt(key, val);
        }
        public void SetFloat(string key, float val)
        {
            EncryptedPlayerPrefs.SetFloat(key, val);
        }
        public void SetString(string key, string val)
        {
            EncryptedPlayerPrefs.SetString(key, val);
        }


        public int GetInt(string key)
        {
            return EncryptedPlayerPrefs.GetInt(key);
        }

        public int GetInt(string key, int defaultval)
        {
            return EncryptedPlayerPrefs.GetInt(key, defaultval);
        }

        public float GetFloat(string key)
        {
            return EncryptedPlayerPrefs.GetFloat(key);
        }
        public string GetString(string key)
        {
            return EncryptedPlayerPrefs.GetString(key);
        }
    }
}