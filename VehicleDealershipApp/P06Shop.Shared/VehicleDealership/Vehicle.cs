using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Shop.Shared.VehicleDealership
{
    // data annotations 
    //public class Vehicle
    //{
    //    [Key]
    //    public int Code { get; set; }

    //    public int Id { get; set; }

    //    [MaxLength(100)]
    //    public string Title { get; set; }

    //    public string Description { get; set; }
    //}

    // fluent api 
    public class Vehicle : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Model { get; set; }
        public string Fuel { get; set; }

        // public string Barcode { get; set; }

        // public double Price { get; set; }

        // public DateTime ReleaseDate { get; set; }


        // private string _description;


        // public string Description
        // {
        //     get { return _description; }
        //     set
        //     {
        //         if (_description != value)
        //         {
        //             _description = value;
        //             OnPropertyChanged(nameof(Description));
        //         }
        //     }
        // }



        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
