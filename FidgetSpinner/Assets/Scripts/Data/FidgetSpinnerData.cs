using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Data
{

    public struct FidgetSpinnerItem
    {
        public string name;
        public string spriteName;

        public FidgetSpinnerItem(string name, string spriteName)
        {
            this.name = name;
            this.spriteName = spriteName;
        }

    }
    public class FidgetSpinnerData
    {
        public List<FidgetSpinnerItem> fidgetSpinnerList = new List<FidgetSpinnerItem>();
        public FidgetSpinnerData()
        {
            fidgetSpinnerList.Clear();
            fidgetSpinnerList.Add(new FidgetSpinnerItem( "Gaxay", "4"));
            fidgetSpinnerList.Add(new FidgetSpinnerItem( "Bat", "2"));
        }

       

        
    }
}