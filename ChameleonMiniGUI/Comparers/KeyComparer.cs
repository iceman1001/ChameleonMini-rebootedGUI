using System.Collections.Generic;

namespace ChameleonMiniGUI
{
    public class KeyComparer : IComparer<MyKey>
    {
        public int Compare(MyKey x, MyKey y)
        {
            if (x.Sector > y.Sector)
                return 1;

            if (x.Sector < y.Sector)
                return -1;
            return 0;
        }
    }
}