using HR_System.DTOs.GeneralSettingDto;
using HR_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Repositories.GeneralSettings
{
    public class GeneralSettingsRepository:IGeneralSettingsRepository
    {
        private readonly HREntity hREntity;
        public GeneralSettingsRepository(HREntity hREntity) => this.hREntity = hREntity;
        
        public Models.GeneralSettings GetById(int id)
        {
            return hREntity.GeneralSettings.FirstOrDefault(g=>g.ID==id );
        }

        //public List<GeneralSettingDto> GetAll()
        //{
        //    var setting= hREntity.GeneralSettings.ToList();
        //    var SettingDto = setting.Select(s => new GeneralSettingDto() {
        //        ID = s.ID,
        //        Add_hours = s.Add_hours,
        //        Sub_hours = s.Sub_hours,
        //        vacation1 = s.vacation1,
        //        vacation2 = s.vacation2,
        //    }).ToList();
        //    return(SettingDto);

        //}




        public void UpdateSetting(GeneralSettingDto settingDto)
        {
            var newSetting = new Models.GeneralSettings
            {
                ID = settingDto.ID,
                Add_hours = settingDto.Add_hours,
                Sub_hours = settingDto.Sub_hours,
                vacation1 = settingDto.vacation1,
                vacation2 = settingDto.vacation2,
            };
            hREntity.Update(newSetting);
            hREntity.SaveChanges();
        }

        public int IFWeeklyVaction(int day, int month, int year)
        {
            var dateTime = new DateTime(year, month, day).ToString("dddd");
            var setting = hREntity.GeneralSettings.Where(g=>g.vacation1==dateTime || g.vacation2==dateTime).ToList();
            if(setting.Count()==0)
            {
                return 0;    //not a vocation
            }
            else
            {
                return 1;   // is vocation
            }
            }

        //public void RemoveSetting(int id)
        //{
        //    var old = GetById(id);
        //    if(old != null)
        //    {
        //        old.IsDeleted = true;
        //        hREntity.SaveChanges();
        //    }
        //}




    }
}
