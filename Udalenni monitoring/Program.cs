using System;
using System.ServiceProcess;

namespace RemoteServiceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            // Установите имя сервера, на котором хотите выполнить удаленное управление службами Windows
            string remoteServerName = "имя_сервера";

            // Установите имя службы, с которой хотите работать
            string serviceName = "имя_службы";

            // Установите действие, которое нужно выполнить: Start (запустить), Stop (остановить) или Restart (перезапустить)
            string action = "Start";

            try
            {
                // Создание объекта ServiceController для указанной службы на удаленном сервере
                using (ServiceController serviceController = new ServiceController(serviceName, remoteServerName))
                {
                    // Выводим текущий статус службы
                    Console.WriteLine("Текущий статус службы: " + serviceController.Status);

                    // Выполняем выбранное действие со службой
                    switch (action.ToLower())
                    {
                        case "start":
                            serviceController.Start();
                            break;

                        case "stop":
                            serviceController.Stop();
                            break;

                        case "restart":
                            serviceController.Stop();
                            serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                            serviceController.Start();
                            break;

                        default:
                            Console.WriteLine("Неправильное действие. Допустимые значения: Start, Stop, Restart");
                            break;
                    }

                    // Выводим обновленный статус службы
                    Console.WriteLine("Обновленный статус службы: " + serviceController.Status);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}