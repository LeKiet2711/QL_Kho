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
    }
}