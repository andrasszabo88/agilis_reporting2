using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReportingWebForms
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCustomerInput_Click(object sender, EventArgs e)
        {
            campaignParagraph.Visible = false;
            results.Visible = false;
            ddlCampaigns.Items.Clear();

            using (var context = new Agilis_ReportingEntities())
            {
                var campaignsQuery = context.Campaigns.Where(p => p.Customer.name == txtCustomerName.Text);

                if (campaignsQuery.Count() == 0) return;

                foreach (var campaign in campaignsQuery)
                {
                    ddlCampaigns.Items.Add(new ListItem(campaign.name, campaign.id.ToString()));
                }

                campaignParagraph.Visible = true;
            }
        }

        protected void btnQueryResponse_Click(object sender, EventArgs e)
        {
            results.Visible = true;
            lblEmailClickRatio.Visible = true;
            lblRatio.Visible = true;
            int campaignId;
            int.TryParse(ddlCampaigns.SelectedValue, out campaignId);

            using(var context = new Agilis_ReportingEntities())
            {
                int openedEmailCount = new DbUtil().GetOpenedEmailCount(campaignId);

                int allSentEmails = context.Response_Report.Count(p => p.campaign_id == campaignId);

                if (allSentEmails == 0) lblRatio.Text = "No emails have been sent in this campaign. Sorry :(";
                else
                {
                    double openingRatio = (double)openedEmailCount / allSentEmails * 100;
                    lblRatio.Text = openingRatio.ToString() + "%";
                }
            }
        }

        protected void btnQueryAdvanced_Click(object sender, EventArgs e)
        {
            tblOpenedEmailsByDevice.Rows.Clear();
            results.Visible = true;
            int campaignId;
            int.TryParse(ddlCampaigns.SelectedValue, out campaignId);

            using (var context = new Agilis_ReportingEntities())
            {
                int openedEmailCount = new DbUtil().GetOpenedEmailCount(campaignId);

                var openedEmailCountByDevice = context.Response_Report.GroupBy(ks => ks.Device.name, (key, g) => new { name = key, count = g.Count() });

                if (openedEmailCountByDevice.Count() == 0) { lblEmailClickRatio.Visible = true; lblRatio.Visible = true; lblRatio.Text = "No emails have been sent in this campaign. Sorry :("; }
                else
                {
                    lblEmailClickRatio.Visible = false;
                    lblRatio.Visible = false;
                    tblOpenedEmailsByDevice.Visible = true;

                    foreach (var device in openedEmailCountByDevice)
                    {
                        double openingStats = (double)device.count / openedEmailCount * 100;
                        var row = new TableRow();
                        row.Cells.Add(new TableCell() { Text = device.name+":" });
                        row.Cells.Add(new TableCell() { Text = openingStats.ToString() + "%" });

                        tblOpenedEmailsByDevice.Rows.Add(row);
                    }
                }
            }
        }
    }
}