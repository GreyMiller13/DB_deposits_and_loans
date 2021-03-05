using System;
using MySql.Data.MySqlClient;

namespace DB_deposits_and_loans
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection_string =
                "Server=mysql60.hostland.ru; Database=host1323541_itstep32; Uid=host1323541_itstep; Pwd=269f43dc;";
            var connection = new MySqlConnection(connection_string);

            //Вывод информации о всех клиентах - работает
            void ShowAllCutomers()
            {
                connection.Open();
                var sql =
                    $"SELECT id, name, patronymic, surname, birthday FROM deposit_and_loan_table_customers;";
                var command = new MySqlCommand
                {
                    Connection = connection,
                    CommandText = sql
                };
                var result = command.ExecuteReader();

                while (result.Read())
                {
                    var tempID = result.GetInt32("id");
                    var tempName = result.GetString("name");
                    var tempPatronymic = result.GetString("patronymic");
                    var tempSurname = result.GetString("surname");
                    var tempBirthday = result.GetDateTime("birthday");

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"id\t\t{tempID}\nname\t\t{tempName}\npatronymic\t{tempPatronymic}\nsurname\t\t{tempSurname}\nbirthday\t{tempBirthday:d}");
                    Console.ResetColor();
                }
                connection.Close();
            }
            
            //добавление нового пользователя - работает
            void AddNewCustomer()
            {
                connection.Open();
                Console.WriteLine("Введите имя нового пользователя:");
                var newCustomerName = Console.ReadLine();
                Console.WriteLine("Введите отчество нового пользователя:");
                var newCustomerPatronymic = Console.ReadLine();
                Console.WriteLine("Введите фамилию нового пользователя:");
                var newCustomerSurname = Console.ReadLine();
                Console.WriteLine("Введите дату рождения нового пользователя:");
                var newCustomerBirthday = Console.ReadLine();
                
                var sql =
                    "INSERT INTO deposit_and_loan_table_customers (name, patronymic, surname, birthday) " +
                    $"VALUES ('{newCustomerName}','{newCustomerPatronymic}','{newCustomerSurname}','{newCustomerBirthday}');";
                var command = new MySqlCommand {
                    Connection = connection,
                    CommandText = sql
                };
                
                var result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Данные не добавились");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Данные добавлены");
                    Console.ResetColor();
                }
                connection.Close();
            }

            //добавление нового счета - работает
            void CreateNewDeposit()
            {
                connection.Open();
                Console.WriteLine("Укажите ID клиента:");
                var customer_id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Укажите дату открытия счета:");
                var deposit_opening_date = Console.ReadLine();
                Console.WriteLine("Укажите сумму депозита:");
                var deposit_sum = Convert.ToInt32(Console.ReadLine());

                var sql = "INSERT INTO deposit_and_loan_table_deposits (customer_id, deposit_opening_date, deposit_sum) " +
                    $"VALUES('{customer_id}','{deposit_opening_date}','{deposit_sum}')";
                var command = new MySqlCommand
                {
                    Connection = connection,
                    CommandText = sql
                };

                var result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Данные не добавились");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Данные добавлены");
                    Console.ResetColor();
                }
                connection.Close();
            }
            
            //добавление нового кредита - работает
            void CreateNewLoan()
            {
                connection.Open();
                Console.WriteLine("Укажите ID клиента:");
                var customer_id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Укажите дату взятия кредита:");
                var loan_opening_date = Console.ReadLine();
                Console.WriteLine("Укажите сумму кредита:");
                var loan_sum = Convert.ToInt32(Console.ReadLine());

                var sql = "INSERT INTO deposit_and_loan_table_loans (customer_id, loan_opening_date, loan_sum) " +
                    $"VALUES('{customer_id}','{loan_opening_date}','{loan_sum}')";
                var command = new MySqlCommand
                {
                    Connection = connection,
                    CommandText = sql
                };

                var result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Данные не добавились");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Данные добавлены");
                    Console.ResetColor();
                }
                connection.Close();
            }

            //Вывод информации о депозитных счетах клиента - работает
            //будет ошибка, если в запрос поступает id клиента, который не имеет счетов
            void ShowDepositsInfo()
            {
                connection.Open();
                Console.WriteLine("Укажите ID клиента:");
                var customer_id = Convert.ToInt32(Console.ReadLine());
                var sql =
                    $"SELECT SUM(deposit_sum) AS deposits_sum FROM deposit_and_loan_table_deposits WHERE customer_id = {customer_id};";
                var command = new MySqlCommand
                {
                    Connection = connection,
                    CommandText = sql
                };
                var result = command.ExecuteReader();

                while (result.Read())
                {
                    var tempDepositsSum = result.GetString("deposits_sum");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Deposits sum:\t\t{tempDepositsSum}");
                    Console.ResetColor();
                }
                connection.Close();
            }

            //Вывод информации о кредитных счетах клиента - работает
            //будет ошибка, если в запрос поступает id клиента, который не имеет счетов
            void ShowLoansInfo()
            {
                connection.Open();
                Console.WriteLine("Укажите ID клиента:");
                var customer_id = Convert.ToInt32(Console.ReadLine());
                var sql =
                    $"SELECT SUM(loan_sum) AS loans_sum FROM deposit_and_loan_table_loans WHERE customer_id = {customer_id};";
                var command = new MySqlCommand
                {
                    Connection = connection,
                    CommandText = sql
                };
                var result = command.ExecuteReader();

                while (result.Read())
                {
                    var tempLoansSum = result.GetString("loans_sum");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Loans sum:\t\t{tempLoansSum}");
                    Console.ResetColor();
                }
                connection.Close();
            }

            void ShowMenu()
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать нового клиента");
                Console.WriteLine("2. Создать новый депозит");
                Console.WriteLine("3. Создать новый кредит");
                Console.WriteLine("4. Показать остатки по депозитам");
                Console.WriteLine("5. Показать остатки по кредитам");
                Console.WriteLine("6. Показать информацию по клиентам");
                Console.WriteLine("7. Выход");
            }

            void SelectMenu(ref bool exit)
            {
                Console.WriteLine("Ваш выбор:");
                int select = Convert.ToInt32(Console.ReadLine());
                switch (select)
                {
                    case 1:
                        AddNewCustomer();
                        break;
                    case 2:
                        CreateNewDeposit();
                        break;
                    case 3:
                        CreateNewLoan();
                        break;
                    case 4:
                        ShowDepositsInfo();
                        break;
                    case 5:
                        ShowLoansInfo();
                        break;
                    case 6:
                        ShowAllCutomers();
                        break;
                    case 7:
                        exit = false;
                        break;
                    default:
                        Console.WriteLine("Некорректный запрос. Повторите снова.");
                        break;
                }
            }

            bool exit = true;

            do
            {
                ShowMenu();
                SelectMenu(ref exit);
                
            } while (exit);
        }
        
    }
}
