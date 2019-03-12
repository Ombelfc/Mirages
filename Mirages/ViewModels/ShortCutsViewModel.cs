using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Mirages.Utility;
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
                case Key.W:
                case Key.S:
                case Key.A:
                case Key.D:
                case Key.E:
                case Key.C:
                case Key.K:
                case Key.OemSemicolon:
                case Key.I:
                case Key.P:
                case Key.O:
                case Key.L:
                    Messenger.Default.Send(k.Key, MessengerTokens.KeyPressed);
                    break;
            }
        });

        public ICommand WheelSpin => new RelayCommand<MouseWheelEventArgs>(w =>
        {
            if(w.Delta > 0)
                Messenger.Default.Send(w.Delta, MessengerTokens.MouseWheenSpin);
        });
    }
}
