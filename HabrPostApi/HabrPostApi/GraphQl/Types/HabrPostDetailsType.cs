using GraphQL.Types;
using HabrPostApi.Models;

namespace HabrPostApi.GraphQl.Types
{
    public class HabrPostDetailsType : ObjectGraphType<HabrPostDetails>
    {
        public HabrPostDetailsType() 
            => InitializeFields();

        private void InitializeFields()
        {
            Field(details => details.Mark);
            Field(details => details.CountBookmarks);
            Field(details => details.CountComments);
            Field(details => details.CountViews);
        }
    }
}