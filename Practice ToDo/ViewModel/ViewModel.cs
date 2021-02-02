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
            deadline = DateTime.Today;
            da = new DataAccess();
            LoadPriority();
            todoList = da.ReadDatabase();
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

        public DelegateCommand<object> PriorityCommand
        {
            get
            {
                btnClickCommand = new DelegateCommand<object>(ExecutePriorityChange, CanExecuteUpdClick);
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


        private ObservableCollection<ToDo> todoList;
        public ObservableCollection<ToDo> ToDoList
        {
            get { return todoList; }
            set
            {
                RaisePropertyChanged("ToDoList");

            }
        }

        public Dictionary<int, string> Priorities { get; set; }



        #endregion


        #region メソッド

        /// <summary>
        /// データベースへ保存
        /// </summary>
        private void ExecuteSave(object obj)
        {
            da.Save(todoList);
            MessageBox.Show("Save successful.");
        }


        /// <summary>
        /// 優先度のロード
        /// </summary>
        private void LoadPriority()
        {
            Priorities = new Dictionary<int, string>();
            foreach (int x in Enum.GetValues(typeof(App.Priority)))
            {
                Priorities.Add(x, Enum.GetName(typeof(App.Priority), x));
            }

            Priority = Priorities[1];
        }

        private void UpdatedChange()
        {
            var todo = selectedToDo;
            var tmp = SelectedToDo;
            tmp.Updated = DateTime.Now;
            var index = todoList.IndexOf(todo);
            todoList.Remove(todo);
            todoList.Insert(index, tmp);
        }
        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="obj"></param>
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
            todoList.Add(todo);

        }

        /// <summary>
        /// 消去
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteDelete(object obj)
        {
            var todo = SelectedToDo;
            todoList.Remove(todo);
        }


        private void ExecuteClear(object obj)
        {
            var tempList = new ObservableCollection<ToDo>();

            foreach (var todo in todoList)
            {
                if (todo.Done) tempList.Add(todo);
            }

            foreach (var todo in tempList)  todoList.Remove(todo);
        }

        private void ExecutePriorityChange(object obj)
        {
            var todo = SelectedToDo;
            var tmp = SelectedToDo;
            tmp.Priority = priority.ToString();
            tmp.Updated = DateTime.Now;
            var index = todoList.IndexOf(todo);
            todoList.Remove(todo);
            todoList.Insert(index, tmp);
        }

        #endregion

        #region ボタンの実行判定
        private bool CanExecuteBtnClick()
        {
            return true;
        }

        private bool CanExecuteUpdClick()
        {
            var todo = SelectedToDo;
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
            foreach (var todo in todoList)
            {
                if (todo.Done == true)
                {
                    return true;
                }
            }
            return false;
        }


        #endregion

        #region デバッグ用

        private void ExecuteReset(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete all ToDo?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Error);

            if (result == MessageBoxResult.Yes)
            {
                todoList.Clear();
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

        private void ExecuteBack(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to revert it the last saved?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Error);

            if (result == MessageBoxResult.Yes)
            {
                todoList = da.ReadDatabase();
            }
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

