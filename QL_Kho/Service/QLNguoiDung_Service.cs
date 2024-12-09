using QL_Kho.Models;
using Microsoft.EntityFrameworkCore;
using Blazored.SessionStorage;
using Telerik.SvgIcons;
using Microsoft.JSInterop;

namespace QL_Kho.Service
{
    public class QLNguoiDung_Service
    {
        private readonly AppDbContext _context;
        private readonly ISessionStorageService _sessionStorage;
        private readonly IJSRuntime JSRuntime;
        public string currentTenDangNhap { get; set; }
        public string currentMatKhau { get; set; }
        public QLNguoiDung_Service(AppDbContext context, ISessionStorageService sessionStorage, IJSRuntime jSRuntime)
        {
            _context = context;
            _sessionStorage = sessionStorage;
            JSRuntime = jSRuntime;
        }

        public async Task<QlNguoiDung?> ValidateUserAsync(string tenDangNhap, string matKhau)
        {
            await _sessionStorage.SetItemAsync("currentUserName", tenDangNhap);
            await _sessionStorage.SetItemAsync("currentPassWord", matKhau);
            return await _context.QlNguoiDungs
                .FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap && u.MatKhau == matKhau);
        }

        public async Task<string> GetCurrentTenDangNhap()
        {
            if (string.IsNullOrEmpty(currentTenDangNhap))
            {
                currentTenDangNhap = await _sessionStorage.GetItemAsync<string>("currentUserName");
            }
            return currentTenDangNhap;
        }

        public async Task<string> GetCurrentPassWord()
        {
            return await _sessionStorage.GetItemAsync<string>("currentPassWord");
        }
        public async Task Logout()
        {
            await _sessionStorage.RemoveItemAsync("currentUserName");
            await _sessionStorage.RemoveItemAsync("currentPassWord");
            await JSRuntime.InvokeVoidAsync("localStorage.removeItem", "currentTenDangNhap");
            currentTenDangNhap = null;
            currentMatKhau = null;
        }

        public async Task<List<QlNguoiDung>> GetAllTenDangNhapAsync()
        {
            return await _context.QlNguoiDungs
                .ToListAsync();
        }

        public async Task<List<QlNhomNguoiDung>> GetAllNhomNguoiDung()
        {
            return await _context.QlNhomNguoiDungs
                .ToListAsync();
        }

        public async Task<string> GetUserRole(string tenDangNhap)
        {
            var userRole = await _context.QlNguoiDungNhomNguoiDungs
                .Where(n => n.TenDangNhap == tenDangNhap)
                .Select(n => n.MaNhomNguoiDung)
                .FirstOrDefaultAsync();

            return userRole;
        }


    }
}
