using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PetNameGen.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PetNameController : Controller
	{
		private static readonly List<string> Dog = new List<string>
		{
			"Buddy", "Max", "Charlie", "Rocky", "Rex"
		};

		private static readonly List<string> Cat = new List<string>
		{
			"Whiskers", "Mittens", "Luna", "Simba", "Tiger"
		};

		private static readonly List<string> Bird = new List<string>
		{
			"Tweety", "Sky", "Chirpy", "Raven", "Sunny"
		};

		[HttpPost("generate")]
		public IActionResult GenerateName([FromBody] PetNameRequest request)
		{
			if (request == null || string.IsNullOrEmpty(request.AnimalType))
			{
				return BadRequest(new { error = "The 'animalType' field is required." });
			}

			List<string> names;
			switch (request.AnimalType.ToLower())
			{
				case "dog":
					names = Dog;
					break;
				case "cat":
					names = Cat;
					break;
				case "bird":
					names = Bird;
					break;
				default:
					return BadRequest(new { error = "Invalid animal type. Allowed values: dog, cat, bird." });
			}

			Random rnd = new Random();
			string generatedName = names[rnd.Next(names.Count)];

			if (request.TwoPart == true)
			{
				generatedName += names[rnd.Next(names.Count)];
			}

			return Ok(new { name = generatedName });
		}
	}

	public class PetNameRequest
	{
		public string AnimalType { get; set; }
		public bool? TwoPart { get; set; }
	}
}
