using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportingWebForms
{
    public class DbUtil
    {
        public int GetOpenedEmailCount(int campaignId)
        {
            using (var context = new Agilis_ReportingEntities())
            {
                int openedEmailCount = context.Response_Report.Count(p => p.open_date != null && p.campaign_id == campaignId);

                return openedEmailCount;
            }
        }
    }
}