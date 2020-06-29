using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovaRules.BRE;

namespace NovaRules.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RuleController : ControllerBase
	{
		[HttpGet]
		[Route("Run")]
		public ActionResult<string> Run()
		{
			RuleEngine xre = new RuleEngine();

			//var x = xre.Run();

			var x = xre.LoadAndRun();

			return Ok(x.ToString());
		}

		[HttpGet]
		[Route("BasicTest")]
		public ActionResult<string> BasicTest()
		{
			RuleEngine xre = new RuleEngine();

			var x = xre.TestIt();

			return Ok(x.ToString());
		}
	}

}