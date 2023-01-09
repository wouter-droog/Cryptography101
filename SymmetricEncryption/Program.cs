// See https://aka.ms/new-console-template for more information


using System.Security.Cryptography;
using System.Text;


string plainText = "Hello World!";
string secretKey = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
string iv = "000102030405060708090A0B0C0D0E0F";



Console.WriteLine($"Plain Text: {plainText}");

var cipherBytes = EncryptStringToBytes();
Console.WriteLine($"Cipher Text: {Convert.ToBase64String(cipherBytes)}");

var originalText = DecryptStringFromBytes(cipherBytes);
Console.WriteLine($"Original Text: {originalText}");

Console.ReadLine();




Aes CreateAesCipher()
{
    var cipher = Aes.Create();
    cipher.KeySize = 256;
    cipher.BlockSize = 128;
    cipher.Padding = PaddingMode.ISO10126; // results in most random padding
    cipher.Mode = CipherMode.CBC; // will take in IV as a random seed
    
 
    cipher.Key = Convert.FromHexString(secretKey);
    cipher.IV = Convert.FromHexString(iv); // This should be random and shared with the receiver
    
    Console.WriteLine($"IV: {Convert.ToBase64String(cipher.IV)}");
    
    return cipher;
}

// encrypt method using the CreateAesCipher method
byte[] EncryptStringToBytes()
{
    Aes cipher = CreateAesCipher();
    
    ICryptoTransform encryptor = cipher.CreateEncryptor();
    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
    byte[] cipherText = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
    
    return cipherText;
}

// decrypt method using the CreateAesCipher method
string DecryptStringFromBytes(byte[] cipherText)
{
    Aes cipher = CreateAesCipher();
    
    ICryptoTransform decryptor = cipher.CreateDecryptor();
    byte[] plainTextBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
    
    return Encoding.UTF8.GetString(plainTextBytes);
}