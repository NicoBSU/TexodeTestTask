using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TTTApp.Consts;
using TTTApp.Models;
using TTTApp.Services;

namespace TTTApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string,UserData> UserData;

        private UserService _userService;
        private JsonReader _jsonReader;
        private JsonWriter _jsonWriter;

        private UserData currentUserData;

        public MainWindow()
        {
            _jsonReader = new JsonReader();
            _jsonWriter = new JsonWriter();
            _userService = new UserService(_jsonReader);
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserData = _userService.GetAllUsersData();
            UsersDataGrid.ItemsSource = UserData;
            InitializeChart();
            currentUserData = UserData.ElementAt(0).Value;
            ChangeChartData(0);
            CheckPercentageDifference(0);
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Ensure row was clicked and not empty space
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                                e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;

            var index = row.GetIndex();
            currentUserData = UserData.ElementAt(index).Value;
            ChangeChartData(index);
            CheckPercentageDifference(index);
        }

        private void SaveData(object sender, RoutedEventArgs e)
        {
            _jsonWriter.WriteData(currentUserData,PathConsts.WritePath);
        }

        private void InitializeChart()
        {
            var range = Enumerable.Range(1, 31);

            var labels = range.Select(x => x.ToString()).ToList();


            DataChart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Day",
                Labels = labels
            });

            DataChart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Steps"
            });

            DataChart.LegendLocation = LiveCharts.LegendLocation.None;

        }

        private void ChangeChartData(int index)
        {
            DataChart.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var dailyData = UserData.ElementAt(index).Value.DailyResultsList;

            series.Add(new LineSeries() { Title = "Steps", Values = new ChartValues<int>(dailyData) });

            DataChart.Series = series;
        }

        private void CheckPercentageDifference(int index)
        {
            foreach (var user in UserData)
            {
                var averageSteps = user.Value.AverageSteps;
                if (user.Value.WorstResult > 1.2 * averageSteps ||
                    user.Value.BestResult > 1.2 * averageSteps ||
                    user.Value.WorstResult < 0.8 * averageSteps ||
                    user.Value.BestResult < 0.8 * averageSteps)
                {
                    user.Value.StepsComparisonPassed = true;
                }

                else
                {
                    user.Value.StepsComparisonPassed = false;
                }
            }
        }
    }
}
