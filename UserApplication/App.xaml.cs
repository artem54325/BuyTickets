using Prism.Ioc;
using Prism.Unity;
using System.ComponentModel;
using System.Windows;
using UserApplication.Views;
using UserApplication.Views.Dialogs;

namespace UserApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <inheritdoc />
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        /// <inheritdoc />
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
