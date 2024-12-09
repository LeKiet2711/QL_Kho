﻿using QL_Kho.Models;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Service
{
    public class QLNguoiDungNhomNguoiDung_Service
    {
        private readonly AppDbContext _context;

        public QLNguoiDungNhomNguoiDung_Service(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QlNguoiDungNhomNguoiDung>> GetAllNhomQuyen(string maNhomNguoiDung)
        {
            return await _context.QlNguoiDungNhomNguoiDungs
                .Where(n => n.MaNhomNguoiDung == maNhomNguoiDung)
                .ToListAsync();
        }

        public async Task AddUserToNhomQuyen(QlNguoiDungNhomNguoiDung newUser)
        {
            _context.QlNguoiDungNhomNguoiDungs.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserFromNhomQuyen(string tenDangNhap, string maNhomNguoiDung)
        {
            var user = await _context.QlNguoiDungNhomNguoiDungs
                .FirstOrDefaultAsync(n => n.TenDangNhap == tenDangNhap && n.MaNhomNguoiDung == maNhomNguoiDung);
            if (user != null)
            {
                _context.QlNguoiDungNhomNguoiDungs.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<QlNguoiDungNhomNguoiDung>> GetAllAssignedUsers()
        {
            return await _context.QlNguoiDungNhomNguoiDungs.ToListAsync();
        }

    }
}
