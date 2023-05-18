/*
Maxence Roy
1957042
Assignment 2
*/

using MedalTracker.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedalTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryForm : ContentPage
    {
        private Country editCountry;
        private Country country;

        /// <summary>
        /// Initialize a country form.
        /// If country parameter is not provided, a new blank country will be made. 
        /// </summary>
        public CountryForm(Country editCountry = null)
        {
            this.editCountry = editCountry;
            if (editCountry == null)
            {
                country = new Country()
                {
                    FlagURL = "https://flagsapi.com/XX/flat/64.png"
                };
            }
            else
            {
                country = new Country();
                country.ApplyProperties(editCountry);
            }

            InitializeComponent();
            Title = editCountry == null ? "Add New Country" : $"Edit: {this.country.Name}";

            BindingContext = country;
        }

        public void btnSave_Clicked(object sender, EventArgs e)
        {
            country.Color = colorWheel.SelectedColor.ToHex();
            if (this.editCountry == null)
            {
                App.Repo.AddCountry(country);
            }
            else
            {
                editCountry.ApplyProperties(country);
                App.Repo.SaveList();
            }

            Navigation.PopAsync();
        }
    }
}