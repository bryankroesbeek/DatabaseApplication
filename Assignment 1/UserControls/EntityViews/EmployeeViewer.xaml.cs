﻿using System;
using System.Collections.Generic;
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
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.UserControls.EntityViews {
    /// <summary>
    /// Interaction logic for EmployeeViewer.xaml
    /// </summary>
    public partial class EmployeeViewer : UserControl {
        private DatabaseConnection _connection;
        private List<Employee> employeeList;
        public EmployeeViewer() {
            InitializeComponent();
            _connection = new DatabaseConnection("localhost", "assignment1", "root", "");
            employeeList = EntityContentSelector.SelectEmployee(this._connection);
            foreach (Employee employee in employeeList) {
                EmployeeList.Items.Add(employee);
            }
        }

        public Employee GetSelectedEmployee() {
            return employeeList[EmployeeList.SelectedIndex];
        }

        public bool IsSelected() {
            return EmployeeList.SelectedIndex >= 0;
        }
    }
}