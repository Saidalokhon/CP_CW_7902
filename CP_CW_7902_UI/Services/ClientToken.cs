using System;

namespace CP_CW_7902_UI.Services
{
    public static class ClientToken
    {
        /// <summary>
        /// The method generates unique token from uppercase and lowercase letters
        /// (65-90 and 97-122 values of the ASCII table).
        /// </summary>
        public static string GenerateToken()
        {
            Random random = new Random();
            string token = "";
            for (int i = 0; i < 30; i++)
            {
                token += (char)random.Next(65, 90);
                token += (char)random.Next(97, 122);
            }
            return token;
        }
    }
}
