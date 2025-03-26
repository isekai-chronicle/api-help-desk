using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static api_help_desk.Controllers.Domain.DomainModel;

namespace api_help_desk.Controllers.Domain
{
    public interface DomainInterface
    {
        Task<List<DomainListOut>> Get();
        Task<List<DomainComboOut>> GetCombo();
        Task<DomainDataOut> Post(DomainDataIn data);
        Task<DomainDataOut> Put(DomainDataIn data);
        Task<IActionResult> Delete(DomainDataIdIn data);
    }
}