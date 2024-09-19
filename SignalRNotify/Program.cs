using SignalRNotify.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
var SignalROrigins = "_signalROrigins";

builder.Services.AddCors(opt=>{
    opt.AddPolicy(name:SignalROrigins,
    policy=>{
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseCors(SignalROrigins);
app.MapHub<NotifyHub>("/notifyHub");

app.Run();
