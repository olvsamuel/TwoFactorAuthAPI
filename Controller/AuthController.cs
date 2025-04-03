using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly TwoFactorAuthenticator _tfa;

    public AuthController()
    {
        _tfa = new TwoFactorAuthenticator();
    }

    [HttpGet("generateqr")]
    public ActionResult<string> GenerateQR(string email)
    {

        string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        Console.WriteLine("chave gerada: " + key);
        SetupCode setupInfo = _tfa.GenerateSetupCode("2FA Validation", email, key, false, 3);

        return setupInfo.QrCodeSetupImageUrl;
    }

    [HttpGet("validatecode")]
    public ActionResult<bool> ValidateCode(string code, string key)
    {
        return _tfa.ValidateTwoFactorPIN(key, code);
    }

}