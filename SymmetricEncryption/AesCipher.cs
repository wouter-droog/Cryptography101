using System.Security.Cryptography;
using System.Text;

namespace SymmetricEncryption;

public class AesCipher
{
    // This should come from keyvault
    private readonly string _secretKey = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
    private readonly string _iv = "000102030405060708090A0B0C0D0E0F";
    
    private Aes _aesCipher;
    
    public AesCipher(string secretKey, string iv)
    {
        // _secretKey = GenerateSecretKey();
        // _iv = GenerateIV();
        
        _secretKey = secretKey;
        _iv = iv;
        
        _aesCipher = CreateCipher(_secretKey, _iv);
    }

    public byte[] EncryptStringToBytes(string plainText)
    {
        Console.WriteLine($"Key: {Convert.ToHexString(_aesCipher.Key)}");
        Console.WriteLine($"IV: {Convert.ToBase64String(_aesCipher.IV)}");
    
        ICryptoTransform encryptor = _aesCipher.CreateEncryptor();
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] cipherText = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
    
        return cipherText;
    }
    
    public string DecryptStringFromBytes(byte[] cipherText)
    {
        ICryptoTransform decryptor = _aesCipher.CreateDecryptor();
        byte[] plainTextBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
    
        return Encoding.UTF8.GetString(plainTextBytes);
    }
    
    public string DecryptStringFromBase64(string cipherText)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        return DecryptStringFromBytes(cipherTextBytes);
    }

    private Aes CreateCipher(string secretKey, string iv)
    {
        var cipher = Aes.Create();
        cipher.KeySize = 256;
        cipher.BlockSize = 128;
        cipher.Padding = PaddingMode.ISO10126; // results in most random padding
        cipher.Mode = CipherMode.CBC; // will take in IV as a random seed
    
        // Without the following line, the IV and key will be semi random
        cipher.Key = Convert.FromHexString(secretKey); // This should be created randomly and stored/shared securely
        cipher.IV = Convert.FromHexString(iv); // This should be random and shared with the receiver, does not need to be secret

        Console.WriteLine($"IV: {Convert.ToBase64String(cipher.IV)}");
    
        return cipher;
    }
    
    /// <summary>
    /// This will generate a true random key
    /// </summary>
    /// <returns></returns>
    private string GenerateSecretKey()
    {
        var key = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
        }
    
        return Convert.ToHexString(key);
    }
    
    private string GenerateIV()
    {
        var key = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
        }
    
        return Convert.ToHexString(key);
    }
}