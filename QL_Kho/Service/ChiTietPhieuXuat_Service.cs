using Microsoft.EntityFrameworkCore;
using QL_Kho.AI;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class ChiTietPhieuXuat_Service
    {
        public readonly AppDbContext _dbconnect;
        public ChiTietPhieuXuat_Service(AppDbContext dbConnect)
        {
            _dbconnect = dbConnect;
        }

        public async Task<List<XNK_XuatKhoRawData>> GetAllChiTietPhieuXuat()
        {
            return await _dbconnect.XNK_XuatKhoRawData.Where(ctpx => ctpx.IsDeleted == false).ToListAsync();
        }


        public async Task<bool> AddChiTietPhieuXuat(XNK_XuatKhoRawData ctpx)
        {
            await _dbconnect.XNK_XuatKhoRawData.AddAsync(ctpx);
            _dbconnect.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteChiTietPhieuXuat(int id)
        {
            var lstCTPX = await _dbconnect.XNK_XuatKhoRawData.Where(ctpn => ctpn.XuatKhoId == id && ctpn.IsDeleted == false).ToListAsync();
            if (lstCTPX != null)
            {
                foreach (var ctpn in lstCTPX)
                {
                    ctpn.IsDeleted = true;
                }

                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteChiTietPhieuXuatByXuatKho(int id, int XuatKhoID)
        {
            var lstCTPX = await _dbconnect.XNK_XuatKhoRawData.Where(ctpx => ctpx.XuatKhoId == XuatKhoID && ctpx.IsDeleted == false && ctpx.AutoId == id).ToListAsync();
            if (lstCTPX != null)
            {
                foreach (var ctpx in lstCTPX)
                {
                    ctpx.IsDeleted = true;
                }

                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<XNK_XuatKhoRawData>> GetChiTietByPhieuXuat(int id)
        {
            return await _dbconnect.XNK_XuatKhoRawData.Where(ctpx => ctpx.XuatKhoId == id && ctpx.IsDeleted == false).ToListAsync();
        }

        public async Task<double> GetChiTietPhieuXuatsTotalByID(int id)
        {
            var chitietPhieuXuats = await _dbconnect.XNK_XuatKhoRawData
                                                   .Where(x => x.XuatKhoId == id)
                                                   .SumAsync(x => x.DonGiaXuat * x.SlXuat);
            return (double)chitietPhieuXuats;
        }

        public async Task<List<XuatKhoData>> GetTrainingData()
        {
            return await (from ctpx in _dbconnect.XNK_XuatKhoRawData
                          join px in _dbconnect.XNK_XuatKho on ctpx.XuatKhoId equals px.AutoId
                          where ctpx.IsDeleted == false && px.IsDeleted == false
                          select new XuatKhoData
                          {
                              SlXuat = (float)ctpx.SlXuat,
                              DonGiaXuat = (float)ctpx.DonGiaXuat,
                              NgayXuatKho = px.NgayXuatKho.HasValue ? (float)(px.NgayXuatKho.Value - new DateTime(1970, 1, 1)).TotalDays : 0,
                          }).ToListAsync();
        }

    }
}
