using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class StaffAssignmentViewModel : ViewModelBase
    {
        public ObservableCollection<Semana> GridSemana { get; set; }
        public CollectionViewSource ViewSource { get; set; }
        public StaffAssignmentViewModel()
        {
            GridSemana = new ObservableCollection<Semana>();
            GridSemana.Add(new Semana() { NroSemana = "1",
                Lunes = new Semana.DiaSemana {
                    ReferenceDate = DateTime.Now,
                    Turnos = new ObservableCollection<Semana.DiaSemana.TurnoAsignado>() { new Semana.DiaSemana.TurnoAsignado {Turno="T1", P1="J. P.", P2 = "J. X.", P3 = "J. X.", P4 = "J. X.", P5 = "J. X." }, } },
                Martes = new Semana.DiaSemana
                {
                    ReferenceDate = DateTime.Now,
                    Turnos = new ObservableCollection<Semana.DiaSemana.TurnoAsignado>() { new Semana.DiaSemana.TurnoAsignado { Turno = "T1", P1 = "J. P.", P2 = "J. X.", P3 = "J. X.", P4 = "J. X.", P5 = "J. X." }, }
                },
                Miercoles = new Semana.DiaSemana
                {
                    ReferenceDate = DateTime.Now,
                    Turnos = new ObservableCollection<Semana.DiaSemana.TurnoAsignado>() { new Semana.DiaSemana.TurnoAsignado { Turno = "T1", P1 = "J. P.", P2 = "J. X.", P3 = "J. X.", P4 = "J. X.", P5 = "J. X." }, }
                },
                Jueves = new Semana.DiaSemana
                {
                    ReferenceDate = DateTime.Now,
                    Turnos = new ObservableCollection<Semana.DiaSemana.TurnoAsignado>() { new Semana.DiaSemana.TurnoAsignado { Turno = "T1", P1 = "J. P.", P2 = "J. X.", P3 = "J. X.", P4 = "J. X.", P5 = "J. X." }, }
                },
                Viernes = new Semana.DiaSemana
                {
                    ReferenceDate = DateTime.Now,
                    Turnos = new ObservableCollection<Semana.DiaSemana.TurnoAsignado>() { new Semana.DiaSemana.TurnoAsignado { Turno = "T1", P1 = "J. P.", P2 = "J. X.", P3 = "J. X.", P4 = "J. X.", P5 = "J. X." }, }
                },
                Sabado = new Semana.DiaSemana
                {
                    ReferenceDate = DateTime.Now,
                    Turnos = new ObservableCollection<Semana.DiaSemana.TurnoAsignado>() { new Semana.DiaSemana.TurnoAsignado { Turno = "T1", P1 = "J. P.", P2 = "J. X.", P3 = "J. X.", P4 = "J. X.", P5 = "J. X." }, }
                },
                Domingo = new Semana.DiaSemana
                {
                    ReferenceDate = DateTime.Now,
                    Turnos = new ObservableCollection<Semana.DiaSemana.TurnoAsignado>() { new Semana.DiaSemana.TurnoAsignado { Turno = "T1", P1 = "J. P.", P2 = "J. X.", P3 = "J. X.", P4 = "J. X.", P5 = "J. X." }, }
                }
            });
            GridSemana.Add(new Semana() { NroSemana = "2" });
            GridSemana.Add(new Semana() { NroSemana = "3" });
            GridSemana.Add(new Semana() { NroSemana = "4" });
            GridSemana.Add(new Semana() { NroSemana = "5" });

            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = GridSemana;
        }

        public class Semana  : BindableBase
        {
            private string _nrosemana;
            private DiaSemana _lunes;
            private DiaSemana _martes;
            private DiaSemana _miercoles;
            private DiaSemana _jueves;
            private DiaSemana _viernes;
            private DiaSemana _sabado;
            private DiaSemana _domingo;

            public string NroSemana
            {
                get { return _nrosemana; }
                set
                {
                    if (_nrosemana == value) return;
                    _nrosemana = value;
                    SetProperty(ref _nrosemana, value);
                }
            }
            public DiaSemana Lunes
            {
                get { return _lunes; }
                set
                {
                    if (_lunes == value) return;
                    _lunes = value;
                    SetProperty(ref _lunes, value);
                }
            }
            public DiaSemana Martes
            {
                get { return _martes; }
                set
                {
                    if (_martes == value) return;
                    _martes = value;
                    SetProperty(ref _martes, value);
                }
            }
            public DiaSemana Miercoles
            {
                get { return _miercoles; }
                set
                {
                    if (_miercoles == value) return;
                    _miercoles = value;
                    SetProperty(ref _miercoles, value);
                }
            }
            public DiaSemana Jueves
            {
                get { return _jueves; }
                set
                {
                    if (_jueves == value) return;
                    _jueves = value;
                    SetProperty(ref _jueves, value);
                }
            }
            public DiaSemana Viernes
            {
                get { return _viernes; }
                set
                {
                    if (_viernes == value) return;
                    _viernes = value;
                    SetProperty(ref _viernes, value);
                }
            }
            public DiaSemana Sabado
            {
                get { return _sabado; }
                set
                {
                    if (_sabado == value) return;
                    _sabado = value;
                    SetProperty(ref _sabado, value);
                }
            }
            public DiaSemana Domingo
            {
                get { return _domingo; }
                set
                {
                    if (_domingo == value) return;
                    _domingo = value;
                    SetProperty(ref _domingo, value);
                }
            }

            public class DiaSemana : BindableBase
            {
                private DateTime _referenceDate;

                public DateTime ReferenceDate
                {
                    get { return _referenceDate; }
                    set
                    {
                        if (_referenceDate == value) return;
                        _referenceDate = value;
                        SetProperty(ref _referenceDate, value);
                    }
                }
                public ObservableCollection<TurnoAsignado> Turnos;

                public class TurnoAsignado : BindableBase
                {
                    private string _p1;
                    private string _p2;
                    private string _p3;
                    private string _p4;
                    private string _p5;
                    private string _turno;

                    public string Turno
                    {
                        get { return _turno; }
                        set
                        {
                            if (_turno == value) return;
                            _turno = value;
                            SetProperty(ref _turno, value);
                        }
                    }

                    public string P1
                    {
                        get { return _p1; }
                        set
                        {
                            if (_p1 == value) return;
                            _p1 = value;
                            SetProperty(ref _p1, value);
                        }
                    }

                    public string P2
                    {
                        get { return _p2; }
                        set
                        {
                            if (_p2 == value) return;
                            _p2 = value;
                            SetProperty(ref _p2, value);
                        }
                    }

                    public string P3
                    {
                        get { return _p3; }
                        set
                        {
                            if (_p3 == value) return;
                            _p3 = value;
                            SetProperty(ref _p3, value);
                        }
                    }

                    public string P4
                    {
                        get { return _p4; }
                        set
                        {
                            if (_p4 == value) return;
                            _p4 = value;
                            SetProperty(ref _p4, value);
                        }
                    }

                    public string P5
                    {
                        get { return _p5; }
                        set
                        {
                            if (_p5 == value) return;
                            _p5 = value;
                            SetProperty(ref _p5, value);
                        }
                    }
                }
            }
        }
    }
}
