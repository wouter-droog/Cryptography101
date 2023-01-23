// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Text;


string plainText = "Hello World!";
const string RSA_KEYS =
    "<RSAKeyValue><Modulus>51l+rhtsFd/CsNoE9Uoduj+KEjwAvafTfb57vev+wovQn7hUDkw9BmUL97RH/sh/nuSvBIwDdeUVSg2Ciz8lNLrf4Y5e2b55KMePsGyHWoZmxinGPS7ur4KJHOfeBa+GxdC8/4pWBJ6E+pBj3dCbPDPKYVz7DQMHdXcQZ4Bq4v8=</Modulus><Exponent>AQAB</Exponent><P>+QvGlLMxjvLhrEf/ZoMuVIAGNq1xzaJmpfBka4t3lcDYGlhQR59vsYaNDl3U43iUYgrXkCmlUgEpyApTKNa/tQ==</P><Q>7c843Eetk3JjxAQD1JPh4C1N6Crx+dX5cIy5gldcd789XzrESgSP8DX7ySjnOeWflDvirGHzaSWvfVi3J5vAYw==</Q><DP>ORDasu4QmAnNbjWdLzc14YToZ5T8s7rXvIRF7mKpxzXGDttXoeHFrS8AmV8kze6uSXzkghMY356GnWDIR15V1Q==</DP><DQ>HyT/dmHwypm1lStNcR64+0oTpO9S53xtgZ78gKR+WLR0Di+9G1CDpVr8kbjIp516C8jYA+mEHmYwGINw4UAVrw==</DQ><InverseQ>RUz0T08x6Rhx8SdLCgPUsMzrJoojB6CNdv2JNpsZ7cgsY508DB00wodBQkzotHbtAXSkUl7gtAr4LiEz5NENzg==</InverseQ><D>IVeOFin16rR20DB+V3BTls89JxdGZLmmatsZkAvONHFHDhstjhP3FZAEPgeu+pgggHYP3UAP6EgC80sS0zO0uOhtPb349e9+6Zxe22aietY1ZlYPOm5v/XlNGfXNed/n8TaBYDpwbvSUL4Oc5xRNyagSlx2/F7Xw4pdBl4poKgU=</D></RSAKeyValue>";


Console.WriteLine($"Plain Text: {plainText}");


var encrypedText = Encrypt();
Console.WriteLine($"Cipher Text: {encrypedText}");

var originalText = Decrypt(encrypedText);
Console.WriteLine($"Original Text: {originalText}");

Console.ReadLine();


// Encrypt method
string Encrypt()
{
    var cipher = CreateCipher(RSA_KEYS);
    byte[] data = Encoding.UTF8.GetBytes(plainText);
    byte[] encryptedData = cipher.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
    return Convert.ToBase64String(encryptedData);
}

// Decrypt method
string Decrypt(string cipherText)
{
    var cipher = CreateCipher(RSA_KEYS);
    byte[] data = Convert.FromBase64String(cipherText);
    byte[] decryptedData = cipher.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
    return Encoding.UTF8.GetString(decryptedData);
}


// Implement CreateCipher
RSA CreateCipher(string keys)
{
    var rsa = RSA.Create();
    rsa.FromXmlString(keys);

    // Console.WriteLine($"public key: {rsa.ToXmlString(false)}");
    // Console.WriteLine($"private key: {rsa.ToXmlString(true)}");

    return rsa;
}

