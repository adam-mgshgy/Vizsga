using System;
using System.Collections.Generic;
using System.Text;

namespace MoveYourBody.Service.Auth
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
