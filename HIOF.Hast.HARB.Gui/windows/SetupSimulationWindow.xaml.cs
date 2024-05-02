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
    /// Interaction logic for SetupSimulationWindow.xaml
    /// </summary>
    public partial class SetupSimulationWindow : Window
	{

        private Harbor harbor;
        private Random random = new Random();
        private DateTime start;

        public SetupSimulationWindow(Harbor harbor, DateTime start)
        {
            InitializeComponent();
            PopulateComboBoxes();
            this.harbor = harbor;
            this.start = start;
        }

        private void AddLargeWarehouse_Click(object sender, RoutedEventArgs e)
        {
            harbor.AddWarehouse(new Warehouse("Large warehouse", 20));
        }

        private void AddSmallWarehouse_Click(object sender, RoutedEventArgs e)
        {
            harbor.AddWarehouse(new Warehouse("Small warehouse", 10));
        }

        private void PopulateComboBoxes()
        {
            shipSizeComboBox.ItemsSource = Enum.GetValues(typeof(ShipSize));
            recurringSailingComboBox.ItemsSource = Enum.GetValues(typeof(RecurringSailing));
        }

        private void GenerateCargo_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(amountOfItemsTextBox.Text, out int amountOfItems))
            {
                MessageBox.Show("Invalid number for 'Amount of Items'");
                return;
            }
            if (!double.TryParse(minWeightTextBox.Text, out double min))
            {
                MessageBox.Show("Invalid number for 'Min Weight'");
                return;
            }
            if (!double.TryParse(maxWeightTextBox.Text, out double max))
            {
                MessageBox.Show("Invalid number for 'Max Weight'");
                return;
            }

            List<Cargo> cargoList = new List<Cargo>();
            for (int i = 0; i < amountOfItems; i++)
            {
                double weight = Math.Round(random.NextDouble() * (max - min) + min, 2);
                cargoList.Add(new Cargo("Cargo", weight, Teu.One));
            }
            foreach (Cargo cargo in cargoList)
            {
                harbor.AddCargo(cargo);
            }
            statusTextBlock.Text = "Cargo has been successfully added!";

        }
        private void AddMediumPort_Click(object sender, RoutedEventArgs e)
        {
            harbor.AddPort(new Port("Medium Port", ShipSize.Medium));
        }

        private void AddLargePort_Click(object sender, RoutedEventArgs e)
        {
            harbor.AddPort(new Port("Large Port", ShipSize.Large));
        }

        private void AddShip_Click(object sender, RoutedEventArgs e)
        {
            DateTime? sailingDate = string.IsNullOrWhiteSpace(sailingDateTextBox.Text) ? null : DateTime.Parse(sailingDateTextBox.Text);
            string name = nameTextBox.Text;
            ShipSize size = (ShipSize)shipSizeComboBox.SelectedItem;
            double maxCargoWeightInTons = double.Parse(maxCargoWeightInTonsTextBox.Text);
            string destination = destinationTextBox.Text;
            int tripLength = int.Parse(tripLengthTextBox.Text);
            RecurringSailing recurringSailing = (RecurringSailing)recurringSailingComboBox.SelectedItem;

            Ship newShip = new Ship(name, size, maxCargoWeightInTons, sailingDate, destination, tripLength, recurringSailing);
            
            harbor.AddShip(newShip);
            statusTextBlockShip.Text = "Ship added successfully!";
        }

        private void amountOfItemsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                
                if (!int.TryParse(textBox.Text, out _))
                {
                    
                    textBox.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    
                    textBox.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
        }
    }
}