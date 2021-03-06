﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReportingWebForms
{
    public partial class Default : System.Web.UI.Page
    {
        private DbUtil dbUtil { get; set; }

        public Default()
        {
            dbUtil = new DbUtil();
        }

        protected void btnCustomerInput_Click(object sender, EventArgs e)
        {
            campaignParagraph.Visible = false;
            results.Visible = false;
            ddlCampaigns.Items.Clear();

            var campaigns = dbUtil.GetCampaignsByCustomerName(txtCustomerName.Text);

            if (campaigns.Count() == 0)
            {
                return;
            }

            foreach (var campaign in campaigns)
            {
                ddlCampaigns.Items.Add(new ListItem(campaign.name, campaign.id.ToString()));
            }

            campaignParagraph.Visible = true;
        }

        protected void btnQueryResponse_Click(object sender, EventArgs e)
        {
            results.Visible = true;
            lblEmailClickRatio.Visible = true;
            lblRatio.Visible = true;
            int campaignId;
            int.TryParse(ddlCampaigns.SelectedValue, out campaignId);

            int openedEmailCount = dbUtil.GetOpenedEmailCountByCampaignId(campaignId);
            int allSentEmails = dbUtil.GetAllSentEmailsByCampaignId(campaignId);

            if (allSentEmails == 0)
            {
                lblRatio.Text = MessageResource.NoEmailSentMessage;
            }
            else
            {
                double openingRatio = (double)openedEmailCount / allSentEmails * 100;
                lblRatio.Text = openingRatio.ToString() + "%";
            }
        }

        protected void btnQueryAdvanced_Click(object sender, EventArgs e)
        {
            tblOpenedEmailsByDevice.Rows.Clear();
            results.Visible = true;
            int campaignId;
            int.TryParse(ddlCampaigns.SelectedValue, out campaignId);

            int openedEmailCount = dbUtil.GetOpenedEmailCountByCampaignId(campaignId);
            var openedEmailCountByDevice = dbUtil.GetOpenedEmailBasedOnDevice();

            if (openedEmailCountByDevice.Count() == 0)
            { 
                lblEmailClickRatio.Visible = true;
                lblRatio.Visible = true;
                lblRatio.Text = MessageResource.NoEmailSentMessage;
            }
            else
            {
                lblEmailClickRatio.Visible = false;
                lblRatio.Visible = false;
                tblOpenedEmailsByDevice.Visible = true;

                foreach (var device in openedEmailCountByDevice)
                {
                    double openingStats = (double)device.OpenCount / openedEmailCount * 100;
                    var row = new TableRow();
                    row.Cells.Add(new TableCell() { Text = device.DeviceName + ":" });
                    row.Cells.Add(new TableCell() { Text = openingStats.ToString() + "%" });

                    tblOpenedEmailsByDevice.Rows.Add(row);
                }
            }
        }
    }
}