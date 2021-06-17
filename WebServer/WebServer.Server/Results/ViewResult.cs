﻿using System.IO;
using System.Linq;
using WebServer.Server.Http;

namespace WebServer.Server.Results
{
    public class ViewResult : ActionResult
    {
        private const char PathSeparator = '/';
        
        public ViewResult(
            HttpResponse response,
            string viewName,
            string controllerName, 
            object model)
            : base(response)
        {
            this.GetHtml(viewName, controllerName, model);
        }

        private void GetHtml(string viewName, string controllerName, object model)
        {
            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }
            
            var viewPath = Path.GetFullPath($"./Views/" + viewName.TrimStart(PathSeparator) + ".cshtml");
            
            if (!File.Exists(viewPath))
            {
                this.PrepareMissingViewError(viewPath);

                return;
            }

            var viewContent = File.ReadAllText(viewPath);

            if (model != null)
            {
                viewContent = this.PopulateModel(viewContent, model);
            }

            this.SetContent(viewContent, HttpContentType.Html);
        }

        private void PrepareMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.NotFound;

            var errorMessage = $"View '{viewPath}' was not found.";

            this.SetContent(errorMessage, HttpContentType.PlainText);
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model
                .GetType()
                .GetProperties()
                .Select(prop => new
                {
                    Name = prop.Name,
                    Value = prop.GetValue(model)
                });

            foreach (var entry in data)
            {
                const string openingBrackets = "{{";
                const string closingBrackets = "}}";
                
                viewContent = viewContent.Replace($"{openingBrackets}{entry.Name}{closingBrackets}", entry.Value.ToString());
            }

            return viewContent;
        }
    }
}
