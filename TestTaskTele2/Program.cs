using TestTaskTele2;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var databaseManager = new DatabaseManager();
databaseManager.fillDatabase();
var viewManager = new ViewManager();

app.MapGet("/users/age/{ageStart}-{ageStop}", (int ageStart, int ageStop) => viewManager.displayAgePart(ageStart, ageStop));
app.MapGet("/users/{sex}", (string sex) => viewManager.displayGenderPart(sex));
app.MapGet("/users", () => viewManager.displayAllUsers());
app.MapGet("/", () => viewManager.displayMainPage());
app.MapGet("/users/detail/{id}", (string id) => viewManager.displayDetailPage(id));

app.Run();

