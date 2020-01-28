using System;

namespace SPZ_Lab2
{

    public interface IRecordingStudio {

        // добавить/удалить комнату
        void AddRoom();
        void RemoveRoom();

        // нанять/уволить сотрудника
        void AddWorker();
        void RemoveWorker();

        // купить/продать инструменты
        void AddTools(int amount);
        void RemoveTools(int amount);

        // доход студии за месяц
        float ProfitForMonth();

    }



    class RecordingStudio : IRecordingStudio, ICloneable
    {

        private int _amountWorkers;          // количество сотрудников
        private string _name;                // имя студии
        private string _address;             // адрес студии
        private float _trackCost;            // стоимость 1 трэка
        private int _trackTime;            // время создания 1 трэка
        private float _salaryWorker;         // зарплата 1 сотрудника
        private float _account;              // касса студии
        private int _amountTools;            // количество инструментов
        private int _amountRooms;            // количество комнат
        private float _roomCost;             // стоимость комнаты
        private float _toolCost;             // стоимость 1 инструмента


        // свойства класса
        
        // имя студии
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        // адрес студии
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        // цена за один трэк
        public float TrackCost
        {
            get { return _trackCost; }
            set { _trackCost = value >= 0 ? value : 0; }
        }
        // время создания 1 трэка
        public int TrackTime
        {
            get { return _trackTime; }
            set { _trackTime = value > 0 ? value : 1; }
        }
        // зарплата 1 сотрудника
        public float SalaryWorker
        {
           get { return _salaryWorker; }
           set { _salaryWorker = value > 0 ? value : 1; }
        }
        // счет студии (касса)
        public float Account
        {
            get { return _account; }
            set { _account = value >= 0 ? value : 0; }
        }
        // стоимость 1 комнаты
        public float RoomCost
        {
            get { return _roomCost; }
            set { _roomCost = value >= 0 ? value : 0; }
        }
        // стоимость 1 инструмента
        public float ToolCost
        {
            get { return _toolCost; }
            set { _toolCost = value >= 0 ? value : 0; }
        }
        // зарплата всех сотрудников
        public float SalaryWorkers
        {
            get { return (SalaryWorker * _amountWorkers); }
        }
        // количество сотрудников
        public int AmountWorkers
        {
            get { return _amountWorkers; }
            private set { _amountWorkers = value >= 0 ? value : 0; }
        }
        // количество комнат
        public int AmountRooms
        {
            get { return _amountRooms; }
            private set { _amountRooms = value >= 0 ? value : 0; }
        }
        // количество инструментов
        public int AmountTools
        {
            get { return _amountTools; }
            private set { _amountTools = value >= 0 ? value : 0; }
        }


        // конструктор класса RecordingStudio
        public RecordingStudio(string name, string address,
                               float trackCost, int trackTime,
                               float salary, float account,
                               float roomCost, float toolCost )
        {
            Name = name;
            Address = address;
            TrackCost = trackCost;
            TrackTime = trackTime;
            SalaryWorker = salary;
            Account = account;
            RoomCost = roomCost;
            ToolCost = toolCost;
            _amountWorkers = _amountTools = _amountRooms = 0;
        }

        // конструктор по умолчанию
        public RecordingStudio() : this("This is studio name", "221b, Baker street", 100, 4, 1000, 5000, 500, 50) { }


       // реализация интерфейса IRecordingStudio

       // добавление комнаты
       public void AddRoom()
        {
            // количество требуемых инструментов
            int amountNeedTools = (2 - (AmountTools - 2 * AmountRooms));

            // уточнение требуемых инструментов только для новой комнаты
            if (amountNeedTools > 2) amountNeedTools = 2;
            else if (amountNeedTools < 0) amountNeedTools = 0;

            // цена за комнату с учетом требуемых инструментов
            float cost = (RoomCost + ToolCost * amountNeedTools);

            // если денег хватает, мы покупаем комнату
            if ( cost <= Account)
            {
                Account -= cost;
                AmountRooms++;
                AmountTools += amountNeedTools;
            }
        }

       // удаление комнаты
       public void RemoveRoom()
        {
            _amountRooms--;
        }

        //добавление сотрудника
        public void AddWorker()
        {
            _amountWorkers++;
        }

        // увольнение сотрудника
        public void RemoveWorker()
        {
            _amountWorkers--;
        }

        // покупка инструментов
        public void AddTools(int amount)
        {
            if (amount > 0 && amount * ToolCost < Account)
            {
                Account -= amount * ToolCost;
                AmountTools += amount;
            }
        }

        // продажа инструментов
        public void RemoveTools(int amount)
        {
            if (amount > 0 && amount <= AmountTools)
            {
                Account += amount * ToolCost;
                AmountTools -= amount;
            }
        }

        // доход студии за месяц
        public float ProfitForMonth()
        {
            // определение количество комнат с 2 инструментами и 2 рабочими
            int amountWorkingRooms;
            if (AmountRooms * 2 <= AmountTools && AmountRooms * 2 <= AmountWorkers)
            {
                amountWorkingRooms = AmountRooms;
            }
            else if (AmountTools <= AmountRooms * 2 && AmountTools < AmountWorkers)
            {
                amountWorkingRooms = AmountTools / 2;
            }
            else
            {
                amountWorkingRooms = AmountWorkers / 2;
            }

            // доход = количество дней в месяце * количество рабочих часов в день / время создания трэка *
            // * количество рабочих комнат * стоимость трэка - сумма зарплат всех сотрудников
            return ((30 * 8 / TrackTime) * amountWorkingRooms * TrackCost  - SalaryWorkers);
        }

        // перегрузка оператора ++ (добавление комнаты)
        public static RecordingStudio operator ++(RecordingStudio value)
        { 
            value.AddRoom();
            return value;
        }

        // перегрузка оператора -- (удаление комнаты)
        public static RecordingStudio operator --(RecordingStudio value)
        {
            value.RemoveRoom();
            return value;
        }

        // индексатор по заработной плате одного сотрудника,
        // общей заработной плате и кассой студии
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return SalaryWorker;
                    case 1:
                        return SalaryWorkers;
                    case 2:
                        return Account;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
           
        }

        // реализация интерфейса ICloneable
        public object Clone()
        {
            var result = new RecordingStudio(Name, Address, TrackCost, TrackTime, SalaryWorker, Account, RoomCost, ToolCost);

            result.AmountWorkers = AmountWorkers;
            result.AmountRooms = AmountRooms;
            result.AmountTools = AmountTools;

            return result;
        }


    }
}
