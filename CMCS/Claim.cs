using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMCS
{
    internal class Claim
    {
        public int ClaimId { get; set; }
        public int LecturerId { get; set; }
        public DateTime DateWorked { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public string Comment { get; set; }
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
        public string SupportingDocumentPath { get; set; }

        public double TotalAmount => HoursWorked * HourlyRate;
    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
