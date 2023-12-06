using VehicleDealershipApp.VehicleDomain.Service.VehicleService;

using VehicleDealershipApp.VehicleDomain.Repository;
using VehicleDealershipApp.VehicleDomain.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IVehicleService>(provider =>
{
    var vehicleRepository = new VehicleRepository(VehicleDataSeeder.GenerateVehicleData());
    return new VehicleService(vehicleRepository);
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run($"https://localhost:{7255}");
