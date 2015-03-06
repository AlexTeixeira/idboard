using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace idboard_v1.ViewModel
{
    public class OfferViewModel : ViewModelBase
    {
        public InfoViewModel Info { get; set; }

       
        public OfferViewModel(INavigationService navigationService)
        {
            Info = new InfoViewModel(navigationService);
        }
    }
}
