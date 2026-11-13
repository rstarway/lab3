using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Фасад CRUD: передает чтение читающему репозиторию, а запись пишущему.
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public class Repository<T> : IRepository<T> where T : class, IDomainObject
    {
        IReadRepository<T> reader;
        IWriteRepository<T> writer;

        /// <summary>
        /// Получает репозитории чтения и записи из контейнера.
        /// </summary>
        /// <param name="reader">Репозиторий чтения</param>
        /// <param name="writer">Репозиторий записи</param>
        public Repository(IReadRepository<T> reader, IWriteRepository<T> writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        /// <summary>
        /// Возвращает все объекты по возрастанию ID.
        /// </summary>
        /// <returns>Список объектов по возрастанию ID (пустой, если записей нет).</returns>
        public List<T> ReadAll()
        {
            return reader.ReadAll();
        }

        /// <summary>
        /// Возвращает объект с указанным ID или null.
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns>Найденный объект или null, если такого нет.</returns>
        public T ReadById(int id)
        {
            return reader.ReadById(id);
        }

        /// <summary>
        /// Добавляет новый объект.
        /// </summary>
        /// <param name="r">Объект для сохранения</param>
        /// <returns>true, если объект добавлен; false, если передан null.</returns>
        public bool Add(T r)
        {
            return writer.Add(r);
        }

        /// <summary>
        /// Удаляет объект с указанным ID.
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns>true, если объект удален; false, если такого нет.</returns>
        public bool Delete(int id)
        {
            return writer.Delete(id);
        }

        /// <summary>
        /// Сохраняет изменения объекта.
        /// </summary>
        /// <param name="r">Объект для сохранения</param>
        /// <returns>true, если объект найден и сохранен; false, если передан null или объекта нет.</returns>
        public bool Update(T r)
        {
            return writer.Update(r);
        }
    }
}
