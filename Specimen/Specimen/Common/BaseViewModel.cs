using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Specimen
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public static byte[] byteArray { get; set; }
        internal virtual Task Initialize(params object[] args)
        {
            return Task.FromResult(0);
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }


        protected void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetObservableProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region RunSafe

        public async Task RunSafe(Action actionToExecute)
        {
            try
            {
                IsBusy = true;
                await Task.Run(() => { actionToExecute.Invoke(); });
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                string errorMessage = ex.Message;
                if (string.IsNullOrEmpty(errorMessage))
                {
                    //  errorMessage = Constant.SomethingWentWrong;
                }
                //await Application.Current.MainPage.DisplayAlert(Constant.ErrorTitle, errorMessage, Constant.CancelButtonTitle);
            }
        }

        public virtual void OnDisappear()
        {
            //Clean your stuff
        }

        public virtual void OnAppear()
        {
            //Create your stuff
        }

        #endregion
    }
}

