using HallOfFame.DTOs;

namespace HallOfFame.Interfaces
{
    public interface IPersonsService
    {
        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns> DTO всех сотрудников </returns>
        Task<List<PersonDTO>> GetAllPersons();
        /// <summary>
        /// Получить одного сотрудника.
        /// </summary>
        /// <param name="id">Id сотрудника.</param>
        /// <returns>DTO одного сотрудника или Null если сотрудник не найден.</returns>
        Task<PersonDTO?> GetPerson(long id);
        /// <summary>
        /// Создать нового сотрудника.
        /// </summary>
        /// <param name="personDTO"></param>
        /// <returns>DTO созданного сотрудника</returns>
        Task<PersonDTO> CreatePerson(PersonDTO personDTO);
        /// <summary>
        /// Изменить сотрудника.
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <param name="personDTO">DTO с актуальными значениями</param>
        /// <returns>DTO обновленного сотрудника или Null, если сотрудник не найден</returns>
        Task<PersonDTO?> UpdatePerson(long id, PersonDTO personDTO);
        Task<PersonDTO?> DeletePerson(long id);
    }
}
