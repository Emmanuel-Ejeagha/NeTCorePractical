using NBomber.CSharp;
using NBomber.Http.CSharp;

using var httpClient = new HttpClient();

var scenario = Scenario.Create("search_mom",
    async context =>
    {
        var request = Http.CreateRequest("GET", "http://localhost:5278/search?searchText=M");

        var response = await Http.Send(httpClient, request);
        return response;
    })
.WithoutWarmUp()
.WithLoadSimulations(Simulation.Inject(
    rate: 100,
    interval: TimeSpan.FromSeconds(1),
    during: TimeSpan.FromSeconds(30))
);

var scenario1 = Scenario.Create("search_movie",
    async context =>
    {
        var request = Http.CreateRequest("GET", "http://localhost:5278/search?searchText=r");

        var response = await Http.Send(httpClient, request);
        return response;
    })
    .WithoutWarmUp()
    .WithLoadSimulations(Simulation.Inject(
        rate: 100,

        interval: TimeSpan.FromSeconds(1),
        during: TimeSpan.FromSeconds(30))
);



NBomberRunner
   .RegisterScenarios(scenario, scenario1)

   .Run();