using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Homework1_sem2;

public class EmailService
{
    /// <summary>
    /// Метод для отправки писем списку пользователей
    /// </summary>
    /// <param name="users">Список получателей</param>
    /// <param name="smtpHost">Адрес SMTP сервера (например, smtp.gmail.com)</param>
    /// <param name="smtpPort">Порт (обычно 587)</param>
    /// <param name="login">Логин от почты</param>
    /// <param name="password">Пароль от почты</param>
    public async Task SendNotificationsAsync(List<User> users, string smtpHost, int smtpPort, string login, string password)
    {
        Console.WriteLine($"Начинаем рассылку для {users.Count} пользователей...");

        foreach (var user in users)
        {
            try
            {
                // 1. Формируем HTML шаблон письма
                string htmlBody = $@"
                    <html>
                        <body style='font-family: Arial, sans-serif;'>
                            <h2 style='color: #333;'>Привет, {user.Name}!</h2>
                            <p>Это тестовое уведомление из вашей домашней работы.</p>
                            <p>Спасибо, что используете наш сервис.</p>
                            <hr/>
                            <small style='color: gray;'>Отправлено автоматически.</small>
                        </body>
                    </html>";

                // 2. Создаем письмо
                MailMessage message = new MailMessage();
                message.From = new MailAddress(login, "Мой Сервис"); // От кого
                message.To.Add(user.Email); // Кому
                message.Subject = "Важное уведомление";
                message.Body = htmlBody;
                message.IsBodyHtml = true; // Важно: указываем, что тело письма - это HTML

                // 3. Настраиваем SMTP клиент
                using (SmtpClient client = new SmtpClient(smtpHost, smtpPort))
                {
                    client.Credentials = new NetworkCredential(login, password);
                    client.EnableSsl = true; // Обычно требуется SSL

                    // 4. Отправляем письмо
                    await client.SendMailAsync(message);
                    Console.WriteLine($"Письмо успешно отправлено пользователю: {user.Email}");
                }

                // 5. Делаем задержку перед следующим письмом (чтобы не заблокировали спам-фильтры)
                // Задержка 2 секунды
                await Task.Delay(2000); 
            }
            catch (Exception ex)
            {
                // Если у одного пользователя ошибка (неверный email), мы не останавливаем всю рассылку, а просто пишем в консоль
                Console.WriteLine($"Ошибка при отправке пользователю {user.Email}: {ex.Message}");
            }
        }

        Console.WriteLine("Рассылка завершена.");
    }
}