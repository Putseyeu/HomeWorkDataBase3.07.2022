using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasePlayer3._0List
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userInput = "";
            DataBase dataBase = new DataBase();
            while (userInput != "5")
            {
                Console.WriteLine("База данных игроков. Выберите действие.");
                Console.WriteLine(" 1 - Добавить игрока.\n 2 - Удалить игрока.\n 3 - Изменить статус игрока.\n" +
                    " 4 - Список игроков.\n 5 - Выйти из базы данных.");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        dataBase.AddPlayer();
                        break;

                    case "2":
                        dataBase.DeletePlayer();
                        break;

                    case "3":
                        dataBase.ChangeStatus();
                        break;

                    case "4":
                        dataBase.ShowInfo();
                        break;
                }
                Console.WriteLine();
            }
        }

        class DataBase
        {
            private List<Player> _users = new List<Player>();

            public void AddPlayer()
            {
                _users.Add(new Player());
            }

            public void DeletePlayer()
            {
                {
                    if (_users.Count > 0)
                    {
                        if (TryGetPlayer(out Player player, out int index))
                        {
                            _users.RemoveAt(index);                           
                        }
                    }
                    else
                    {
                        Console.WriteLine("База данных не заполнена.");
                    }
                }
            }

            public void ChangeStatus()
            {
                if (_users.Count > 0)
                {
                    if (TryGetPlayer(out Player player, out int index))
                    {
                        _users[index].InputStatusBanned();
                    }
                }
                else
                {
                    Console.WriteLine("База данных не заполнена.");
                }
            }

            public void ShowInfo()
            {
                if (_users.Count > 0)
                {
                    string statusPlayer;
                    for (int i = 0; i < _users.Count; i++)
                    {
                        if (_users[i].Banned == true)
                        {
                            statusPlayer = " - заблокирован";
                        }
                        else
                        {
                            statusPlayer = " - не заблокирован";
                        }
                        Console.WriteLine($" Порядковый номер {i + 1} | никнейм {_users[i].Name}" +
                            $" | уровень {_users[i].Level}| Статутс игрока {statusPlayer}.");
                    }
                }
                else
                {
                    Console.WriteLine("База данных не заполнена.");
                }
            }

            private bool TryGetPlayer(out Player player, out int index)
            {
                player = null;
                Console.WriteLine("Введите порядковый номер игрока");
                string indexPlayer = Console.ReadLine();
                if (int.TryParse(indexPlayer, out int intValue))
                {
                    intValue--;
                    if (intValue < _users.Count && intValue >= 0)
                    {
                        index = intValue;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Игрок с порядковым номером {intValue + 1} отсутствует.");
                        index = 0;
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Не верный порядковый номер игрока");
                }
                index = 0;
                return false;
            }
        }
    }

    class Player
    {
        private int _minLevel = 1;
        private int _maxLevel = 99;

        public bool Banned { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }

        public Player()
        {
            InputName();
            InputLevel(_minLevel, _maxLevel);
            InputStatusBanned();
        }

        private void InputName()
        {
            Console.WriteLine("Внесите никнейм для игрока");
            Name = Console.ReadLine();
        }

        private void InputLevel(int minLevel, int maxLevel)
        {
            bool completed = false;
            int intValue = 0;
            Console.WriteLine($"Укажите уровень игрока  от {minLevel} до {maxLevel}");

            while (completed == false)
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out intValue))
                {
                    if (intValue >= minLevel && intValue <= maxLevel)
                    {
                        completed = true;
                    }
                    else
                    {
                        Console.WriteLine($"Не верный ввод значения.Введите целое число от {minLevel} до {maxLevel}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Не верный ввод значения.Введите целое число от {minLevel} до {maxLevel}.");
                }
            }
            Level = intValue;
        }

        public bool InputStatusBanned()
        {            
            bool completed = false;
            string userInput;

            while (completed == false)
            {
                Console.WriteLine("Установите статус игрока.\n 1 - Заблокированый игрок.\n 2 - Разблокированый игрок.");
                userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    AppoinStatusBan();
                    completed = true;
                }
                else if (userInput == "2")
                {
                    AppoinStatusUnban();
                    completed = true;
                }
            }
            return Banned;
        }

        private bool AppoinStatusBan()
        {
            Banned = true;
            return Banned;
        }
        
        private bool AppoinStatusUnban()
        {
            Banned = false;
            return Banned;
        }
    }
}

