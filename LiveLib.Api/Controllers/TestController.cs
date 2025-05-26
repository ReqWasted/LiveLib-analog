using LiveLib.Api.Common;
using LiveLib.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Xml;

namespace LiveLib.Api.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class TestController : ControllerApiBase
	{
		private readonly ICacheProvider _cache;

		public TestController(ICacheProvider cacheProvider)
		{
			_cache = cacheProvider;
		}

		[HttpPost("upload")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Upload(IFormFile file, CancellationToken ct)
		{
			if (file == null || file.Length == 0)
				return BadRequest("Файл не выбран");

			//var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

			byte[] imageBytes;
			using (var ms = new MemoryStream())
			{
				await file.CopyToAsync(ms, ct);
				imageBytes = ms.ToArray();
			}

			//if (!Directory.Exists(uploadsFolder))
			//	Directory.CreateDirectory(uploadsFolder);

			//var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			//var filePath = Path.Combine(uploadsFolder, fileName);

			//using (var stream = new FileStream(filePath, FileMode.Create))
			//{
			//	await file.CopyToAsync(stream);
			//}

			await _cache.BytesSetAsync($"image:{fileName}", imageBytes);

			var fileUrl = $"/images/{fileName}"; // URL для доступа к файлу
			return Ok(new { url = fileUrl });
		}

		[HttpGet("get/{fileName}")]
		public async Task<IActionResult> Get([FromRoute] string fileName, CancellationToken ct)
		{
			byte[] imageBytes = await _cache.BytesGetAsync($"image:{fileName}");
			if (imageBytes != null && imageBytes.Length > 0)
			{
				return File(imageBytes, "image/jpeg");
			}
			return NotFound();
		}
	}
}
