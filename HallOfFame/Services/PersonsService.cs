using HallOfFame.Data;
using HallOfFame.DTOs;
using HallOfFame.Interfaces;
using HallOfFame.Models;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly PersonsAPIDbContext _context;

        public PersonsService(PersonsAPIDbContext context)
        {
            _context = context;
        }

        public async Task<List<PersonDTO>> GetAllPersons()
        {
            // Можно отключить отслеживание контекста для оптимизации поиска сотрудников в базе
            var persons = await _context.Persons.AsNoTracking().Select(x => new PersonDTO
            {
                Id = x.Id,
                Name = x.Name,
                DisplayName = x.DisplayName,
                Skills = x.Skills.Select(y => new SkillDTO
                {
                    Id = y.Id,
                    Name = y.Name,
                    Level = y.Level,
                }).ToList()
            }).ToListAsync();

            return persons;
        }

        public async Task<PersonDTO?> GetPerson(long id) 
        {
            var person = await _context.Persons.AsNoTracking().Where(x => x.Id == id).Select(x => new PersonDTO
            {
                Id = x.Id,
                Name = x.Name,
                DisplayName = x.DisplayName,
                Skills = x.Skills.Select(y => new SkillDTO
                {
                    Id = y.Id,
                    Name = y.Name,
                    Level = y.Level,
                }).ToList()
            }).SingleOrDefaultAsync();

            return person;
        }

        public async Task<PersonDTO> CreatePerson(PersonDTO personDTO) 
        {
            var person = new Person
            {
                Name = personDTO.Name,
                DisplayName = personDTO.DisplayName,
                Skills = personDTO.Skills.Select(x => new Skill 
                {
                    Name = x.Name,
                    Level = x.Level,
                }).ToList(),
            };
            
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            var createdPersonDTO = new PersonDTO 
            {
                Id = person.Id,
                Name = person.Name,
                DisplayName = person.DisplayName,
                Skills = person.Skills.Select(x => new SkillDTO 
                {
                    Id = x.Id,
                    Name = x.Name,
                    Level = x.Level,
                }).ToList(),
            };

            return createdPersonDTO; 
        }

        public async Task<PersonDTO?> UpdatePerson(long id, PersonDTO personDTO)
        {
            var person = await _context.Persons.Where(x => x.Id == id).Include(x => x.Skills).SingleOrDefaultAsync();

            if (person != null)
            {
                person.Name = personDTO.Name;
                person.DisplayName = personDTO.DisplayName;

                // Вариант "В лоб" - обойти оба списка навыков (записанный в базе и измененный), соответсвия обновить,
                // не соответсвия удалить, новые навыки создать (Skill.Id == 0).
                foreach (var s in person.Skills)
                {
                    foreach (var ss in personDTO.Skills)
                    {
                        // Если поля Skill.Id совпадают - обновляем остальные поля навыка и снимаем пометку на удаление.
                        if (s.Id == ss.Id)
                        {
                            s.Name = ss.Name;
                            s.Level = ss.Level;
                            _context.Entry(s).State = EntityState.Modified;
                            break;
                        }
                        else
                            _context.Skills.Remove(s);
                    };
                };

                foreach (var s in personDTO.Skills)
                {
                    if (s.Id == 0)
                    {
                        person.Skills.Add(new Skill
                        {
                            Name = s.Name,
                            Level = s.Level,
                        });
                    }
                };

                // Вносим в базу все отслеживаемые изменения
                await _context.SaveChangesAsync();
                var updatedPerson = new PersonDTO
                {
                    Id = person.Id,
                    Name = person.Name,
                    DisplayName = person.DisplayName,
                    Skills = person.Skills.Select(x => new SkillDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Level = x.Level,

                    }).ToList(),
                };

                return updatedPerson;
            }

            return null;
        }

        public async Task<PersonDTO?> DeletePerson(long id) 
        {
            var person = await _context.Persons.Where(x => x.Id == id).Select(x => new PersonDTO 
            {
                Id = x.Id,
                Name = x.Name,
                DisplayName = x.DisplayName,
                Skills = x.Skills.Select(y => new SkillDTO 
                {
                    Id = y.Id,
                    Name = y.Name,
                    Level = y.Level,
                }).ToList(),
            }).SingleOrDefaultAsync();

            if (person != null)
            {
                var personToDel = await _context.Persons.FindAsync(id);
                _context.Remove(personToDel);
                await _context.SaveChangesAsync();
            }

            return person;
        }
    }
}
