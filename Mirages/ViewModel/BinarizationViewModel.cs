using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Mirages.ViewModel
{
    public class BinarizationViewModel : ViewModelBase
    {
        #region Fields

        private ImageSource originalImage;

        public ImageSource OriginalImage
        {
            get => originalImage;
            set
            {
                originalImage = value;
                RaisePropertyChanged("OriginalImage");
            }
        }

        private ImageSource editedImage;

        public ImageSource EditedImage
        {
            get => editedImage;
            set
            {
                editedImage = value;
                RaisePropertyChanged("EditedImage");
            }
        }

        private int thresholdValue;

        public int ThresholdValue
        {
            get => thresholdValue;
            set
            {
                thresholdValue = value;
                RaisePropertyChanged("ThresholdValue");
            }
        }

        #endregion

        public ICommand Gthreshold => new RelayCommand(() =>
        {

        });

        public ICommand Hthreshold => new RelayCommand(() =>
        {

        });

        public ICommand Lthreshold => new RelayCommand(() =>
        {

        });
    }
}
