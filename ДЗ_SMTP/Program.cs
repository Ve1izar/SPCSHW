using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

namespace EmailSenderApp
{
    class Program
    {
        // ------------------------------------------------
        const string SenderEmail = "v1adg0re4ko@gmail.com";
        const string SenderPassword = "lnnf jnxq mwia xruj";
        const string SmtpHost = "smtp.gmail.com";
        const int SmtpPort = 587;
        // ------------------------------------------------


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Відправка email");

            try
            {
                Console.Write("Введіть email отримувача: ");
                string toAddress = Console.ReadLine();

                Console.Write("Введіть тему листа: ");
                string subject = Console.ReadLine();

                Console.Write("Введіть шлях до файлу з тілом листа (txt або html): ");
                string bodyFilePath = Console.ReadLine();

                if (!File.Exists(bodyFilePath))
                {
                    Console.WriteLine("Помилка: Файл з текстом листа не знайдено!");
                    return;
                }

                string bodyContent = File.ReadAllText(bodyFilePath);
                string extension = Path.GetExtension(bodyFilePath).ToLower();
                bool isHtml = (extension == ".html" || extension == ".htm");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(SenderEmail);
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = bodyContent;
                mail.IsBodyHtml = isHtml;

                if (isHtml)
                {
                    Console.WriteLine("-> Виявлено HTML файл. Лист буде відправлено з форматуванням.");
                }

                while (true)
                {
                    Console.Write("\nДодати файл? (y/n): ");
                    string choice = Console.ReadLine();

                    if (choice?.ToLower() != "y") break;

                    Console.Write("Введіть шлях до файлу: ");
                    string attachPath = Console.ReadLine();

                    if (File.Exists(attachPath))
                    {
                        mail.Attachments.Add(new Attachment(attachPath));
                        Console.WriteLine($"-> Файл '{Path.GetFileName(attachPath)}' прикріплено.");
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Файл не знайдено.");
                    }
                }

                Console.WriteLine("\nСпроба відправки...");

                using (SmtpClient smtp = new SmtpClient(SmtpHost, SmtpPort))
                {
                    smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                }

                Console.WriteLine("Успіх! Лист відправлено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nСталася помилка при відправці: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
                Console.ReadKey();
            }
        }
    }
}