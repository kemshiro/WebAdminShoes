using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebAdminShoes.Common;

namespace WebAdminShoes.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            //kiểm tra xem đã tồn tại session login chưa
            if (session == null) // nếu chưa thì trả về trang đăng nhập
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            // nếu đã đăng nhập thì thực hiện tiếp
            base.OnActionExecuting(filterContext);
        }
    }
}