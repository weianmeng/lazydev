using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rock.WebApi.Controllers
{

    [Authorize]
    public class ManagerControllerBase : ControllerBase
    {
    }
}
