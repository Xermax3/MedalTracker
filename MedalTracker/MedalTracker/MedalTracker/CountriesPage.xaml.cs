/*
Maxence Roy
1957042
Assignment 2
*/

using MedalTracker.Models;
using System;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedalTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountriesPage : ContentPage
    {
        public CountriesPage()
        {
            InitializeComponent();
            lvCountries.ItemsSource = App.Repo.Countries;
        }

        public void btnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CountryForm());
        }

        public void btnEdit_Clicked(object sender, EventArgs e)
        {
            Country country = (sender as MenuItem).CommandParameter as Country;
            Navigation.PushAsync(new CountryForm(country));
        }

        public async void btnDelete_Clicked(object sender, EventArgs e)
        {
            Country country = (sender as MenuItem).CommandParameter as Country;
            bool answer = await DisplayAlert
            (
                $"Confirm Deletion",
                $"Are you sure you want to delete {country.Name}?",
                "Yes",
                "No"
            );
            if (!answer)
                return;
            App.Repo.DeleteCountry(country);
        }

        public void btnSort_Clicked(object sender, EventArgs e)
        {
            bool sortAscending = App.Repo.ChangeSort();
            btnSort.IconImageSource = sortAscending ? ImageSource.FromFile("sortdown30.png") : ImageSource.FromFile("sortup30.png");
        }

        public async void btnShare_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Share...", "Cancel", null, "Top 3 Countries", "All Results", "Countries File");
            if (action == "Cancel")
                return;

            if (App.Repo.Countries.Count == 0)
            {
                await DisplayAlert("An error occured", "Country list is empty!", "OK");
                return;
            }

            // Generate the appropriate text or file.
            StringBuilder stringBuilder = new StringBuilder();
            switch (action)
            {
                case "Top 3 Countries":
                    Country[] topCountries = App.Repo.GetTopThree();
                    stringBuilder.AppendLine("Top 3 medal winning countries in the Olympics:");
                    for (int i = 0; i < topCountries.Length; i++)
                    {
                        stringBuilder.Append($"* {topCountries[i].Name}: {topCountries[i].Gold} Gold, {topCountries[i].Silver} Silver, and {topCountries[i].Bronze} Bronze");
                        if (i < topCountries.Length - 1)
                            stringBuilder.AppendLine();
                    }
                    await Share.RequestAsync(new ShareTextRequest(stringBuilder.ToString()));
                    break;

                case "All Results":
                    Country[] allCountries = App.Repo.GetAll();
                    stringBuilder.AppendLine("Olympics Medal Count:");
                    for (int i = 0; i < allCountries.Length; i++)
                    {
                        stringBuilder.Append($"* {allCountries[i].Name} # {allCountries[i].Total} Medals");
                        if (i < allCountries.Length - 1)
                            stringBuilder.AppendLine();
                    }
                    await Share.RequestAsync(new ShareTextRequest(stringBuilder.ToString()));
                    break;

                case "Countries File":
                    await Share.RequestAsync(new ShareFileRequest(new ShareFile(App.Repo.GetFilePath())));
                    break;
            }
        }


        public async void lvCountries_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Country country = (Country)e.Item;
            await DisplayAlert
            (
                $"{country.Name}'s Olympic Medals",
                $"✨{country.Gold}🥇✨\n✨{country.Silver}🥈✨\n✨{country.Bronze}🥉✨",
                "OK"
            );
        }
    }
}