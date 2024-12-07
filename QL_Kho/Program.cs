using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO;
using Blazored.Toast;
using Microsoft.EntityFrameworkCore;
using QL_Kho.Components;
using QL_Kho.Models;
using QL_Kho.Service;
using Blazored.SessionStorage;





var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages().AddNewtonsoftJson();
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.TryAddSingleton<IReportServiceConfiguration>(sp => new ReportServiceConfiguration
{
	ReportingEngineConfiguration = sp.GetService<IConfiguration>(),
	HostAppId = "QL_Kho",
	Storage = new FileStorage(),
	ReportSourceResolver = new UriReportSourceResolver(
		System.IO.Path.Combine(GetReportsDir(sp)))
});


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DonViTinh_Service>();
builder.Services.AddScoped<LoaiSanPham_Service>();
builder.Services.AddScoped<SanPham_Service>();
builder.Services.AddScoped<Kho_Service>();
builder.Services.AddScoped<NhaCungCap_Service>();
builder.Services.AddScoped<PhieuNhap_Service>();
builder.Services.AddScoped<PhieuXuat_Service>();
builder.Services.AddScoped<ChiTietPhieuNhap_Service>();
builder.Services.AddScoped<ChiTietPhieuXuat_Service>();
builder.Services.AddScoped<QLNguoiDung_Service>();
builder.Services.AddScoped<QLNguoiDungNhomNguoiDung_Service>();
builder.Services.AddScoped<QlNguoiDung>();
builder.Services.AddScoped<TonKho_Service>(provider => new TonKho_Service("Data Source=KIETBANHTRAI\\SQLEXPRESS;Initial Catalog=QL_Kho;Integrated Security=True;"));


builder.Services.AddBlazoredToast();
builder.Services.AddHttpContextAccessor();
builder.Services.AddBlazoredSessionStorage();

var app = builder.Build();

app.UseRouting();
app.UseAntiforgery();
app.UseAntiforgery();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
	// ... 
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static string GetReportsDir(IServiceProvider sp)
{
	return Path.Combine(sp.GetService<IWebHostEnvironment>().ContentRootPath, "Reports");
}
