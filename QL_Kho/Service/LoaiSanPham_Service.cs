using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class LoaiSanPham_Service
    {
        private readonly AppDbContext _dbconnect;
        public LoaiSanPham_Service(AppDbContext dbconnect)
        {
            _dbconnect = dbconnect;
        }

        public async Task<List<DanhMucLoaiSanPham>> GetDanhMucLSP()
        {
            return await _dbconnect.DanhMucLoaiSanPham.Where(lsp => lsp.IsDeleted == false).ToListAsync();
        }

        public async Task<bool> AddDanhMucLSP(DanhMucLoaiSanPham danhmucloaisanpham)
        {
            await _dbconnect.DanhMucLoaiSanPham.AddAsync(danhmucloaisanpham);
            await _dbconnect.SaveChangesAsync();
            return true;
        }

        public async Task<DanhMucLoaiSanPham> GetDanhMucLSPByID(int id)
        {
            DanhMucLoaiSanPham lsp = await _dbconnect.DanhMucLoaiSanPham.FirstOrDefaultAsync(lsp => lsp.AutoId == id);
            return lsp;
        }

        public async Task<bool> UpdateDanhMucLSP(DanhMucLoaiSanPham danhmuclsp)
        {
            var existingEntity = await _dbconnect.DanhMucLoaiSanPham.FirstOrDefaultAsync(lsp => lsp.MaLsp == danhmuclsp.MaLsp && lsp.IsDeleted == false);
            if (existingEntity != null)
            {
                existingEntity.MaLsp = danhmuclsp.MaLsp;
                existingEntity.TenLsp = danhmuclsp.TenLsp;
                existingEntity.GhiChu = danhmuclsp.GhiChu;

                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDanhMucLSP(DanhMucLoaiSanPham danhmuclsp)
        {
            var existingEntity = await _dbconnect.DanhMucLoaiSanPham.FirstOrDefaultAsync(lsp => lsp.AutoId == danhmuclsp.AutoId && lsp.IsDeleted == false);

            if (existingEntity != null)
            {
                existingEntity.IsDeleted = true;
                await _dbconnect.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> IsTenLSPExists(string tenLSP, string ID)
        {

            var result = await (from lsp in _dbconnect.DanhMucLoaiSanPham
                                where lsp.TenLsp == tenLSP && lsp.MaLsp != ID && lsp.IsDeleted == false
                                select lsp).AnyAsync();
            return result;
        }

        public async Task<bool> IsMaLSPExists(string maLSP, string ID)
        {

            var result = await (from lsp in _dbconnect.DanhMucLoaiSanPham
                                where lsp.MaLsp == maLSP && lsp.MaLsp != ID && lsp.IsDeleted == false
                                select lsp).AnyAsync();
            return result;
        }
    }
}
