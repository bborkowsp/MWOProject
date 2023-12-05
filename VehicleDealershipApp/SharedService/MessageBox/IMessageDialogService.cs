using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.MessageBox
{
    public interface IMessageDialogService
    {
        void ShowMessage(string message);
    }
}
