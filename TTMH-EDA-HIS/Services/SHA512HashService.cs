using TTMH_EDA_HIS.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace TTMH_EDA_HIS.Services
{
	public class SHA512HashService:IHashService
	{
		public string SHA512Hash(string rawString)
		{
			if (rawString==null || rawString.Length == 0)
			{
				return "";
			}
			byte[] bytes = SHA512.HashData(Encoding.UTF8.GetBytes(rawString));
			return BitConverter.ToString(bytes).Replace("-", String.Empty);
        }
    }
}
