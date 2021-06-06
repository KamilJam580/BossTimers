using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPp.Models;

namespace TestAPp.Services
{
    public interface IBossDataStore<T>
    {
        Task<bool> AddBossAsync(T boss);
        Task<bool> UpdateBossAsync(T boss);
        Task<bool> DeleteBossAsync(string id);
        Task<T> GetBossAsync(string id);
        Task<IEnumerable<T>> GetBossesAsync(bool forceRefresh = false);
        Task<bool> Defeat(Boss boss);
        Task<bool> SetDefeatTime(Boss boss, DateTimeOffset time);
        Task<bool> RefreshTimer(Boss boss);
        Task<bool> DeleteAll();
        string GetPath();
        void SetPath(string path);
        void FirstCreateBosses();
    }
}
