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
            DataSource.ConnectionString = @"";

            user.Password = Hash(user.Password);
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
