using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Mirages.ViewModels
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
            SimpleIoc.Default.Register<SceneLoadingViewModel>();
            SimpleIoc.Default.Register<RayTracerViewModel>();
            SimpleIoc.Default.Register<ShortCutsViewModel>();
        }

        #region ViewModels

        public MainViewModel Main
        {
            get => ServiceLocator.Current.GetInstance<MainViewModel>();
        }
        
        public ElementaryViewModel Elementary
        {
            get => ServiceLocator.Current.GetInstance<ElementaryViewModel>();
        }

        public BinarizationViewModel Binarization
        {
            get => ServiceLocator.Current.GetInstance<BinarizationViewModel>();
        }

        public FiltersViewModel Filters
        {
            get => ServiceLocator.Current.GetInstance<FiltersViewModel>();
        }

        public SceneLoadingViewModel SceneLoading
        {
            get => ServiceLocator.Current.GetInstance<SceneLoadingViewModel>();
        }

        public RayTracerViewModel RayTracer
        {
            get => ServiceLocator.Current.GetInstance<RayTracerViewModel>();
        }

        public ShortCutsViewModel ShortCuts
        {
            get => ServiceLocator.Current.GetInstance<ShortCutsViewModel>();
        }

        #endregion

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}