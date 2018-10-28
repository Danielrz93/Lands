namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class LoginViewModel : BaseViewModel
    {

        #region Events
        //Evento para refrescar los campos en patalla
        //using System.ComponentModel;
        // No se necesita por la clase BaseViewModel
        //public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        // Las propiedades que queramos modificar igual a las pulicas pero en minuscula
        private string psswd;
        private bool isrunning;
        private bool isenabled;
        #endregion

        #region Properties
        public string Email { get; set; }
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
            get
            {
                return this.isrunning;
            }
            set
            {
                SetValue(ref this.isrunning, value);
            }
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
                    "Error",
                    "Your must enter an email.",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Psswd))
            {
                // using Xamarin.Forms;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Your must enter a password.",
                    "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "danielr9339@gmail.com" || this.Psswd != "daniel" )
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "User or Password isn't correct. Try again",
                    "Accept");
                this.Psswd = string.Empty;
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                   "Ok",
                   "Login Correct!",
                   "Accept");
            return;

        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
        }
        #endregion
    }
}
