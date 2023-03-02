using System;
using System.Net;

namespace ControlConsumo.Shared.Models.Json
{
    public class JResult
    {
        public String Json { get; set; }
        public Boolean isOk { get; set; }
        public Exception ex { get; set; }
        public Int64 SizePackageUploading { get; set; }
        public Int64 SizePackageDownloading { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
