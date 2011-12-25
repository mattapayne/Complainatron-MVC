using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Complainatron.Domain;

namespace Complainatron.Core.Extensions
{
    public static class GoogleMapsExtensions
    {
        public static string ToMapJson(this IEnumerable<Complaint> items)
        {
            var o = items.Select(i => new { 
                user = i.FacebookUserName,
                lat = i.Latitude,
                lng = i.Longitude,
                txt = i.ComplaintText,
                severity = i.Severity.Name,
                datecreated = i.DateCreated.ToShortDateString()
            });

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(o);
        }
    }
}
