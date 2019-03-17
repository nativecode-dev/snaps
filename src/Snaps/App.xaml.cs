namespace Snaps
{
    using System.Windows;
    using Services;
    using Unity;
    using Unity.Lifetime;

    public partial class App : Application
    {
        public App()
        {
            this.Container = new UnityContainer();
            this.Container.RegisterType<ActiveWindowWatcher>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<MainWindow>(new ContainerControlledLifetimeManager());
        }

        public IUnityContainer Container { get; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.Container.Resolve<MainWindow>().Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.Container.Dispose();

            base.OnExit(e);
        }
    }
}