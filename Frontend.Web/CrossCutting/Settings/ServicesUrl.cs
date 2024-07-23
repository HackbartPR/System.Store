namespace Frontend.Web.CrossCutting.Settings;

/// <summary>
/// Classe responsável por recuperar as URLs dos services cadastrados no appSettings.json
/// </summary>
public class ServicesUrl
{
    public const string Identifier = "ServicesUrl";

    public string CouponAPI {  get; set; } = string.Empty;
}
