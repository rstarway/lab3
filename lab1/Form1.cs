using BusinessLogic;
using Model;
using Ninject;

namespace lab1
{
    /// <summary>
    /// Главное окно: таблица записей, поиск и камеры.
    /// </summary>
    public partial class Form1 : Form
    {
        IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
        ILogic logic;

        /// <summary>
        /// Открывает окно и загружает записи из базы.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            logic = ninjectKernel.Get<ILogic>();
            Fill(logic.GetAll());
        }

        /// <summary>
        /// Показывает записи в таблице.
        /// </summary>
        /// <param name="list">Записи для показа</param>
        private void Fill(List<Record> list)
        {
            dataGridView1.Rows.Clear();
            foreach (Record r in list)
            {
                dataGridView1.Rows.Add(r.ID, r.Name, r.Age, r.Date.ToShortDateString(), r.Cause, r.Cell);
            }
        }

        /// <summary>
        /// Кнопка «Добавить»: открывает карточку, пока запись не добавится или пользователь не нажмет «Отмена».
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Данные события</param>
        private void button1_Click(object sender, EventArgs e)
        {
            RecordForm f = new RecordForm();
            while (f.ShowDialog() == DialogResult.OK)
            {
                string err = logic.Add(f.name, f.age, f.date, f.cause, f.cell);
                if (err == "")
                {
                    Fill(logic.GetAll());
                    return;
                }
                MessageBox.Show(err);
            }
        }

        /// <summary>
        /// Кнопка «Изменить»: открывает карточку выбранной записи.
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Данные события</param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выбери запись в таблице");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            Record r = logic.GetById(id);
            if (r == null)
            {
                MessageBox.Show("Записи уже нет, обнови таблицу");
                Fill(logic.GetAll());
                return;
            }

            RecordForm f = new RecordForm(r);
            while (f.ShowDialog() == DialogResult.OK)
            {
                string err = logic.Edit(id, f.name, f.age, f.date, f.cause, f.cell);
                if (err == "")
                {
                    Fill(logic.GetAll());
                    return;
                }
                MessageBox.Show(err);
            }
        }

        /// <summary>
        /// Кнопка «Удалить»: удаляет выбранную запись после подтверждения.
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Данные события</param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выбери запись в таблице");
                return;
            }

            if (MessageBox.Show("Точно удалить?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!logic.Delete(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)))
                    MessageBox.Show("Записи уже нет");
                Fill(logic.GetAll());
            }
        }

        /// <summary>
        /// Кнопка «Найти»: показывает записи с нужной причиной смерти.
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Данные события</param>
        private void button5_Click(object sender, EventArgs e)
        {
            List<Record> res = logic.FindByCause(textBox6.Text);
            Fill(res);
            if (res.Count == 0)
                MessageBox.Show("Ничего не найдено");
        }

        /// <summary>
        /// Кнопка «Показать все»: сбрасывает поиск.
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Данные события</param>
        private void button6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
            Fill(logic.GetAll());
        }

        /// <summary>
        /// Кнопка «Камеры»: показывает занятые и свободные камеры.
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Данные события</param>
        private void button7_Click(object sender, EventArgs e)
        {
            List<int> busy = logic.BusyCells();
            List<int> free = logic.FreeCells();
            label8.Text = "Занято: " + busy.Count + " (" + string.Join(", ", busy) + ")\nСвободно: " + free.Count + " (" + string.Join(", ", free) + ")";
        }
    }
}
