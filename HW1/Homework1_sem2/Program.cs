namespace Homework1_sem2;

class Program
{
    static async Task Main(string[] args)
    {

        var users = new List<User>
        {
            new User { Email = "test1@example.com", Name = "Алексей" },
            new User { Email = "test2@example.com", Name = "Мария" },
            new User { Email = "test3@example.com", Name = "Иван" }
        };

        var emailService = new EmailService();

        // === НАСТРОЙКИ SMTP (ЗАПОЛНИТЬ СВОИМИ ДАННЫМИ) ===
        // Для Gmail: smtp.gmail.com, порт 587
        // Для Яндекс: smtp.yandex.ru, порт 465
        string smtpHost = "smtp.gmail.com";
        int smtpPort = 587;
        string login = "gmail@gmail.com";
        string password = "password";

        // Запуск рассылки
        await emailService.SendNotificationsAsync(users, smtpHost, smtpPort, login, password);

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}