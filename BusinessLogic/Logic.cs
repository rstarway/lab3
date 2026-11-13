using DataAccessLayer;
using Model;

namespace BusinessLogic
{
    /// <summary>
    /// Бизнес-логика учета поступлений: проверяет данные и работает с записями через репозиторий.
    /// </summary>
    public class Logic : ILogic
    {
        IRepository<Record> repository;
        CellService cellService;
        RecordValidator validator;

        /// <summary>
        /// Получает репозиторий и классы проверки из контейнера.
        /// </summary>
        /// <param name="repository">Репозиторий записей</param>
        /// <param name="cellService">Проверка и подсчет камер</param>
        /// <param name="validator">Проверка полей записи</param>
        public Logic(IRepository<Record> repository, CellService cellService, RecordValidator validator)
        {
            this.repository = repository;
            this.cellService = cellService;
            this.validator = validator;
        }

        /// <summary>
        /// Общее количество холодильных камер (номера от 1 до Cells).
        /// </summary>
        public int Cells { get; } = 10;

        /// <summary>
        /// Проверяет данные и добавляет новую запись о поступлении.
        /// </summary>
        /// <param name="name">ФИО</param>
        /// <param name="age">Возраст</param>
        /// <param name="date">Дата поступления</param>
        /// <param name="cause">Причина смерти</param>
        /// <param name="cell">Номер камеры</param>
        /// <returns>Пустая строка, если запись добавлена, иначе текст ошибки.</returns>
        public string Add(string name, int age, DateTime date, string cause, int cell)
        {
            string err = validator.Check(name, age, date, cause);
            if (err != "")
                return err;
            if (!cellService.CanUse(cell, 0, Cells))
                return "Камера занята или нет такой (1-" + Cells + ")";

            Record r = new Record();
            r.Name = name.Trim();
            r.Age = age;
            r.Date = date.Date;
            r.Cause = cause.Trim();
            r.Cell = cell;
            if (!repository.Add(r))
                return "Не удалось сохранить запись";
            return "";
        }

        /// <summary>
        /// Возвращает список всех записей.
        /// </summary>
        /// <returns>Список всех записей по возрастанию ID.</returns>
        public List<Record> GetAll()
        {
            return repository.ReadAll();
        }

        /// <summary>
        /// Возвращает запись с указанным id.
        /// </summary>
        /// <param name="id">Номер записи</param>
        /// <returns>Найденная запись или null, если такой нет.</returns>
        public Record GetById(int id)
        {
            return repository.ReadById(id);
        }

        /// <summary>
        /// Проверяет данные и изменяет запись с указанным id.
        /// </summary>
        /// <param name="id">Номер изменяемой записи</param>
        /// <param name="name">ФИО</param>
        /// <param name="age">Возраст</param>
        /// <param name="date">Дата поступления</param>
        /// <param name="cause">Причина смерти</param>
        /// <param name="cell">Номер камеры</param>
        /// <returns>Пустая строка, если запись изменена, иначе текст ошибки.</returns>
        public string Edit(int id, string name, int age, DateTime date, string cause, int cell)
        {
            string err = validator.Check(name, age, date, cause);
            if (err != "")
                return err;
            Record r = repository.ReadById(id);
            if (r == null)
                return "Записи с таким id нет";
            if (!cellService.CanUse(cell, id, Cells))
                return "Камера занята или нет такой (1-" + Cells + ")";

            r.Name = name.Trim();
            r.Age = age;
            r.Date = date.Date;
            r.Cause = cause.Trim();
            r.Cell = cell;
            if (!repository.Update(r))
                return "Не удалось сохранить запись";
            return "";
        }

        /// <summary>
        /// Удаляет запись с указанным id.
        /// </summary>
        /// <param name="id">Номер удаляемой записи</param>
        /// <returns>true, если запись удалена; false, если такой записи нет.</returns>
        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        /// <summary>
        /// Бизнес-функция 1: поиск записей по причине смерти (часть слова, без учета регистра).
        /// </summary>
        /// <param name="cause">Текст для поиска</param>
        /// <returns>Список найденных записей.</returns>
        public List<Record> FindByCause(string cause)
        {
            if (cause == null)
                cause = "";
            List<Record> list = repository.ReadAll();
            List<Record> res = new List<Record>();
            foreach (Record r in list)
            {
                if (r.Cause != null && r.Cause.ToLower().Contains(cause.Trim().ToLower()))
                    res.Add(r);
            }
            return res;
        }

        /// <summary>
        /// Бизнес-функция 2: номера занятых камер по возрастанию.
        /// </summary>
        /// <returns>Список номеров занятых камер.</returns>
        public List<int> BusyCells()
        {
            return cellService.BusyCells();
        }

        /// <summary>
        /// Бизнес-функция 2: номера свободных камер по возрастанию.
        /// </summary>
        /// <returns>Список номеров свободных камер.</returns>
        public List<int> FreeCells()
        {
            return cellService.FreeCells(Cells);
        }
    }
}
