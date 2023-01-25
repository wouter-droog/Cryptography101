// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Text;


var plainText = "This is a simple demonstration of hashing";

SHA512 hashSvc = SHA512.Create();

// Convert the input string to a byte array and compute the hash.
// SHa512 is a 512 bit hash, so the output is 64 bytes long (512/8, 8 bits in a byte)
var hash = hashSvc.ComputeHash(Encoding.UTF8.GetBytes(plainText));

// Convert the byte array to a hex string.
var hex = BitConverter.ToString(hash).Replace("-", "");


Console.WriteLine($"SHA512 hash of '{plainText}' is:");
Console.WriteLine(hex);

Console.WriteLine("-------------------");

// Now let's try it with a salt
var salt =  "This is a salt"u8.ToArray(); // should be unique for each user

// Combine the salt and the plain text
var saltedPlainText = new byte[salt.Length + plainText.Length];

// Copy the salt to the beginning of the array
Array.Copy(salt, saltedPlainText, salt.Length);

// Copy the plain text to the end of the array
Array.Copy(Encoding.UTF8.GetBytes(plainText), 0, saltedPlainText, salt.Length, plainText.Length);

// Compute the hash
var saltedHash = hashSvc.ComputeHash(saltedPlainText);

// Convert the byte array to a hex string.
var saltedHex = BitConverter.ToString(saltedHash).Replace("-", "");

Console.WriteLine($"SHA512 hash of '{plainText}' with salt is:");
Console.WriteLine(saltedHex);


Console.WriteLine("-------------------");

// Now let's try it with a keyed hash algorithm

// Create a key
var key = "some key";
var keyBytes = Encoding.UTF8.GetBytes(key);

// Create a keyed hash algorithm
var keyedHashSvc = new HMACSHA512(keyBytes);

// Compute the hash
var keyedHash = keyedHashSvc.ComputeHash(Encoding.UTF8.GetBytes(plainText));

// Convert the byte array to a hex string.
var keyedHex = BitConverter.ToString(keyedHash).Replace("-", "");

Console.WriteLine($"HMACSHA512 keyed hash of '{plainText}' with key is:");
Console.WriteLine(keyedHex);


Console.ReadLine();
