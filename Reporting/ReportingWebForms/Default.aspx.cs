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

            using (var ctx = new Agilis_ReportingEntities())
            {
                var query = ctx.Campaigns.Where(p => p.Customer.name == txtCustomerName.Text);

                if (query.Count() == 0) return;

                foreach (var item in query)
                {
                    ddlCampaigns.Items.Add(new ListItem(item.name, item.id.ToString()));
                }

                campaignParagraph.Visible = true;
            }
        }

        protected void btnQueryResponse_Click(object sender, EventArgs e)
        {
            results.Visible = true;
            Label1.Visible = true;
            lblRatio.Visible = true;
            int campaignId;
            int.TryParse(ddlCampaigns.SelectedValue, out campaignId);

            using(var ctx = new Agilis_ReportingEntities())
            {
                int opened = ctx.Response_Report.Count(p => p.open_date != null && p.campaign_id == campaignId);

                int all = ctx.Response_Report.Count(p => p.campaign_id == campaignId);

                if (all == 0) lblRatio.Text = "No emails have been sent in this campaign. Sorry :(";
                else
                {
                    double openingStats = (double)opened / all * 100;
                    lblRatio.Text = openingStats.ToString() + "%";
                }
            }
        }

        protected void btnQueryAdvanced_Click(object sender, EventArgs e)
        {
            Table1.Rows.Clear();
            results.Visible = true;
            int campaignId;
            int.TryParse(ddlCampaigns.SelectedValue, out campaignId);

            using (var ctx = new Agilis_ReportingEntities())
            {
                int opened = ctx.Response_Report.Count(p => p.open_date != null && p.campaign_id == campaignId);

                var q = ctx.Response_Report.GroupBy(ks => ks.Device.name, (key, g) => new { name = key, count = g.Count() });

                if (q.Count() == 0) { Label1.Visible = true; lblRatio.Visible = true; lblRatio.Text = "No emails have been sent in this campaign. Sorry :("; }
                else
                {
                    Label1.Visible = false;
                    lblRatio.Visible = false;
                    Table1.Visible = true;

                    foreach (var device in q)
                    {
                        double openingStats = (double)device.count / opened * 100;
                        TableRow r = new TableRow();
                        r.Cells.Add(new TableCell() { Text = device.name+":" });
                        r.Cells.Add(new TableCell() { Text = openingStats.ToString() + "%" });

                        Table1.Rows.Add(r);
                    }
                }
            }
        }
    }
}