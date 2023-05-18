/*
Maxence Roy
1957042
Assignment 2
*/

using MedalTracker.Repos;
using System;
using System.IO;
using Xamarin.Forms;

namespace MedalTracker
{
    public partial class App : Application
    {
        private static CountriesRepo repo;

        public static CountriesRepo Repo
        {
            get
            {
                // Create a new countries json file if it does not exist already.
                if (repo == null)
                {
                    string savingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string filePath = Path.Combine(savingDirectory, "countries3.json");
                    repo = new CountriesRepo(filePath);
                }
                return repo;
            }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            //repo.SaveList();
        }

        protected override void OnResume()
        {
        }
    }
}
