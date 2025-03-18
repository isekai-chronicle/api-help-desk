using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static api_help_desk.Controllers.Language.LanguageModel;

namespace api_help_desk.Controllers.Language
{
    public interface LanguageInterface
    {
        Task<List<LanguageListOut>> Get();
        Task<List<LanguageComboOut>> GetCombo();
        Task<LanguageDataOut> Post(LanguageDataIn data);
        Task<LanguageDataOut> Put(LanguageDataIn data);
        Task<object> Delete(LanguageDataIdIn data);
    }
}