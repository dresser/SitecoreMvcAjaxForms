using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SitecoreMvcAjaxForms.Models
{
    public class AjaxResult : JsonResult
    {
        public class ResultData
        {
            public bool Success
            {
                get { return !Errors.Any(); }
            }

            public IList<Action> Actions { get; set; }
            public Dictionary<string, string> Errors { get; set; }

            public ResultData()
            {
                Errors = new Dictionary<string, string>();
                Actions = new List<Action>();
            }
        }

        public class Action
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }

        protected ResultData ResultObject
        {
            get { return (ResultData) base.Data; }
        }

        public IList<Action> Actions
        {
            get { return ResultObject.Actions; }
            set { ResultObject.Actions = value; }
        }

        public Dictionary<string, string> Errors
        {
            get { return ResultObject.Errors; }
            set { ResultObject.Errors = value; }
        }

        public AjaxResult()
        {
            base.ContentType = null;
            base.JsonRequestBehavior = JsonRequestBehavior.DenyGet;
            base.ContentEncoding = null;
            base.Data = new ResultData();
        }
    }
}