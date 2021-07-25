using NewFeed.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NewFeed.ViewModels
{
    public class NewsDetailsViewViewModel : ViewModelBase
    {
        private Article _Article;

        public Article Article
        {
            get { return _Article; }
            set {
                SetProperty(ref _Article, value);
            }
        }

        public DelegateCommand OpenWebSiteCommand { get; set; }
        public NewsDetailsViewViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Article Detail";
            OpenWebSiteCommand = new DelegateCommand(OpenWebSiteCommandActionMethod);
        }

        private async void OpenWebSiteCommandActionMethod()
        {
            try
            {
                bool IsInternetConnected = checkInternetConnectivity().Result;
                if (!IsInternetConnected)
                    return;

                await Browser.OpenAsync(Article.url, new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Color.Black,
                    PreferredControlColor = Color.Black,
                });
            }
            catch (Exception ex)
            {
                
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            
            if(parameters!=null && parameters.ContainsKey("Article"))
            {
                Article = parameters["Article"] as Article;
            }
            base.OnNavigatedTo(parameters);
        }
    }
}
