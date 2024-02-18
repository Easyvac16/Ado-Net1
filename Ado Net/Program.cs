using System.Threading;
using System.Data.SqlClient;
using System.Text;

namespace Ado_Net
{
    internal class Program
    {
        public static string ConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Marks;Integrated Security=True;Connect Timeout=30;";
        static void Main()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            /*string firstName = Console.ReadLine();
            string lastName = Console.ReadLine();
            string email = Console.ReadLine();*/

            //string queryAll = "SELECT * FROM StudentsGrades;";
            /*string insert = @$"INSERT INTO Users (FirstName, LastName, Email)
                              VALUES(@FirstName, @LastName, @Email)";*/
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Відобразити всю інформацію з таблиці зі студентами та оцінками.");
                Console.WriteLine("2. Відобразити ПІБ усіх студентів.");
                Console.WriteLine("3. Відобразити усі середні оцінки.");
                Console.WriteLine("4. Показати ПІБ усіх студентів з мінімальною оцінкою, більшою, ніж зазначена.");
                Console.WriteLine("5. Показати назви усіх предметів із мінімальними середніми оцінками.");
                Console.WriteLine("6. Показати мінімальну середню оцінку.");
                Console.WriteLine("7. Показати максимальну середню оцінку.");
                Console.WriteLine("8. Показати кількість студентів з мінімальною середньою оцінкою з математики.");
                Console.WriteLine("9. Показати кількість студентів, в яких максимальна середня оцінка з математики.");
                Console.WriteLine("10. Показати кількість студентів у кожній групі.");
                Console.WriteLine("11. Показати середню оцінку групи.");
                Console.WriteLine("0. Вийти з програми");

                Console.Write("Виберіть опцію: ");
                choice = int.Parse(Console.ReadLine());


                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();

                        switch (choice)
                        {
                            case 1:
                                DisplayAllData(connection);
                                break;
                            case 2:
                                DisplayAllStudentNames(connection);
                                break;
                            case 3:
                                DisplayAllAverageGrades(connection);
                                break;
                            case 4:
                                DisplayStudentsWithMinGrade(connection);
                                break;
                            case 5:
                                DisplaySubjectsWithMinAverageGrades(connection);
                                break;
                            case 6:
                                DisplayMinAverageGrade(connection);
                                break;
                            case 7:
                                DisplayMaxAverageGrade(connection);
                                break;
                            case 8:
                                DisplayStudentsWithMinMathGradeCount(connection);
                                break;
                            case 9:
                                DisplayStudentsWithMaxMathGradeCount(connection);
                                break;
                            case 10:
                                DisplayStudentsInEachGroupCount(connection);
                                break;
                            case 11:
                                DisplayAverageGradeForEachGroup(connection);
                                break;
                            case 12:
                                DisplayData(connection);
                                break;
                            case 13:
                               DeleteData(connection);
                                break;
                            case 0:
                                Console.WriteLine("Poka!");
                                break;
                            default:
                                Console.WriteLine("Неправильний вибір.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                }
                Thread.Sleep(5000);
            } while (choice != 0);



            /*static void DisplayData(SqlConnection connection)
            {
                string dataQuery = "SELECT * FROM StudentsGrades";
                using (SqlCommand command = new SqlCommand(dataQuery,connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) 
                    {
                        Console.WriteLine($"ПІБ: {reader["FullName"]} Середня оцінка {reader["AverageGrade"]}");
                    }
                }
            }
            static void DeleteData(SqlConnection connection)
            {
                string dataQuery = "DELETE FROM StudentsGrades WHERE StudentID = 3";
                using (SqlCommand command = new SqlCommand(dataQuery,connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"Дані студента Grigoriev Dmytro Igorovich видалено {rowsAffected}");
                    
                }
            }
*/































            /*using (SqlCommand cmd = new SqlCommand(queryAll, connection))
            {
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = firstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = lastName;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = email;
                int result = cmd.ExecuteNonQuery();
                Console.WriteLine(result);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["FullName"]);
                    Console.WriteLine(reader["GroupName"]);
                    Console.WriteLine(reader["AverageGrade"]);
                    Console.WriteLine(reader["MinSubject"]);
                    Console.WriteLine(reader["MaxSubject"]);
                }

                //FullName, GroupName, AverageGrade, MinSubject, MaxSubject
            }*/
            static void DisplayAllData(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM StudentsGrades", connection)) { 
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ПІБ: {reader["FullName"]}, Група: {reader["GroupName"]}, Середня оцінка: {reader["AverageGrade"]}, Мін. предмет: {reader["MinSubject"]}, Макс. предмет: {reader["MaxSubject"]}");
                        Console.WriteLine("-----------------");
                        Console.WriteLine("\t");
                    }
                }
            }

            static void DisplayAllStudentNames(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT FullName FROM StudentsGrades", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ПІБ: {reader["FullName"]}");
                        Console.WriteLine("\t");
                    }
                }

            }

            static void DisplayAllAverageGrades(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT FullName, AverageGrade FROM StudentsGrades", connection)) { 
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Середня оцінка ПІБ: {reader["FullName"]}, {reader["AverageGrade"]}");
                        Console.WriteLine("\t");
                    }
                }
            }

            static void DisplayStudentsWithMinGrade(SqlConnection connection)
            {
                int minGrade = 3;

                using(SqlCommand command = new SqlCommand($"SELECT AverageGrade, FullName FROM StudentsGrades WHERE AverageGrade > {minGrade}", connection)){
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ПІБ: {reader["FullName"]}, AVG Grade:{reader["AverageGrade"]}");
                        Console.WriteLine("\t");
                    }
                }

            }

            static void DisplaySubjectsWithMinAverageGrades(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT DISTINCT FullName, MinSubject FROM StudentsGrades", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Студент: {reader["FullName"]}, Предмет з мінімальною середньою оцінкою: {reader["MinSubject"]}");
                        Console.WriteLine("\t");
                    }
                }
            }

            static void DisplayMinAverageGrade(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT MIN(AverageGrade) AS MinAverageGrade FROM StudentsGrades", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Мінімальна середня оцінка: {reader["MinAverageGrade"]}");
                        Console.WriteLine("\t");
                    }
                }
            }

            static void DisplayMaxAverageGrade(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT MAX(AverageGrade) AS MaxAverageGrade FROM StudentsGrades", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Максимальна середня оцінка: {reader["MaxAverageGrade"]}");
                        Console.WriteLine("\t");
                    }
                }
            }

            static void DisplayStudentsWithMinMathGradeCount(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) AS StudentCount FROM StudentsGrades WHERE MinSubject = 'Математика' AND AverageGrade = (SELECT MIN(AverageGrade) FROM StudentsGrades)", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Кількість студентів з мінімальною середньою оцінкою з математики: {reader["StudentCount"]}");
                        Console.WriteLine("\t");
                    }
                }
            }

            static void DisplayStudentsWithMaxMathGradeCount(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) AS StudentCount FROM StudentsGrades WHERE MaxSubject = 'Математика' AND AverageGrade = (SELECT MAX(AverageGrade) FROM StudentsGrades)", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Кількість студентів з максимальною середньою оцінкою з математики: {reader["StudentCount"]}");
                        Console.WriteLine("\t");
                    }
                }
            }

            static void DisplayStudentsInEachGroupCount(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT GroupName, COUNT(*) AS StudentCount FROM StudentsGrades GROUP BY GroupName", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Група: {reader["GroupName"]}, Кількість студентів: {reader["StudentCount"]}");
                        Console.WriteLine("\t");
                    }
                }
            }

            static void DisplayAverageGradeForEachGroup(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT GroupName, AVG(AverageGrade) AS AverageGrade FROM StudentsGrades GROUP BY GroupName", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Група: {reader["GroupName"]}, Середня оцінка: {reader["AverageGrade"]}");
                        Console.WriteLine("\t");
                    }
                }
            }


        }
    }
}
