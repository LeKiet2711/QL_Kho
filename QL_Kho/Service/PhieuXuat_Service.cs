using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class PhieuXuat_Service
    {
        public readonly AppDbContext _dbconnect;
        public PhieuXuat_Service(AppDbContext dbconnect)
        {
            _dbconnect = dbconnect;
        }

        public async Task<List<XNK_XuatKho>> GetPhieuXuat()
        {
            return await _dbconnect.XNK_XuatKho.Where(px => px.IsDeleted == false).ToListAsync();
        }

        public async Task<List<DanhMucNCC>> GetDanhMucNCC()
        {
            return await _dbconnect.DanhMucNCC
                           .Where(ncc => ncc.IsDeleted == false) // Lọc các bản ghi chưa bị xóa
                           .ToListAsync();
        }

        public async Task<List<DanhMucKho>> GetDanhMucKho()
        {
            return await _dbconnect.DanhMucKho
                           .Where(kho => kho.IsDeleted == false) // Lọc các bản ghi chưa bị xóa
                           .ToListAsync();
        }

        public async Task<List<DanhMucSanPham>> GetDanhMucSanPham()
        {
            return await _dbconnect.DanhMucSanPham
                           .Where(sp => sp.IsDeleted == false) // Lọc các bản ghi chưa bị xóa
                           .ToListAsync();
        }

        public async Task<bool> IsSoPhieuXuatExists(string soPhieuXuat, string ID)
        {
            var result = await (from px in _dbconnect.XNK_XuatKho
                                where px.SoPhieuXuat == soPhieuXuat && px.SoPhieuXuat != ID && px.IsDeleted == false
                                select px).AnyAsync();
            return result;
        }

        public async Task<bool> AddPhieuXuat(XNK_XuatKho phieuxuat)
        {
            await _dbconnect.XNK_XuatKho.AddAsync(phieuxuat);
            _dbconnect.SaveChanges();
            return true;
        }

        public async Task<bool> UpdatePhieuXuat(XNK_XuatKho phieuxuat)
        {
            var existingPX = _dbconnect.XNK_XuatKho.FirstOrDefault(px => px.AutoId == phieuxuat.AutoId && px.IsDeleted == false);
            if (existingPX != null)
            {
                existingPX.SoPhieuXuat = phieuxuat.SoPhieuXuat;
                existingPX.KhoId = phieuxuat.KhoId;
                existingPX.NgayXuatKho = phieuxuat.NgayXuatKho;
                existingPX.GhiChu = phieuxuat.GhiChu;

                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<string> GetTenKhoByPhieuXuatId(int autoid)
        {
            var phieuXuat = await _dbconnect.XNK_XuatKho
                .FirstOrDefaultAsync(px => px.AutoId == autoid && px.IsDeleted == false);

            if (phieuXuat != null)
            {
                var kho = await _dbconnect.DanhMucKho
                    .FirstOrDefaultAsync(k => k.AutoId == phieuXuat.KhoId && k.IsDeleted == false);

                if (kho != null)
                {
                    return kho.TenKho;
                }
            }

            return "Kho không tồn tại";
        }

        public async Task<bool> DeletePhieuXuat(XNK_XuatKho phieuxuat)
        {
            var existingPX = _dbconnect.XNK_XuatKho.FirstOrDefault(px => px.AutoId == phieuxuat.AutoId && px.IsDeleted == false);
            if (existingPX != null)
            {
                existingPX.IsDeleted = true;
                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
