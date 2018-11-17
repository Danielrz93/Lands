namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    using Services;
    using Helpers;
    public class LoginViewModel : BaseViewModel
    {

        #region Events
        //Evento para refrescar los campos en patalla
        //using System.ComponentModel;
        // No se necesita por la clase BaseViewModel
        //public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        // Las propiedades que queramos modificar igual a las pulicas pero en minuscula
        private string email;
        private string psswd;
        private bool isrunning;
        private bool isenabled;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }

        }
        public string Psswd
        {
            get
            {
                return this.psswd;
            }
            set
            {
                //// si el valor cambia enviarle el nuevo valor
                //if (this.psswd != value)
                //{
                //    this.psswd = value;
                //    PropertyChanged?.Invoke(
                //        this,
                //        new PropertyChangedEventArgs(nameof(this.Psswd)));
                //}

                // Se reemplaza por lo anterior comentado , con la clase de BaseViewModel
                SetValue(ref this.psswd, value);
            }
        }
        public bool IsRunning
        {
            get{return this.isrunning;}
            set{SetValue(ref this.isrunning, value);}
        }
        public bool IsRemembered { get; set; }
        public bool IsEnabled
        {
            get
            {
                return this.isenabled;
            }
            set
            {
                SetValue(ref this.isenabled, value);
            }
        }
        #endregion

        #region Commands
        // Se debe importar el using System.Windows.Input; 
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login() // Metodo asyncrono
        {
            // using Xamarin.Forms;
            if (string.IsNullOrEmpty(this.Email))
            {
                // using Xamarin.Forms;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Psswd))
            {
                // using Xamarin.Forms;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            // Hacer token al servicio API
            var conection = await this.apiService.CheckConnection();

            if (!conection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    conection.Message,
                    "Accept");
                return;
            }

            var token = await this.apiService.GetToken(
                "https://landsapidr.azurewebsites.net",
                this.Email,
                this.Psswd);

            //Error al consumir el servicio
            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Somethings was wrong, please try again.",
                    "Accept");

                return;
            }

            if (string.IsNullOrEmpty(token.TokenType))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    token.ErrorDescription,
                    "Accept");

                this.Psswd = string.Empty;
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            // Navegar a la siguiente pagina 
            mainViewModel.Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPages());


            this.IsRunning = false;
            this.IsEnabled = true;

            //this.Email = string.Empty;
            this.Psswd = string.Empty;

           
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();

            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Email = "danielr9339@gmail.com";
            this.Psswd = "danielr";

            // https://restcountries.eu/rest/v2/all
        }
        #endregion
    }
}
