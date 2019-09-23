using DEV.API.App.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.API.App.Domain.Core.Interfaces
{
    public interface IStringLocalization
    {
        LocalizedString this[string name] { get; }
        LocalizedString this[string name, params object[] arguments] { get; }
        IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures);
        void SetLocalizationValueList();
    }
}
