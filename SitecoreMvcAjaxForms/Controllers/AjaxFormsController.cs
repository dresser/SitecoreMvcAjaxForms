using System.Collections.Generic;
using System.Web.Mvc;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using SitecoreMvcAjaxForms.Models;
using SitecoreMvcAjaxForms.Extensions;

namespace SitecoreMvcAjaxForms.Controllers
{

    //IDEA See if we can modify the sitecore route to make sure that all the ActionResults returned here contain an ID in the path

    //IDEA Look at where/how the MVC rendering context is initialized and see if there is some way we can get this to be initialized for custom routes

    public class AjaxFormsController : Controller
    {
        private static readonly ID BaseFormTemplateId = new ID("{E04351A8-17DA-422C-99B8-0AD0231711F0}");

        private string GetActionType(Item linkItem)
        {
            if (linkItem.InheritsTemplate(BaseFormTemplateId))
            {
                return "AjaxLink";
            }
            return "PageLink";
        }

        private IList<AjaxResult.Action> GetActions(Item dataSource, string fieldName)
        {
            var actions = new List<AjaxResult.Action>();
            var itemIds = ID.ParseArray(dataSource[fieldName]);
            foreach (var id in itemIds)
            {
                var item = Sitecore.Context.Database.GetItem(id);
                if (item != null)
                {
                    actions.Add(new AjaxResult.Action
                    {
                        Type = GetActionType(item),
                        Value = Sitecore.Links.LinkManager.GetItemUrl(item)
                    });
                }
            }
            return actions;
        }

        private IList<AjaxResult.Action> GetSuccessActions(Item dataSource)
        {
            return GetActions(dataSource, "SuccessActions");
        }

        [HttpGet]
        public PartialViewResult Login()
        {
            var viewModel = new LoginViewModel
            {
                Item = RenderingContext.Current.Rendering.Item
            };
            return PartialView("/Views/Renderings/AjaxForms/Login.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.UserName == "admin" &&
                    viewModel.Password == "b")
                {
                    return new AjaxResult
                    {
                        Actions = GetSuccessActions(viewModel.Item),
                    };
                }
                return new AjaxResult
                {
                    Errors =
                        new Dictionary<string, string> {{"Summary", "Incorrect username or password"}},
                };
            }
            return new AjaxResult
            {
                Errors = new Dictionary<string, string> {{"SomeKey", "Some message"}}
            };
        }

        [HttpGet]
        public PartialViewResult Account()
        {
            var viewModel = new AccountViewModel
            {
                Item = RenderingContext.Current.Rendering.Item
            };
            return PartialView("/Views/Renderings/AjaxForms/Account.cshtml", viewModel);
        }

        [HttpGet]
        public PartialViewResult CreateAccount()
        {
            var viewModel = new CreateAccountViewModel
            {
                Item = RenderingContext.Current.Rendering.Item
            };
            return PartialView("/Views/Renderings/AjaxForms/CreateAccount.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult CreateAccount(CreateAccountViewModel createAccountModel)
        {
            //if (ModelState.IsValid)
            //{
            return new AjaxResult
            {
                Actions = GetSuccessActions(createAccountModel.Item)
            };
            //}
        }
    }
}
