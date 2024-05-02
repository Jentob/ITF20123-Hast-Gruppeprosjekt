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
using System.Windows.Threading;

namespace HIOF.Hast.HARB.Gui.windows
{
    /// <summary>
    /// Interaction logic for SummaryWindow.xaml
    /// </summary>
    public partial class SummaryWindow : Window
	{
        private DispatcherTimer timer;
        private Harbor harbor;

        public SummaryWindow(Harbor harbor)
        {
            InitializeComponent();
            this.harbor = harbor;
            LoadData();
        }

        private void LoadData()
        {
            // Load Warehouses
            foreach (Warehouse warehouse in harbor.GetWarehouses())
            {
                var item = new ListBoxItem
                {
                    Content = warehouse.ToString(),
                    FontWeight = FontWeights.Bold
                };
                warehousesListBox.Items.Add(item);

                foreach (Cargo cargo in warehouse.GetInventory())
                {
                    warehousesListBox.Items.Add(new ListBoxItem { Content = $"\t{cargo}" });
                }
            }

            // Load Ports
            foreach (Port port in harbor.GetPorts())
            {
                var item = new ListBoxItem
                {
                    Content = port.ToString(),
                    FontWeight = FontWeights.Bold
                };
                portsListBox.Items.Add(item);

                if (port.OccupyingShip != null)
                {
                    foreach (Cargo cargo in port.OccupyingShip.GetCargohold())
                    {
                        portsListBox.Items.Add(new ListBoxItem { Content = $"\t{cargo}" });
                    }
                }
            }

            // Load Ships in Queue
            shipsInQueueListBox.ItemsSource = harbor.GetWaitingQueue();

            // Load Sailing Ships
            sailingShipsListBox.ItemsSource = harbor.GetSailingShips();

            // Load Ship History 
            foreach (Ship ship in harbor.GetAllShips())
            {
                // Add ship summary item
                shipHistoryListBox.Items.Add(new ListBoxItem
                {
                    Content = ship.ToString(),
                    FontWeight = FontWeights.Bold
                });

                // Add log entries for each ship
                foreach (LogEntry logEntry in ship.GetLog())
                {
                    if (!logEntry.Message.Contains("cargohold"))
                    {
                        shipHistoryListBox.Items.Add(new ListBoxItem
                        {
                            Content = logEntry.ToString()
                        });
                    }
                }
            }
        }
    }
}