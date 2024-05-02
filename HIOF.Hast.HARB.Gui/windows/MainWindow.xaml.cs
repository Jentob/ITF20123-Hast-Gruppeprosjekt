using HIOF.Hast.HARB.Framework;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HIOF.Hast.HARB.Gui.windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool isSimulationRunning = false;
        private Harbor harbor;
        private DateTime start, end;

        public MainWindow()
        {
            InitializeComponent();
            UpdateSimulationStatus();


            harbor = new("HarborName");
            start = DateTime.Now;
            end = DateTime.Now.AddMonths(1);
        }
        private void SetupPremises_Click(object sender, RoutedEventArgs e)
        {
            // Open the Setup Simulation Window
            var setupWindow = new SetupSimulationWindow(harbor, start);
            setupWindow.Show();

        }

        private void RunSimulation_Click(object sender, RoutedEventArgs e)
        {
            // Toggle simulation running state
            isSimulationRunning = !isSimulationRunning;
            UpdateSimulationStatus();
            if (isSimulationRunning)
            {
                SimulationDriver driver = new(harbor);
                driver.Run(start, end);
            }
            else
            {
                // Stop simulation logic TODO Do we have a way of stopping the simulation?
                runSimulationButton.Content = "Run Simulation";
            }

        }

        private void UpdateSimulationStatus()
        {
            if (isSimulationRunning)
            {
                simulationStatus.Text = "Simulation is running";
                statusIcon.Text = "✓";
                statusIcon.Foreground = new SolidColorBrush(Color.FromRgb(0, 128, 0)); // Green color
                runSimulationButton.Content = "Stop Simulation";
            }
            else
            {
                simulationStatus.Text = "Simulation is not running";
                statusIcon.Text = "✗";
                statusIcon.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0)); // Red color
                runSimulationButton.Content = "Run Simulation";
            }
        }

        private void ViewEvents_Click(object sender, RoutedEventArgs e)
        {
            // Open the Real-Time Events Viewer Window
            var eventsWindow = new EventsWindow();
            eventsWindow.Show();
        }

        private void ViewSummary_Click(object sender, RoutedEventArgs e)
        {
            // Open the Simulation Summary Window
            var summaryWindow = new SummaryWindow(this.harbor);
            summaryWindow.Show();
        }
    }
}