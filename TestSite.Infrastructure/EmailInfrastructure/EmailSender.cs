using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using TestSite.Domain;

namespace TestSite.Infrastructure.EmailInfrastructure;

/// <summary>
/// Сервис по отправке сообщений.
/// </summary>
public class EmailSender: IEmailSender<User>, IEmailSender
{
	/// <summary>
	/// Конфигурация проекта.
	/// </summary>
	private readonly IConfiguration _configuration;

	public EmailSender(IConfiguration configuration)
	{
		_configuration = configuration;
	}
	
	/// <summary>
	/// Отправить сообщение.
	/// </summary>
	/// <param name="toEmail">Адрес почты.</param>
	/// <param name="subject">Тема сообщения.</param>
	/// <param name="message">Сообщение.</param>
	public async Task SendEmailAsync(string toEmail, string subject, string message)
	{
		var email = "CyberSamuraiForgotHisOwnName@yandex.ru";
		var password = "amactlfquupbtoln"; // amactlfquupbtoln
		
		var from = new MailAddress(email, "Somee.com");
		// кому отправляем
		var to = new MailAddress(toEmail);
		// создаем объект сообщения
		var m = new MailMessage(from, to);
		// тема письма
		m.Subject = subject;
		// текст письма
		m.Body = message;
		// письмо представляет код html
		m.IsBodyHtml = true;
		// адрес smtp-сервера и порт, с которого будем отправлять письмо
		var smtp = new SmtpClient("smtp.yandex.com", 25);
		// логин и пароль
		smtp.Credentials = new NetworkCredential(email, password);
		smtp.EnableSsl = true;
		
		await smtp.SendMailAsync(m);
	}
	
	/// <summary>
	/// Отправить ссылку для подтверждения аккаунта.
	/// </summary>
	/// <param name="user">Пользователь</param>
	/// <param name="email">Почта.</param>
	/// <param name="confirmationLink">Ссылка.</param>
	public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
	{
		await SendEmailAsync(email, "Confirmation email", confirmationLink);
	}

	/// <summary>
	/// Отправить ссылку для изменение пароля.
	/// </summary>
	/// <param name="user">Пользователь.</param>
	/// <param name="email">Почта.</param>
	/// <param name="resetLink">Ссылка.</param>
	public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
	{
		await SendEmailAsync(email, "Confirmation email", resetLink);
	}

	/// <summary>
	/// Отправить код для изменеия пароля.
	/// </summary>
	/// <param name="user">Пользователь.</param>
	/// <param name="email">Почта.</param>
	/// <param name="resetCode">Код.</param>
	public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
	{
		await SendEmailAsync(email, "Confirmation email", resetCode);
	}
}