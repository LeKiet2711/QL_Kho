using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class ChiTietPhieuNhap_Service
    {
        public readonly AppDbContext _dbconnect;
        public ChiTietPhieuNhap_Service(AppDbContext dbConnect)
        {
            _dbconnect= dbConnect;
        }

        public async Task<List<XNK_NhapKhoRawData>> GetAllChiTietPhieuNhap()
        {
            return await _dbconnect.XNK_NhapKhoRawData.Where(ctpn=>ctpn.IsDeleted==false).ToListAsync();
        }

        public async Task<bool> AddChiTietPhieuNhap(XNK_NhapKhoRawData ctpn)
        {
           await _dbconnect.XNK_NhapKhoRawData.AddAsync(ctpn);
            _dbconnect.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteChiTietPhieuNhap(int id)
        {
            var lstCTPN = await _dbconnect.XNK_NhapKhoRawData.Where(ctpn => ctpn.NhapKhoId == id && ctpn.IsDeleted == false).ToListAsync();
            if (lstCTPN != null)
            {
                foreach (var ctpn in lstCTPN)
                {
                    ctpn.IsDeleted = true;
                }

                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
