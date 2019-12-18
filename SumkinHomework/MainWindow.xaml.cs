using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using OpenQA.Selenium;
using Newtonsoft.Json;
using System.Threading;

namespace SumkinHomework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public static IWebDriver Browser;
        public String pathToJSON = @"C:\Users\Senco\source\repos\SumkinHomework\SumkinHomework\bin\Debug/post";
        public MainWindow()
        {
            InitializeComponent();
           
        }
        private void FirstThread() 
        {
           
        }
        private void SecondThread()
        {
           
        }
        private void ThirdThread()
        {
            
        }
        private void FourthThread()
        {
/*            Browser4 = new OpenQA.Selenium.Chrome.ChromeDriver();
            Browser4.Manage().Window.Maximize();

            Browser4.Navigate().GoToUrl("https://habr.com/en/top/monthly/");

            LaunchMultiThread(Browser4, "4");*/
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            Browser.Manage().Window.Maximize();

            Browser.Navigate().GoToUrl("https://vk.com/awdee");

            List<IWebElement> elements = Browser.FindElements(By.CssSelector(".wall_post_cont._wall_post_cont")).ToList();
            
            if (disMult.IsChecked == false)
            {
                
            } else{

                News currentStack = new News(Browser);

                JsonSerializer serializer = new JsonSerializer();
                string output = JsonConvert.SerializeObject(currentStack);

                using (StreamWriter sw = new StreamWriter(pathToJSON + "1.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, output);
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { 
/*            List<IWebElement> elements = Browser1.FindElements(By.CssSelector(".content-list__item_post article h2 a")).ToList();
            elements[1].Click();

            System.Threading.Thread.Sleep(1000);

            News currentPost = new News(Browser1);

            textBox1.AppendText(currentPost.Article);
            textBox2.AppendText(currentPost.Content);

            currentPost.Links.ForEach(x => textBox3.AppendText("\n Cсылка:" + x + "\n"));
            currentPost.Source.ForEach(x => textBox3.AppendText("\n Путь к картинке:" + x  + "\n"));


            JsonSerializer serializer = new JsonSerializer();
            string output = JsonConvert.SerializeObject(currentPost);

            using (StreamWriter sw = new StreamWriter(pathToJSON))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, output);
            }
*/
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            String json = JsonConvert.DeserializeObject<String>(File.ReadAllText(pathToJSON));
            News post = JsonConvert.DeserializeObject<News>(json);

            textBox2.AppendText(post.Content);

            post.Links.ForEach(x => textBox3.AppendText("\n Cсылка:" + x + "\n"));
            post.Source.ForEach(x => textBox3.AppendText("\n Путь к картинке:" + x + "\n"));
        }  

        }

}
