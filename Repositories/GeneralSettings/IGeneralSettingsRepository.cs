using HR_System.DTOs.GeneralSettingDto;

namespace HR_System.Repositories.GeneralSettings
{
    public interface IGeneralSettingsRepository
    {
         Models.GeneralSettings GetById(int id);
        void UpdateSetting(GeneralSettingDto settingDto);
        int IFWeeklyVaction(int day, int month, int year);

    }
}
