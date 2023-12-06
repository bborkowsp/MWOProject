using System.ComponentModel;


namespace VehicleDealershipApp.VehicleDomain.Model
{
    public class Vehicle : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Fuel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
