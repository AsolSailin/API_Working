using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestingAPI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await Refresh();
            await GetRoles();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "" || tbAge.Text == "" || cbRole.SelectedItem == null)
            {
                return;
            }

            var role = (await NetManager.Get<Role>($"api/Roles/{cbRole.SelectedItem}"));

            var user = new User()
            {
                Name = tbName.Text,
                Age = int.Parse(tbAge.Text),
                RoleId = role.Id,
                Role = role.Name
            };

            await NetManager.Post("api/Users/Add", user);

            await Refresh();
        }


        private async Task Refresh()
        {
            dgUsers.ItemsSource = await NetManager.Get<List<User>>("api/Users/GetAllUsers");
        }

        private async Task GetRoles()
        {
            cbRole.ItemsSource = (await NetManager.Get<List<Role>>("api/Roles/GetAllRoles")).Select(r => r.Name);
        }
    }
}
