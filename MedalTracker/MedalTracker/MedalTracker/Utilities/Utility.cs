/*
Maxence Roy
1957042
Assignment 2
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace MedalTracker.Utilities
{
    /// <summary> Collection of various generic methods. </summary>
    public static class Utility
    {
        // Source: https://stackoverflow.com/questions/19112922/sort-observablecollectionstring-through-c-sharp
        public static void Sort<T>(this ObservableCollection<T> collection, Comparison<T> comparison)
        {
            var sortableList = new List<T>(collection);
            sortableList.Sort(comparison);

            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }
    }
}
