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
        public List<News> ContexPosts = new List<News>();

        [JsonProperty("Content")]
        public String Content { get; set; }

        [JsonProperty("Links")]
        public List<String> Links { get; set; }

        [JsonProperty("Source")]
        public List<String> Source { get; set; }

        public News(IWebDriver Browser = null, String content = null, List<string> links = null, List<string> source = null)
        {
        
            if(Browser != null)
            {
                List<IWebElement> postContentFind = Browser.FindElements(By.CssSelector(".wall_post_cont._wall_post_cont .wall_post_text")).ToList();

                foreach(IWebElement allPosts in postContentFind)
                {
                    bool check = Browser.FindElement(By.CssSelector(".wall_post_text a")).Displayed;
                    if (Browser.FindElement(By.CssSelector(".wall_post_text a")).Displayed)
                    {
                        allPosts.Click();
                    }
                    allPosts.Click();

                    System.Threading.Thread.Sleep(400);

                    List<IWebElement> postContent = Browser.FindElements(By.CssSelector("#wk_content .wall_post_text")).ToList();
                    IWebElement postText;

                    if (!(postContent.Count > 0))
                    {
                        allPosts.Click();
                        System.Threading.Thread.Sleep(400);
                        postText = Browser.FindElement(By.CssSelector("#wk_content .wall_post_text"));

                    } else {
                        postText = Browser.FindElement(By.CssSelector("#wk_content .wall_post_text"));
                    }
                    List<IWebElement> postLinksInContent = Browser.FindElements(By.CssSelector(".wall_post_text a")).ToList();
                    List<IWebElement> postLinksToImages = Browser.FindElements(By.CssSelector(".page_post_sized_thumbs a")).ToList();

                    List<string> postLinks = new List<string>();
                    List<string> postSourceImg = new List<string>();

                    foreach (IWebElement path in postLinksToImages)
                    {
                        String src = path.GetCssValue("background-image");
                        if (src != "" ||
                           src != null)
                            postSourceImg.Add(src);
                    }

                    foreach (IWebElement path in postLinksInContent)
                    {
                        if (!(path.GetAttribute("href") == null))
                        {
                            postLinks.Add(path.GetAttribute("href"));
                        }
                    }

                    content = postText.Text;
                    source = postSourceImg;
                    links = postLinks;

                    
                    ContexPosts.Add(new News{Content = content, Source = source, Links = links});

                    IWebElement closePost = Browser.FindElement(By.CssSelector(".wk_right_nav.no_select"));
                    closePost.Click();
                    System.Threading.Thread.Sleep(300);
                }
                Content = content;
                Source = source;
                Links = links;       
            }
        }
    }
}
