using System;
using System.Collections.Specialized;
using System.Web;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;

namespace SitecoreMvcAjaxPlaceholder.Commands
{
    [Serializable]
    public class SelectEditingComponent : Sitecore.Shell.Applications.WebEdit.Commands.WebEditCommand
    {
        public override void Execute(Sitecore.Shell.Framework.Commands.CommandContext context)
        {
            Assert.ArgumentNotNull((object) context, "context");
            ItemUri uri = ItemUri.ParseQueryString();
            Item pageItem = null;
            if (uri != (ItemUri) null)
            {
                pageItem = Database.GetItem(uri);
                if (pageItem != null && !WebEditUtil.CanDesignItem(pageItem))
                {
                    SheerResponse.Alert("The action cannot be executed because of security restrictions.");
                    return;
                }
            }
            if (pageItem == null)
            {
                SheerResponse.Alert("The action cannot be executed because the page could not be found");
                return;
            }
            var deviceId = WebEditUtil.GetClientDeviceId();
            var parameters = new NameValueCollection();
            parameters["pageItemId"] = pageItem.ID.ToString();
            Assert.IsNotNull(deviceId, "deviceId must not be null");
            parameters["deviceId"] = deviceId.ToString();
            string referenceId = context.Parameters["referenceId"];
            Assert.IsNotNullOrEmpty(referenceId, "referenceId must not be empty");
            parameters["referenceId"] = referenceId;
            string renderingId = context.Parameters["renderingId"];
            Assert.IsNotNullOrEmpty(renderingId, "renderingId must not be empty");
            parameters["renderingId"] = renderingId;
            Context.ClientPage.Start((object) this, "Run", parameters);
        }

        protected void Run(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull((object)args, "args");
            if (!args.IsPostBack)
            {
                Item renderingItem = Client.ContentDatabase.GetItem(args.Parameters["renderingId"]);
                if (renderingItem == null)
                {
                    SheerResponse.Alert("Item not found.", true);
                }
                else
                {
                    //GetRenderingDatasourceArgs pipelineArgs = SetDatasource.CreatePipelineArgs(args, renderingItem);
                    //CorePipeline.Run("getRenderingDatasource", (PipelineArgs)pipelineArgs);
                    //if (string.IsNullOrEmpty(pipelineArgs.DialogUrl))
                    //{
                    //    SheerResponse.Alert("An error ocurred.");
                    //}
                    //else
                    //{
                    var p = args.Parameters.ToString();
                    var queryParams = HttpUtility.ParseQueryString(string.Empty);
                    queryParams.Add("pageItemId", args.Parameters["pageItemId"]);
                    queryParams.Add("deviceId", args.Parameters["deviceId"]);
                    queryParams.Add("referenceId", args.Parameters["referenceId"]);
                    queryParams.Add("renderingId", args.Parameters["renderingId"]);
                    string url = "/sitecore/client/your%20apps/ajaxplaceholder/ajaxplaceholderdialog?sc_lang=en&" +
                                 queryParams.ToString();
                    SheerResponse.ShowModalDialog(new ModalDialogOptions(url)
                    {
                        Width = "340px",
                        Height = "400px",
                        Response = true,
                        ForceDialogSize = true,
                        Maximizable = false
                    });
                        args.WaitForPostBack();
                    //}
                }
            }
            else if (args.HasResult)
            {
                //Item datasourceItem = Client.ContentDatabase.GetItem(args.Result);
                //if (datasourceItem == null)
                //{
                //    if (args.Result.Contains(":"))
                //    {
                //        LayoutDefinition layoutDefinition = SetDatasource.SetNewDatasource(args.Result, args);
                //        if (layoutDefinition == null)
                //        {
                //            SheerResponse.SetAttribute("scLayoutDefinition", "value", string.Empty);
                //            SheerResponse.Alert("An error ocurred.");
                //        }
                //        else
                //        {
                //            SheerResponse.SetAttribute("scLayoutDefinition", "value", WebEditUtil.ConvertXMLLayoutToJSON(layoutDefinition.ToXml()));
                //            SheerResponse.Eval("window.parent.Sitecore.PageModes.ChromeManager.handleMessage('chrome:rendering:propertiescompleted');");
                //        }
                //    }
                //    else
                //        SheerResponse.Alert(string.Format("Item \"{0}\" not found.", (object)args.Result));
                //}
                //else
                //{
                //    LayoutDefinition layoutDefinition = SetDatasource.SetNewDatasource(datasourceItem, args);
                //    if (layoutDefinition == null)
                //    {
                //        SheerResponse.SetAttribute("scLayoutDefinition", "value", string.Empty);
                //        SheerResponse.Alert("An error ocurred.");
                //    }
                //    else
                //    {
                //        SheerResponse.SetAttribute("scLayoutDefinition", "value", WebEditUtil.ConvertXMLLayoutToJSON(layoutDefinition.ToXml()));
                //        SheerResponse.Eval("window.parent.Sitecore.PageModes.ChromeManager.handleMessage('chrome:rendering:propertiescompleted');");
                //    }
                //}
            }
            else
                SheerResponse.SetAttribute("scLayoutDefinition", "value", string.Empty);
        }

        public void SetSelectedRendering(Guid renderingId)
        {
            System.Web.HttpContext.Current.Session["RenderingId"] = renderingId;
            //ControllerContext.HttpContext.Session["RenderingId"] = renderingId;
        }
    }
}