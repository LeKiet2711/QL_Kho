using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class PhieuNhap_Service
    {
        public readonly AppDbContext _dbconnect;
        public PhieuNhap_Service(AppDbContext dbconnect)
        {
            _dbconnect= dbconnect;
        }

        public async Task<List<XNK_NhapKho>> GetPhieuNhap()
        {
            return await _dbconnect.XNK_NhapKho.Where(pn => pn.IsDeleted == false).ToListAsync();
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

        public async Task<bool> IsSoPhieuNhapExists(string soPhieuNhap, string ID)
        {
            var result = await (from pn in _dbconnect.XNK_NhapKho
                                where pn.SoPhieuNhap == soPhieuNhap && pn.SoPhieuNhap != ID && pn.IsDeleted == false
                                select pn).AnyAsync();
            return result;
        }

        public async Task<bool> AddPhieuNhap(XNK_NhapKho phieunhap)
        {
            await _dbconnect.XNK_NhapKho.AddAsync(phieunhap);
            _dbconnect.SaveChanges();
            return true;
        }

        public async Task<bool> UpdatePhieuNhap(XNK_NhapKho phieunhap)
        {
            var existingPN = _dbconnect.XNK_NhapKho.FirstOrDefault(pn => pn.AutoId == phieunhap.AutoId && pn.IsDeleted == false);
            if (existingPN != null)
            {
                existingPN.SoPhieuNhap = phieunhap.SoPhieuNhap;
                existingPN.NccId = phieunhap.NccId;
                existingPN.KhoId = phieunhap.KhoId;
                existingPN.NgayNhapKho = phieunhap.NgayNhapKho;
                existingPN.GhiChu = phieunhap.GhiChu;

                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeletePhieuNhap(XNK_NhapKho phieunhap)
        {
            var existingPN = _dbconnect.XNK_NhapKho.FirstOrDefault(pn => pn.AutoId == phieunhap.AutoId && pn.IsDeleted == false);
            if (existingPN != null)
            {
                existingPN.IsDeleted = true;
                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
