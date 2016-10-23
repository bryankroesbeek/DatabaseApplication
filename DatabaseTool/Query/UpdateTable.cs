﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseTool.Connector;
using DatabaseTool.Entities;

namespace DatabaseTool.Query {
    public static class UpdateTable {
        public static void UpdateEmployee(Employee employee, string newName, string newSurname, string newBuildingName) {
            ConnectionHolder.Connection.Connection.Open();
            string baseQuery = "UPDATE employee e SET ";
            string selectedEmployee = $"WHERE bsn = '{employee.Bsn.ToString("000000000")}'";
            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            baseQuery += $"e.name = '{newName}', e.surname = '{newSurname}', e.building_name = '{newBuildingName}' ";
            employee.Name = newName;
            employee.Surname = newSurname;
            employee.MainBuildingName = newBuildingName;

            baseQuery += selectedEmployee;
            command.CommandText = baseQuery;
            command.ExecuteNonQuery();
            ConnectionHolder.Connection.Connection.Close();
        }

        public static void UpdateEmployeeAddress(EmployeeAddress oldEmployeeAddress, EmployeeAddress newEmployeeAddress) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();

            byte bit = 0;
            if (newEmployeeAddress.IsResidence) {
                bit = 1;
            }

            string baseQuery = "UPDATE employee_address ea ";

            string getCurrentEmployee =
                $"WHERE ea.bsn = '{oldEmployeeAddress.Bsn}' AND ea.postal_code = '{oldEmployeeAddress.PostalCode}' AND ea.country = '{oldEmployeeAddress.Country}' ";
            string changeValues =
                $"SET ea.bsn = '{newEmployeeAddress.Bsn}', ea.postal_code = '{newEmployeeAddress.PostalCode}', ea.country = '{newEmployeeAddress.Country}', ea.is_residence = {bit} ";

            string query = baseQuery + changeValues + getCurrentEmployee;
            command.CommandText = query;
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }

        public static void UpdateProject(int oldProjectId, float newBudget, float newTotalHours, string newBuildingName) {
            ConnectionHolder.Connection.Connection.Open();

            var command = ConnectionHolder.Connection.Connection.CreateCommand();
            command.CommandText =
                $"UPDATE project p SET p.budget = '{newBudget}', p.total_hours = '{newTotalHours}', p.building_name = '{newBuildingName}' WHERE p.project_id = '{oldProjectId}'";
            command.ExecuteNonQuery();

            ConnectionHolder.Connection.Connection.Close();
        }
    }
}