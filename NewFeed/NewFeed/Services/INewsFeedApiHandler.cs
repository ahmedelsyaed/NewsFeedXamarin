using NewFeed.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewFeed.Services
{
    public interface INewsFeedApiHandler
    {
        Task<List<Article>> GetArticles(string Source);
    }
}
