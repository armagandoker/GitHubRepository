using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using fyiReporting.RdlDesign.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace fyiReporting.RdlDesign
{
    internal partial class SingleCtlDialog
    {        

        #region [_PRIVATES_]        

        private void ChangeControlsPropertiesOnLoad()
        {            
            bOK.Text = "Tamam";
            bCancel.Text = "İptal";

            switch (this._Type)
            {
                case SingleCtlTypeEnum.InteractivityCtl:                   
                    UserControlPropertiesHelper.ChangeControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
                case SingleCtlTypeEnum.VisibilityCtl:                    
                    UserControlPropertiesHelper.ChangeControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
                case SingleCtlTypeEnum.BorderCtl:                    
                    UserControlPropertiesHelper.ChangeStyleBorderControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
                case SingleCtlTypeEnum.FontCtl:                    
                    UserControlPropertiesHelper.ChangeFontControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
                case SingleCtlTypeEnum.BackgroundCtl:                    
                    UserControlPropertiesHelper.ChangeBackgroundControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
                case SingleCtlTypeEnum.ImageCtl:                    
                    UserControlPropertiesHelper.ChangeImageControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
                case SingleCtlTypeEnum.SubreportCtl:                    
                    UserControlPropertiesHelper.ChangeSubreportControlsPropertiesOnLoad((Control)this._Ctl);                                        
                    break;
                case SingleCtlTypeEnum.FiltersCtl:                    
                    UserControlPropertiesHelper.ChangeFiltersControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
                case SingleCtlTypeEnum.SortingCtl:                    
                    UserControlPropertiesHelper.ChangeSortingControlsPropertiesOnLoad((Control)this._Ctl);                    
                    break;
                case SingleCtlTypeEnum.GroupingCtl:                    
                    UserControlPropertiesHelper.ChangeGroupingControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
                case SingleCtlTypeEnum.ReportParameterCtl:                    
                    UserControlPropertiesHelper.ChangeReportParameterControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
                case SingleCtlTypeEnum.ReportCodeCtl:                    
                    UserControlPropertiesHelper.ChangeCodeControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
                case SingleCtlTypeEnum.ReportModulesClassesCtl:                    
                    UserControlPropertiesHelper.ChangeModulesControlsPropertiesOnLoad((Control)this._Ctl);
                    break;
            }
            this.Text = this.Text.Replace("Properties", "Özellikler");                                   
        }        

        #endregion

        #region [_PROTECTEDS_]

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);            
            ChangeControlsPropertiesOnLoad();
            FakeTelericThemeHelper.ApplyToolBar(this);  
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {            
            base.OnPaintBackground(e);
            FakeTelericThemeHelper.PaintToolBar(e, this);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

        #endregion

    }
}
