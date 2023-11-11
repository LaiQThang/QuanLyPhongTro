using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuanLyPhongTro.Controllers.Components;

namespace QuanLyPhongTro.ActionFilter
{
    public class FilterRoleClient : IAsyncActionFilter
    {
        private readonly ComponentsFilterController _componentsController;
        public FilterRoleClient(ComponentsFilterController componentsController)
        {
            _componentsController = componentsController;

        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Logic bạn muốn thực hiện trước khi hành động được thực hiện

            // Lấy tên action và controller từ context
            //var actionName = context.ActionDescriptor.DisplayName;
            //var controllerName = context.Controller.ToString();
            // Gọi hàm từ một controller khác
            //await CallFunctionFromAnotherController();
            bool check = await _componentsController.CallFunctionCheckRoleClient();
            if (check == true)
            {
                context.Result = new RedirectToActionResult("Denied", "Authencation", null);
                return;
            }

            // Tiếp tục thực hiện hành động
            var resultContext = await next();

        }
    }
}
