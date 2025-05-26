using LiveLib.Application.Commom.Mapping;
using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Collections.UpdateCollection
{
	public class UpdateCollectionDto : IMapWith<Collection>
	{
		public string? Title { get; set; }

	}
}
