using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Specimen
{
  public class RegisterViewModel : BaseViewModel
    {
        #region Variables

        private string passwordImage = "view.png";
        private bool isPassword = true;
        public bool IsPassword { get { return isPassword; } set { isPassword = value; OnPropertyChanged(); } }
        public string PasswordImage { get { return passwordImage; } set { passwordImage = value; OnPropertyChanged("PasswordImage"); } }
        public bool IsActiveRegister { get { return IsActiveRegister; } set { IsActiveRegister = value; OnPropertyChanged(); } }
        public ICommand PasswordShowCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        #endregion

        public RegisterViewModel(INavigation navigation)
        {
            Navigation = navigation;
            IsActiveRegister=false;
            PasswordShowCommand = new Command(async () => await RunSafe(ShowHidePassword));
            RegisterCommand = new Command(Register,()=>IsActiveRegister);
        }

     public void ShowHidePassword()
        {
            isPassword = !isPassword;
            passwordImage = isPassword == true ? "view.png" : "hide.png";
        }

        public void Register()
        {
        }
    }
}
