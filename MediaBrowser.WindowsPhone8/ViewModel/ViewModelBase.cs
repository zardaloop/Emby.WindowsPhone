﻿using MediaBrowser.Model.ApiClient;
using MediaBrowser.Model.Dto;
using MediaBrowser.WindowsPhone.Model.Interfaces;

namespace MediaBrowser.WindowsPhone.ViewModel
{
    public class ViewModelBase : ScottIsAFool.WindowsPhone.ViewModel.ViewModelBase
    {
        protected readonly INavigationService NavigationService;
        protected readonly IConnectionManager ConnectionManager;

        protected IHasServerId ServerIdItem;

        public ViewModelBase(INavigationService navigationService, IConnectionManager connectionManager)
        {
            NavigationService = navigationService;
            ConnectionManager = connectionManager;
        }

        protected IApiClient ApiClient
        {
            get { return ConnectionManager.CurrentApiClient; }
        }
    }
}
