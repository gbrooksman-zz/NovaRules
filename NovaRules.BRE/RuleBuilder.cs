using NRules.RuleModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using NRules.RuleModel.Builders;
using System.Linq;
using NovaRules.BRE.Entities;

namespace NovaRules.BRE
{
    public class RuleBuilder
    {
        public RuleBuilder()
        {

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

            PatternBuilder xrulePattern = builder.LeftHandSide().Pattern(typeof(XRule), "xrule");
            ParameterExpression xruleParameter = xrulePattern.Declaration.ToParameterExpression();
            var ruleWhenCondition = Expression.Lambda(
                Expression.Equal(
                    Expression.Property(xruleParameter, "Name"),
                    Expression.Constant("John Do")),
                xruleParameter);
            xrulePattern.Condition(ruleWhenCondition);

            PatternBuilder orderPattern = builder.LeftHandSide().Pattern(typeof(Order), "order");
            Expression<Func<Order, Customer, bool>> orderCondition1 = (order, customer) => order.Customer == customer;
            orderPattern.Condition(orderCondition1);
            Expression<Func<Order, bool>> orderCondition2 = order => order.Price > 100.00;
            orderPattern.Condition(orderCondition2);

            Expression<Action<IContext, Customer, Order>> action =
                (ctx, customer, order) => Console.WriteLine("Customer {0} has an order in amount of ${1}", customer.Name, order.Price);
            builder.RightHandSide().Action(action);

            return builder.Build();
        }

        private IRuleDefinition BuildJohnDoLargeOrderRule()
        {
            //rule "John Do Large Order Rule"
            //when
            //    customer = Customer(x => x.Name == "John Do");
            //    order = Order(x => x.Customer == customer, x => x.Amount > 100);
            //then
            //    Console.WriteLine("Customer {0} has an order in amount of ${1}", customer.Name, order.Amount);

            var builder = new NRules.RuleModel.Builders.RuleBuilder();
            builder.Name("John Do Large Order Rule");

            PatternBuilder customerPattern = builder.LeftHandSide().Pattern(typeof(Customer), "customer");
            ParameterExpression customerParameter = customerPattern.Declaration.ToParameterExpression();
            var customerCondition = Expression.Lambda(
                Expression.Equal(
                    Expression.Property(customerParameter, "Name"),
                    Expression.Constant("John Do")),
                customerParameter);
            customerPattern.Condition(customerCondition);

            PatternBuilder orderPattern = builder.LeftHandSide().Pattern(typeof(Order), "order");
            Expression<Func<Order, Customer, bool>> orderCondition1 = (order, customer) => order.Customer == customer;
            orderPattern.Condition(orderCondition1);
            Expression<Func<Order, bool>> orderCondition2 = order => order.Price > 100.00;
            orderPattern.Condition(orderCondition2);

            Expression<Action<IContext, Customer, Order>> action =
                (ctx, customer, order) => Console.WriteLine("Customer {0} has an order in amount of ${1}", customer.Name, order.Price);
            builder.RightHandSide().Action(action);

            return builder.Build();
        }

      

        private IRuleDefinition BuildImportantCustomerRule()
        {
            //rule "Important Customer Rule"
            //when
            //    customer = Customer(x => x.IsPreferred);
            //    or
            //    (
            //        customer = Customer(x => !x.IsPreferred)
            //        exists Order(o => o.Customer == customer, o => o.Amount >= 1000.00)
            //    )
            //then
            //    Console.WriteLine("Customer {0} is important", customer.Name);

            var builder = new NRules.RuleModel.Builders.RuleBuilder();
            builder.Name("Important Customer Rule");
            builder.Priority(10);

            var orGroup = builder.LeftHandSide().Group(GroupType.Or);

            PatternBuilder customerPattern1 = orGroup.Pattern(typeof(Customer), "customer");
            Expression<Func<Customer, bool>> customerCondition1 = customer => customer.IsPreferred;
            customerPattern1.Condition(customerCondition1);

            var andGroup = orGroup.Group(GroupType.And);

            PatternBuilder customerPattern2 = andGroup.Pattern(typeof(Customer), "customer");
            Expression<Func<Customer, bool>> customerCondition2 = customer => !customer.IsPreferred;
            customerPattern2.Condition(customerCondition2);

            PatternBuilder orderExistsPattern = andGroup.Exists().Pattern(typeof(Order), "order");
            Expression<Func<Order, bool>> orderCondition = order => order.Price >= 1000;
            orderExistsPattern.Condition(orderCondition);

            Expression<Action<IContext, Customer>> action =
                (ctx, customer) => Console.WriteLine("Customer {0} is important", customer.Name);
            builder.RightHandSide().Action(action);

            return builder.Build();
        }
    }
}

