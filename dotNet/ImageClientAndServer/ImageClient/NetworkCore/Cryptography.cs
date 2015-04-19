using System;
using System.IO;
using System.Security.Cryptography;

namespace NetworkCore
{
	public static class Cryptography
	{

		public static byte[] Encrypt(string plainText, byte[] key, byte[] iV)
		{
			try
			{
				return EncryptStringToBytes(plainText, key, iV);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iV)
		{
			// Check arguments. 
			if (plainText == null || plainText.Length <= 0)
				throw new ArgumentNullException("plainText");
			if (key == null || key.Length <= 0)
				throw new ArgumentNullException("key");
			if (iV == null || iV.Length <= 0)
				throw new ArgumentNullException("iV");
			byte[] encrypted;

			// Create an RijndaelManaged object with the specified key and IV. 
			using (var rijAlg = new RijndaelManaged())
			{
				rijAlg.Key = key;
				rijAlg.IV = iV;

				// Create a decryptor to perform the stream transform.
				ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

				// Create the streams used for encryption. 
				using (var msEncrypt = new MemoryStream())
				{
					using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (var swEncrypt = new StreamWriter(csEncrypt))
						{
							//Write all data to the stream.
							swEncrypt.Write(plainText);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}

			// Return the encrypted bytes from the memory stream. 
			return encrypted;
		}

		public static string Decrypt(byte[] cipherText, byte[] key, byte[] iV)
		{
			try
			{
				return DecryptStringFromBytes(cipherText, key, iV);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iV)
		{
			// Check arguments. 
			if (cipherText == null || cipherText.Length <= 0)
				throw new ArgumentNullException("cipherText");
			if (key == null || key.Length <= 0)
				throw new ArgumentNullException("key");
			if (iV == null || iV.Length <= 0)
				throw new ArgumentNullException("iV");

			string plaintext = null;

			// Create an RijndaelManaged object with the specified key and IV. 
			using (var rijAlg = new RijndaelManaged())
			{
				rijAlg.Key = key;
				rijAlg.IV = iV;

				// Create a decrytor to perform the stream transform.
				ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

				// Create the streams used for decryption. 
				using (var msDecrypt = new MemoryStream(cipherText))
				{
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (var srDecrypt = new StreamReader(csDecrypt))
						{
							// Read the decrypted bytes from the decrypting stream and place them in a string.
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}
			}

			return plaintext;
		}

	}
}

