using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_ToDo.ViewModel
{
     public class DataGridRow : ViewModelBase
    {

        #region プロパティ
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                updated = DateTime.Now;
                RaisePropertyChanged("Title");
                RaisePropertyChanged("Updated");
            }
        }

        private DateTime deadline;
        public DateTime Deadline
        {
            get { return deadline; }
            set
            {
                deadline = value;
                updated = DateTime.Now;
                RaisePropertyChanged("Deadline");
                RaisePropertyChanged("Updated");
            }
        }

        private bool done;
        public bool Done
        {
            get { return done; }
            set
            {
                done = value;
                updated = DateTime.Now;
                RaisePropertyChanged("Done");
                RaisePropertyChanged("Updated");
            }
        }

        private DateTime created;
        public DateTime Created
        {
            get { return created; }
            set
            {
                created = value;
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

        /// <summary>
        /// 個別の優先度
        /// </summary>
        public string prioritySt { get; set; }

        public string PrioritySt
        {
            get { return prioritySt; }
            set
            {
                prioritySt = value;
                updated = DateTime.Now;
                RaisePropertyChanged("PrioritySt");
                RaisePropertyChanged("Updated");
            }
        }

        /// <summary>
        /// 優先度のリスト
        /// </summary>
        public Dictionary<int, string> Priorities { get; set; } = new Dictionary<int, string> { { 1, "A" }, { 2, "B" }, { 3, "C" }, };

        #endregion
    }
}
