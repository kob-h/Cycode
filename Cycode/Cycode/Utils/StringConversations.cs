using System;
using System.Text;

namespace Cycode.Utils
{
	public static class StringConversations
	{
		public static string FromBase64(this string encoded)
		{
            byte[] data = Convert.FromBase64String(encoded);
            string decodedString = Encoding.UTF8.GetString(data);

			return decodedString;
        }
	}
}

