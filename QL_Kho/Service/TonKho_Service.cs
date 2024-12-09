using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class TonKho_Service
    {
        private readonly string _connectionString;
        public TonKho_Service(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<TonKho>> BaoCaoTonKhoAsync(DateTime ngayNhap, DateTime ngayXuat)
        {
            var result = new List<TonKho>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("BaoCaoTonKho", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NgayNhap", ngayNhap);
                    command.Parameters.AddWithValue("@NgayXuat", ngayXuat);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var report = new TonKho
                            {
                                MaSanPham = reader["Ma_San_Pham"].ToString(),
                                TenSanPham = reader["Ten_San_Pham"].ToString(),
                                SLDauKy = reader.GetInt32(reader.GetOrdinal("SLDauKy")),
                                SLNhap = reader.GetInt32(reader.GetOrdinal("SLNhap")),
                                SLXuat = reader.GetInt32(reader.GetOrdinal("SLXuat")),
                                SLCuoiKy = reader.GetInt32(reader.GetOrdinal("SLCuoiKy"))
                            };
                            result.Add(report);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<List<NhapKho>> GetNhapKhoByDateRangeAsync(DateTime dateFrom, DateTime dateTo)
        {
            var result = new List<NhapKho>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_GetNhapKhoByDateRange", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DATEFROM", dateFrom);
                    command.Parameters.AddWithValue("@DATETO", dateTo);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var nhapKho = new NhapKho
                            {
                                SoPhieuNhap = reader["So_Phieu_Nhap"].ToString(),
                                NgayNhapKho = reader.GetDateTime(reader.GetOrdinal("Ngay_Nhap_Kho")),
                                TenNCC = reader["Ten_NCC"].ToString(),
                                SanPhamID = reader.GetInt32(reader.GetOrdinal("San_Pham_ID")),
                                SLNhap = reader.GetDecimal(reader.GetOrdinal("SL_Nhap")),
                                DonGiaNhap = reader.GetDecimal(reader.GetOrdinal("Don_Gia_Nhap")),
                                TenSanPham = reader["Ten_San_Pham"].ToString()
                            };
                            result.Add(nhapKho);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<List<XuatKho>> GetXuatKhoByDateRangeAsync(DateTime dateFrom, DateTime dateTo)
        {
            var result = new List<XuatKho>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_GetXuatKhoByDateRange", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DATEFROM", dateFrom);
                    command.Parameters.AddWithValue("@DATETO", dateTo);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var xuatKho = new XuatKho
                            {
                                SoPhieuXuat = reader["So_Phieu_Xuat"].ToString(),
                                NgayXuatKho = reader.GetDateTime(reader.GetOrdinal("Ngay_Xuat_Kho")),
                                SanPhamID = reader.GetInt32(reader.GetOrdinal("San_Pham_ID")),
                                TenSanPham = reader["Ten_San_Pham"].ToString(),
                                SLXuat = reader.GetDecimal(reader.GetOrdinal("SL_Xuat")),
                                DonGiaXuat = reader.GetDecimal(reader.GetOrdinal("Don_Gia_Xuat"))
                            };
                            result.Add(xuatKho);
                        }
                    }
                }
            }
            return result;
        }
    }

    public class NhapKho
    {
        public string SoPhieuNhap { get; set; }
        public DateTime NgayNhapKho { get; set; }
        public string TenNCC { get; set; }
        public int SanPhamID { get; set; }
        public decimal SLNhap { get; set; }
        public decimal DonGiaNhap { get; set; }
        public string TenSanPham { get; set; }
    }
    public class XuatKho
    {
        public string SoPhieuXuat { get; set; }
        public DateTime NgayXuatKho { get; set; }
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public decimal SLXuat { get; set; }
        public decimal DonGiaXuat { get; set; }
    }
}
