using ScanRequestContract = Cycode.DataContracts.ScanRequest;
using ScanRequestModel = Cycode.Model.ScanRequest;

namespace Cycode.BusinessServices.Mappers
{
    public interface IScanRequestMapper
    {
        ScanRequestModel Map(ScanRequestContract contract);
    }
}