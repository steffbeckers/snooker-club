using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.BLL
{
    public class SettingBLL
    {
        private readonly SettingRepository settingRepository;

        public SettingBLL(SettingRepository settingRepository)
        {
            this.settingRepository = settingRepository;
        }

        public async Task<Setting> CreateSettingAsync(Setting setting)
        {
            return await this.settingRepository.InsertAsync(setting);
        }

        public async Task<Setting> UpdateSettingAsync(Guid id, Setting settingUpdate)
        {
            // Retrieve existing
            Setting setting = await this.settingRepository.GetByIdAsync(id);
            if (setting == null)
            {
                return null;
            }

            // Mapping
            setting.Key = settingUpdate.Key;
            setting.Value = settingUpdate.Value;

            return await this.settingRepository.UpdateAsync(setting);
        }

        public async Task<bool> RemoveSettingAsync(Guid id)
        {
            // Retrieve existing
            Setting setting = await this.settingRepository.GetByIdAsync(id);
            if (setting == null)
            {
                return true;
            }

            await this.settingRepository.DeleteAsync(setting);

            return true;
        }
    }
}
