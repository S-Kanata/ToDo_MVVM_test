using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Practice_ToDo.Model
{
    public class DataAccess
    {

        private ObservableCollection<ToDo> _todoList = new ObservableCollection<ToDo>();

        ///<summary>
        ///データベースのロード
        /// </summary> 
        private void Load()
        {
            _todoList.Clear();
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<ToDo>();
                foreach (var item in connection.Table<ToDo>())
                    _todoList.Add(item);
            }
        }
        public ObservableCollection<ToDo> ReadDatabase()
        {
                Load();
                return _todoList;
        }

        /// <summary>
        /// データベースへの保存
        /// </summary>
        /// <param name="todoList"></param>
        public void Save(ObservableCollection<ToDo> todoList)
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.DeleteAll<ToDo>();
                foreach (var todo in todoList)
                {
                    connection.Insert(todo);
                }
            }
        }

    }
}

