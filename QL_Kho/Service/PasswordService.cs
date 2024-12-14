using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;
namespace QL_Kho.Service
{
    public class PasswordService
    {
        private readonly AppDbContext _dbconnect;
        public PasswordService(AppDbContext dbconnect)
        {
            _dbconnect = dbconnect;
        }
        public async Task<bool> UpdataTTnguoidung(string tenDangNhap, string matKhauMoi)
        {
            // Tìm người dùng theo TenDangNhap
            var existingEntity = await _dbconnect.QlNguoiDungs
                .FirstOrDefaultAsync(x => x.TenDangNhap == tenDangNhap && x.IsDeleted == false);

            if (existingEntity != null)
            {
                // Cập nhật mật khẩu
                existingEntity.MatKhau = matKhauMoi;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbconnect.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
   
}
