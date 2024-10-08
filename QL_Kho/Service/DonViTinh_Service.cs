﻿using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class DonViTinh_Service
    {
        private readonly AppDbContext _dbconnect;
        public DonViTinh_Service(AppDbContext dbconnect)
        {
            _dbconnect = dbconnect;
        }
        public async Task<List<DanhMucDonViTinh>> GetDanhMucDonViTinh()
        {
            return await _dbconnect.DanhMucDonViTinh
                           .Where(dvt => dvt.IsDeleted==false) // Lọc các bản ghi chưa bị xóa
                           .ToListAsync();
        }

        public async Task<bool> AddDanhMucDonViTinh(DanhMucDonViTinh danhmucdonvitinh)
        {
            await _dbconnect.DanhMucDonViTinh.AddAsync(danhmucdonvitinh);
            await _dbconnect.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsTenDVTExists(string tenDVT, string ID)
        {

            var result = await (from dvt in _dbconnect.DanhMucDonViTinh
                                where dvt.TenDvt == tenDVT && dvt.MaDvt != ID && dvt.IsDeleted == false
                                select dvt).AnyAsync();
            return result;
        }

        public async Task<bool> IsMADVTExists(string maDVT, string ID)
        {

            var result = await (from dvt in _dbconnect.DanhMucDonViTinh
                                where dvt.MaDvt == maDVT && dvt.MaDvt != ID && dvt.IsDeleted == false
                                select dvt).AnyAsync();
            return result;
        }

        public async Task<DanhMucDonViTinh> GetDanhMucDonViTinhByID(int id)
        {
            DanhMucDonViTinh danhMucDonViTinh = await _dbconnect.DanhMucDonViTinh.FirstOrDefaultAsync(x => x.AutoId == id);
            return danhMucDonViTinh;
        }

        public async Task<bool> UpdateDanhMucDonViTinh(DanhMucDonViTinh danhmucdonvitinh)
        {
            var existingEntity = await _dbconnect.DanhMucDonViTinh
        .FirstOrDefaultAsync(x => x.AutoId == danhmucdonvitinh.AutoId && x.IsDeleted == false);

            if (existingEntity != null)
            {
                existingEntity.MaDvt = danhmucdonvitinh.MaDvt;
                existingEntity.TenDvt = danhmucdonvitinh.TenDvt;
                existingEntity.GhiChu = danhmucdonvitinh.GhiChu;

                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDanhMucDonViTinh(DanhMucDonViTinh danhmucdonvitinh)
        {
            var existingEntity = await _dbconnect.DanhMucDonViTinh
        .FirstOrDefaultAsync(x => x.AutoId == danhmucdonvitinh.AutoId && x.IsDeleted==false);

            if (existingEntity != null)
            {
                existingEntity.IsDeleted = true; // Đánh dấu là đã xóa
                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
