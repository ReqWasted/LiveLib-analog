using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Commom.Mapping
{
	public interface IMapWith<T>
	{
		void Mapping(Profile profile) =>
			profile.CreateMap(typeof(T), GetType());
	}
}
