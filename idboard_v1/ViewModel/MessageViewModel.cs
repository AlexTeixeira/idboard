using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idboard_v1.ViewModel
{
    public class MessageViewModel : ViewModelBase
    {
        public InfoViewModel Info { get; set; }


        public MessageViewModel(INavigationService navigationService)
        {
            Info = new InfoViewModel(navigationService);
        }
    }
}
