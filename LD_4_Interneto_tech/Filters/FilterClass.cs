using Microsoft.AspNetCore.Mvc.Filters;

namespace LD_4_Interneto_tech.Filters
{
    public class FilterClass : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This filter Executed: onActionExecuting");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This filter Executed: onActionExecuted");

        }


    }
}
