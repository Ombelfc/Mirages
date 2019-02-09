using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mirages.ViewModels
{
    public class ShortCutsViewModel : ViewModelBase
    {
        public ShortCutsViewModel() { }

        public ICommand KeyDown => new RelayCommand<KeyEventArgs>(k =>
        {
            switch (k.Key)
            {

            }
        });
    }
}
