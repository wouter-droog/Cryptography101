// See https://aka.ms/new-console-template for more information

using System.Text;
using SymmetricEncryption;


string plainText = "Hello World!";
string secretKey = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";
string iv = "000102030405060708090A0B0C0D0E0F";


AesCipher aesCipher = new AesCipher(secretKey, iv);

Console.WriteLine($"Plain Text: {plainText}");


var cipherBytes = aesCipher.EncryptStringToBytes(plainText);
Console.WriteLine($"Cipher Text: {Convert.ToBase64String(cipherBytes)}");

var originalText = aesCipher.DecryptStringFromBytes(cipherBytes);
Console.WriteLine($"Original Text: {originalText}");

Console.ReadLine();

// The byte array size should match the key size (256 bits = 32 bytes)
// The iv by array size should match the block size (128 bits = 16 bytes)
var secretKeyBytes = Enumerable.Take("You can not take random string as secret key or IV."u8.ToArray(), 32);

var aesCipher2 = new AesCipher(Convert.ToHexString(secretKeyBytes.ToArray()), iv);

var cipherBytes2 = aesCipher2.EncryptStringToBytes(plainText);
Console.WriteLine($"Cipher Text: {Convert.ToBase64String(cipherBytes2)}");

var originalText2 = aesCipher2.DecryptStringFromBytes(cipherBytes2);
Console.WriteLine($"Original Text: {originalText2}");

Console.ReadLine();