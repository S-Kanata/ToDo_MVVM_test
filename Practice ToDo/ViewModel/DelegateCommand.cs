using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Practice_ToDo
{
    public class DelegateCommand<T> : ICommand
    {
        /// <summary>
        /// コマンドを実行するためのメソッドです。
        /// </summary>
        private Action<T> mExecute;

        /// <summary>
        /// コマンドが実行可能かどうかの判定です。
        /// </summary>
        private Func<bool> mCanExecute;

        /// <summary> 
        /// コマンドのExecuteメソッドで実行する処理を指定してDelegateCommandのインスタンスを生成します。
        /// </summary> 
        /// <param name="execute">Executeメソッドで実行する処理</param> 
        public DelegateCommand(Action<T> execute)
        {
            this.mExecute = execute;
        }

        /// <summary> 
        /// コマンドのExecuteメソッドで実行する処理を指定してDelegateCommandのインスタンスを生成します。
        /// </summary> 
        /// <param name="execute">Executeメソッドで実行する処理</param> 
        /// <param name="canExecute">CanExecuteメソッドで実行する処理</param> 
        public DelegateCommand(Action<T> execute, Func<bool> canExecute)
        {
            this.mExecute = execute;
            this.mCanExecute = canExecute;
        }

        /// <summary> 
        /// コマンドを実行します。 
        /// </summary> 
        public void Execute(object parameter)
        {
            this.mExecute((T)parameter);
        }

        /// <summary> 
        /// コマンドが実行可能な状態かどうか問い合わせます。 
        /// </summary> 
        /// <returns>実行可能な場合はtrue</returns> 
        public bool CanExecute()
        {
            return this.mCanExecute();
        }

        /// <summary> 
        /// ICommand.Executeの明示的な実装。Executeメソッドに処理を委譲します。 
        /// </summary> 
        /// <param name="parameter"></param> 
        void ICommand.Execute(object parameter)
        {
            this.Execute((T)parameter);
        }

        /// <summary> 
        /// ICommand.CanExecuteの明示的な実装。CanExecuteメソッドに処理を委譲します。 
        /// </summary> 
        /// <param name="parameter"></param> 
        /// <returns></returns> 
        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute();
        }

        /// <summary> 
        /// CanExecuteの結果に変更があったことを通知するイベントです。 
        /// WPFのCommandManagerのRequerySuggestedイベントをラップする形で実装しています。 
        /// </summary> 
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

    }
}
