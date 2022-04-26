using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SH.Inventory
{
    [Serializable]
    public class Item
    {
        public string id;
        /// <summary>
        /// Image to use if player is out of range and in inventory panel
        /// </summary>
        public Sprite image;
        /// <summary>
        /// Image to use if player is in range
        /// </summary>
        public Sprite inRangeImage;
        public string name;
        [TextArea]
        public string description;
        public int count = 1;
    }
}
