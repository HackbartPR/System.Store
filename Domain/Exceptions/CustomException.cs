using System.Net;

namespace Domain.Exceptions;

/// <summary>
/// Classe de erro customizada
/// </summary>
public sealed class CustomException : Exception
{
    public HttpStatusCode? StatusCode { get; private set; }

    /// <summary>
    /// Erro genérico sem parâmetros.
    /// </summary>
    public CustomException() : base(message: "Ocorreu um erro inesperado ao processar a sua solicitação.") { }

    /// <summary>
    /// Erro passando o HttpStatusCode
    /// </summary>
    /// <param name="statusCode"></param>
    public CustomException(HttpStatusCode? statusCode) : base(message: "Ocorreu um erro inesperado ao processar a sua solicitação.")
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Erro passando a mensagem e o HttpStatusCode
    /// </summary>
    /// <param name="message"></param>
    /// <param name="statusCode"></param>
    public CustomException(string message, HttpStatusCode? statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Erro passando a mensagem, HttpStatusCode e a exception
    /// </summary>
    /// <param name="message"></param>
    /// <param name="statusCode"></param>
    /// <param name="innerException"></param>
    public CustomException(string message, HttpStatusCode? statusCode, Exception innerException) : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}
