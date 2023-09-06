using HR_System.DTOs.OfficialvocationDto;
using HR_System.Models;

namespace HR_System.Repositories.Official_Vocations
{
    public interface IOfficialVocationsRepository
    {
        Models.OfficialVocations GetByID(int id);
         List<OfficialVocationDto> GetAll();
         OffficialVocationGetDto GetVocationById(int id);
         void Insert(OfficialVocationDto vocationDto);
        void Update(OfficialVocationDto vocationDto);
         void Delete(int id);
        int ifNameExist(int id, string Name);

        int ifVocation(int day, int month, int year);
        int ifDateExist(int id,DateTime Date);



        }
}
