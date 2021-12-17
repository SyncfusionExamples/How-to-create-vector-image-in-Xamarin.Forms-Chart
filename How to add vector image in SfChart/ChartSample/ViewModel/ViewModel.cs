using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ChartSample
{
    public class Model
    {
        public double XValue { get; set; }
        public double YValue { get; set; }

        public Model(double xVal, double yVal)
        {
            XValue = xVal;
            YValue = yVal;
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Model> data;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Model> Data
        {
            get { return data; }
            set
            {
                data = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Data"));
            }
        }

        public ViewModel()
        {
            Data = new ObservableCollection<Model>()
            {
                new Model(12, 40),
                new Model(14, 45),
                new Model(16, 48),
                new Model(18, 52),
            };

        }
    }
}