using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Practice_ToDo
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>

    #region DBパスの指定
    public partial class App : Application
    {
        static string databaseName = "ToDo.db";
        static string folderPath = Environment.CurrentDirectory;
        public static string DatabasePath = System.IO.Path.Combine(folderPath, databaseName);

        public enum Priority
        {
            A = 1,
            B = 2,
            C = 3
        }
    }
    # endregion
}
