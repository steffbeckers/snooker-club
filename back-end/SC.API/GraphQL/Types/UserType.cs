using GraphQL.Types;
using SC.API.Models;

namespace SC.API.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.FirstName, nullable: true).Description("The first name of the user");
            Field(x => x.LastName, nullable: true).Description("The last name of the user");
            Field(x => x.UserName).Description("The user name of the user");
            Field(x => x.Email).Description("The email address of the user");
            Field(x => x.PhoneNumber, nullable: true).Description("The phone number of the user");
        }
    }
}
