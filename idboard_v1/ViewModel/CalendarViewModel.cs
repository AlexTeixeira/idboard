using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace idboard_v1.ViewModel
{
    public class CalendarViewModel : ViewModelBase
    {
        /** Méthode d'une commance avec utilisation d'un delegate*/
        public InfoViewModel Info { get; set; }


        public CalendarViewModel(INavigationService navigationService)
        {
            Info = new InfoViewModel(navigationService);
        }
    }
}
