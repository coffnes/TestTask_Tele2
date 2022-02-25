using System.Net.Mime;
using System.Text;

namespace TestTaskTele2
{
    public class ViewManager
    {
        public IResult displayAllUsers()
        {
            string temp = string.Empty;
            DatabaseManager dbManager = new DatabaseManager();
            //dbManager.fillDatabse();
            IQueryable<User> users = dbManager.getAllData();
            if (users != null)
            {
                foreach (var e in users)
                {
                    temp += $"<a href='/users/detail/{e.Id}'>{e.Name}</a><br/>";
                }
            }
            return Results.Extensions.Html(@$"<!doctype html>
                                        <html>
                                            <head><title>All users</title></head>
                                            <body>
                                                <h1>All users</h1>
                                                {temp}
                                                <br/><a href='/'>Return to main page</a>
                                            </body>
                                        </html>");
        }

        public IResult displayGenderPart(string sex)
        {
            string temp = string.Empty;
            DatabaseManager dbManager = new DatabaseManager();
            //dbManager.fillDatabse();
            IQueryable<User> users = dbManager.getPartialDataSex(sex);
            if (users != null)
            {
                foreach (var e in users)
                {
                    temp += $"<a href='/users/detail/{e.Id}'>{e.Name}</a><br/>";
                }
            }
            return Results.Extensions.Html(@$"<!doctype html>
                                        <html>
                                            <head><title>{sex}</title></head>
                                            <body>
                                                <h1>{sex} users</h1>
                                                {temp}
                                                <br/><a href='/'>Return to main page</a>
                                            </body>
                                        </html>");
        }

        public IResult displayAgePart(int ageStart, int ageStop)
        {
            string temp = string.Empty;
            DatabaseManager dbManager = new DatabaseManager();
            //dbManager.fillDatabse();
            IQueryable<User> users = dbManager.getPartialDataAge(ageStart, ageStop);
            if (users != null)
            {
                foreach (var e in users)
                {
                    temp += $"<a href='/users/detail/{e.Id}'>{e.Name}</a><br/>";
                }
            }
            return Results.Extensions.Html(@$"<!doctype html>
                                        <html>
                                            <head><title>age from {ageStart} to {ageStop}</title></head>
                                            <body>
                                                <h1>Display users with age from {ageStart} to {ageStop}</h1>
                                                {temp}
                                                <br/><a href='/'>Return to main page</a>
                                            </body>
                                        </html>");
        }

        public IResult displayMainPage()
        {
            return Results.Extensions.Html(@$"<!doctype html>
                                        <html>
                                            <head><title>Main page</title></head>
                                            <body>
                                                <h1>Main page</h1>
                                                <a href='/users'>Display all users</a><br/>
                                                <a href='/users/male'>Display male</a><br/>
                                                <a href='/users/female'>Display female</a><br/>
                                                <a href='/users/age/15-30'>Display users with defenite age</a><br/>
                                            </body>
                                        </html>");
        }

        public IResult displayDetailPage(string id)
        {
            string temp = string.Empty;
            DatabaseManager dbManager = new DatabaseManager();
            User currentUser = dbManager.getCurrentUser(id);
            if (currentUser != null)
            {
                temp += $"<p>Name: {currentUser.Name} </br> Gender: {currentUser.Sex} </br> Age: {currentUser.Age}</p><br/>";
            }
            return Results.Extensions.Html(@$"<!doctype html>
                                        <html>
                                            <head><title>Detail page</title></head>
                                            <body>
                                                <h1>Detail page of {currentUser.Name}</h1>
                                                {temp}
                                                <br/><a href='/'>Return to main page</a>
                                            </body>
                                        </html>");
        }
    }

    static class ResultsExtensions
    {
        public static IResult Html(this IResultExtensions resultExtensions, string html)
        {
            ArgumentNullException.ThrowIfNull(resultExtensions);

            return new HtmlResult(html);
        }
    }

    class HtmlResult : IResult
    {
        private readonly string _html;

        public HtmlResult(string html)
        {
            _html = html;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = MediaTypeNames.Text.Html;
            httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(_html);
            return httpContext.Response.WriteAsync(_html);
        }
    }
}
