using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateLists
{
    class UI
    {
        public void ShowMessage(string mssg)
        {
            Console.WriteLine(mssg);
        }
        public string ReadMessage()
        {
            string message = Console.ReadLine();
            return message;
        }
        public int GetChoice()
        {
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
