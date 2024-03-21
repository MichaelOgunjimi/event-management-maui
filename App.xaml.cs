namespace EventyMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            MainPage = new AppShell();

            // If i want to use NavigationPage
            //MainPage = new NavigationPage(new EventyMaui.Views.StartPage());
        }
    }
}
