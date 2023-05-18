/*
Maxence Roy
1957042
Assignment 2
*/

using MedalTracker.Utilities;
using System;
using System.ComponentModel;

namespace MedalTracker.Models
{
    /// <summary> Tracker for a country's identity and amount of medals. </summary>
    public class Country : INotifyPropertyChanged, IComparable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public string FlagURL { get; set; }
        public string Color { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
        public int Total
        {
            get
            {
                return Gold + Silver + Bronze;
            }
        }

        /// <summary> Returns 1 if the country has more medals, -1 if less, and 0 if equal. </summary>
        public int CompareTo(object obj)
        {
            Country otherCountry = (Country)obj;
            return string.Compare(this.Name, otherCountry.Name);
        }

        /// <summary> Turns the current country instance into a shallow copy of the provided country. </summary>
        public void ApplyProperties(Country reference)
        {
            this.Name = reference.Name;
            this.FlagURL = reference.FlagURL;
            this.Color = reference.Color;
            this.Gold = reference.Gold;
            this.Silver = reference.Silver;
            this.Bronze = reference.Bronze;
        }
    }
}
