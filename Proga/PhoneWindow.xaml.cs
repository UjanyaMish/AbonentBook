﻿using Npgsql;
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
using System.Windows.Shapes;
using Dapper;
using System.ComponentModel;

namespace Proga
{
    /// <summary>
    /// Логика взаимодействия для PhoneWindow.xaml
    /// </summary>
    /// 
    
    public partial class PhoneWindow : Window
    {
        List<Key> allowed = new List<Key> { Key.Back, Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9 };

        public PhoneWindow()
        {
            InitializeComponent();
        }

        private void Buttonn(object sender, RoutedEventArgs e)
        {
            string phone = PhoneNumber.Text;
            var list = Logic.FindByPhone(phone);
            FindPhone.ItemsSource = list;
            if (list.Count() == 0)
            {
                MessageBox.Show("Нет абонентов, удовлетворяющих критерию поиска");
            }
        }

        private void PhoneNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!allowed.Contains(e.Key))
            {
                e.Handled = true;
            }
        }

        private void FindPhone_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            MainWindow.RenameColumns(e);
        }
    }
}
