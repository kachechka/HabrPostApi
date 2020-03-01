using GraphQL;
using HabrPostApi.GraphQl.Query;

namespace HabrPostApi.Extensions
{
    internal static class ExecutionOptionsExtensions
    {
        internal static void SetFrom(
            this ExecutionOptions self, 
            GraphQlQuery query)
        {
            self.Query = query.Query;
            self.OperationName = query.OperationName;
            self.Inputs = query.Variables.ToInputs();
        }
    }
}