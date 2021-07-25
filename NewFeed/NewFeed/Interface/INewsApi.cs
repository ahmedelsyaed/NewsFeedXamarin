using NewFeed.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewFeed.Interface
{
    public interface INewsApi
    {
        [Get("/v1/articles?source={source}")]
        Task<NewsObject> GetNewsFeed(string source, string apiKey);
    }
}
