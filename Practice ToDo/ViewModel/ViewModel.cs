using SQLite;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using Practice_ToDo.Model;

namespace Practice_ToDo.ViewModel
{
    public class ViewModel : ViewModelBase
    {

        #region メンバ変数

        private DelegateCommand<object> btnClickCommand;

        DataAccess da;

        #endregion

        #region コンストラクタ
        public ViewModel()
        {
            todoList = new ObservableCollection<ToDo>();
            todoView = new ObservableCollection<DataGridRow>();
            da = new DataAccess();
            todoList = da.ReadDatabase();
            ConvertToRow();
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
                btnClickCommand = new DelegateCommand<object>(ExecuteDelete, CanExecuteUpdClick);
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
                btnClickCommand = new DelegateCommand<object>(ExecuteClear, CanExecuteClrClick);
                return btnClickCommand;
            }
            set
            {
                btnClickCommand = value;
            }
        }

        public DelegateCommand<object> SaveClickCommand
        {
            get
            {
                btnClickCommand = new DelegateCommand<object>(ExecuteSave, CanExecuteBtnClick);
                return btnClickCommand;
            }
            set
            {
                btnClickCommand = value;
            }
        }

        #endregion

        #region プロパティ

        public string Title;

        public DateTime Deadline;

        public bool Done;

        public DateTime Created;

        public DateTime Updated;

        public string Priority;


        private DataGridRow selectedRow;
        public DataGridRow SelectedRow
        {
            get { return selectedRow; }
            set {
                selectedRow = value;
                RaisePropertyChanged("SelectedRow");
            }
        }


        private ObservableCollection<ToDo> todoList;
        public ObservableCollection<ToDo> ToDoList
        {
            get { return todoList; }
            set
            {
                todoList = value;
                RaisePropertyChanged("ToDoList");

            }
        }


        private ObservableCollection<DataGridRow> todoView;
        public ObservableCollection<DataGridRow> ToDoView
        {
            get { return todoView; }
            set
            {
                todoView = value;
                RaisePropertyChanged("ToDoView");

            }
        }

        #endregion


        #region メソッド

        /// <summary>
        /// DataGridRowからToDoへ変換
        /// </summary>
        private void ConvertToDB()
        {
            todoList.Clear();
            foreach (var row in todoView)
            {
                var todo = new ToDo
                {
                    Title = row.Title,
                    Deadline = row.Deadline,
                    Created = row.Created,
                    Updated = row.Updated,
                    Priority = row.prioritySt
                };
                todoList.Add(todo);
            }
        }

        /// <summary>
        /// ToDoからDataGridRowへ変換
        /// </summary>
        private void ConvertToRow()
        {
            todoView.Clear();
            foreach (var todo in todoList)
            {
                var row = new DataGridRow
                {
                    Title = todo.Title,
                    Deadline = todo.Deadline,
                    Created = todo.Created,
                    Updated = todo.Updated,
                    PrioritySt = todo.Priority
                };
                todoView.Add(row);
            }
        }

        /// <summary>
        /// データベースへ保存
        /// </summary>
        private void ExecuteSave(object obj)
        {
            ConvertToDB();
            da.Save(todoList);
            MessageBox.Show("Save successful.");
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteAdd(object obj)
        {
            var todo = new ToDo
            {
                Title = "",
                Deadline = DateTime.Now,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Priority = "A"
            };            
            todoList.Add(todo);
            ConvertToRow();
        }

        /// <summary>
        /// 消去
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteDelete(object obj)
        {
            var row = SelectedRow;
            todoView.Remove(row);
            ConvertToDB();
        }


        private void ExecuteClear(object obj)
        {
            var tempList = new ObservableCollection<DataGridRow>();

            foreach (var todo in todoView)
            {
                if (todo.Done) tempList.Add(todo);
            }

            foreach (var todo in tempList)  todoView.Remove(todo);
            ConvertToDB();
        }

        #endregion

        #region ボタンの実行判定
        private bool CanExecuteBtnClick()
        {
            return true;
        }

        private bool CanExecuteUpdClick()
        {
            var todo = SelectedRow;
            if (todo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CanExecuteClrClick()
        {
            foreach (var todo in todoView)
            {
                if (todo.Done == true)
                {
                    return true;
                }
            }
            return false;
        }


        #endregion

        #region リセット

        private void ExecuteReset(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete all ToDo?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Error);

            if (result == MessageBoxResult.Yes)
            {
                todoView.Clear();
            }
            ConvertToDB();
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

        private void ExecuteBack(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to revert it the last saved?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Error);

            if (result == MessageBoxResult.Yes)
            {
                todoList = da.ReadDatabase();
            }
            ConvertToRow();
        }

        public DelegateCommand<object> Back
        {
            get
            {
                btnClickCommand = new DelegateCommand<object>(ExecuteBack, CanExecuteBtnClick);
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

