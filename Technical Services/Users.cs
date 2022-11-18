using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication_Lab.Domain;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Authentication_Lab.Technical_Services
{
    public class Users
    {
        public void AddUser (User user)
        {
            SqlConnection DataSource = new SqlConnection();
            DataSource.ConnectionString = @"Persist Security Info=False; Database=ptrninkov1;User ID=ptrninkov1;Password=xxxxx;server=dev1.baist.ca";
            DataSource.Open();

            SqlCommand AddUser = new SqlCommand()
            {
                Connection = DataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "BAIS3110"
            };

            SqlParameter parameter = new()
            {
                ParameterName = "Username",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = user.Username
            };
            AddUser.Parameters.Add(parameter);

            parameter = new()
            {
                ParameterName = "Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = user.Email
            };
            AddUser.Parameters.Add(parameter);

            user.Password = Hash(user.Password);

            parameter = new()
            {
                ParameterName = "Password",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = user.Password
            };
            AddUser.Parameters.Add(parameter);

            parameter = new()
            {
                ParameterName = "Role",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = user.Role
            };
            AddUser.Parameters.Add(parameter);

            AddUser.ExecuteNonQuery();

            DataSource.Close();
        }


        private string Hash(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCSP = new RNGCryptoServiceProvider())
            {
                rngCSP.GetNonZeroBytes(salt);
            }
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));
            return hash;
        }
    }
}
