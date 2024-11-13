using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class SanPham_Service
    {
        private readonly AppDbContext _dbconnect;
        public SanPham_Service(AppDbContext dbconnect)
        {
            _dbconnect = dbconnect;
        }

        public async Task<List<DanhMucSanPham>> GetDanhMucSanPham()
        {
            return await _dbconnect.DanhMucSanPham
                           .Where(sp => sp.IsDeleted == false) // Lọc các bản ghi chưa bị xóa
                           .ToListAsync();
        }

        public async Task<List<DanhMucDonViTinh>> GetDanhMucDonViTinh()
        {
            return await _dbconnect.DanhMucDonViTinh
                           .Where(dvt => dvt.IsDeleted == false) // Lọc các bản ghi chưa bị xóa
                           .ToListAsync();
        }
        public async Task<List<DanhMucLoaiSanPham>> GetDanhMucLSP()
        {
            return await _dbconnect.DanhMucLoaiSanPham.Where(lsp => lsp.IsDeleted == false).ToListAsync();
        }

        public async Task<bool> AddDanhMucSanPham(DanhMucSanPham danhmucsanpham)
        {
            await _dbconnect.DanhMucSanPham.AddAsync(danhmucsanpham);
            await _dbconnect.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsMaSanPhamExists(string maSanPham, string ID)
        {
            var result = await (from sp in _dbconnect.DanhMucSanPham
                                where sp.MaSanPham == maSanPham && sp.MaSanPham != ID && sp.IsDeleted == false
                                select sp).AnyAsync();
            return result;
        }

        public async Task<bool> IsTenSanPhamExists(string tenSP,string ID)
        {

            var result = await (from sp in _dbconnect.DanhMucSanPham
                                where sp.TenSanPham == tenSP && sp.MaSanPham != ID && sp.IsDeleted == false
                                select sp).AnyAsync();
            return result;
        }

        public async Task<DanhMucSanPham> GetDanhMucSanPhamByID(int id)
        {
            DanhMucSanPham danhMucSanPham = await _dbconnect.DanhMucSanPham.FirstOrDefaultAsync(x => x.AutoId == id);
            return danhMucSanPham;
        }

        public async Task<bool> UpdateDanhMucSanPham(DanhMucSanPham danhmucsanpham)
        {
            var existingEntity = await _dbconnect.DanhMucSanPham
                .FirstOrDefaultAsync(x => x.AutoId == danhmucsanpham.AutoId && x.IsDeleted == false);

            if (existingEntity != null)
            {
                existingEntity.MaSanPham = danhmucsanpham.MaSanPham;
                existingEntity.TenSanPham = danhmucsanpham.TenSanPham;
                existingEntity.LoaiSanPhamId = danhmucsanpham.LoaiSanPhamId;
                existingEntity.DonViTinhId = danhmucsanpham.DonViTinhId;
                existingEntity.GhiChu = danhmucsanpham.GhiChu;

                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDanhMucSanPham(DanhMucSanPham danhmucsanpham)
        {
            var existingEntity = await _dbconnect.DanhMucSanPham
                .FirstOrDefaultAsync(x => x.AutoId == danhmucsanpham.AutoId && x.IsDeleted == false);

            if (existingEntity != null)
            {
                existingEntity.IsDeleted = true; 
                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public string GetProductName(int sanPhamId)
        {
            var product = _dbconnect.DanhMucSanPham.FirstOrDefault(p => p.AutoId == sanPhamId);
            return product.TenSanPham;
        }
    }
}
