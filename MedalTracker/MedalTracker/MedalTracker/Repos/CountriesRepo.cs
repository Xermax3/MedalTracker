/*
Maxence Roy
1957042
Assignment 2
*/

using MedalTracker.Models;
using MedalTracker.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace MedalTracker.Repos
{
    /// <summary> Handles the application's country list. </summary>
    public class CountriesRepo
    {
        private ObservableCollection<Country> countries;

        public ObservableCollection<Country> Countries { get { return countries; } }

        private readonly string filePath;
        private bool sortAscending = true;

        public CountriesRepo(string filePath)
        {
            this.filePath = filePath;
            LoadList();
        }

        /// <summary> Populates the country list with default countries. </summary>
        private void AddTestData()
        {
            countries = new ObservableCollection<Country>();
            Country canada = new Country()
            {
                Name = "Canada",
                Color = "#E72923",
                FlagURL = "https://flagsapi.com/CA/flat/64.png",
                Gold = 4,
                Silver = 2,
                Bronze = 1
            };
            Country brazil = new Country()
            {
                Name = "Brazil",
                Color = "#009639",
                FlagURL = "https://flagsapi.com/BR/flat/64.png",
                Gold = 3,
                Silver = 0,
                Bronze = 3
            };
            AddCountry(canada);
            AddCountry(brazil);
        }

        public void AddCountry(Country country)
        {
            countries.Add(country);
            SaveList();
        }

        public void DeleteCountry(Country country)
        {
            countries.Remove(country);
            SaveList();
        }

        /// <summary>
        /// Changes the order of the country list between alphabetical or reverse alphabetical.
        /// Returns true if new sort is alphabetical.
        /// </summary>
        public bool ChangeSort()
        {
            sortAscending = !sortAscending;
            Sort();
            return sortAscending;
        }

        private void Sort()
        {
            if (sortAscending)
            {
                countries.Sort((a, b) => { return a.CompareTo(b); });
            }
            else
            {
                countries.Sort((a, b) => { return b.CompareTo(a); });
            }
        }

        /// <summary> Stores the country list on the device. </summary>
        public void SaveList()
        {
            Sort();
            string serializedObject = JsonConvert.SerializeObject(countries);
            try
            {
                File.WriteAllText(filePath, serializedObject);
            }
            catch
            {
                Console.WriteLine("Failed to save.");
            }
        }

        /// <summary> 
        /// Retrieves the device's stored country list.
        /// If not found, default countries will be added.
        /// </summary>
        public void LoadList()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string serializedObject = File.ReadAllText(filePath);
                    countries = JsonConvert.DeserializeObject<ObservableCollection<Country>>(serializedObject);
                }
                catch
                {
                    Console.WriteLine("Failed to load.");
                }
            }
            else
            {
                AddTestData();
            }
            Sort();
        }

        /// <summary> Returns the full path to the repo's save location. </summary>
        public string GetFilePath()
        {
            return filePath;
        }

        /// <summary> Returns the three countries with the most medals, sorted by total medals. </summary>
        public Country[] GetTopThree()
        {
            Country[] sortedCountries = countries.OrderByDescending(c => c.Total)
                .ThenByDescending(c => c.Gold)
                .ThenByDescending(c => c.Silver)
                .ToArray();

            // If many countries are in third place with the exact same amount of medals, include them all.
            if (sortedCountries.Length > 3)
            {
                for (int i = 2; i < sortedCountries.Length - 1; i++)
                {
                    if (sortedCountries[i].Bronze > sortedCountries[i + 1].Bronze)
                    {
                        Array.Resize(ref sortedCountries, i + 1);
                        break;
                    }
                }
            }
            return sortedCountries;
        }

        /// <summary> Returns every country in the list, sorted alphabetically. </summary>
        public Country[] GetAll()
        {
            return countries.OrderBy(c => c.Name).ToArray();
        }
    }
}
