using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using Telerik.DataSource.Extensions;



namespace QL_Kho.Service
{
    public class SendMail_Service
    {
        private readonly AppDbContext _dbconnect;
        public SendMail_Service(AppDbContext dbconnect)
        {
            _dbconnect = dbconnect;
        }
        public bool SendMail(string to, string generatedCode)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("scmmanagesoftware@gmail.com");
                    mail.To.Add($"{to}");
                    mail.Subject = "Xác thực người dùng";
                    mail.Body = $"<h1>Mã của bạn: {generatedCode}</h1>";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential("scmmanagesoftware@gmail.com", "xeyqostwyeqsdxqh");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            return true;
        }
        public async Task<string?> GenerateAuthCodeAsync()
        {
            using var connection = _dbconnect.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "GenerateAuthCode";
            command.CommandType = CommandType.StoredProcedure;

            // Thêm tham số đầu ra @Key
            var keyParam = command.CreateParameter();
            keyParam.ParameterName = "@Key";
            keyParam.DbType = DbType.String;
            keyParam.Size = 100;
            keyParam.Direction = ParameterDirection.Output;
            command.Parameters.Add(keyParam);

            // Thực thi thủ tục
            await command.ExecuteNonQueryAsync();

            // Trả về giá trị của tham số @Key
            return keyParam.Value?.ToString();
        }

        public async Task<string?> GetEmailByUsername(string username)
        {
            var user = await _dbconnect.QlNguoiDungs
                           .Where(u => u.TenDangNhap == username)
                           .FirstOrDefaultAsync();
            return user?.Email;
        }
        public async Task<bool> SendAuthCodeByEmailAsync(string username)
        {
            try
            {
                // Lấy email từ tên đăng nhập
                var email = await GetEmailByUsername(username);
                if (string.IsNullOrEmpty(email))
                {
                    throw new Exception("Email không tồn tại.");
                }

                // Tạo mã xác thực
                var authCode = await GenerateAuthCodeAsync();
                if (string.IsNullOrEmpty(authCode))
                {
                    throw new Exception("Không thể tạo mã xác thực.");
                }

                // Nội dung email
                //var subject = "Mã xác thực của bạn";
                //var body = $"Xin chào {username},\n\nMã xác thực của bạn là: {authCode}\n\nVui lòng không chia sẻ mã này với bất kỳ ai.";

                // Gửi email
                var result = SendMail(email,authCode);
                return result;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                Console.WriteLine($"Lỗi khi gửi mã xác thực: {ex.Message}");
                return false;
            }
        }
        public async Task<string> VerifyAuthCodeAsync(string inputCode)
        {
            if (string.IsNullOrEmpty(inputCode))
            {
                return "Invalid code. Please try again.";
            }

            try
            {
                
                using (var context = new AppDbContext())
                {
                  
                    var authCode = await context.AuthCodes
                        .FirstOrDefaultAsync(code => code.KeyValue == inputCode);

                    if (authCode == null)
                    {
                        return null;
                    }
                    // Nếu mã hợp lệ
                    return "true";
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return $"An error occurred: {ex.Message}";
            }
        }


    }
}


