using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_ToDo
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティ変更時に発行
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> 
        /// PropertyChangedイベントを発行 
        /// </summary> 
        /// <param name="propertyName">プロパティ名</param> 
        public virtual void RaisePropertyChanged(string propertyName)
        {
            var h = this.PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
