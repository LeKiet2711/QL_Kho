using Blazored.SessionStorage;
using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class QLNguoiDungNhomNguoiDung_Service
    {
        private readonly AppDbContext _context;
        private readonly ISessionStorageService _sessionStorage;
        public string currentTenDangNhap { get; set; }
        public string currentMatKhau { get; set; }
        public QLNguoiDungNhomNguoiDung_Service(AppDbContext context, ISessionStorageService sessionStorage)
        {
            _context = context;
            _sessionStorage = sessionStorage;
        }

        public async Task<List<QlNguoiDungNhomNguoiDung>> GetAllNhomQuyen()
        {
            return await _context.QlNguoiDungNhomNguoiDungs
                .ToListAsync();
        }

    }
}