using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApiDTO_DataAccess.Repository.IRepository;
using TodoApiDTO_Models;

namespace TodoApiDTO_Business.Tests.TestDoubles
{
    /// <summary>
    /// Класс для работы с БД (Юнит тесты - заглушка)
    /// </summary>
    public class TodoItemsRepositoryStub : ITodoItemsRepository
    {
        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TodoItem entity)
        {
            //Заглушка
            entity.Id = 10;
        }

        /// <summary>
        /// Проверка существования записи
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<TodoItem, bool>> filter = null)
        {
            var testList = new List<TodoItem>()
            {
                new TodoItem()
                {
                    Id = 1,
                    Name = "Test1",
                    IsComplete = true,
                    Secret = "SecretTest1"
                },
                new TodoItem()
                {
                    Id = 2,
                    Name = "Test2",
                    IsComplete = false,
                    Secret = "SecretTest2"
                },
                new TodoItem()
                {
                    Id = 3,
                    Name = "Test3",
                    IsComplete = true,
                    Secret = "SecretTest3"
                }
            };

            IQueryable<TodoItem> query = testList.AsQueryable();

            //Фильтр
            if (filter != null)
            {
                return query.Any(filter);
            }

            return query.Any();
        }

        /// <summary>
        /// Поиск записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TodoItem> FindAsync(long id)
        {
            var testList = new List<TodoItem>()
            {
                new TodoItem()
                {
                    Id = 1,
                    Name = "Test1",
                    IsComplete = true,
                    Secret = "SecretTest1"
                },
                new TodoItem()
                {
                    Id = 2,
                    Name = "Test2",
                    IsComplete = false,
                    Secret = "SecretTest2"
                },
                new TodoItem()
                {
                    Id = 3,
                    Name = "Test3",
                    IsComplete = true,
                    Secret = "SecretTest3"
                }
            };

            //Заглушка
            return await Task.Run(() => testList.FirstOrDefault(v => v.Id == id));
        }

        /// <summary>
        /// Вывод всех записей
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="isTracking"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TodoItem>> GetAllAsync(
            Expression<Func<TodoItem, bool>> filter = null,
            Func<IQueryable<TodoItem>, IOrderedQueryable<TodoItem>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true)
        {
            //Заглушка
            return await Task.Run(() => new List<TodoItem>()
            {
                new TodoItem()
                {
                    Id = 1,
                    Name = "Test1",
                    IsComplete = true,
                    Secret = "SecretTest1"
                },
                new TodoItem()
                {
                    Id = 2,
                    Name = "Test2",
                    IsComplete = false,
                    Secret = "SecretTest2"
                },
                new TodoItem()
                {
                    Id = 3,
                    Name = "Test3",
                    IsComplete = true,
                    Secret = "SecretTest3"
                }
            });
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TodoItem entity)
        {
            //Заглушка
        }

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            //Заглушка
            await Task.CompletedTask;
        }
    }
}
