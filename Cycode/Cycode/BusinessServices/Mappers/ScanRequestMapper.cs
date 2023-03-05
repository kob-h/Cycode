using System;
using Cycode.Utils;
using ScanRequestContract = Cycode.DataContracts.ScanRequest;
using ScanRequestModel = Cycode.Model.ScanRequest;

namespace Cycode.BusinessServices.Mappers
{
	public class ScanRequestMapper : IScanRequestMapper
    {
		public ScanRequestModel Map(ScanRequestContract contract)
		{
			return new ScanRequestModel()
            {
                Ecosystem = contract.Ecosystem.ToUpper(),
                FileContent = contract.FileContent.FromBase64()
            };
        }
    }
}

