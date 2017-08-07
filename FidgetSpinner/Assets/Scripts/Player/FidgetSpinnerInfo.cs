using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Player
{
    public class FidgetSpinnerInfo 
    {
        public FidgetSpinnerInfo()
        {

        }

        public bool IsOwnFidgetSpinner(int index)
        {
            if(User.Instance.GetFidgetSpinnerLevel(index) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}