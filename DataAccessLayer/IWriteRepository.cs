using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Добавление, изменение и удаление объектов в базе.
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public interface IWriteRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Добавляет новый объект с ID = 0 в базу. После добавления у объекта заполняется новый ID.
        /// </summary>
        /// <param name="r">Объект для сохранения</param>
        /// <returns>true, если объект добавлен; false, если передан null.</returns>
        bool Add(T r);

        /// <summary>
        /// Удаляет объект с указанным ID.
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns>true, если объект удален; false, если такого нет.</returns>
        bool Delete(int id);

        /// <summary>
        /// Сохраняет значения объекта с указанным ID, даже если значения не изменились.
        /// </summary>
        /// <param name="r">Объект для сохранения</param>
        /// <returns>true, если объект найден и сохранен; false, если передан null или объекта нет.</returns>
        bool Update(T r);
    }
}
