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
            var lstCTPN = await _dbconnect.XNK_NhapKhoRawData.Where(ctpn => ctpn.NhapKhoId == id && ctpn.IsDeleted == false ).ToListAsync();
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

        public async Task<bool> DeleteChiTietPhieuNhapByNhapKho(int id, int NhapKhoID)
        {
            var lstCTPN = await _dbconnect.XNK_NhapKhoRawData.Where(ctpn => ctpn.NhapKhoId == NhapKhoID && ctpn.IsDeleted == false && ctpn.AutoId == id).ToListAsync();
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

        public async Task<List<XNK_NhapKhoRawData>> GetChiTietByPhieuNhap(int id)
        {
            return await _dbconnect.XNK_NhapKhoRawData.Where(ctpn => ctpn.NhapKhoId == id && ctpn.IsDeleted == false).ToListAsync();
        }

        public async Task<double> GetChiTietPhieuNhapsTotalByID(int id)
        {
            var chitietPhieuNhaps = await _dbconnect.XNK_NhapKhoRawData
                                                   .Where(x => x.NhapKhoId == id)
                                                   .SumAsync(x => x.DonGiaNhap * x.SlNhap);
            return (double)chitietPhieuNhaps;
        }

    }
}
