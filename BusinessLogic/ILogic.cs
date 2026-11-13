using Model;

namespace BusinessLogic
{
    /// <summary>
    /// Бизнес-логика учета поступлений. Слой представления работает только с этим интерфейсом.
    /// </summary>
    public interface ILogic
    {
        /// <summary>
        /// Общее количество холодильных камер (номера от 1 до Cells).
        /// </summary>
        int Cells { get; }

        /// <summary>
        /// Проверяет данные и добавляет новую запись о поступлении.
        /// </summary>
        /// <param name="name">ФИО</param>
        /// <param name="age">Возраст</param>
        /// <param name="date">Дата поступления</param>
        /// <param name="cause">Причина смерти</param>
        /// <param name="cell">Номер камеры</param>
        /// <returns>Пустая строка, если запись добавлена, иначе текст ошибки.</returns>
        string Add(string name, int age, DateTime date, string cause, int cell);

        /// <summary>
        /// Возвращает список всех записей.
        /// </summary>
        /// <returns>Список всех записей по возрастанию ID.</returns>
        List<Record> GetAll();

        /// <summary>
        /// Возвращает запись с указанным id.
        /// </summary>
        /// <param name="id">Номер записи</param>
        /// <returns>Найденная запись или null, если такой нет.</returns>
        Record GetById(int id);

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
        string Edit(int id, string name, int age, DateTime date, string cause, int cell);

        /// <summary>
        /// Удаляет запись с указанным id.
        /// </summary>
        /// <param name="id">Номер удаляемой записи</param>
        /// <returns>true, если запись удалена; false, если такой записи нет.</returns>
        bool Delete(int id);

        /// <summary>
        /// Ищет записи по причине смерти (часть слова, без учета регистра).
        /// </summary>
        /// <param name="cause">Текст для поиска</param>
        /// <returns>Список найденных записей.</returns>
        List<Record> FindByCause(string cause);

        /// <summary>
        /// Возвращает номера занятых камер по возрастанию.
        /// </summary>
        /// <returns>Список номеров занятых камер.</returns>
        List<int> BusyCells();

        /// <summary>
        /// Возвращает номера свободных камер по возрастанию.
        /// </summary>
        /// <returns>Список номеров свободных камер.</returns>
        List<int> FreeCells();
    }
}
