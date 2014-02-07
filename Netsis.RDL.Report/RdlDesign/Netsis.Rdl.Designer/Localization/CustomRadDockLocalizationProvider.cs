using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI.Localization;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Localization
{
    public class CustomRadDockLocalizationProvider : RadDockLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadDockStringId.ContextMenuFloating:
                    return "Ayrı";
                case RadDockStringId.ContextMenuDockable:
                    return "Yapışık";
                case RadDockStringId.ContextMenuTabbedDocument:
                    return "Sekme Doküman";
                case RadDockStringId.ContextMenuAutoHide:
                    return "Otomatik Gizle";
                case RadDockStringId.ContextMenuHide:
                    return "Gizle";
                case RadDockStringId.ContextMenuClose:
                    return "Kapat";
                case RadDockStringId.ContextMenuCloseAll:
                    return "Tümünü Kapat";
                case RadDockStringId.ContextMenuCloseAllButThis:
                    return "Bunun Dışındakileri Kapat";
                case RadDockStringId.ContextMenuMoveToPreviousTabGroup:
                    return "Önceki Sekme Gurubuna Taşı";
                case RadDockStringId.ContextMenuMoveToNextTabGroup:
                    return "Sonraki Sekme Gurubuna Taşı";
                case RadDockStringId.ContextMenuNewHorizontalTabGroup:
                    return "Yeni Yatay Tab Gurup";
                case RadDockStringId.ContextMenuNewVerticalTabGroup:
                    return "Yeni Dikey Sekme Gurup";
                case RadDockStringId.ToolTabStripCloseButton:
                    return "Pencereyi Kapat";
                case RadDockStringId.ToolTabStripDockStateButton:
                    return "Pencere Durumu";
                case RadDockStringId.ToolTabStripUnpinButton:
                    return "Otomatik Gizle";
                case RadDockStringId.ToolTabStripPinButton:
                    return "Yapışık";
                case RadDockStringId.DocumentTabStripCloseButton:
                    return "Dokümanı Kapat";
                case RadDockStringId.DocumentTabStripListButton:
                    return "Aktif Dokümanlar";
            }

            return string.Empty;
        }
    }
}
