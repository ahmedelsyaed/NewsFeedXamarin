using NewFeed.Interface;
using NewFeed.Models;
using NewFeed.Services;
using NewFeed.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NewFeed.ViewModels
{
    public class ArticlesSectionViewModel : ViewModelBase
    {
        INavigationService navigationService;
        INewsFeedApiHandler _newsFeed;
        private List<Article> _ArticlesCollection = new List<Article>();

        public List<Article> ArticlesCollection { get {
                return _ArticlesCollection;
            } set {
                SetProperty(ref _ArticlesCollection, value);
            } }

     
        public DelegateCommand<Article> ViewDetailsCommand { get; set; }
        public ArticlesSectionViewModel(INavigationService navigationService , INewsFeedApiHandler newsFeed) : base(navigationService)
        {
            Title = "Articles";
            this.navigationService = navigationService;
            ViewDetailsCommand = new DelegateCommand<Article>(ViewDetailsCommandActionMethod);
            _newsFeed = newsFeed;
            getArticlesAsync();
        }

        private void ViewDetailsCommandActionMethod(Article obj)
        {
            if (obj == null) return;
            var parameters = new NavigationParameters();
            parameters.Add("Article", obj);
            navigationService.NavigateAsync(nameof(NewsDetailsView), parameters);
        }

        private async Task getArticlesAsync()
        {
            bool IsInternetConnected = checkInternetConnectivity().Result;
            if (!IsInternetConnected)
                return;
            List<string> ArticlesSource = new List<string>();
            ArticlesSource.Add("the-next-web");
            ArticlesSource.Add("associated-press");
            Loading = true;
            foreach (var item in ArticlesSource)
            {
            var Articles = await _newsFeed.GetArticles(item);

                _ArticlesCollection.AddRange(Articles);
            }
            Loading = false;
            ArticlesCollection = _ArticlesCollection.OrderByDescending(a=>a.publishedAt).ToList();
        }
        
    }
}
