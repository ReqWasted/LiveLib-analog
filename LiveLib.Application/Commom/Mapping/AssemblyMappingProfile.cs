using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Commom.Mapping
{
	public class AssemblyMappingProfile : Profile
	{
		public AssemblyMappingProfile(Assembly assembly)
		{
			ApplyMappingsFromAssembly(assembly);
		}

		public AssemblyMappingProfile()
		{
			ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
		}
		private void ApplyMappingsFromAssembly(Assembly assembly)
		{
			var types = assembly.GetExportedTypes()
				.Where(t => t.GetInterfaces()
					.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
				.ToList();

			foreach (var type in types)
			{
				var instance = Activator.CreateInstance(type);
				var methodInfo = type.GetMethod("Mapping")
					?? type.GetInterface("IMapWith`1").GetMethod("Mapping");

				methodInfo?.Invoke(instance, new object[] { this });
			}
		}

	}
}
