using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using OpenQA.Selenium;




namespace SumkinHomework
{

    class News
    {
        [JsonProperty("Article")]
        public String Article { get; set; }

        [JsonProperty("Content")]
        public String Content { get; set; }

        [JsonProperty("Links")]
        public List<String> Links { get; set; }

        [JsonProperty("Source")]
        public List<String> Source { get; set; }

        public News(IWebDriver Browser)
        {
            IWebElement postArticleFind = Browser.FindElement(By.CssSelector(".post.post_full h1.post__title span"));
            IWebElement postContentFind = Browser.FindElement(By.CssSelector(".post.post_full .post__body.post__body_full .post__text"));

            List<IWebElement> postLinksInContent = Browser.FindElements(By.CssSelector(".post.post_full .post__body.post__body_full .post__text a")).ToList();
            List<IWebElement> postLinksToImages = Browser.FindElements(By.CssSelector(".post.post_full .post__body.post__body_full .post__text img")).ToList();

            List<IWebElement> postSumLinks = postLinksInContent.Union(postLinksToImages).ToList();

            List<string> postLinks = new List<string>();
            List<string> postSourceImg = new List<string>();

            foreach (IWebElement links in postSumLinks)
            {
                if (links.GetAttribute("href") != null)
                {
                    postLinks.Add(links.GetAttribute("href"));
                }
                else if (
                    links.GetAttribute("src") != null ||
                    links.GetAttribute("src") != ""
                )
                {
                    postSourceImg.Add(links.GetAttribute("src"));
                }
            }

            Article = postArticleFind.Text;
            Content = postContentFind.Text;
            Source = postSourceImg;
            Links = postLinks;

        }
    }
}
