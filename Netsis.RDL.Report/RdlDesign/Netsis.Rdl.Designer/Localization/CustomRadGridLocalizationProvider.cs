using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI.Localization;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Localization
{
    public class CustomRadGridLocalizationProvider : RadGridLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadGridStringId.GroupingPanelDefaultMessage:
                    return "Gruplamak istediğiniz kolonu buraya sürükleyiniz";
                case RadGridStringId.GroupingPanelHeader:
                    return "Grup";
                case RadGridStringId.FilterOperatorNoFilter:
                case RadGridStringId.FilterFunctionNoFilter:
                    return "Filtresiz";
                case RadGridStringId.FilterOperatorContains:
                case RadGridStringId.FilterFunctionContains:
                    return "İçeren";
                case RadGridStringId.FilterOperatorBetween:
                case RadGridStringId.FilterFunctionBetween:
                    return "Arasında";
                case RadGridStringId.FilterOperatorNotBetween:
                case RadGridStringId.FilterFunctionNotBetween:
                    return "Arasında Olmayan";
                case RadGridStringId.FilterOperatorCustom:
                case RadGridStringId.FilterFunctionCustom:
                    return "Özel";
                case RadGridStringId.FilterOperatorDoesNotContain:
                case RadGridStringId.FilterFunctionDoesNotContain:
                    return "İçermeyen";
                case RadGridStringId.FilterOperatorEndsWith:
                case RadGridStringId.FilterFunctionEndsWith:
                    return "İle Biten";
                case RadGridStringId.FilterOperatorEqualTo:
                case RadGridStringId.FilterFunctionEqualTo:
                    return "Eşit";
                case RadGridStringId.FilterOperatorGreaterThan:
                case RadGridStringId.FilterFunctionGreaterThan:
                    return "Büyük";
                case RadGridStringId.FilterOperatorGreaterThanOrEqualTo:
                case RadGridStringId.FilterFunctionGreaterThanOrEqualTo:
                    return "Büyük Eşit";
                case RadGridStringId.FilterOperatorLessThan:
                case RadGridStringId.FilterFunctionLessThan:
                    return "Küçük";
                case RadGridStringId.FilterOperatorLessThanOrEqualTo:
                case RadGridStringId.FilterFunctionLessThanOrEqualTo:
                    return "Küçük Eşit";
                case RadGridStringId.FilterOperatorStartsWith:
                case RadGridStringId.FilterFunctionStartsWith:
                    return "İle Başlayan";
                case RadGridStringId.FilterOperatorNotEqualTo:
                case RadGridStringId.FilterFunctionNotEqualTo:
                    return "Eşit Değil";
                case RadGridStringId.FilterOperatorIsNull:
                case RadGridStringId.FilterFunctionIsNull:
                    return "Null Olan";
                case RadGridStringId.FilterOperatorNotIsNull:
                case RadGridStringId.FilterFunctionNotIsNull:
                    return "Null Olmayan";
            }
            return base.GetLocalizedString(id);
        }
    }
}
