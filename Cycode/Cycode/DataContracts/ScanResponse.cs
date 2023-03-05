using System;
namespace Cycode.DataContracts
{
    public class ScanResponse
    {
        public List<VulnerablePackage> VulnerablePackages { get; set; }
        public ScanResponse()
        {
            VulnerablePackages = new List<VulnerablePackage>();
        }
    }

    public class VulnerablePackage
    {
        public string? Name { get; set; }
        public string? Version { get; set; }
        public string? Severity { get; set; }
        public string? FirstPatchedVersion { get; set; }
    }

}

