using NewFeed.Services;
using NewFeed.ViewModels;
using NewFeed.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace NewFeed
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("FlyoutPage/NavigationPage/ArticlesSection");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<FlyoutPage, FlyoutPageViewModel>();
            containerRegistry.RegisterForNavigation<ArticlesSection, ArticlesSectionViewModel>();

            containerRegistry.RegisterInstance<INewsFeedApiHandler>(new ApiResponseHandler());
            containerRegistry.RegisterForNavigation<NewsDetailsView, NewsDetailsViewViewModel>();
        }
    }
}
