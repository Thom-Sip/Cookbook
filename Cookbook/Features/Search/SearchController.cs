using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Cookbook.Features.Search
{
    public class SearchController : Controller
    { 
        public ActionResult SearchForm()
        {
            // var viewLocation = HostingEnvironment.MapPath("~/Features/Search/SearchForm.cshtml");

            //return PartialView("~/Features/Search/test.cshtml"); //, new SearchFormModel());

            return PartialView(new SearchFormModel());

            // return Content("Hello");
        }

        public List<SearchResultItem> SearchBook(string str, string orderBy, int pageSize, int pageNo, out int totalResults)
        {
            string index = $"sitecore_{Sitecore.Context.Database.Name}_index";

            using (var context = ContentSearchManager.GetIndex(index).CreateSearchContext())
            {
                var query = context.GetQueryable<SearchResultItem>()
                                .Where(p => p.Path.StartsWith("/sitecore/Content/home"))
                                .Where(p => p.TemplateName == "book")
                                .Where(p => p.Name.Contains(str));

                totalResults = query.Count();

                if(!string.IsNullOrWhiteSpace(orderBy))
                {
                    if (orderBy == "name")
                        query = query.OrderBy(p => p.Name);

                    else if (orderBy == "date")
                        query = query.OrderBy(p => p.Updated);
                }

                query = query.Page(pageNo - 1, pageSize);
                return query.ToList();
            }
        }
    }
}