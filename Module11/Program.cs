using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module11
{
    //с кошками не стал делать, думаю этого хватит;p
    enum Vacancies { Манагер = 1, Босс, Клерк, Продажник }
    struct employee
    {
        public string name;
        public DateTime hiringDate; // дата принятия на работу
        public double salary;
        public string position;
        public void printFullInfo()
        {
            Console.WriteLine("Имя сотрудника:\t{0}\nДата наёма:\t{1}\nЗаработная плата:\t{2}\nДолжность: {3}", name, hiringDate, salary, position);
        }
        public bool printManagerInfo(Vacancies emp, employee[] empArr)
        {
            double average = 0;
            int count = 0;
            foreach (var item in empArr)
            {
                if (item.position == emp.ToString())
                { average += item.salary; count++; }
            }
            average /= count;
            if (this.salary > average)
                return true;
            else
                return false;
        }
    }

    class Program
    {
        static void Main()
        {
            Vacancies vac1;
            for (vac1 = Vacancies.Манагер; vac1 <= Vacancies.Продажник; vac1++)
                Console.WriteLine("Элемент: \"{0}\", значение {1}", vac1, (int)vac1);
            Vacancies vac;
            //List<employee> empList = new List<employee>();

            Console.WriteLine("Введи количество сотрудников, которых нужно добавить: ");
            int num = Int32.Parse(Console.ReadLine());
            employee[] empArr = new employee[num];
            int bosscount = 0;
            for (int i = 0; i < num; i++)
            {
                start:
                employee emp = new employee();
                Console.WriteLine("Введите имя сотрудника");
                emp.name = Console.ReadLine();
                Console.WriteLine("Введите дату принятия на работу (dd/mm/yy)");
                mojnoTak: //приколXD
                try
                {
                    emp.hiringDate = DateTime.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2146233033)
                        Console.WriteLine("Введите правильный формат даты (dd/mm/yy)");
                    goto mojnoTak;
                }
                Console.WriteLine("Введите зп");
                emp.salary = Double.Parse(Console.ReadLine());
                Console.WriteLine("Введите должность (цифру) 1. Манагер 2. Босс 3. Клерк 4. Продажник");
                string pos = Console.ReadLine();
                //start: goto start;
                for (vac = Vacancies.Манагер; vac <= Vacancies.Продажник; vac++)//дебилизм конечно, но нужно использовать enum
                {
                    if (bosscount == 1 && Int32.Parse(pos) == 2)
                    { Console.WriteLine("Босс есть!"); goto start; }
                    if ((int)vac == Int32.Parse(pos) && (int)vac != 2)
                        emp.position = vac.ToString();
                    else if ((int)vac == Int32.Parse(pos) && bosscount == 0 && Int32.Parse(pos) == 2)
                    { emp.position = vac.ToString(); bosscount++; empArr[i] = emp; i++; }

                }
                //верхний цикл ужасный говнокод
                empArr[i] = emp;
            }
            Console.Clear();
            while (true)
            {
                Console.WriteLine("1. Вывести инфу о всех сотрудниках\n" +
                    "2. Найти в массиве всех менеджеров, зарплата которых больше средней зарплаты " +
                    "всех клерков, вывести на экран полную информацию о таких менеджерах отсортированной по их фамилии" +
                    "3. Распечатать информацию обо всех сотрудниках, принятых на работу позже " +
                    "босса, отсортированную в алфавитном порядке по фамилии сотрудника\n exit для выхода");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            foreach (var item in empArr)
                            {
                                item.printFullInfo();
                            }
                        }
                        break;
                    case "2":
                        {
                            Console.WriteLine("Введите должность (цифру) группы сотрудников" +
                                       "зп которых нужно сравнить\n1. Манагер 2. Босс 3. Клерк 4. Продажник");
                            string pos = Console.ReadLine();
                            foreach (var item in empArr)
                            {
                                for (vac = Vacancies.Манагер; vac <= Vacancies.Продажник; vac++)
                                    if ((int)vac == Int32.Parse(pos))
                                        if (item.printManagerInfo(vac, empArr))
                                            item.printFullInfo();
                            }
                        }
                        break;
                    case "3":
                        {
                            Console.WriteLine("3. Распечатать информацию обо всех сотрудниках, принятых на " +
                                "работу позже босса, отсортированную в алфавитном порядке по фамилии сотрудника");
                            Array.Sort(empArr, (emp1, emp2) => emp1.name.CompareTo(emp2.name));
                            employee boss = empArr.Where(bossP => bossP.position == "Босс").Single();
                            foreach (var item in empArr)
                            {
                                if (DateTime.Compare(item.hiringDate, boss.hiringDate) > 0)
                                    item.printFullInfo();
                            }
                        }
                        break;
                    case "exit":
                        Environment.Exit(125);
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
