using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using PrismUnityApp1.Database;
using System.Linq;
using PrismUnityApp1.Model;
using Prism.Services;

namespace PrismUnityApp1.ViewModels
{
    public class AddNewReminderViewModel : BindableBase, IConfirmNavigation,INavigatingAware
    {
        private INavigationService _navigationService;
        public DelegateCommand AddTaskCommand { get; private set; }
        private ReminderItemDatabase _database;
        private MainPageViewModel _parent;
        private IPageDialogService _dialogService;


        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); RaisePropertyChanged(); }
        }

        private DateTime _dateselected=DateTime.Now.Date;
        public DateTime DateSelected
        {
            get { return _dateselected; }
            set {
                _dateselected = value;
                RaisePropertyChanged(); }
        }

        private TimeSpan _timeSelected = DateTime.Now.TimeOfDay;
        

        public TimeSpan TimeSelected
        {
            get { return _timeSelected; }
            set
            {
                _timeSelected = value;
                RaisePropertyChanged();
            }
        }

        

        public AddNewReminderViewModel(ReminderItemDatabase database,MainPageViewModel parent, INavigationService navigationService,IPageDialogService dialogService )
        {
            _database = database;
            _parent = parent;
            _navigationService = navigationService;
            _dialogService = dialogService;
            AddTaskCommand = new DelegateCommand(AddTask);
            Message = "Add you message here";
        }


        public bool CanNavigate(NavigationParameters parameters)
        {
            return true;
        }

        public void AddTask()
        {
            ReminderItem reminder = new ReminderItem();
            reminder.Message = Message;
            reminder.RemindingTime = DateSelected;
            reminder.RemindingTime=reminder.RemindingTime.AddHours(Convert.ToDouble(TimeSelected.Hours));
            reminder.RemindingTime=reminder.RemindingTime.AddMinutes(Convert.ToDouble(TimeSelected.Minutes));
            _database.AddThought(reminder);
            var remiderService = Xamarin.Forms.DependencyService.Get<IReminderService>();
            remiderService.Remind(reminder.RemindingTime, Message, Message);
            _parent.Refresh();
            _navigationService.GoBackAsync();
            
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }




    }
}
