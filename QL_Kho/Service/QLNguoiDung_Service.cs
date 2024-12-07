using QL_Kho.Models;
using Microsoft.EntityFrameworkCore;
using Blazored.SessionStorage;
using Telerik.SvgIcons;

namespace QL_Kho.Service
{
    public class QLNguoiDung_Service
    {
        private readonly AppDbContext _context;
        private readonly ISessionStorageService _sessionStorage;
        public string currentTenDangNhap { get; set; }
        public string currentMatKhau { get; set; }
        public QLNguoiDung_Service(AppDbContext context, ISessionStorageService sessionStorage)
        {
            _context = context;
            _sessionStorage = sessionStorage;
        }

        public async Task<QlNguoiDung?> ValidateUserAsync(string tenDangNhap, string matKhau)
        {
            await _sessionStorage.SetItemAsync("currentUserName", tenDangNhap);
            await _sessionStorage.SetItemAsync("currentPassWord", matKhau);
            return await _context.QlNguoiDungs
                .FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap && u.MatKhau == matKhau);
        }

        public async Task<string> GetCurrentUserName()
        {
            return await _sessionStorage.GetItemAsync<string>("currentUserName");
        }

        public async Task<string> GetCurrentPassWord()
        {
            return await _sessionStorage.GetItemAsync<string>("currentPassWord");
        }
        public async Task Logout()
        {
            string username = await _sessionStorage.GetItemAsync<string>("currentUserName");
            await _sessionStorage.RemoveItemAsync("currentUserName");
            await _sessionStorage.RemoveItemAsync("currentPassWord");
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
    }
}