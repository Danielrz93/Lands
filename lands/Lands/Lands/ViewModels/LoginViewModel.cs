using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lands.ViewModels
{
    public class LoginViewModel
    {
        #region Properties
        public string Email { get; set; }
        public string Psswd { get; set; }
        public bool IsRunning { get; set; }
        public bool IsRemembered { get; set; }
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
            if (this.Email != "danielr9339@gmail.com" || this.Psswd != "daniel" )
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "User or Password isn't correct. Try again",
                    "Accept");
                return;
            }

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
        }
        #endregion
    }
}
