

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text.Json;
using NovaRules.BRE.Entities;
using NRules.RuleModel;
using NRules.RuleModel.Builders;

namespace NovaRules.BRE
{

	public class XRuleRepository : IRuleRepository
	{
		private readonly IRuleSet _ruleSet = new RuleSet("XRuleSet");

		public IEnumerable<IRuleSet> GetRuleSets()
		{
			return new[] {_ruleSet};
		}

		public void LoadRules()
		{
			JsonSerializerOptions options = new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true,
				IgnoreNullValues = true,
				WriteIndented = true,
			};

			List<XRule> xRuleList = new List<XRule>();

			string ruleContent = File.ReadAllText("xrules.json");


			// List<LeftFragment> leftItems = new List<LeftFragment>();
			// leftItems.Add(new LeftFragment()
			// {
			// 	Key = "1.11",
			// 	Value = "*"
			// });

			// List<RightFragment> rightItems = new List<RightFragment>();
			// rightItems.Add(new RightFragment()
			// {
			// 	Key = "UND",
			// 	Value = "99999"
			// });

			// List<ResultItem> resultItems = new List<ResultItem>();

			// XRule rule = new XRule()
			// {
			// 	Name = "A Test Rule",
			// 	Description = "A sample Description",
			// 	Priority = 10,
			// 	Tag = "A Tag",	
			// 	LeftItems = leftItems,
			// 	RightItems = rightItems,
			// 	ResultItems = resultItems
			// };

			// xRuleList.Add(rule);

			// string ruleContent = JsonSerializer.Serialize(xRuleList, options);


			xRuleList = JsonSerializer.Deserialize<List<XRule>>(ruleContent, options);

			xRuleList.ForEach ( xr => 
			{
				var rule = BuildXRule(xr);
				_ruleSet.Add(new []{rule});
			});

			
		}


		    private IRuleDefinition BuildXRule(XRule rule)
        {
            //rule "John Do Large Order Rule"
            //when
            //    customer = Customer(x => x.Name == "John Do");
            //    order = Order(x => x.Customer == customer, x => x.Amount > 100);
            //then
            //    Console.WriteLine("Customer {0} has an order in amount of ${1}", customer.Name, order.Amount);

            var builder = new NRules.RuleModel.Builders.RuleBuilder();
            builder.Name(rule.Name);

            PatternBuilder xrulePattern = builder.LeftHandSide().Pattern(typeof(LeftFragment), "xrule-leftfrag");
            ParameterExpression xruleParameter = xrulePattern.Declaration.ToParameterExpression();
			
			foreach (var li in rule.LeftItems)
			{
				var ruleWhenCondition = Expression.Lambda(
					Expression.Equal(
						Expression.Property(xruleParameter, li.Key),
						Expression.Constant(li.Value)),
					xruleParameter);
					xrulePattern.Condition(ruleWhenCondition);
			}
		
			rule.ResultItems = new List<ResultItem>();

			rule.RightItems.ForEach( ri => 
			{
				Expression<Action<IContext>> action =
                	(ctx) => Console.WriteLine($" rule output: key: {ri.Key}  Value: {ri.Value} ");
            	builder.RightHandSide().Action(action);
				Expression<Action<IContext>> action2 =
                	(ctx) => rule.ResultItems.Add(new ResultItem{Key = ri.Key, Value = ri.Value});
            	builder.RightHandSide().Action(action2);
			});

            return builder.Build();
        }

	}

}