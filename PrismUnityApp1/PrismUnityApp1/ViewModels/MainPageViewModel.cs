using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismUnityApp1.Database;
using PrismUnityApp1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace PrismUnityApp1.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {

        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        public DelegateCommand NavigateToSpeakPageCommand { get; private set; }
        public DelegateCommand TappedCommand { get; private set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand<ReminderItemDB> ListItemTappedCommand { get; set; }
        private ReminderItemDatabase _database;


        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private List<ReminderItemDB> listOfReminderItem;


        public List<ReminderItemDB> ListOfReminderItem
        {
            get { return listOfReminderItem; }
            set {
                listOfReminderItem = value;
                    RaisePropertyChanged();
            }
        }

        private List<ReminderItemDB> tmpOfReminderItem;


        public List<ReminderItemDB> TmpOfReminderItem
        {
            get { return tmpOfReminderItem; }
            set
            {
                tmpOfReminderItem = value;               
            }
        }

        public MainPageViewModel(INavigationService navigationService, ReminderItemDatabase database, IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            NavigateToSpeakPageCommand = new DelegateCommand(NavigateToSpeakPage);
            TappedCommand = new DelegateCommand(Tapped);
            AddCommand = new DelegateCommand(Add);
            ListItemTappedCommand = new DelegateCommand<ReminderItemDB>(ListItemTapped);

            _database = database;
            

            GetListofReminders();
            
            
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            GetListofReminders();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }

        private void NavigateToSpeakPage()
        {
            _navigationService.NavigateAsync("SpeakPage");
        }


        private void Tapped()
        {
            _navigationService.NavigateAsync("AddNewReminder");
        }
        private void Add()
        {
            _navigationService.NavigateAsync("SpeakPage");
        }

        public void Refresh()
        {
            GetListofReminders();
        }

        private void ListItemTapped(ReminderItemDB param)
        {
            ReminderItemDB list = param as ReminderItemDB;
            IActionSheetButton cancelAction = ActionSheetButton.CreateCancelButton("Cancel", new DelegateCommand(() =>
            {
                CancelListhighlight();
            }));
            IActionSheetButton destroyAction = ActionSheetButton.CreateDestroyButton("Destroy", new DelegateCommand(() => 
            {
                DeleteListItem(list);

            }));
            _pageDialogService.DisplayActionSheetAsync("My Action Sheet", cancelAction, destroyAction);

        }

        private void DeleteListItem(ReminderItemDB list)
        {
            var itemSelected = list;
            _database.DeleteReminder(itemSelected.ID);
            Refresh();
        }

        private void CancelListhighlight()
        {
            Refresh();
        }


        public void GetListofReminders()
        {
            TmpOfReminderItem = _database.GetReminders().ToList();
            foreach (ReminderItemDB r in TmpOfReminderItem)
            {
                if (r.dateTime.ToLocalTime() < DateTime.Now)
                {
                    _database.DeleteReminder(r.ID);
                }
            }
            ListOfReminderItem = _database.GetReminders().ToList();
            for (int i = 0; i < ListOfReminderItem.Count; i++)
            {
                ListOfReminderItem[i].dateTime = ListOfReminderItem[i].dateTime.ToLocalTime();
                ListOfReminderItem[i].dateTime = Convert.ToDateTime(ListOfReminderItem[i].dateTime.ToString("dd/MM/yyyy hh:mm:ss.fff",
                                        CultureInfo.InvariantCulture));
            }
        }
    }
}
