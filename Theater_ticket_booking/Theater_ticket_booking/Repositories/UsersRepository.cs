using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(TheaterContext db) : base(db)
        {
        }

        /// <summary>
        /// Проверяет существование пользователей в БД по идентификаторам
        /// </summary>
        /// <param name="usersId">Идентификаторы пользователей</param>
        /// <returns>Идентификаторы пользователей которых нет в БД</returns>
        public async Task<List<int>> IsExists(List<int> usersId)
        {
            try
            {
                usersId = usersId.Distinct().ToList();

                var successUsersId = await GetList().Where(p => usersId.Contains(p.Id)).Select(p => p.Id).ToListAsync();

                return usersId.Except(successUsersId).ToList();

            }
            catch (Exception ex)
            {
            }
            return new List<int>();
        }
    }
}
