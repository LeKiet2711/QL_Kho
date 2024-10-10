using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class NhaCungCap_Service
    {
        private readonly AppDbContext _dbconnect;
        public NhaCungCap_Service(AppDbContext dbconnect)
        {
            _dbconnect = dbconnect;
        }

        public async Task<List<DanhMucNCC>> GetDanhMucNCC()
        {
            return await _dbconnect.DanhMucNCC
                           .Where(ncc => ncc.IsDeleted == false) // Lọc các bản ghi chưa bị xóa
                           .ToListAsync();
        }

        public async Task<bool> AddDanhMucNCC(DanhMucNCC DanhMucNCC)
        {
            await _dbconnect.DanhMucNCC.AddAsync(DanhMucNCC);
            await _dbconnect.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsTenNCCExists(string tenNCC, string ID)
        {
            var result = await (from ncc in _dbconnect.DanhMucNCC
                                where ncc.TenNcc == tenNCC && ncc.MaNcc != ID && ncc.IsDeleted == false
                                select ncc).AnyAsync();
            return result;
        }

        public async Task<bool> IsMaNCCExists(string maNCC, string ID)
        {
            var result = await (from ncc in _dbconnect.DanhMucNCC
                                where ncc.MaNcc == maNCC && ncc.MaNcc != ID && ncc.IsDeleted == false
                                select ncc).AnyAsync();
            return result;
        }

        public async Task<DanhMucNCC> GetDanhMucNCCByID(int id)
        {
            DanhMucNCC DanhMucNCC = await _dbconnect.DanhMucNCC.FirstOrDefaultAsync(x => x.AutoId == id);
            return DanhMucNCC;
        }

        public async Task<bool> UpdateDanhMucNCC(DanhMucNCC DanhMucNCC)
        {
            var existingEntity = await _dbconnect.DanhMucNCC
                .FirstOrDefaultAsync(x => x.AutoId == DanhMucNCC.AutoId && x.IsDeleted == false);

            if (existingEntity != null)
            {
                existingEntity.MaNcc = DanhMucNCC.MaNcc;
                existingEntity.TenNcc = DanhMucNCC.TenNcc;
                existingEntity.GhiChu = DanhMucNCC.GhiChu;

                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDanhMucNCC(DanhMucNCC DanhMucNCC)
        {
            var existingEntity = await _dbconnect.DanhMucNCC
                .FirstOrDefaultAsync(x => x.AutoId == DanhMucNCC.AutoId && x.IsDeleted == false);

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
