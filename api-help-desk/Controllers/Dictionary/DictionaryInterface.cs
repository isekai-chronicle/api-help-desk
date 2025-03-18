using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static api_help_desk.Controllers.Dictionary.DictionaryModel;

namespace api_help_desk.Controllers.Dictionary
{
    public interface DictionaryInterface
    {
        Task<List<DictionaryListOut>> PostById(DictionaryListIn data);
        Task<Dictionary<string, DictionaryTraductorDetails>> PostComponentById(DictionaryTraductorIn data);
        Task<object> Post(DictionaryDataIn data);
        Task<List<DictionaryDataWordOut>> Put(List<DictionaryDataWordIn> data);
        Task<object> Delete(DictionaryDataIdIn data);
    }
}