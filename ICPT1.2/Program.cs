using System;
using System.Data;
using Npgsql; 

namespace QuestBookingApp
{
    class Program
    {
        static string connectionString = "Host=localhost;Port=5432;Database=QuestBookingICPT;Username=postgres;Password=+L4679013y";

        static void Main(string[] args)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); 
                    Console.WriteLine("Успішне підключення до PostgreSQL!");

                    Console.WriteLine("\n--- Список доступних квест-кімнат ---");
                    string sqlRooms = "SELECT title, base_price FROM QuestRooms";

                    using (var cmdRooms = new NpgsqlCommand(sqlRooms, connection))
                    {
                        using (var reader = cmdRooms.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Кімната: {reader["title"]} | Ціна: {reader["base_price"]} грн");
                            }
                        }
                    }

                    Console.WriteLine("\nСпроба додати нового клієнта...");
                    string sqlInsertClient = "INSERT INTO Clients (full_name, phone) VALUES (@name, @phone) RETURNING id";

                    using (var cmdInsert = new NpgsqlCommand(sqlInsertClient, connection))
                    {
                        cmdInsert.Parameters.AddWithValue("@name", "Олексій Тестовий");
                        cmdInsert.Parameters.AddWithValue("@phone", "+380501112233");

                        var newId = cmdInsert.ExecuteScalar();
                        Console.WriteLine($"Успіх! Нового клієнта додано з ID: {newId}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Сталася помилка: " + ex.Message);
                }
            } 

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}