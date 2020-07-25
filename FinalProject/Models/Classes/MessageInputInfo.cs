using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Classes
{
    public class MessageInputInfo
    {
        public DateTime Date { get; set; }
        public string Login { get; set; }
        public string Content { get; set; }
        public bool Registered { get; set; }
    }
}
