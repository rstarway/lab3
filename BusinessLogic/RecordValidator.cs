namespace BusinessLogic
{
    /// <summary>
    /// Проверяет обязательные поля записи и возраст.
    /// </summary>
    public class RecordValidator
    {
        /// <summary>
        /// Проверяет, что ФИО и причина заполнены, возраст от 0 до 120, а дата не раньше 1900 года.
        /// </summary>
        /// <param name="name">ФИО</param>
        /// <param name="age">Возраст</param>
        /// <param name="date">Дата поступления</param>
        /// <param name="cause">Причина смерти</param>
        /// <returns>Текст ошибки или пустая строка, если все верно.</returns>
        public string Check(string name, int age, DateTime date, string cause)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Заполни ФИО";
            if (string.IsNullOrWhiteSpace(cause))
                return "Заполни причину смерти";
            if (age < 0 || age > 120)
                return "Возраст должен быть от 0 до 120";
            if (date.Year < 1900)
                return "Дата не раньше 1900 года";
            return "";
        }
    }
}
