using Model;
using System.Data.Entity;
using System.Data.SQLite;

namespace DataAccessLayer
{
    /// <summary>
    /// Контекст Entity Framework для базы SQLite.
    /// </summary>
    [DbConfigurationType(typeof(DBConfig))]
    public class DBContext : DbContext
    {
        /// <summary>
        /// Подключается к файлу базы из DBConnection.
        /// </summary>
        public DBContext() : base(new SQLiteConnection(DBConnection.str), true)
        {
            Database.SetInitializer<DBContext>(null);
        }

        /// <summary>
        /// Таблица записей.
        /// </summary>
        public DbSet<Record> Records { get; set; }

        /// <summary>
        /// Связывает класс Record с таблицей Records.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели Entity Framework</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>().ToTable("Records", "main");
        }
    }
}
