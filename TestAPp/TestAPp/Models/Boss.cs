using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Realms;
using static Android.Resource;


namespace TestAPp.Models
{
 

    public class Boss : RealmObject, INotifyPropertyChanged
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public int CooldownHours { get; set; } = 20;

        public bool Show { get; set; } = true;

        private DateTimeOffset defeated { get; set; } = DateTimeOffset.Now.AddDays(-1000);
        public DateTimeOffset Defeated { 
            get 
            {
                return defeated;
            } 
            set 
            {
                if (defeated != value)
                {
                    defeated = value;
                    OnPropertyChanged("Defeated");
                }
            }
        }

        private string timeToDefeat { get; set; } = "OK";


        public string TimeToDefeat
        {
            get 
            {
                return timeToDefeat;
            }
            set 
            {

                if (timeToDefeat != value)
                {
                    timeToDefeat = value;
                    OnPropertyChanged("TimeToDefeat");
                }

            }
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }
}
