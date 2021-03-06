﻿using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncoreTickets.SDK.Utilities
{
    public static class HashHelper
    {
        public static string CreateMd5Hash(string sourceData)
        {
            if (sourceData == null)
            {
                return null;
            }

            var md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(sourceData);
            var hashBytes = md5.ComputeHash(inputBytes);
            return string.Concat(hashBytes.Select(x => x.ToString("x2")));
        }
    }
}
