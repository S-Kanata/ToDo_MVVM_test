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


        #region ViewModelから直接操作する時用のメソッド

        public ObservableCollection<ToDo> Add(ToDo todo)
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.Insert(todo);
            }
            Load();
            return _todoList;
        }

        public ObservableCollection<ToDo> Delete(ToDo todo)
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.Delete(todo);
            }
            Load();
            return _todoList;
        }

        public ObservableCollection<ToDo> Clear()
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                foreach (var todo in _todoList)
                {
                    if (todo.Done) connection.Delete(todo);
                }
            }
            Load();
            return _todoList;
        }


        public ObservableCollection<ToDo> Reset()
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                foreach (var todo in _todoList)
                {
                    connection.Delete(todo);

                }
            }
            Load();
            return _todoList;
        }
        
       
        public ObservableCollection<ToDo> Update(ToDo todo)
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.Update(todo);
            }
            Load();
            return _todoList;
        }
      
        #endregion
    }
}

