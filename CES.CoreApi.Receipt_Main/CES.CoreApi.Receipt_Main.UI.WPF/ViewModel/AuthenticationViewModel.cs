﻿using CES.CoreApi.Receipt_Main.Domain;
using CES.CoreApi.Receipt_Main.UI.WPF.Security;
using CES.CoreApi.Receipt_Main.UI.WPF.View;
using Ninject;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class AuthenticationViewModel : IViewModel, INotifyPropertyChanged
    {        
        private readonly IAuthenticationService _authenticationService;
        private readonly DelegateCommand<object> _loginCommand;
        private readonly DelegateCommand<object> _logoutCommand;
        private readonly DelegateCommand<object> _showViewCommand;
        private string _username;
        private string _status;

        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _loginCommand = new DelegateCommand<object>(Login, CanLogin);
            _logoutCommand = new DelegateCommand<object>(Logout, CanLogout);
            _showViewCommand = new DelegateCommand<object>(ShowView, CanShow);
        }

        #region Properties
        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyPropertyChanged("Username"); }
        }

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format("Signed in as {0}. {1}",
                          Thread.CurrentPrincipal.Identity.Name,
                          Thread.CurrentPrincipal.IsInRole("Administrators") ? "You are an administrator!"
                              : "You are NOT a member of the administrators group.");

                return "Not authenticated!";
            }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged("Status"); }
        }

        private bool _IsVisible;
        public bool IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                NotifyPropertyChanged("IsVisible");
            }
        }
        #endregion

        #region Commands
        public DelegateCommand<object> LoginCommand { get { return _loginCommand; } }

        public DelegateCommand<object> LogoutCommand { get { return _logoutCommand; } }

        public DelegateCommand<object> ShowViewCommand { get { return _showViewCommand; } }
        #endregion

        private void Login(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var clearTextPassword = passwordBox.Password;
            try
            {
                //Validate credentials through the authentication service
                var user = _authenticationService.AuthenticateUser(Username, clearTextPassword);

                //Get the current principal object
                var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                //Authenticate the user
                customPrincipal.Identity = new CustomIdentity(user.Username, user.Email, user.Roles);

                //Update UI
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
                _loginCommand.RaiseCanExecuteChanged();
                _logoutCommand.RaiseCanExecuteChanged();
                Username = string.Empty; //reset
                passwordBox.Password = string.Empty; //reset
                Status = string.Empty;
                IsVisible = false;
                //NotifyPropertyChanged("Visible");
                Application.Current.MainWindow.Show();
            }
            catch (UnauthorizedAccessException)
            {
                Status = "Login failed! Please provide some valid credentials.";
            }
            catch (Exception ex)
            {
                Status = $"ERROR: {ex.Message}";
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        private void Logout(object parameter)
        {
            var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
                _loginCommand.RaiseCanExecuteChanged();
                _logoutCommand.RaiseCanExecuteChanged();
                Status = string.Empty;
            }
        }

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }

        private void ShowView(object parameter)
        {
            try
            {
                Status = string.Empty;
                IView view;
                if (parameter == null)
                    view = new SecretWindow();
                else
                    view = new AdminWindow();

               ((Window)view).Show();
            }
            catch (SecurityException)
            {
                Status = "You are not authorized!";
            }
        }
        private bool CanShow(object parameter)
        {
            return true;
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
