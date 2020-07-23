using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Services.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(string toAddress);
    }
}
