using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Calendar;
using Telerik.Windows.Controls.ScheduleView;

namespace Calendar.Views.Schedule
{
    public class ViewModel : ExampleViewModel<Appointment>
    {
        private DelegateCommand setCategoryCommand;
        private DelegateCommand setTimeMarkerCommand;
        private DelegateCommand setWeekViewCommand;
        private DelegateCommand setTodayCommand;
        private DelegateCommand setWorkWeekCommand;

        private IOccurrence selectedAppointment;
        private bool showNewsChannel = true;
        private bool showEntertainmentChannel = true;
        private bool showSportsChannel = true;
        private GroupDescriptionCollection groupDescriptions;
        private Func<object, bool> groupFilter;
        private DateTime currentDate;
        private DateTime displayDate;
        private int activeViewDefinitionIndex;
        private Slot selectedSlot;
        private DisplayMode displayMode;

        public ViewModel()
        {
            this.SetCategoryCommand = new DelegateCommand(this.SetCategoryCommandExecuted, this.SetCategoryCommandCanExecute);
            this.SetTimeMarkerCommand = new DelegateCommand(this.SetTimeMarkerCommandExecuted, this.SetTimeMarkerCommandCanExecute);
            this.SetTodayCommand = new DelegateCommand(this.SetTodayCommandExecuted, this.SetTodayCommandCanExecute);
            this.SetWorkWeekCommand = new DelegateCommand(this.SetWorkWeekCommandExecuted, this.SetWorkWeekCommandCanExecute);
            this.SetWeekViewCommand = new DelegateCommand(this.SetWeekViewCommandExecuted, this.SetWeekViewCommandCanExecute);
            this.CurrentDate = DateTime.Today;
        }

        public DelegateCommand SetCategoryCommand
        {
            get
            {
                return this.setCategoryCommand;
            }
            set
            {
                this.setCategoryCommand = value;
            }
        }

        public DelegateCommand SetTimeMarkerCommand
        {
            get
            {
                return this.setTimeMarkerCommand;
            }
            set
            {
                this.setTimeMarkerCommand = value;
            }
        }

        public DelegateCommand SetTodayCommand
        {
            get
            {
                return this.setTodayCommand;
            }
            set
            {
                this.setTodayCommand = value;
            }
        }

        public DelegateCommand SetWorkWeekCommand
        {
            get
            {
                return this.setWorkWeekCommand;
            }
            set
            {
                this.setWorkWeekCommand = value;
            }
        }

        public DelegateCommand SetWeekViewCommand
        {
            get
            {
                return this.setWeekViewCommand;
            }
            set
            {
                this.setWeekViewCommand = value;
            }
        }


        public Slot SelectedSlot
        {
            get
            {
                return this.selectedSlot;
            }
            set
            {
                if (this.selectedSlot != value)
                {
                    this.selectedSlot = value;
                    this.OnPropertyChanged(() => this.SelectedSlot);
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public IOccurrence SelectedAppointment
        {
            get
            {
                return this.selectedAppointment;
            }
            set
            {
                if (this.selectedAppointment != value)
                {
                    this.selectedAppointment = value;
                    this.OnPropertyChanged(() => this.SelectedAppointment);

                    CommandManager.InvalidateRequerySuggested();
                    this.SetCategoryCommand.InvalidateCanExecute();
                    this.SetTimeMarkerCommand.InvalidateCanExecute();
                }
            }
        }

        public bool ShowNewsChannel
        {
            get
            {
                return this.showNewsChannel;
            }
            set
            {
                if (this.showNewsChannel != value)
                {
                    this.showNewsChannel = value;
                    this.OnPropertyChanged(() => this.ShowNewsChannel);
                    this.UpdateGroupFilter();
                }
            }
        }

        public bool ShowEntertainmentChannel
        {
            get
            {
                return this.showEntertainmentChannel;
            }
            set
            {
                if (this.showEntertainmentChannel != value)
                {
                    this.showEntertainmentChannel = value;
                    this.OnPropertyChanged(() => this.ShowEntertainmentChannel);
                    this.UpdateGroupFilter();
                }
            }
        }

        public bool ShowSportsChannel
        {
            get
            {
                return this.showSportsChannel;
            }
            set
            {
                if (this.showSportsChannel != value)
                {
                    this.showSportsChannel = value;
                    this.OnPropertyChanged(() => this.ShowSportsChannel);
                    this.UpdateGroupFilter();
                }
            }
        }

        public GroupDescriptionCollection GroupDescriptions
        {
            get
            {
                if (this.groupDescriptions == null)
                {
                    this.groupDescriptions = new GroupDescriptionCollection() { new DateGroupDescription() };
                    this.UpdateGroupDescriptions();
                }
                return this.groupDescriptions;
            }
        }

        public Func<object, bool> GroupFilter
        {
            get
            {
                return this.groupFilter;
            }
            set
            {
                this.groupFilter = value;
                this.OnPropertyChanged(() => this.GroupFilter);
            }
        }

        public DateTime CurrentDate
        {
            get
            {
                return this.currentDate;
            }
            set
            {
                this.currentDate = value;
                this.DisplayDate = value;
                this.DisplayMode = DisplayMode.MonthView;
                this.OnPropertyChanged(() => this.CurrentDate);
            }
        }

        public DateTime DisplayDate
        {
            get
            {
                return this.displayDate;
            }
            set
            {
                this.displayDate = value;
                this.OnPropertyChanged(() => this.DisplayDate);
            }
        }

        public DisplayMode DisplayMode
        {
            get
            {
                return displayMode;
            }
            set
            {
                displayMode = value;
                this.OnPropertyChanged(() => this.DisplayMode);
            }
        }

        public int ActiveViewDefinitionIndex
        {
            get
            {
                return this.activeViewDefinitionIndex;
            }
            set
            {
                this.activeViewDefinitionIndex = value;
                this.OnPropertyChanged(() => this.ActiveViewDefinitionIndex);
                this.SetWorkWeekCommand.InvalidateCanExecute();
                this.SetWeekViewCommand.InvalidateCanExecute();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public void SetCategoryCommandExecuted(object parameter)
        {
            if (this.SelectedAppointment == null)
            {
                RadWindow.Alert("Please, select an appointment first.");
            }
            else
            {
                Appointment appointmentToEdit = null;
                Category c = parameter as Category;

                if (this.SelectedAppointment is Appointment)
                {
                    appointmentToEdit = (from app in this.Appointments where app.Equals(this.SelectedAppointment) select app).FirstOrDefault();
                    appointmentToEdit.Category = c;
                }
                else
                {
                    appointmentToEdit = this.Appointments.Where(a => a.Equals((this.SelectedAppointment as Occurrence).Master)).FirstOrDefault();

                    var exceptionAppointment = appointmentToEdit.RecurrenceRule.Exceptions.SingleOrDefault(a => a.Appointment.Equals((this.SelectedAppointment as Occurrence).Appointment));
                    if (exceptionAppointment != null)
                    {
                        appointmentToEdit.RecurrenceRule.Exceptions.Remove(exceptionAppointment);
                        (exceptionAppointment.Appointment as Appointment).Category = c;
                        appointmentToEdit.RecurrenceRule.Exceptions.Add(exceptionAppointment);
                    }
                    else
                    {
                        appointmentToEdit.Category = c;
                    }

                }

                var index = this.Appointments.IndexOf(appointmentToEdit);
                this.Appointments.Remove(appointmentToEdit);
                this.Appointments.Insert(index, appointmentToEdit);
            }
        }

        public void SetTimeMarkerCommandExecuted(object parameter)
        {
            if (this.SelectedAppointment == null)
            {
                RadWindow.Alert("Please, select an appointment first.");
            }
            else
            {
                Appointment appointmentToEdit = null;
                TimeMarker t = parameter as TimeMarker;

                if (this.SelectedAppointment is Appointment)
                {
                    appointmentToEdit = (from app in this.Appointments where app.Equals(this.SelectedAppointment) select app).FirstOrDefault();
                    appointmentToEdit.TimeMarker = t;
                }
                else
                {
                    appointmentToEdit = this.Appointments.Where(a => a.Equals((this.SelectedAppointment as Occurrence).Master)).FirstOrDefault();

                    var exceptionAppointment = appointmentToEdit.RecurrenceRule.Exceptions.SingleOrDefault(a => a.Appointment.Equals((this.SelectedAppointment as Occurrence).Appointment));
                    if (exceptionAppointment != null)
                    {
                        appointmentToEdit.RecurrenceRule.Exceptions.Remove(exceptionAppointment);
                        (exceptionAppointment.Appointment as Appointment).TimeMarker = t;
                        appointmentToEdit.RecurrenceRule.Exceptions.Add(exceptionAppointment);
                    }
                    else
                    {
                        appointmentToEdit.TimeMarker = t;
                    }
                }

                var index = this.Appointments.IndexOf(appointmentToEdit);
                this.Appointments.Remove(appointmentToEdit);
                this.Appointments.Insert(index, appointmentToEdit);
            }
        }

        public void SetTodayCommandExecuted(object parameter)
        {
            this.CurrentDate = DateTime.Today;
        }

        public void SetWorkWeekCommandExecuted(object parameter)
        {
            this.ActiveViewDefinitionIndex = 2;
        }

        public void SetWeekViewCommandExecuted(object parameter)
        {
            this.ActiveViewDefinitionIndex = 1;
        }

        public bool CheckAppointmentSelection()
        {
            return this.SelectedAppointment != null;
        }

        public bool SetCategoryCommandCanExecute(object parameter)
        {
            return this.CheckAppointmentSelection();
        }

        public bool SetTodayCommandCanExecute(object parameter)
        {
            return true;
        }

        public bool SetWorkWeekCommandCanExecute(object parameter)
        {
            return this.ActiveViewDefinitionIndex != 2;
        }

        public bool SetWeekViewCommandCanExecute(object parameter)
        {
            return this.ActiveViewDefinitionIndex != 1;
        }

        public bool SetTimeMarkerCommandCanExecute(object parameter)
        {
            return this.CheckAppointmentSelection();
        }

        private void UpdateGroupFilter()
        {
            this.GroupFilter = new Func<object, bool>(this.GroupFilterFunc);
        }

        private bool GroupFilterFunc(object groupName)
        {
            IResource resource = groupName as IResource;

            return resource == null ? true : this.GetEnabledGroups().Contains(resource.ResourceName, StringComparer.OrdinalIgnoreCase);
        }

        private IEnumerable<string> GetEnabledGroups()
        {
            List<string> enabledGroups = new List<string>();

            if (this.ShowNewsChannel) enabledGroups.Add("LiveCastNews");
            if (this.ShowEntertainmentChannel) enabledGroups.Add("Voozy");
            if (this.ShowSportsChannel) enabledGroups.Add("Sportix");

            return enabledGroups;
        }

        private void UpdateGroupDescriptions()
        {
            ResourceGroupDescription groupDescription = new ResourceGroupDescription();
            groupDescription.ResourceType = "TV";
            this.GroupDescriptions.Add(groupDescription);
        }
    }
}
