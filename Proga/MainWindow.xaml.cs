using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Dapper;
using Npgsql;

namespace Proga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestGrid.ItemsSource = Logic.Abonents; 
        }

        private void ButtonnFind(object sender, RoutedEventArgs e)
        {
            PhoneWindow find = new PhoneWindow();
            find.Show();
        }

        private void ButtonnCSV(object sender, RoutedEventArgs e)
        {
            Logic.SaveAbonentsCsv(TestGrid.ItemsSource.Cast<Abonent>(), "results");
        }

        private void ButtonnStreets(object sender, RoutedEventArgs e)
        {
            StreetWindow find = new StreetWindow();
            find.Show();
        }

        private void TestGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            RenameColumns(e);
        }

        private void Firstnamebox_Initialized(object sender, EventArgs e)
        {
            if (sender is ComboBox box)
            {
                foreach (string firstname in Logic.UniqueFirstNames)
                {
                    box.Items.Add(firstname);
                }
            }
        }

        private void Lastnamebox_Initialized(object sender, EventArgs e)
        {
            if (sender is ComboBox box)
            {
                foreach (string lastname in Logic.UniqueLastNames)
                {
                    box.Items.Add(lastname);
                }
            }
        }

        private void Fathernamebox_Initialized(object sender, EventArgs e)
        {
            if (sender is ComboBox box)
            {
                foreach (string fathername in Logic.UniqueFatherNames)
                {
                    box.Items.Add(fathername);
                }
            }
        }

        private void Streetname_Initialized(object sender, EventArgs e)
        {
            if (sender is ComboBox box)
            {
                foreach (string streetname in Logic.UniqueStreetNames)
                {
                    box.Items.Add(streetname);
                }
            }
        }

        private void Housenumber_Initialized(object sender, EventArgs e)
        {
            if (sender is ComboBox box)
            {
                foreach (string housenumber in Logic.UniqueHouseNumbers)
                {
                    box.Items.Add(housenumber);
                }
            }
        }

        private void Home_Initialized(object sender, EventArgs e)
        {
            if (sender is ComboBox box)
            {
                foreach (string home in Logic.UniqueHome)
                {
                    box.Items.Add(home);
                }
            }
        }

        private void Work_Initialized(object sender, EventArgs e)
        {
            if (sender is ComboBox box)
            {
                foreach (string work in Logic.UniqueWork)
                {
                    box.Items.Add(work);
                }
            }
        }

        private void Mobile_Initialized(object sender, EventArgs e)
        {
            if (sender is ComboBox box)
            {
                foreach (string mobile in Logic.UniqueMobile)
                {
                    box.Items.Add(mobile);
                }
            }
        }

        public static void RenameColumns(DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor desc)
            {
                switch (desc.DisplayName)
                {
                    case "FirstName":
                        e.Column.Header = "Имя";
                        break;
                    case "LastName":
                        e.Column.Header = "Фамилия";
                        break;
                    case "FatherName":
                        e.Column.Header = "Отчество";
                        break;
                    case "StreetName":
                        e.Column.Header = "Название улицы";
                        break;
                    case "HouseNumber":
                        e.Column.Header = "Номер дома";
                        break;
                    case "Home":
                        e.Column.Header = "Домашний";
                        break;
                    case "Work":
                        e.Column.Header = "Рабочий";
                        break;
                    case "Mobile":
                        e.Column.Header = "Мобильный";
                        break;
                    case "Count":
                        e.Column.Header = "Количество абонентов";
                        break;
                }
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = Logic.Abonents.Select(x => x);
            if (firstnamebox.SelectedIndex != -1)
            {
                items = items.Where(x => x.FirstName == firstnamebox.SelectedItem as string);
            }
            if (lastnamebox.SelectedIndex != -1)
            {
                items = items.Where(x => x.LastName == lastnamebox.SelectedItem as string);
            }
            if (fathernamebox.SelectedIndex != -1)
            {
                items = items.Where(x => x.FatherName == fathernamebox.SelectedItem as string);
            }
            if (streetnamebox.SelectedIndex != -1)
            {
                items = items.Where(x => x.StreetName == streetnamebox.SelectedItem as string);
            }
            if (housenumberbox.SelectedIndex != -1)
            {
                items = items.Where(x => x.HouseNumber == housenumberbox.SelectedItem as string);
            }
            if (homebox.SelectedIndex != -1)
            {
                items = items.Where(x => x.Home == homebox.SelectedItem as string);
            }
            if (workbox.SelectedIndex != -1)
            {
                items = items.Where(x => x.Work == workbox.SelectedItem as string);
            }
            if (mobilebox.SelectedIndex != -1)
            {
                items = items.Where(x => x.Mobile == mobilebox.SelectedItem as string);
            }
            TestGrid.ItemsSource = items;
            if (items.Count() == 0)
            {
                MessageBox.Show("Нет абонентов, удовлетворяющих критерию фильтрации");
            }
        }
    }
}
