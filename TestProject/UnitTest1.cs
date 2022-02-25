using Xunit;
using TestTaskTele2;
using Microsoft.AspNetCore.Http;

namespace TestProject
{
    public class UnitTest1
    {
        DataManager dataManager = new DataManager();
        DatabaseManager dbManager = new DatabaseManager();
        ViewManager vManager = new ViewManager();

        [Fact]
        public void testDataAccess()
        {
            dbManager.fillDatabase();
            Assert.NotNull(dbManager.getPartialDataSex("male"));
            Assert.NotNull(dbManager.getPartialDataAge(10, 20));
            Assert.NotNull(dbManager.getAllData());
            Assert.NotNull(dbManager.getCurrentUser("guyqwhoij6"));
        }

        [Fact]
        public void testDataManagement()
        {
            Assert.NotNull(dataManager.Users);
        }

        [Fact]
        public void testViewManagement()
        {
            Assert.NotNull(vManager.displayAllUsers());
            Assert.NotNull(vManager.displayGenderPart("female"));
            Assert.NotNull(vManager.displayDetailPage("guyqwhoij6"));
            Assert.NotNull(vManager.displayAgePart(10, 20));
        }
    }
}