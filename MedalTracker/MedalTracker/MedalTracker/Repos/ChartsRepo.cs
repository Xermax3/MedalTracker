/*
Maxence Roy
1957042
Assignment 2
*/

using MedalTracker.Models;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;

namespace MedalTracker.Repos
{
    /// <summary> Provides methods for creating various country charts. </summary>
    public static class ChartsRepo
    {
        /// <summary> Returns a bar chart based on the provided country list. </summary>
        public static BarChart GetBarChart(IEnumerable<Country> countries)
        {
            List<ChartEntry> chartEntries = new List<ChartEntry>();
            BarChart barChart = new BarChart();

            foreach (Country country in countries)
            {
                chartEntries.Add(new ChartEntry(country.Total)
                {
                    Label = country.Name,
                    Color = SKColor.Parse(country.Color),
                    ValueLabel = country.Total.ToString()
                });
            }

            barChart.Entries = chartEntries;
            barChart.ValueLabelOrientation = Orientation.Horizontal;
            barChart.LabelOrientation = Orientation.Horizontal;
            barChart.LabelTextSize = 40;
            barChart.BackgroundColor = SKColor.Empty;
            return barChart;
        }

        /// <summary> Returns a pie chart based on the provided country list. </summary>
        public static PieChart GetPieChart(IEnumerable<Country> countries)
        {
            List<ChartEntry> chartEntries = new List<ChartEntry>();
            PieChart pieChart = new PieChart();

            foreach (Country country in countries)
            {
                chartEntries.Add(new ChartEntry(country.Total)
                {
                    Label = country.Name,
                    Color = SKColor.Parse(country.Color),
                    ValueLabel = country.Total.ToString()
                });
            }

            pieChart.Entries = chartEntries;
            pieChart.LabelTextSize = 40;
            pieChart.BackgroundColor = SKColor.Empty;
            return pieChart;
        }
    }
}
