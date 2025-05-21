using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Interfaces
{
	public interface IPassowrdHasher
	{
		string Hash(string password);

		bool Verify(string password, string hash);
	}
}
