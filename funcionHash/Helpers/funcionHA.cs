using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace funcionHash.Helpers
{
    public static class funcionHA
    {

		public static string cifrarDatos(string data)
		{
			SHA256Managed sha = new SHA256Managed();
			byte[] dataSinCifrar = Encoding.Default.GetBytes(data);
			byte[] dataCifrada = sha.ComputeHash(dataSinCifrar);

			return BitConverter.ToString(dataCifrada).Replace("-", "");
		}



	}
}
