using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportingWebForms
{
    public class DbUtil
    {
        public int GetOpenedEmailCountByCampaignId(int campaignId)
        {
            using (var context = new Agilis_ReportingEntities())
            {
                int openedEmailCount = context.Response_Report.Count(p => p.open_date != null && p.campaign_id == campaignId);

                return openedEmailCount;
            }
        }

        public int GetAllSentEmailsByCampaignId(int campaignId)
        {
            using (var context = new Agilis_ReportingEntities())
            {
                int allSentEmails = context.Response_Report.Count(p => p.campaign_id == campaignId);

                return allSentEmails;
            }
        }

        public List<DeviceInfo> GetOpenedEmailBasedOnDevice()
        {
            using (var context = new Agilis_ReportingEntities())
            {
                var openedEmailCountByDevice = context.Response_Report.GroupBy(ks => ks.Device.name, (key, g) => new DeviceInfo() { DeviceName = key, OpenCount = g.Count() });
                return openedEmailCountByDevice.ToList();
            }
        }

        public List<Campaign> GetCampaignsByCustomerName(string customerName)
        {
            using (var context = new Agilis_ReportingEntities())
            {
                var campaignsQuery = context.Campaigns.Where(p => p.Customer.name == customerName);
                return campaignsQuery.ToList();
            }
        }
    }
}