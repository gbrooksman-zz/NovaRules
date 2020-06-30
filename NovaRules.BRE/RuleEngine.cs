using System;
using System.Linq;
using NRules;
using NRules.Fluent;
using Serilog;

namespace NovaRules.BRE
{
    public class RuleEngine
    {
        public RuleEngine()
        {

        }


		public string LoadAndRun()
		{
			var repository = new XRuleRepository();

			repository.LoadRules();

			ISessionFactory factory = repository.Compile();

			ISession session = factory.CreateSession();

			

			// var customer = new Customer("John Do");
			// session.Insert(customer);
			// session.Insert(new Order(customer, 90.00m));
			// session.Insert(new Order(customer, 110.00m));

			session.Fire();

			return "true";

		}

        public string Run()
        {
			//Load rules
			var repository = new RuleRepository();
			repository.Load(x => x.From(typeof(PreferredCustomerDiscountRule).Assembly).To("testRuleSet"));

			var ruleSets = repository.GetRuleSets();
			var compiler = new RuleCompiler();
			//Compile rules
			ISessionFactory factory = compiler.Compile(ruleSets.Where(x => x.Name == "testRuleSet"));

			//Create a working session
			var session = factory.CreateSession();

			//Load domain model
			var customer = new Customer("John Doe") { IsPreferred = true };
			var order1 = new Order(123456, customer, 2, 25.0);
			var order2 = new Order(123457, customer, 1, 100.0);

			//Insert facts into rules engine's memory
			session.Insert(customer);
			session.Insert(order1);
			session.Insert(order2);

			//Start match/resolve/act cycle
			session.Fire();

			Order ret = session.Query<Order>().Where(x => x.Id == 123456).FirstOrDefault();

			Order ret1 = session.Query<Order>().Where(x => x.Id == 123457).FirstOrDefault();

			return "true";
		}


        public string TestIt()
        {


            return "basic test ok";
        }

    }
}
