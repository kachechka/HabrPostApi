using GraphQL.Types;
using HabrPostApi.Models;

namespace HabrPostApi.GraphQl.Types
{
    public class HabrHubType : ObjectGraphType<HabrHub>
    {
        public HabrHubType() 
            => InitializeFields();

        private void InitializeFields()
        {
            Field(hub => hub.Link);
            Field(hub => hub.Name);
        }
    }
}