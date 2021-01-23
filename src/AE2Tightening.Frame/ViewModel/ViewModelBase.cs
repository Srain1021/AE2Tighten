using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AE2Tightening.Frame.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //调用主线程
        public ISynchronizeInvoke SynchronizeInvoker { get; set; }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        protected void SetProperty<T>(ref T obj, T value, [CallerMemberName] string propertyName = null)
        {
            if (obj != null && value != null && obj.Equals(value)) return;
            obj = value;
            this.OnPropertyChanged(propertyName);
        }

        protected void SetPropertyEqual<T>(ref T obj,T value, [CallerMemberName] string propertyName = null)
        {
            obj = value;
            this.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// 更新控件
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null || SynchronizeInvoker == null) return;

            var e = new PropertyChangedEventArgs(propertyName);

            if (SynchronizeInvoker.InvokeRequired)
            {
                SynchronizeInvoker.Invoke(PropertyChanged, new object[] { this, e });
            }
            else
            {
                PropertyChanged(this, e);
            }
        }

    }
}
