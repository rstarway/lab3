using Dapper;
using Model;
using System.Data.SQLite;

namespace DataAccessLayer
{
    /// <summary>
    /// Добавление, изменение и удаление записей через Dapper (SQL-запросы к таблице Records).
    /// </summary>
    public class DapperWriteRepository : IWriteRepository<Record>
    {
        /// <summary>
        /// Создает таблицу в базе, если ее еще нет.
        /// </summary>
        public DapperWriteRepository()
        {
            DBConnection.CreateTable();
        }

        /// <summary>
        /// Добавляет запись в базу и записывает ей новый ID.
        /// </summary>
        /// <param name="r">Объект для сохранения</param>
        /// <returns>true, если объект добавлен; false, если передан null.</returns>
        public bool Add(Record r)
        {
            if (r == null)
                return false;
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                string sql = "insert into Records (Name, Age, Date, Cause, Cell) values (@Name, @Age, @Date, @Cause, @Cell); select last_insert_rowid();";
                r.ID = con.ExecuteScalar<int>(sql, r);
            }
            return r.ID > 0;
        }

        /// <summary>
        /// Удаляет запись с указанным ID.
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns>true, если объект удален; false, если такого нет.</returns>
        public bool Delete(int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return con.Execute("delete from Records where ID = @id", new { id = id }) > 0;
            }
        }

        /// <summary>
        /// Сохраняет изменения записи.
        /// </summary>
        /// <param name="r">Объект для сохранения</param>
        /// <returns>true, если объект найден и сохранен; false, если передан null или объекта нет.</returns>
        public bool Update(Record r)
        {
            if (r == null)
                return false;
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                string sql = "update Records set Name = @Name, Age = @Age, Date = @Date, Cause = @Cause, Cell = @Cell where ID = @ID";
                return con.Execute(sql, r) > 0;
            }
        }
    }
}
