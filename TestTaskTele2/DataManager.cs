using System.Net;
using Newtonsoft.Json;

namespace TestTaskTele2
{
    public class DataManager
    {
        public List<User> Users { get; }

        public DataManager()
        {
            Users = webDataToList();
        }

        static List<User> webDataToList()
        {
            string jsonStr = getWebData("http://testlodtask20172.azurewebsites.net/task");

            jsonStr = jsonStr.Replace("[", string.Empty);
            jsonStr = jsonStr.Replace("]", string.Empty);

            var splitJsonStr = jsonStr.Split("},");
            var rightJsonStr = new List<string>();
            for (int i = 0; i < splitJsonStr.Length - 1; i++)
            {
                string tempString = splitJsonStr[i] + "}";
                rightJsonStr.Add(tempString);
            }
            rightJsonStr.Add(splitJsonStr[splitJsonStr.Length - 1]);

            var peoples = new List<UserCommon>();

            foreach (var c in rightJsonStr)
            {
                var temp = JsonConvert.DeserializeObject<UserCommon>(c);
                peoples.Add(temp);
            }

            var main = new List<User>();
            foreach (var c in peoples)
            {
                string tempJson = getWebData("https://testlodtask20172.azurewebsites.net/task/" + c.Id);
                var temporaryUser = JsonConvert.DeserializeObject<UserDetail>(tempJson);
                main.Add(new User(c.Id, c.Name, c.Sex, temporaryUser.Age));
            }

            return main;
        }

        static string getWebData(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    class UserCommon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
    }

    class UserDetail
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }

        public User(string id, string name, string sex, int age)
        {
            Id = id;
            Name = name;
            Sex = sex;
            Age = age;
        }
    }


}
