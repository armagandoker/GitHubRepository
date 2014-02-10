using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers
{
    internal static class DesignerContextMenuStriptHelper
    {

        internal static Dictionary<string, ToolStripMenuItem>  ChangeMenuStriptText(MDIChild mc)
        {            
            ChangeContextMenuChartItemsText(mc);
            ChangeContextMenuMatrixItemsText(mc);
            ChangeContextMenuSubreportItemsText(mc);
            ChangeContextMenuGridItemsText(mc);
            ChangeContextMenuTableItemsText(mc);
            return ChangeContextMenuItemsText(mc);
        }

        private static Dictionary<string, ToolStripMenuItem> ChangeContextMenuItemsText(MDIChild mc)
        {
            Dictionary<string, ToolStripMenuItem> retDictionary = null;

            ContextMenuStrip mainStript = (ContextMenuStrip)mc.RdlEditor.DesignCtl.GetType().GetField("ContextMenuDefault",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField)
                .GetValue(mc.RdlEditor.DesignCtl);

            foreach (ToolStripItem itm in mainStript.Items)
            {
                switch (itm.Name)
                {
                    case "MenuDefaultProperties":
                        itm.Text = "Özellikler";
                        break;
                    case "MenuDefaultCopy":
                        itm.Text = "Kopyala";
                        break;
                    case "MenuDefaultPaste":
                        itm.Text = "Yapıştır";
                        break;
                    case "MenuDefaultDelete":
                        itm.Text = "Sil";
                        break;
                    case "MenuDefaultSelectAll":
                        itm.Text = "Tümünü Seç";
                        break;
                    case "MenuDefaultInsert":
                        itm.Text = "Ekle";
                        retDictionary = GetInsertSubMenuItems((ToolStripMenuItem)itm);
                        ChangeInsertSubMenuItemsText((ToolStripMenuItem)itm);
                        break;
                    default:
                        break;
                }
            }
            return retDictionary;

        }

        private static Dictionary<string, ToolStripMenuItem> GetInsertSubMenuItems(ToolStripMenuItem item)
        {
            Dictionary<string, ToolStripMenuItem> dict = new Dictionary<string, ToolStripMenuItem>();
            foreach (ToolStripItem itm in item.DropDownItems)
            {
                if (itm.GetType() != typeof(ToolStripMenuItem))
                    continue;
                string name = (itm.Tag != null ? (string)itm.Tag : itm.Name);                
                dict.Add(name, (ToolStripMenuItem)itm);                
            }

            ToolStripMenuItem VetricalTableToolStripMenuItem = new ToolStripMenuItem();
            VetricalTableToolStripMenuItem.Name = "VetricalTableToolStripMenuItem";
            VetricalTableToolStripMenuItem.Tag = "VetricalTableToolStripMenuItem";
            VetricalTableToolStripMenuItem.Click += frmNetsisRdlDeginer.VetricalTableToolStripMenuItem_Click;
            dict.Add(VetricalTableToolStripMenuItem.Name, VetricalTableToolStripMenuItem);

            return dict;
        }        

        private static void ChangeInsertSubMenuItemsText(ToolStripMenuItem item)
        {
            foreach (ToolStripItem itm in item.DropDownItems)
            {
                try
                {                    
                    if (itm.Name.Substring(itm.Name.Count() - 4, 4) == "Grid")
                        itm.Text = "Grid";
                    else if (itm.Name.Substring(itm.Name.Count() - 4, 4) == "Line")
                        itm.Text = "Çizgi";
                    else if (itm.Name.Substring(itm.Name.Count() - 4, 4) == "List")
                        itm.Text = "Liste";
                    else if (itm.Name.Substring(itm.Name.Count() - 5, 5) == "Chart")
                        itm.Text = "Grafik";
                    else if (itm.Name.Substring(itm.Name.Count() - 5, 5) == "Table")
                        itm.Text = "Tablo";
                    else if (itm.Name.Substring(itm.Name.Count() - 5, 5) == "Image")
                        itm.Text = "Resim";
                    else if (itm.Name.Substring(itm.Name.Count() - 6, 6) == "Matrix")
                        itm.Text = "Matris";
                    else if (itm.Name.Substring(itm.Name.Count() - 7, 7) == "Textbox")
                        itm.Text = "Metin Kutusu";
                    else if (itm.Name.Substring(itm.Name.Count() - 9, 9) == "Rectangle")
                        itm.Text = "Diktörtgen";
                    else if (itm.Name.Substring(itm.Name.Count() - 9, 9) == "Subreport")
                        itm.Text = "Alt Rapor";
                }
                catch { }  
            }
        }

        private static void ChangeContextMenuChartItemsText(MDIChild mc)
        {
            ContextMenuStrip mainStript = (ContextMenuStrip)mc.RdlEditor.DesignCtl.GetType().GetField("ContextMenuChart",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField)
                .GetValue(mc.RdlEditor.DesignCtl);

            foreach (ToolStripItem itm in mainStript.Items)
            {
                switch (itm.Name)
                {
                    case "MenuChartProperties":
                        itm.Text = "Özellikler";
                        break;
                    case "MenuChartLegend":
                        itm.Text = "Açıklamalar";
                        break;
                    case "MenuChartTitle":
                        itm.Text = "Başlık";
                        break;
                    case "MenuChartInsertCategoryGrouping":
                        itm.Text = "Kategori Gurubu Ekle";
                        break;
                    case "MenuChartEditCategoryGrouping":
                        itm.Text = "Kategori Gurubu Düzenle";
                        break;
                    case "MenuChartDeleteCategoryGrouping":
                        itm.Text = "Kategori Gurubu Sil";                        
                        break;
                    case "MenuChartCategoryAxis":
                        itm.Text = "Kategori X Ekseni";
                        break;
                    case "MenuChartCategoryAxisTitle":
                        itm.Text = "Kategori X Ekseni Başlığı";
                        break;
                    case "MenuChartInsertSeriesGrouping":
                        itm.Text = "Seri Gurubu Ekle";
                        break;
                    case "MenuChartEditSeriesGrouping":
                        itm.Text = "Seri Gurubu Düzenle";
                        break;
                    case "MenuChartDeleteSeriesGrouping":
                        itm.Text = "Seri Gurubu Sil";
                        break;
                    case "MenuChartValueAxis":
                        itm.Text = "Değer Y Ekseni";
                        break;
                    case "MenuChartValueAxisTitle":
                        itm.Text = "Değer Y Ekseni Başlığı";
                        break;
                    case "MenuChartValueAxisRightTitle":
                        itm.Text = "Değer Y Ekseni (Sağ) Başlığı";
                        break;
                    case "MenuChartCopy":
                        itm.Text = "Kopyala";
                        break;
                    case "MenuChartPaste":
                        itm.Text = "Yapıştır";
                        break;
                    case "MenuChartDelete":
                        itm.Text = "Sil";
                        break;
                    case "MenuChartSelectAll":
                        itm.Text = "Tümünü Seç";
                        break;                        
                    default:
                        break;
                }
            }
        }

        private static void ChangeContextMenuMatrixItemsText(MDIChild mc)
        {
            ContextMenuStrip mainStript = (ContextMenuStrip)mc.RdlEditor.DesignCtl.GetType().GetField("ContextMenuMatrix",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField)
                .GetValue(mc.RdlEditor.DesignCtl);

            foreach (ToolStripItem itm in mainStript.Items)
            {
                switch (itm.Name)
                {
                    case "MenuMatrixProperties":
                        itm.Text = "Özellikler";
                        break;
                    case "MenuMatrixMatrixProperties":
                        itm.Text = "Matris Özellikleri";
                        break;
                    case "MenuMatrixInsertColumnGroup":
                        itm.Text = "Grup Kolonu Ekle";
                        break;
                    case "MenuMatrixEditColumnGroup":
                        itm.Text = "Grup Kolonu Düzenle";
                        break;
                    case "MenuMatrixDeleteColumnGroup":
                        itm.Text = "Grup Kolonu Sil";
                        break;
                    case "MenuMatrixInsertRowGroup":
                        itm.Text = "Satır Gurubu Ekle";
                        break;
                    case "MenuMatrixEditRowGroup":
                        itm.Text = "Satır Gurubu Düzenle";
                        break;
                    case "MenuMatrixDeleteRowGroup":
                        itm.Text = "Satır Gurubu Sil";
                        break;
                    case "MenuMatrixDeleteMatrix":
                        itm.Text = "Matrisi Sil";
                        break;
                    case "MenuMatrixCopy":
                        itm.Text = "Kopyala";
                        break;
                    case "MenuMatrixPaste":
                        itm.Text = "Yapıştır";
                        break;
                    case "MenuMatrixDelete":
                        itm.Text = "Sil";
                        break;
                    case "MenuMatrixSelectAll":
                        itm.Text = "Tümünü Seç";
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ChangeContextMenuSubreportItemsText(MDIChild mc)
        {
            ContextMenuStrip mainStript = (ContextMenuStrip)mc.RdlEditor.DesignCtl.GetType().GetField("ContextMenuSubreport",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField)
                .GetValue(mc.RdlEditor.DesignCtl);

            foreach (ToolStripItem itm in mainStript.Items)
            {
                switch (itm.Name)
                {
                    case "MenuSubreportProperties":
                        itm.Text = "Özellikler";
                        break;
                    case "MenuSubreportOpen":
                        itm.Text = "Alt Raporu Aç";
                        break;
                    case "MenuSubreportCopy":
                        itm.Text = "Kopyala";
                        break;
                    case "MenuSubreportPaste":
                        itm.Text = "Yapıştır";
                        break;
                    case "MenuSubreportDelete":
                        itm.Text = "Sil";
                        break;
                    case "MenuSubreportSelectAll":
                        itm.Text = "Tümünü Seç";
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ChangeContextMenuGridItemsText(MDIChild mc)
        {
            ContextMenuStrip mainStript = (ContextMenuStrip)mc.RdlEditor.DesignCtl.GetType().GetField("ContextMenuGrid",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField)
                .GetValue(mc.RdlEditor.DesignCtl);

            foreach (ToolStripItem itm in mainStript.Items)
            {
                switch (itm.Name)
                {
                    case "MenuGridProperties":
                        itm.Text = "Özellikler";
                        break;
                    case "MenuGridGridProperties":
                        itm.Text = "Grid Özellikleri";
                        break;
                    case "MenuGridReplaceCell":
                        itm.Text = "Hücreyi Değiştir";
                        ChangeInsertSubMenuItemsText((ToolStripMenuItem)itm);
                        break;
                    case "MenuGridInsertColumnBefore":
                        itm.Text = "Başına Kolon Ekle";
                        break;
                    case "MenuGridInsertColumnAfter":
                        itm.Text = "Sonuna Kolon Ekle";
                        break;
                    case "MenuGridInsertRowBefore":
                        itm.Text = "Başına Satır Ekle";
                        break;
                    case "MenuGridInsertRowAfter":
                        itm.Text = "Sonuna Satır Ekle";
                        break;
                    case "MenuGridDeleteColumn":
                        itm.Text = "Kolon Sil";
                        break;
                    case "MenuGridDeleteRow":
                        itm.Text = "Satır Sil";
                        break;
                    case "MenuGridDeleteGrid":
                        itm.Text = "Gridi Sil";
                        break;
                    case "MenuGridCopy":
                        itm.Text = "Kopyala";
                        break;
                    case "MenuGridPaste":
                        itm.Text = "Yapıştır";
                        break;
                    case "MenuGridDelete":
                        itm.Text = "Sil";
                        break;
                    case "MenuGridSelectAll":
                        itm.Text = "Tümünü Seç";
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ChangeContextMenuTableItemsText(MDIChild mc)
        {
            ContextMenuStrip mainStript = (ContextMenuStrip)mc.RdlEditor.DesignCtl.GetType().GetField("ContextMenuTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField)
                .GetValue(mc.RdlEditor.DesignCtl);

            foreach (ToolStripItem itm in mainStript.Items)
            {
                switch (itm.Name)
                {
                    case "MenuTableProperties":
                        itm.Text = "Özellikler";
                        break;
                    case "MenuTableTableProperties":
                        itm.Text = "Tablo Özellikleri";
                        break;
                    case "MenuTableReplaceCell":
                        itm.Text = "Hücreyi Değiştir";
                        ChangeInsertSubMenuItemsText((ToolStripMenuItem)itm);
                        break;
                    case "MenuTableInsertColumnBefore":
                        itm.Text = "Başına Kolon Ekle";
                        break;
                    case "MenuTableInsertColumnAfter":
                        itm.Text = "Sonuna Kolon Ekle";
                        break;
                    case "MenuTableInsertRowBefore":
                        itm.Text = "Başına Satır Ekle";
                        break;
                    case "MenuTableInsertRowAfter":
                        itm.Text = "Sonuna Satır Ekle";
                        break;
                    case "MenuTableInsertGroup":
                        itm.Text = "Grup Ekle";
                        break;
                    case "MenuTableEditGroup":
                        itm.Text = "Grup Düzenle";
                        break;
                    case "MenuTableDeleteGroup":
                        itm.Text = "Grup Sil";
                        break;
                    case "MenuTableDeleteColumn":
                        itm.Text = "Kolon Sil";
                        break;
                    case "MenuTableDeleteRow":
                        itm.Text = "Satır Sil";
                        break;
                    case "MenuTableDeleteTable":
                        itm.Text = "Tabloyu Sil";
                        break;
                    case "MenuTableCopy":
                        itm.Text = "Kopyala";
                        break;
                    case "MenuTablePaste":
                        itm.Text = "Yapıştır";
                        break;
                    case "MenuTableDelete":
                        itm.Text = "Sil";
                        break;
                    case "MenuTableSelectAll":
                        itm.Text = "Tümünü Seç";
                        break;
                    default:
                        break;
                }
            }
        }
        
    }
}
