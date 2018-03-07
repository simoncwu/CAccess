using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfb.CandidatePortal.Web.MvcApplication.Models
{
    public interface ITabPageViewModel
    {
        IDictionary<string, string> Controllers { get; }
        string Area { get; }
        string AreaName { get; }
    }
}