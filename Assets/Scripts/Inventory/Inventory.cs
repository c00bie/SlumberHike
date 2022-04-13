using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

namespace SH.Inventory
{
    public static class Inventory
    {
        private static List<Item> items = new List<Item>();
        private static IReadOnlyList<Item> Items => items;

        public static Item GetItem(string id) => items.Find(x => x.id == id);

        /// <summary>
        /// Checks if inventory contains spcified <see cref="Item"/> or item with the same ID
        /// </summary>
        /// <param name="i">Item to find</param>
        /// <param name="instanceOnly">If set to <see langword="true"/>, function will find only instance of given <see cref="Item"/>, without looking for ID</param>
        public static bool HasItem(Item i, bool instanceOnly = false) => items.Contains(i) || (instanceOnly ? false : HasItem(i.id));

        /// <summary>
        /// Checks if inventory contains item with specified ID
        /// </summary>
        /// <param name="id">ID of the item to find</param>
        public static bool HasItem(string id) => items.Any(x => x.id == id);

        /// <summary>
        /// Checks if inventory contains any item with specified name
        /// </summary>
        /// <param name="name">Name of the item to find</param>
        public static bool HasAnyItem(string name) => items.Any(x => x.name == name);

        /// <summary>
        /// Checks if inventory contains any item with name matching specified <see cref="Regex"/>
        /// </summary>
        /// <param name="nameExp">Regex to find matches to</param>
        public static bool HasAnyItem(Regex nameExp) => items.Any(x => nameExp.IsMatch(x.name));

        /// <summary>
        /// Adds <see cref="Item"/> to inventory, or increases count if already exists
        /// </summary>
        /// <param name="i">Item to add</param>
        public static void AddItem(Item i)
        {
            if (HasItem(i))
                GetItem(i.id).count += i.count;
            else
                items.Add(i);
            Debug.Log(items);
        }

        /// <summary>
        /// Decrease quantity of item with specified ID and remove it if quantity reaches 0
        /// </summary>
        /// <param name="id">ID of item to use</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/>if item is not in inventory</returns>
        public static bool UseItem(string id)
        {
            if (!HasItem(id))
                return false;
            Item i = GetItem(id);
            if (i.count > 1)
                i.count--;
            else
                RemoveItem(id);
            return true;
        }

        /// <summary>
        /// Decrease quantity of specified <see cref="Item"/> and remove it if quantity reaches 0
        /// </summary>
        /// <param name="i">Item to use</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/>if item is not in inventory</returns>
        public static bool UseItem(Item i) => UseItem(i.id);

        /// <summary>
        /// Remove item with specified ID from inventory
        /// </summary>
        /// <param name="id">ID of item to remove</param>
        public static void RemoveItem(string id)
        {
            if (HasItem(id))
                items.RemoveAll(x => x.id == id);
        }

        /// <summary>
        /// Remove specified <see cref="Item"/> from inventory
        /// </summary>
        /// <param name="i">Item to remove</param>
        public static void RemoveItem(Item i) => RemoveItem(i.id);
    }
}
