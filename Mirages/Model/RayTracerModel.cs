using GalaSoft.MvvmLight;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.Model
{
    public class RayTracerModel : ObservableObject
    {
        #region Non-Binding properties

        /// <summary>
        /// Holds the back-buffer pushed to the image-source periodically
        /// </summary>
        public WriteableBitmap WriteableBitmap { get; set; }

        #endregion

        #region Binding Properties

        private bool isLoadSceneEnabled = false;
        /// <summary>
        /// Determines whether the load-scene button is enabled.
        /// </summary>
        public bool IsLoadSceneEnabled
        {
            get => isLoadSceneEnabled;
            set
            {
                isLoadSceneEnabled = value;

                RaisePropertyChanged("IsLoadSceneEnabled");
            }
        }

        private bool isUnloadSceneEnabled = false;
        /// <summary>
        /// Determines whether the unload-scene button is enabled.
        /// </summary>
        public bool IsUnloadSceneEnabled
        {
            get => isUnloadSceneEnabled;
            set
            {
                isUnloadSceneEnabled = value;

                RaisePropertyChanged("IsUnloadSceneEnabled");
            }
        }

        private ImageSource image;
        /// <summary>
        /// Source of the rendered scene.
        /// </summary>
        public ImageSource Image
        {
            get => image;
            set
            {
                image = value;
                RaisePropertyChanged("Image");
            }
        }

        #endregion

        public RayTracerModel()
        {
            IsLoadSceneEnabled = true;
            IsUnloadSceneEnabled = false;
        }
    }
}
