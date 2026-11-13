using DataAccessLayer;
using Model;

namespace BusinessLogic
{
    /// <summary>
    /// Проверяет занятость камер и считает свободные камеры.
    /// </summary>
    public class CellService
    {
        IReadRepository<Record> repository;

        /// <summary>
        /// Получает репозиторий только для чтения записей.
        /// </summary>
        /// <param name="repository">Репозиторий для чтения записей</param>
        public CellService(IReadRepository<Record> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Проверяет номер камеры и ее занятость другими записями. Для новой записи id равен 0.
        /// </summary>
        /// <param name="cell">Номер камеры</param>
        /// <param name="id">ID записи, которой нужна камера (0 для новой записи)</param>
        /// <param name="cells">Количество камер</param>
        /// <returns>true, если камеру можно занять.</returns>
        public bool CanUse(int cell, int id, int cells)
        {
            if (cell < 1 || cell > cells)
                return false;
            List<Record> list = repository.ReadAll();
            foreach (Record r in list)
            {
                if (r.Cell == cell && r.ID != id)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Возвращает номера занятых камер по возрастанию.
        /// </summary>
        /// <returns>Список номеров занятых камер.</returns>
        public List<int> BusyCells()
        {
            List<Record> list = repository.ReadAll();
            List<int> res = new List<int>();
            foreach (Record r in list)
                res.Add(r.Cell);
            res.Sort();
            return res;
        }

        /// <summary>
        /// Возвращает номера свободных камер от 1 до cells по возрастанию.
        /// </summary>
        /// <param name="cells">Количество камер</param>
        /// <returns>Список номеров свободных камер.</returns>
        public List<int> FreeCells(int cells)
        {
            List<int> busy = BusyCells();
            List<int> res = new List<int>();
            for (int i = 1; i <= cells; i++)
            {
                if (!busy.Contains(i))
                    res.Add(i);
            }
            return res;
        }
    }
}
