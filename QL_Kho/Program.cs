using Blazored.Toast;
using Microsoft.EntityFrameworkCore;
using QL_Kho.Components;
using QL_Kho.Models;
using QL_Kho.Service;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<ChiTietPhieuNhap_Service>();

builder.Services.AddBlazoredToast();

var app = builder.Build();

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
