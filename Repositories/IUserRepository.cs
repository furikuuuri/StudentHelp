using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sheldy.Repositories
{
    interface IUserRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetUserList(); // получение всех объектов
        T GetUser(T item); // получение одного объекта по id
        T Create(T item); // создание объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}
