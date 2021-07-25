using NewFeed.Configs;
using NewFeed.Interface;
using NewFeed.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewFeed.Services
{
    public class ApiResponseHandler : INewsFeedApiHandler
    {

        public async Task<List<Article>> GetArticles(string Source)
        {
            try
            {
            var Response = RestService.For<INewsApi>(ApiConfigs.API_URL);
            var NewsFeedResponse = await Response.GetNewsFeed(Source, ApiConfigs.API_Key);

            if (NewsFeedResponse.status != "ok")
                return null; 
                return NewsFeedResponse.articles;
            }
            catch (Exception)
            {
                return null;
            }
           
        }

    }
}
