using Dapper;
using Model;
using System.Data.SQLite;

namespace DataAccessLayer
{
    /// <summary>
    /// Чтение записей через Dapper (SQL-запросы к таблице Records).
    /// </summary>
    public class DapperReadRepository : IReadRepository<Record>
    {
        /// <summary>
        /// Создает таблицу в базе, если ее еще нет.
        /// </summary>
        public DapperReadRepository()
        {
            DBConnection.CreateTable();
        }

        /// <summary>
        /// Возвращает все записи по возрастанию ID.
        /// </summary>
        /// <returns>Список объектов по возрастанию ID (пустой, если записей нет).</returns>
        public List<Record> ReadAll()
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return new List<Record>(con.Query<Record>("select * from Records order by ID"));
            }
        }

        /// <summary>
        /// Возвращает запись с указанным ID или null.
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns>Найденный объект или null, если такого нет.</returns>
        public Record ReadById(int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return con.QueryFirstOrDefault<Record>("select * from Records where ID = @id", new { id = id });
            }
        }
    }
}
