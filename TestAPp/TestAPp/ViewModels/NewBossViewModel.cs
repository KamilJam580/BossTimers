using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TestAPp.Models;
using Xamarin.Forms;

namespace TestAPp.ViewModels
{
    public class NewBossViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private string name;
        private int colddown;

        public NewBossViewModel()
        {
            Title = "Add new boss";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public int ColdDown
        {
            get => colddown;
            set => SetProperty(ref colddown, value);
        }


        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Boss boss = new Boss() { Name = Name, CooldownHours = ColdDown, ImagePath = "Alptramun.png", Id = Guid.NewGuid().ToString() };
            await UserBossDataStore.AddBossAsync(boss);

            await Shell.Current.GoToAsync("..");
        }
    }
}
