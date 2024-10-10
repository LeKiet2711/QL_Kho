using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class Kho_Service
    {
        private readonly AppDbContext _dbconnect;
        public Kho_Service(AppDbContext dbconnect)
        {
            _dbconnect = dbconnect;
        }

        public async Task<List<DanhMucKho>> GetDanhMucKho()
        {
            return await _dbconnect.DanhMucKho
                           .Where(kho => kho.IsDeleted == false) // Lọc các bản ghi chưa bị xóa
                           .ToListAsync();
        }

        public async Task<bool> AddDanhMucKho(DanhMucKho danhmuckho)
        {
            await _dbconnect.DanhMucKho.AddAsync(danhmuckho);
            await _dbconnect.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsTenKhoExists(string tenKho, string ID)
        {
            var result = await (from kho in _dbconnect.DanhMucKho
                                where kho.TenKho == tenKho && kho.MaKho != ID && kho.IsDeleted == false
                                select kho).AnyAsync();
            return result;
        }

        public async Task<bool> IsMaKhoExists(string maKho, string ID)
        {
            var result = await (from kho in _dbconnect.DanhMucKho
                                where kho.MaKho == maKho && kho.MaKho != ID && kho.IsDeleted == false
                                select kho).AnyAsync();
            return result;
        }

        public async Task<DanhMucKho> GetDanhMucKhoByID(int id)
        {
            DanhMucKho danhMucKho = await _dbconnect.DanhMucKho.FirstOrDefaultAsync(x => x.AutoId == id);
            return danhMucKho;
        }

        public async Task<bool> UpdateDanhMucKho(DanhMucKho danhmuckho)
        {
            var existingEntity = await _dbconnect.DanhMucKho
                .FirstOrDefaultAsync(x => x.AutoId == danhmuckho.AutoId && x.IsDeleted == false);

            if (existingEntity != null)
            {
                existingEntity.MaKho = danhmuckho.MaKho;
                existingEntity.TenKho = danhmuckho.TenKho;
                existingEntity.GhiChu = danhmuckho.GhiChu;

                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDanhMucKho(DanhMucKho danhmuckho)
        {
            var existingEntity = await _dbconnect.DanhMucKho
                .FirstOrDefaultAsync(x => x.AutoId == danhmuckho.AutoId && x.IsDeleted == false);

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
