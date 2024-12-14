using Microsoft.EntityFrameworkCore;
using QL_Kho.Models;

namespace QL_Kho.Service
{
    public class TheoDoi_Service
    {
        private readonly AppDbContext _dbconnect;

        public TheoDoi_Service(AppDbContext dbconnect)
        {
            _dbconnect = dbconnect;
        }

        public async Task<bool> AddTheoDoi(TheoDoi doituong)
        {
            doituong.AutoId = 0;
            await _dbconnect.TheoDois.AddAsync(doituong);
            await _dbconnect.SaveChangesAsync();
            return true;
        }

    }
}
