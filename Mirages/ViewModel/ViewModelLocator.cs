using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Mirages.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ElementaryViewModel>();
            SimpleIoc.Default.Register<BinarizationViewModel>();
            SimpleIoc.Default.Register<FiltersViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public ElementaryViewModel Elementary
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ElementaryViewModel>();
            }
        }

        public BinarizationViewModel Binarization
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BinarizationViewModel>();
            }
        }

        public FiltersViewModel Filters
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FiltersViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}