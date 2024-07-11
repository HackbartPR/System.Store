namespace Service.Coupon.Core.Settings
{
    /// <summary>
    /// Representação das configurações referentes a base de dados das variáveis de ambiente.
    /// Será utilizado de acordo com o Options Pattern
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Identificador da variável de ambiente no AppSettings
        /// </summary>
        public const string Identifier = "Database";
        
        /// <summary>
        /// String de conexão
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
    }
}
