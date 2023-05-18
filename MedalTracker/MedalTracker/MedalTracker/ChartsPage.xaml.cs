/*
Maxence Roy
1957042
Assignment 2
*/

using MedalTracker.Repos;
using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedalTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartsPage : ContentPage
    {
        public ChartsPage()
        {
            InitializeComponent();
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (picker.SelectedIndex)
            {
                case 0:
                    chartView.Chart = ChartsRepo.GetBarChart(App.Repo.Countries.ToList());
                    break;
                case 1:
                    chartView.Chart = ChartsRepo.GetPieChart(App.Repo.Countries.ToList());
                    break;
                default:
                    chartView.Chart = null;
                    break;
            }
        }
    }
}