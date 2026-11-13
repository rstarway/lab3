using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Добавление, изменение и удаление объектов через Entity Framework.
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public class EntityWriteRepository<T> : IWriteRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Создает таблицу в базе, если ее еще нет.
        /// </summary>
        public EntityWriteRepository()
        {
            DBConnection.CreateTable();
        }

        /// <summary>
        /// Добавляет объект в базу.
        /// </summary>
        /// <param name="r">Объект для сохранения</param>
        /// <returns>true, если объект добавлен; false, если передан null.</returns>
        public bool Add(T r)
        {
            if (r == null)
                return false;
            using (DBContext db = new DBContext())
            {
                db.Set<T>().Add(r);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Удаляет объект с указанным ID.
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns>true, если объект удален; false, если такого нет.</returns>
        public bool Delete(int id)
        {
            using (DBContext db = new DBContext())
            {
                T r = db.Set<T>().Find(id);
                if (r == null)
                    return false;
                db.Set<T>().Remove(r);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Сохраняет изменения объекта.
        /// </summary>
        /// <param name="r">Объект для сохранения</param>
        /// <returns>true, если объект найден и сохранен; false, если передан null или объекта нет.</returns>
        public bool Update(T r)
        {
            if (r == null)
                return false;
            using (DBContext db = new DBContext())
            {
                T x = db.Set<T>().Find(r.ID);
                if (x == null)
                    return false;
                db.Entry(x).CurrentValues.SetValues(r);
                db.SaveChanges();
                return true;
            }
        }
    }
}
