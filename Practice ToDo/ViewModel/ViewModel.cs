using SQLite;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;

namespace Practice_ToDo
{
    public class ViewModel : ViewModelBase
    {

        #region メンバ変数

        private ObservableCollection<ToDo> todoList;
        public ObservableCollection<ToDo> ToDoList
        {
            get { return todoList; }
            set
            {
                RaisePropertyChanged("ToDoList");
            }
        }

        private DelegateCommand<object> btnClickCommand;

        public Dictionary<int, string> Priorities { get; set; }
        #endregion

        #region コンストラクタ
        public ViewModel()
        {
            todoList = new ObservableCollection<ToDo>();
            deadline = DateTime.Today;
            LoadPriority();
            ReadDatabase();
        }
        #endregion

        #region コマンドの実行宣言

        public DelegateCommand<object> AddClickCommand
        {
            get
            {
                btnClickCommand = new DelegateCommand<object>(ExecuteAdd, CanExecuteBtnClick);
                return btnClickCommand;
            }
            set
            {
                btnClickCommand = value;
            }
        }

        public DelegateCommand<object> DltClickCommand
        {
            get
            {
                btnClickCommand = new DelegateCommand<object>(ExecuteDelete, CanExecuteBtnClick);
                return btnClickCommand;
            }
            set
            {
                btnClickCommand = value;
            }
        }

        public DelegateCommand<object> UpdClickCommand
        {
            get
            {
                btnClickCommand = new DelegateCommand<object>(ExecuteUpdate, CanExecuteBtnClick);
                return btnClickCommand;
            }
            set
            {
                btnClickCommand = value;
            }
        }

        public DelegateCommand<object> ClrClickCommand
        {
            get
            {
                btnClickCommand = new DelegateCommand<object>(ExecuteClear, CanExecuteBtnClick);
                return btnClickCommand;
            }
            set
            {
                btnClickCommand = value;
            }
        }

        #endregion

        #region プロパティ

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged("Title");
            }
        }

        private DateTime deadline;
        public DateTime Deadline
        {
            get { return deadline; }
            set
            {
                deadline = value;
                RaisePropertyChanged("Deadline");
            }
        }

        private bool done;
        public bool Done
        {
            get { return done; }
            set
            {
                done = value;
                RaisePropertyChanged("Done");
            }
        }

        private DateTime created;
        public DateTime Created
        {
            get { return created; }
            set
            {
                updated = value;
                RaisePropertyChanged("Created");
            }
        }

        private DateTime updated;
        public DateTime Updated
        {
            get { return updated; }
            set
            {
                updated = value;
                RaisePropertyChanged("Updated");
            }
        }

        private string priority;
        public string Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                RaisePropertyChanged("Priority");
            }
        }

        private ToDo selectedToDo;
        public ToDo SelectedToDo
        {
            get { return selectedToDo; }
            set {
                selectedToDo = value;
                RaisePropertyChanged("SelectedToDo");
            }
        }


        #endregion



        #region メソッド
        private void ReadDatabase()
        {
            todoList.Clear();
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<ToDo>();
                foreach (var item in connection.Table<ToDo>())
                    todoList.Add(item);
            }
        }

        private void LoadPriority()
        {
            Priorities = new Dictionary<int, string>();
            foreach (int x in Enum.GetValues(typeof(App.Priority)))
            {
                Priorities.Add(x, Enum.GetName(typeof(App.Priority), x));
            }

            Priority = Priorities[1];
        }




        private void ExecuteAdd(object obj)
        {
            var todo = new ToDo
            {
                Title = title,
                Deadline = deadline,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Priority = priority.ToString()
            };

            if ((Title != null) && (Deadline != null))
            {
                using (var connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.Insert(todo);
                    connection.Close();
                }
                ReadDatabase();
            }
            else
            {
                MessageBox.Show("Please enter all information");
            }
        }

        private void ExecuteDelete(object obj)
        {
            var todo = SelectedToDo;
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.Delete(todo);
                connection.Close();
            }
            ReadDatabase();
        }

        private void ExecuteUpdate(object obj)
        {
            var todo = SelectedToDo;
            todo.Title = title;
            todo.Deadline = deadline;
            todo.Updated = DateTime.Now;
            todo.Priority = priority.ToString();

            if ((Title != null) && (Deadline != null))
            {
                using (var connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.Update(todo);
                    connection.Close();
                }
                ReadDatabase();
            }
            else
            {
                MessageBox.Show("Please enter all information");
            }
        }

        private void ExecuteClear(object obj)
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                foreach (var todo in todoList)
                {
                    if (todo.Done)
                    {
                        connection.Delete(todo);
                    }

                }
            }
            ReadDatabase();
        }

        private bool CanExecuteBtnClick()
        {
            return true;
        }

        #endregion

        #region デバッグ用

        private void ExecuteReset(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete all ToDo?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Error);

            if (result == MessageBoxResult.Yes)
            {

                using (var connection = new SQLiteConnection(App.DatabasePath))
                {
                    foreach (var todo in todoList)
                    {
                        connection.Delete(todo);
                    }
                }
                ReadDatabase();

            }
        }

        public DelegateCommand<object> Reset
        {
            get
            {
                btnClickCommand = new DelegateCommand<object>(ExecuteReset, CanExecuteBtnClick);
                return btnClickCommand;
            }
            set
            {
                btnClickCommand = value;
            }
        }
        #endregion
    }
}

