namespace Lands
{
    using Views;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    public partial class App : Application
    {
        #region Constructors
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            //MainPage = new LoginPage();
            MainPage = new NavigationPage(new LoginPage()); // Sirve para apilar paginas
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
