using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace Calendar.Views.Schedule
{
    public class ProgrammeAppointment : Appointment
    {
        private string duration;
        private string programme;
        private string programmeLabel;
        private Brush labelBrush;
        private string programmeImageSource;
        private bool live;

        public string Duration
        {
            get
            {
                return this.Storage<ProgrammeAppointment>().duration;
            }
            set
            {
                var storage = this.Storage<ProgrammeAppointment>();
                if (storage.duration != value)
                {
                    storage.duration = value;
                    this.OnPropertyChanged(() => this.Duration);
                }
            }
        }

        public string Programme
        {
            get
            {
                return this.Storage<ProgrammeAppointment>().programme;
            }
            set
            {
                var storage = this.Storage<ProgrammeAppointment>();
                if (storage.programme != value)
                {
                    storage.programme = value;
                    this.OnPropertyChanged(() => this.Programme);
                }
            }
        }

        public string ProgrammeLabel
        {
            get
            {
                return this.Storage<ProgrammeAppointment>().programmeLabel;
            }
            set
            {
                var storage = this.Storage<ProgrammeAppointment>();
                if (storage.programmeLabel != value)
                {
                    storage.programmeLabel = value;
                    this.OnPropertyChanged(() => this.ProgrammeLabel);
                }
            }
        }

        public Brush LabelBrush
        {
            get
            {
                return this.Storage<ProgrammeAppointment>().labelBrush;
            }
            set
            {
                var storage = this.Storage<ProgrammeAppointment>();
                if (storage.labelBrush != value)
                {
                    storage.labelBrush = value;
                    this.OnPropertyChanged(() => this.LabelBrush);
                }
            }
        }

        public string ProgrammeImageSource
        {
            get
            {
                return this.Storage<ProgrammeAppointment>().programmeImageSource;
            }
            set
            {
                var storage = this.Storage<ProgrammeAppointment>();
                if (storage.programmeImageSource != value)
                {
                    storage.programmeImageSource = value;
                    this.OnPropertyChanged(() => this.ProgrammeImageSource);
                }
            }
        }

        public bool IsLive
        {
            get
            {
                return this.Storage<ProgrammeAppointment>().live;
            }
            set
            {
                ProgrammeAppointment storage = this.Storage<ProgrammeAppointment>();
                if (storage.live != value)
                {
                    storage.live = value;
                    this.OnPropertyChanged(() => this.IsLive);
                }
            }
        }

        private Dictionary<string, Brush> ProgrammeBrushes
        {
            get
            {
                var dictionary = new Dictionary<string, Brush>();
                dictionary.Add("NEWS", new SolidColorBrush(Color.FromArgb(0xFF, 0xC6, 0x2B, 0x6D)));
                dictionary.Add("SPORTS", new SolidColorBrush(Color.FromArgb(0xFF, 0x16, 0xAB, 0xA9)));
                dictionary.Add("KIDS", new SolidColorBrush(Color.FromArgb(0xFF, 0x8E, 0xBC, 0x00)));
                dictionary.Add("MOVIES", new SolidColorBrush(Color.FromArgb(0xFF, 0x2F, 0x64, 0x95)));
                dictionary.Add("SHOWS", new SolidColorBrush(Color.FromArgb(0xFF, 0x7E, 0x51, 0xA1)));
                return dictionary;
            }

        }

        public override IAppointment Copy()
        {
            IAppointment appointment = new ProgrammeAppointment();
            appointment.CopyFrom(this);
            return appointment;
        }

        public override void CopyFrom(IAppointment other)
        {
            ProgrammeAppointment appointment = other as ProgrammeAppointment;
            if (appointment != null)
            {
                this.Duration = appointment.Duration;
                if (appointment.Resources.Any())
                {
                    var resourcesCollection = (ResourceCollection)appointment.Resources;
                    this.Programme = resourcesCollection.First(r => r.ResourceType == "TV").DisplayName;
                    this.ProgrammeLabel = resourcesCollection.Count > 1 ? resourcesCollection.First(r => r.ResourceType == "Programme").DisplayName.ToUpperInvariant() : "NEWS";
                    if (resourcesCollection.Count == 1)
                    {
                        appointment.Resources.Add(new Resource() { ResourceType = "Programme", ResourceName = "News" });
                    }
                }
                this.LabelBrush = this.GetProgrammeBrush(this.ProgrammeLabel);
                this.ProgrammeImageSource = appointment.ProgrammeImageSource;
                this.IsLive = appointment.IsLive;
            }
            base.CopyFrom(other);
        }

        private Brush GetProgrammeBrush(string programme)
        {
            if (string.IsNullOrEmpty(programme))
            {
                return this.ProgrammeBrushes["NEWS"];
            }

            return this.ProgrammeBrushes[programme];
        }
    }
}
