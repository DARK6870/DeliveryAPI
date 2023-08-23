using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Test.Models;
using TEST.Models;

namespace Test.Models
{
	public class UserConstants
	{
		public static List<UserModel> GetUsersFromDatabase()
		{
			string connectionString = "server=localhost; database=Delivery_v3; Integrated Security=true; Encrypt=false;";

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				string query = "SELECT * FROM UsersData";

				List<UserModel> users = connection.Query<UserModel>(query).ToList();
				return users;
			}
		}

		public static void UsersData()
		{
			List<UserModel> users = GetUsersFromDatabase();

			foreach (UserModel user in users)
			{
				Console.WriteLine($"id: {user.users_id}");
				Console.WriteLine($"Email: {user.email}");
				Console.WriteLine($"First name: {user.u_firstname}");
				Console.WriteLine($"Last name: {user.u_lastname}");
				Console.WriteLine($"password: {user.user_password}");
				Console.WriteLine($"Role id: {user.role_id}");
				Console.WriteLine($"Role name: {user.rolename}");
				Console.WriteLine();
			}

			Console.ReadLine();
		}
	}
}