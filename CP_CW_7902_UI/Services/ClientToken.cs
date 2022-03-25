using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_CW_7902_UI.Services
{
    public static class ClientToken
    {
        public static string GenerateToken()
        {
            Random random = new Random();
            string token = "";
            for(int i = 0; i < 30; i++)
            {
                token += (char)random.Next(65, 90);
                token += (char)random.Next(97, 122);
            }
            return token;
        }
    }
}
