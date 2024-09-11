using Microsoft.EntityFrameworkCore;
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
            return await _dbconnect.DanhMucDonViTinh.ToListAsync();
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
                                where dvt.TenDvt == tenDVT && dvt.MaDvt != ID
                                select dvt).AnyAsync();
            return result;
        }

        public async Task<bool> IsMADVTExists(string maDVT, string ID)
        {

            var result = await (from dvt in _dbconnect.DanhMucDonViTinh
                                where dvt.MaDvt == maDVT && dvt.MaDvt != ID
                                select dvt).AnyAsync();
            return result;
        }

        public async Task<DanhMucDonViTinh> GetDanhMucDonViTinhByID(string id)
        {
            DanhMucDonViTinh danhMucDonViTinh = await _dbconnect.DanhMucDonViTinh.FirstOrDefaultAsync(x => x.MaDvt == id);
            return danhMucDonViTinh;
        }

        public async Task<bool> UpdateDanhMucDonViTinh(DanhMucDonViTinh danhmucdonvitinh)
        {
            var existingEntity = await _dbconnect.DanhMucDonViTinh
                .FirstOrDefaultAsync(x => x.MaDvt == danhmucdonvitinh.MaDvt);
            if (existingEntity != null)
            {
                existingEntity.TenDvt = danhmucdonvitinh.TenDvt;
                existingEntity.GhiChu = danhmucdonvitinh.GhiChu;
                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false; // Không tìm thấy bản ghi để cập nhật
        }


        public async Task<bool> DeleteDanhMucDonViTinh(DanhMucDonViTinh danhmucdonvitinh)
        {
            var existingEntity = await _dbconnect.DanhMucDonViTinh
                .FirstOrDefaultAsync(x => x.MaDvt == danhmucdonvitinh.MaDvt);
            if (existingEntity != null)
            {
                _dbconnect.DanhMucDonViTinh.Remove(existingEntity);
                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false; // Không tìm thấy bản ghi để xóa
        }


    }
}
