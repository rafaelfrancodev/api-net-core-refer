using DEV.API.App.Domain.Core.Interfaces;
using DEV.API.App.Domain.Core.Model;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace DEV.API.App.Domain.Core.Services
{

    [ExcludeFromCodeCoverage]
    public class StringLocalizationCoreService : IStringLocalization
    {
        private IList<JsonLocalization> _localizationValuesList = new List<JsonLocalization>();
        private readonly IHostingEnvironment _environment;

        public StringLocalizationCoreService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        private string JsonFile()
        {
            return File.ReadAllText(Path.Combine(_environment.ContentRootPath, "App_Data", $"resources.pt-BR.json"));
        }

        public LocalizedString this[string key]
        {
            get
            {
                var translation = GetValueByKey(key);
                return new LocalizedString(key, translation ?? key, translation == null);
            }
        }

        public LocalizedString this[string key, params object[] arguments]
        {
            get
            {
                var translation = GetValueByKey(key);
                var value = string.Format(translation ?? key, arguments);
                return new LocalizedString(key, value, translation == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _localizationValuesList
                .Select(localization => new LocalizedString(localization.Key, localization.Value))
                .AsEnumerable();
        }

        private string GetValueByKey(string key)
        {
            return _localizationValuesList.FirstOrDefault(localization => localization.Key == key)?.Value;
        }

        public void SetLocalizationValueList()
        {
            _localizationValuesList = JsonConvert.DeserializeObject<List<JsonLocalization>>(JsonFile());
        }
    }
}
